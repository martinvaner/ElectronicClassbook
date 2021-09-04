using DataAccess.EntityModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Base.Interfaces
{
	public interface IUserManager
	{
		/// <summary>
		/// Verify user in database.
		/// </summary>
		/// <returns>Returns true if email and password are correct.</returns>
		bool VerifyUser(string email, string password);
		
		/// <summary>
		/// Gets user's roles.
		/// </summary>
		/// <param name="email">E-mail of user</param>
		/// <returns>Returns collection of Roles.</returns>
		ICollection<Role> GetUserRoles(string email);

		/// <summary>
		/// Gets user.
		/// </summary>
		/// <param name="email">E-mail address of user</param>
		/// <returns>Returns instance of User.</returns>
		User GetUser(string email);

		/// <summary>
		/// Get parent
		/// </summary>
		/// <param name="email"></param>
		/// <returns></returns>
		Parent GetParent(string email);

		/// <summary>
		/// Changes user's password to new password.
		/// </summary>
		/// <param name="email">E-mail of user</param>
		/// <param name="newPassword">New password</param>
		/// <returns>Returns true if change was successful.</returns>
		bool ChangeUserPassword(string email, string newPassword);

		/// <summary>
		/// Prepare user account for password change.
		/// </summary>
		/// <param name="email">user's e-mail</param>
		string ResetPasswordRequest(string email);

		/// <summary>
		/// Changes user's password to new password.
		/// </summary>
		/// <param name="email">User's e-mail</param>
		/// <param name="token">Token</param>
		/// <param name="newPassword">New password</param>
		/// <returns>Returns true if change was successful.</returns>
		bool ResetPassword(string email, string token, string newPassword);

		/// <summary>
		/// Sends email.
		/// </summary>
		/// <param name="email">E-mail address</param>
		/// <param name="message">Message of the email</param>
		/// <returns>Returns true if email was successfuly send.</returns>
		bool SendEmail(string email, string message);
	}
}
