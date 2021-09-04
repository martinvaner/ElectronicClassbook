using Admin.Interfaces;
using Base;
using DataAccess.EntityModel;
using DataAccess.Repository.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Admin
{
	public class AdminManager : IAdminManager
	{
		private readonly IAdminRepository adminRepository;
		private readonly IPasswordHasher<User> passwordHasher;

		public AdminManager(IAdminRepository adminRepository)
		{
			this.adminRepository = adminRepository;
			this.passwordHasher = new PasswordHasher();
		}

		public IEnumerable<Class> GetAllClasses()
		{
			return adminRepository.GetAllClasses();
		}

		public IEnumerable<Parent> GetAllParents()
		{
			return adminRepository.GetAllParents();
		}

		public IEnumerable<Role> GetAllRoles()
		{
			return adminRepository.GetAllRoles();
		}

		public IEnumerable<Student> GetAllStudents()
		{
			return adminRepository.GetAllStudents();
		}

		public IEnumerable<Subject> GetAllSubjects()
		{
			return adminRepository.GetAllSubjects();
		}

		public IEnumerable<User> GetAllUsers()
		{
			return adminRepository.GetAllUsers();
		}
		public Role GetRoleById(int id)
		{
			return adminRepository.GetRoleById(id);
		}

		public bool CreateStudent(Student student, bool makeHash = true)
		{
			if(makeHash)
				student.Password = this.GetPasswordHash(student.Password);
			return adminRepository.CreateStudent(student);
		}

		/// <summary>
		/// Temporary method to find hash of given password
		/// </summary>
		/// <param name="password"></param>
		private string GetPasswordHash(string password)
		{
			return passwordHasher.HashPassword(new User { Email = "", Password = password }, password);
		}

		public List<Parent> GetParentsById(int[] parentsId)
		{
			if (parentsId == null) return null;

			List<Parent> parents = new List<Parent>();
			foreach(var id in parentsId)
			{
				parents.Add(this.GetParentById(id));
			}

			return parents;
		}

		public Parent GetParentById(int id)
		{
			return adminRepository.GetParentById(id);
		}

		public List<Teacher> GetTeachersById(int[] ids)
		{
			if (ids == null) return null;

			List<Teacher> teachers = new List<Teacher>();
			foreach (var id in ids)
			{
				teachers.Add(this.GetTeacherById(id));
			}

			return teachers;
		}

		public Teacher GetTeacherById(int id)
		{
			return adminRepository.GetTeacherById(id);
		}

		public bool CreateParent(Parent parent, bool makeHash = true)
		{
			if (makeHash)
				parent.Password = this.GetPasswordHash(parent.Password);
			return adminRepository.CreateParent(parent);
		}

		public List<Student> GetStudentsById(int[] studentsId)
		{
			if (studentsId == null) return null;

			List<Student> students = new List<Student>();
			foreach (var id in studentsId)
			{
				students.Add(this.GetStudentById(id));
			}

			return students;
		}

		public Student GetStudentById(int id)
		{
			return adminRepository.GetStudentById(id);
		}

		public List<Subject> GetSubjectsById(int[] subjectsId)
		{
			if (subjectsId == null) return null;
			List<Subject> subjects = new List<Subject>();
			foreach (var id in subjectsId)
			{
				subjects.Add(this.GetSubjectById(id));
			}

			return subjects;
		}

		public Subject GetSubjectById(int id)
		{
			return adminRepository.GetSubjectById(id);
		}

		public bool CreateUser(User user, bool makeHash = true)
		{
			if (makeHash)
				user.Password = this.GetPasswordHash(user.Password);
			return adminRepository.CreateUser(user);
		}

		public bool CreateTeacher(Teacher teacher, bool makeHash = true)
		{
			if (makeHash)
				teacher.Password = this.GetPasswordHash(teacher.Password);
			return adminRepository.CreateTeacher(teacher);
		}

		public bool CreateClassTeacher(ClassTeacher classTeacher, bool makeHash = true)
		{
			if(makeHash)
				classTeacher.Password = this.GetPasswordHash(classTeacher.Password);
			return adminRepository.CreateClassTeacher(classTeacher);
		}

		public bool CreateClassTeacherWithId(ClassTeacher classTeacher)
		{
			return adminRepository.CreateClassTeacherWithId(classTeacher);
		}

		public bool CreateTeacherWithId(Teacher teacher)
		{
			return adminRepository.CreateTeacherWithId(teacher);
		}

		public bool CreateUserWithId(User user)
		{
			return adminRepository.CreateUserWithId(user);
		}

		public Class GetClassById(int classId)
		{
			return adminRepository.GetClassById(classId);
		}

		public IEnumerable<Teacher> GetAllTeachers()
		{
			return adminRepository.GetAllTeachers();
		}

		public IEnumerable<ClassTeacher> GetAllClassTeachers()
		{
			return adminRepository.GetAllClassTeachers();
		}

		public bool DeleteUser(User dbUser)
		{
			return adminRepository.DeleteUser(dbUser);
		}

		public bool DeleteSubject(Subject s)
		{
			return adminRepository.DeleteSubject(s);
		}

		public bool DeleteTeacher(Teacher t)
		{
			return adminRepository.DeleteTeacher(t);
		}

		public bool DeleteClassTeacher(ClassTeacher ct)
		{
			return adminRepository.DeleteClassTeacher(ct);
		}

		public bool DeleteStudent(Student s)
		{
			return adminRepository.DeleteStudent(s);
		}

		public bool DeleteParent(Parent p)
		{
			return adminRepository.DeleteParent(p);
		}

		public bool UpdateStudent(Student student)
		{
			return adminRepository.UpdateStudent(student);
		}

		public bool UpdateTeacher(Teacher teacher)
		{
			return adminRepository.UpdateTeacher(teacher);
		}

		public bool UpdateClassTeacher(ClassTeacher classTeacher)
		{
			return adminRepository.UpdateClassTeacher(classTeacher);
		}

		public bool UpdateParent(Parent parent)
		{
			return adminRepository.UpdateParent(parent);
		}

		public bool UpdateSubject(Subject s)
		{
			return adminRepository.UpdateSubject(s);
		}

		public bool UpdateUser(User dbUser)
		{
			return adminRepository.UpdateUser(dbUser);
		}

		public bool CreateClass(Class c)
		{
			return adminRepository.CreateClass(c);
		}

		public bool CreateSubject(Subject s)
		{
			return adminRepository.CreateSubject(s);
		}

		public bool UpdateClass(Class c)
		{
			return adminRepository.UpdateClass(c);
		}

		public bool DeleteClass(Class c)
		{
			return adminRepository.DeleteClass(c);
		}
	}
}
