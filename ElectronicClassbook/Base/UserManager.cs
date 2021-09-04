using System;
using System.Collections.Generic;
using System.Text;
using Base.Interfaces;
using DataAccess.EntityModel;
using DataAccess.Repository;
using DataAccess.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Routing;

namespace Base
{
	public class UserManager : IUserManager
	{
		IPasswordHasher<User> passwordHasher;
		IBaseRepository baseRepository;
		SecurityHelper securityHelper;
		IEmailService emailService;
		public UserManager(IBaseRepository baseRepository, IEmailService emailService)
		{
			passwordHasher = new PasswordHasher();
			this.baseRepository = baseRepository;
			this.securityHelper = new SecurityHelper();
			this.emailService = emailService;
		}
		public bool VerifyUser(string email, string password)
		{
			User user = baseRepository.GetUserByEmail(email);
			if (user != null)
			{
				var result = passwordHasher.VerifyHashedPassword(user, user.Password, password);
				if (result.Equals(PasswordVerificationResult.Success) || result.Equals(PasswordVerificationResult.SuccessRehashNeeded))
				{
					return true;
				}
			}

			return false;
		}

		public ICollection<Role> GetUserRoles(string email)
		{
			ICollection<UserRole> userRoles = baseRepository.GetUserRolesByUserEmail(email);
			List<Role> roles = new List<Role>();

			foreach (var userRole in userRoles)
			{
				roles.Add(userRole.Role);
			}

			return roles;
		}

		public User GetUser(string email)
		{
			return baseRepository.GetUserByEmail(email);
		}

		public Parent GetParent(string email)
		{
			return baseRepository.GetParentByEmail(email);
		}

		public bool ChangeUserPassword(string email, string newPassword)
		{
			User user = this.GetUser(email);
			if (user != null)
			{
				string newPasswordHash = passwordHasher.HashPassword(user, newPassword);
				return baseRepository.UpdatePasswordByEmail(email, newPasswordHash);
			}

			return false;
		}

		public string ResetPasswordRequest(string email)
		{
			//get user
			User user = this.GetUser(email);
			if (user != null)
			{
				//create token
				string token = securityHelper.GenerateToken();
				string hashedToken = passwordHasher.HashPassword(user, token);
				DateTime timestamp = DateTime.Now;
				//save hashed token to DB with creation date
				bool success = baseRepository.UpdateUserTokenAndTimestamp(email, hashedToken, timestamp);
				if (success)
				{
					return token;
				}
			}

			return null;
		}

		public bool ResetPassword(string email, string token, string newPassword)
		{

			User user = this.GetUser(email);
			if(user != null)
			{
				//check token and time
				var tokenResult = passwordHasher.VerifyHashedPassword(user, user.ResetToken, token);

				if (user.ResetToken != null && user.ResetTokenTimestamp != null &&
					(DateTime.Now - new TimeSpan(0, 30, 0) < user.ResetTokenTimestamp) &&
					tokenResult.Equals(PasswordVerificationResult.Success) || tokenResult.Equals(PasswordVerificationResult.SuccessRehashNeeded))
				{
					if(this.ChangeUserPassword(email, newPassword))
					{
						//delete token
						baseRepository.UpdateUserTokenAndTimestamp(email, null, null);
						return true;
					}
				}
			}

			return false;
		}

		public bool SendEmail(string email, string message)
		{
			//build html message
			StringBuilder mailBody = new StringBuilder();
			mailBody.AppendFormat("<h3>Žádost o resetování hesla.</h3>");
			mailBody.AppendFormat("<br />");
			mailBody.AppendFormat("<p>{0}</p>", message);

			emailService.Send(email, "Reset hesla", mailBody.ToString());
			return false;
		}
	}
}
