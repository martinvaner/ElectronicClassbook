using System;
using System.Collections.Generic;
using System.Text;
using BCrypt;
using DataAccess.EntityModel;
using Microsoft.AspNetCore.Identity;

namespace Base
{
	public class PasswordHasher : IPasswordHasher<User>
	{
		public string HashPassword(User user, string password)
		{
			return BCrypt.Net.BCrypt.HashPassword(password, 10);
		}

		public PasswordVerificationResult VerifyHashedPassword(User user, string hashedPassword, string providedPassword)
		{
			if (BCrypt.Net.BCrypt.Verify(providedPassword, hashedPassword))
				return PasswordVerificationResult.Success;

			return PasswordVerificationResult.Failed;
		}

		private string SaltPassword(User user, string password)
		{
			return password;
		}
	}
}
