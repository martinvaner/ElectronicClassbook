using DataAccess.EntityModel;
using DataAccess.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess.Repository
{
	public class BaseRepository : IBaseRepository
	{
		private readonly EClassbookDbContext dbContext;
		private bool disposed = false;

		public BaseRepository(EClassbookDbContext dbContext)
		{
			this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
		}

		public User GetUserByEmail(string email)
		{
			return dbContext.Users.Where(u => u.Email.Equals(email)).FirstOrDefault();
		}

		public Parent GetParentByEmail(string email)
		{
			return dbContext.Parents.Include(u => u.Account).Where(u => u.Email.Equals(email)).FirstOrDefault();
		}

		public ICollection<UserRole> GetUserRolesByUserEmail(string email)
		{
			var user = dbContext.Users.Include(u => u.UserRoles).ThenInclude(ur => ur.Role).Where(u => u.Email.Equals(email)).FirstOrDefault();
			return user.UserRoles;
		}

		public bool UpdatePasswordByEmail(string email, string newPassword)
		{
			User user = this.GetUserByEmail(email);
			if (user != null)
			{
				user.Password = newPassword;

				try
				{
					dbContext.Users.Update(user);
					dbContext.SaveChanges();

					return true;
				}
				catch(Exception ex)
				{
					return false;
				}
			}

			return false;
		}

		public bool UpdateUserTokenAndTimestamp(string email, string token, DateTime? timestamp)
		{
			User user = this.GetUserByEmail(email);
			if(user != null)
			{
				user.ResetToken = token;
				user.ResetTokenTimestamp = timestamp;

				try
				{
					dbContext.Users.Update(user);
					dbContext.SaveChanges();

					return true;
				}
				catch(Exception ex)
				{
					return false;
				}
			}

			return false;
		}


		protected virtual void Dispose(bool disposing)
		{
			if (!this.disposed && disposing)
			{
				dbContext.Dispose();
			}
			this.disposed = true;
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}
	}
}
