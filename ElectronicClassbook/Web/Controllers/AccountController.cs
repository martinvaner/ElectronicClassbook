using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Base.Interfaces;
using DataAccess.EntityModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Models;

namespace Web.Controllers
{
	[Authorize]
    public class AccountController : Controller
    {
		private readonly IUserManager userManager;
		public AccountController(IUserManager userManager)
		{
			this.userManager = userManager;
		}
        public IActionResult Index()
        {
            return View();
        }

		[HttpGet]
		[AllowAnonymous]
		public IActionResult Login()
		{
			if (User.Identity.IsAuthenticated)
			{
				return RedirectToAction("Index", "Home");
			}
			return View();
		}

		[HttpPost]
		[AllowAnonymous]
		public async Task<IActionResult> Login(LoginViewModel user)
		{
			if (!ModelState.IsValid)
			{
				return View(user);
			}
			//verify user
			if (!userManager.VerifyUser(user.Email, user.Password))
			{
				ModelState.AddModelError("", "Špatně zadané jméno nebo heslo.");
				return View(user);
			}

			//get user roles
			List<Role> userRoles = (List<Role>)userManager.GetUserRoles(user.Email);
			var claims = new List<Claim>
			{
				new Claim(ClaimTypes.Name, user.Email)
			};

			foreach(var role in userRoles)
			{
				claims.Add(new Claim(ClaimTypes.Role, role.Name));
			}

			var claimsIdentity = new ClaimsIdentity(
				claims, CookieAuthenticationDefaults.AuthenticationScheme);

			var authProperties = new AuthenticationProperties
			{
				AllowRefresh = true,
			};

			await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
				new ClaimsPrincipal(claimsIdentity), authProperties);

			return RedirectToAction("Index", "Home");
		}

		public async Task<IActionResult> Logout()
		{
			await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
			return RedirectToAction("Login");
		}

		[HttpGet]
		public IActionResult AccessDenied()
		{
			return View();
		}

		[HttpGet]
		public IActionResult UserAccount()
		{
			User user = null;
			if(User.IsInRole("Rodič"))
			{
				user = userManager.GetParent(User.Identity.Name);
			}
			else
				user = userManager.GetUser(User.Identity.Name);
			List<Role> userRoles = (List<Role>)userManager.GetUserRoles(User.Identity.Name);

			UserAccountViewModel userViewModel = new UserAccountViewModel()
			{
				Email = user.Email,
				FirstName = user.FirstName,
				LastName = user.LastName,
				Roles = userRoles
			};

			if(User.IsInRole("Rodič"))
			{
				userViewModel.Balance = ((Parent)user).Account == null ? 0 : ((Parent)user).Account.Balance;
			}

			return View(userViewModel);
		}

		[HttpGet]
		public IActionResult AddBalance()
		{
			return View();
		}

		public IActionResult AddBalance(double balance)
		{
			//call

			return RedirectToAction("UserAccount");
		}

		[HttpGet]
		public IActionResult ChangePassword()
		{
			return View();
		}

		[HttpPost]
		public IActionResult ChangePassword(ChangePasswordViewModel model)
		{
			if(!ModelState.IsValid)
			{
				return View(model);
			}
			if (!userManager.VerifyUser(User.Identity.Name, model.CurrentPassword))
			{
				ModelState.AddModelError("", "Špatně zadané heslo.");
				return View(model);
			}

			if(!userManager.ChangeUserPassword(User.Identity.Name, model.NewPassword))
			{
				ModelState.AddModelError("", "Při změně hesla nastala chyba. Heslo nebylo změněno.");
				return View(model);
			}

			return RedirectToAction("UserAccount", "Account");
		}

		[HttpGet]
		[AllowAnonymous]
		public IActionResult ForgotPassword()
		{
			return View();
		}

		[HttpPost]
		[AllowAnonymous]
		public IActionResult ForgotPassword(ForgotPasswordviewModel model)
		{
			if(!ModelState.IsValid)
			{
				return View(model);
			}

			//write to DB a return token
			var token = userManager.ResetPasswordRequest(model.Email);
			if(!string.IsNullOrEmpty(token))
			{
				//build url
				var passwordResetLink = Url.Action("ResetPassword", "Account", new { email = model.Email, token = token}, Request.Scheme);

				//send email
				userManager.SendEmail(model.Email, passwordResetLink);
			}

			return RedirectToAction("Login", "Account");
		}

		[HttpGet]
		[AllowAnonymous]
		public IActionResult ResetPassword(string email, string token)
		{
			if(email == null || token == null)
			{
				ModelState.AddModelError("", "Chybný token nebo email.");
			}

			return View();
		}

		[HttpPost]
		[AllowAnonymous]
		public IActionResult ResetPassword(ResetPasswordViewModel model)
		{
			if(ModelState.IsValid)
			{
				//reset password
				if(!userManager.ResetPassword(model.Email, model.Token, model.NewPassword))
				{
					ModelState.AddModelError("", "Při změně hesla nastala chyba. Heslo nebylo změněno.");
				}
			}

			return RedirectToAction("Login", "Account");
		}
	}
}