using DataAccess.EntityModel;
using DataAccess.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repository
{
	public class AdminRepository : IAdminRepository
	{
		private readonly EClassbookDbContext dbContext;
		private bool disposed = false;

		public AdminRepository(EClassbookDbContext dbContext)
		{
			this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
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

		public IEnumerable<User> GetAllUsers()
		{
			return dbContext.Users.Include(u => u.UserRoles).ThenInclude(ur => ur.Role).ToList();
		}

		public IEnumerable<Role> GetAllRoles()
		{
			return dbContext.Roles.ToList();
		}

		public IEnumerable<Subject> GetAllSubjects()
		{
			return dbContext.Subjects.ToList();
		}

		public IEnumerable<Parent> GetAllParents()
		{
			return dbContext.Parents.Include(p => p.UserRoles).Include(p => p.ParentStudents).ThenInclude(ps => ps.Student).ToList();
		}

		public IEnumerable<Student> GetAllStudents()
		{
			return dbContext.Students.Include(s => s.UserRoles).Include(s => s.ParentStudents).ThenInclude(ps => ps.Parent).ToList();
		}

		public IEnumerable<Teacher> GetAllTeachers()
		{
			return dbContext.Teachers.Include(t => t.UserRoles).Include(s => s.TeacherSubjects).ThenInclude(ts => ts.Subject).ToList();
		}

		public IEnumerable<ClassTeacher> GetAllClassTeachers()
		{
			return dbContext.ClassTeachers.Include(c => c.Class).Include(c => c.UserRoles).Include(c => c.TeacherSubjects).ThenInclude(ts =>ts.Subject).ToList();
		}

		public IEnumerable<Class> GetAllClasses()
		{
			return dbContext.Classes.Include(x => x.ClassTeacher).ToList();
		}

		public Role GetRoleById(int id)
		{
			return dbContext.Roles.FirstOrDefault(r => r.Id.Equals(id));
		}

		public Teacher GetTeacherById(int id)
		{
			return dbContext.Teachers.Include(x => x.TeacherSubjects).ThenInclude(x => x.Subject).Include(x => x.UserRoles).FirstOrDefault(t => t.Id.Equals(id));
		}

		public bool CreateStudent(Student student)
		{
			try
			{
				dbContext.Students.Add(student);
				dbContext.SaveChanges();
				return true;
			}
			catch(Exception ex)
			{
				return false;
			}
		}

		public bool CreateParent(Parent parent)
		{
			try
			{
				dbContext.Parents.Add(parent);
				dbContext.SaveChanges();
				return true;
			}
			catch (Exception ex)
			{
				return false;
			}
		}

		public Parent GetParentById(int id)
		{
			return dbContext.Parents.Include(p => p.UserRoles).Include(p => p.ParentStudents).ThenInclude(ps => ps.Student).Where(p => p.Id.Equals(id)).FirstOrDefault();
		}

		public Student GetStudentById(int id)
		{
			return dbContext.Students.Include(s => s.UserRoles).Include(s => s.ParentStudents).ThenInclude(ps => ps.Parent).Where(s => s.Id.Equals(id)).FirstOrDefault();
		}

		public Subject GetSubjectById(int id)
		{
			return dbContext.Subjects.Include(s =>s.TeacherSubjects).ThenInclude(ts => ts.Teacher).Where(s => s.Id.Equals(id)).FirstOrDefault();
		}

		public bool CreateUser(User user)
		{
			try
			{
				dbContext.Users.Add(user);
				dbContext.SaveChanges();
				return true;
			}
			catch (Exception ex)
			{
				return false;
			}
		}

		public bool CreateTeacher(Teacher teacher)
		{
			try
			{
				dbContext.Teachers.Add(teacher);
				dbContext.SaveChanges();
				return true;
			}
			catch (Exception ex)
			{
				return false;
			}
		}

		public bool CreateClassTeacher(ClassTeacher classTeacher)
		{
			try
			{
				dbContext.ClassTeachers.Add(classTeacher);
				dbContext.SaveChanges();
				return true;
			}
			catch (Exception ex)
			{
				return false;
			}
		}

		public bool CreateClassTeacherWithId(ClassTeacher classTeacher)
		{
			dbContext.Database.OpenConnection();
			try
			{
				dbContext.ClassTeachers.Add(classTeacher);
				dbContext.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Users ON");
				dbContext.SaveChanges();
				dbContext.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Users OFF");
				return true;
			}
			catch (Exception ex)
			{
				return false;
			}
		}

		public bool CreateTeacherWithId(Teacher teacher)
		{
			dbContext.Database.OpenConnection();
			try
			{
				dbContext.Teachers.Add(teacher);
				dbContext.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Users ON");
				dbContext.SaveChanges();
				dbContext.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Users OFF");
				return true;
			}
			catch (Exception ex)
			{
				return false;
			}
		}

		public bool CreateUserWithId(User user)
		{
			dbContext.Database.OpenConnection();
			try
			{
				dbContext.Users.Add(user);
				dbContext.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Users ON");
				dbContext.SaveChanges();
				dbContext.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Users OFF");
				return true;
			}
			catch (Exception ex)
			{
				return false;
			}
		}

		public Class GetClassById(int classId)
		{
			return dbContext.Classes.Include(c => c.ClassTeacher).Include(c => c.Students).Include(c => c.ClassSubjects).ThenInclude(x => x.Subject)
					.Where(c => c.Id.Equals(classId))
					.FirstOrDefault();
		}

		public bool DeleteClass(Class c)
		{
			try
			{
				dbContext.Classes.Remove(c);
				dbContext.SaveChanges();
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}

		public bool DeleteSubject(Subject s)
		{
			try
			{
				dbContext.Subjects.Remove(s);
				dbContext.SaveChanges();
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}

		public bool DeleteUser(User dbUser)
		{
			try
			{
				dbContext.Users.Remove(dbUser);
				dbContext.SaveChanges();
				return true;
			}
			catch(Exception)
			{
				return false;
			}
		}

		public bool DeleteTeacher(Teacher t)
		{
			try
			{
				dbContext.Teachers.Remove(t);
				dbContext.SaveChanges();
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}

		public bool DeleteClassTeacher(ClassTeacher ct)
		{
			try
			{
				dbContext.ClassTeachers.Remove(ct);
				dbContext.SaveChanges();
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}
		public bool DeleteStudent(Student s)
		{
			try
			{
				dbContext.Students.Remove(s);
				dbContext.SaveChanges();
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}

		public bool DeleteParent(Parent p)
		{
			try
			{
				dbContext.Parents.Remove(p);
				dbContext.SaveChanges();
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}

		public bool UpdateStudent(Student student)
		{
			try
			{
				dbContext.Students.Update(student);
				dbContext.SaveChanges();
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}

		public bool UpdateSubject(Subject s)
		{
			try
			{
				dbContext.Subjects.Update(s);
				dbContext.SaveChanges();
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}

		public bool UpdateTeacher(Teacher teacher)
		{
			try
			{
				dbContext.Teachers.Update(teacher);
				dbContext.SaveChanges();
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}

		public bool UpdateClassTeacher(ClassTeacher classTeacher)
		{
			try
			{
				dbContext.ClassTeachers.Update(classTeacher);
				dbContext.SaveChanges();
				return true;
			}
			catch (Exception ex)
			{
				return false;
			}
		}

		public bool UpdateParent(Parent parent)
		{
			try
			{
				dbContext.Parents.Update(parent);
				dbContext.SaveChanges();
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}

		public bool UpdateUser(User dbUser)
		{
			try
			{
				dbContext.Users.Update(dbUser);
				dbContext.SaveChanges();
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}

		public bool CreateClass(Class c)
		{
			try
			{
				dbContext.Classes.Add(c);
				dbContext.SaveChanges();
				return true;
			}
			catch (Exception ex)
			{
				return false;
			}
		}

		public bool CreateSubject(Subject s)
		{
			try
			{
				dbContext.Subjects.Add(s);
				dbContext.SaveChanges();
				return true;
			}
			catch (Exception ex)
			{
				return false;
			}
		}

		public bool UpdateClass(Class c)
		{
			try
			{
				//dbContext.Classes.Update(c);
				dbContext.SaveChanges();
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}
	}
}
