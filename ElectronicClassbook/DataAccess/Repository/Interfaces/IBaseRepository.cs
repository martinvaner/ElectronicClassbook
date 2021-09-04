using DataAccess.EntityModel;
using System;
using System.Collections.Generic;
using System.Text;


namespace DataAccess.Repository.Interfaces
{
	public interface IBaseRepository : IDisposable
	{
		/// <summary>
		/// Gets User by its email.
		/// </summary>
		/// <param name="email">User's email.</param>
		/// <returns>Returns User object.</returns>
		User GetUserByEmail(string email);

		/// <summary>
		/// Gets Parent by its email.
		/// </summary>
		/// <param name="email"></param>
		/// <returns></returns>
		Parent GetParentByEmail(string email);

		/// <summary>
		/// Gets user's roles.
		/// </summary>
		/// <param name="email">User's email.</param>
		/// <returns>Returns collection of UserRoles.</returns>
		ICollection<UserRole> GetUserRolesByUserEmail(string email);

		/// <summary>
		/// Updates user's password.
		/// </summary>
		/// <param name="email">User's e-mail</param>
		/// <param name="newPassword">New password</param>
		/// <returns>Returns true if update was successful.</returns>
		bool UpdatePasswordByEmail(string email, string newPassword);

		/// <summary>
		/// Updates user's token and timestamp of the token.
		/// </summary>
		/// <param name="email">E-mail of the user</param>
		/// <param name="token">Password reset token</param>
		/// <param name="timestamp">Timestamp of the reset token</param>
		/// <returns>Returns true if update was successful.</returns>
		bool UpdateUserTokenAndTimestamp(string email, string token, DateTime? timestamp);
	}
}
