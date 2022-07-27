using DataAccess.EntityModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Repository.Interfaces
{
	public interface IAdminRepository
	{
		/// <summary>
		/// Gets all users from database
		/// </summary>
		/// <returns></returns>
		IEnumerable<User> GetAllUsers();

		/// <summary>
		/// Gets all roles
		/// </summary>
		/// <returns></returns>
		IEnumerable<Role> GetAllRoles();

		/// <summary>
		/// Gets all subjects
		/// </summary>
		/// <returns></returns>
		IEnumerable<Subject> GetAllSubjects();

		/// <summary>
		/// Gets all parents
		/// </summary>
		/// <returns></returns>
		IEnumerable<Parent> GetAllParents();

		/// <summary>
		/// Gets all students
		/// </summary>
		/// <returns></returns>
		IEnumerable<Student> GetAllStudents();

		/// <summary>
		/// Gets all teachers.
		/// </summary>
		/// <returns></returns>
		IEnumerable<Teacher> GetAllTeachers();

		/// <summary>
		/// Gets all classTeachers.
		/// </summary>
		/// <returns></returns>
		IEnumerable<ClassTeacher> GetAllClassTeachers();

		/// <summary>
		/// Gets all classes
		/// </summary>
		/// <returns></returns>
		IEnumerable<Class> GetAllClasses();
		
		/// <summary>
		/// Gets Role by Id
		/// </summary>
		/// <param name="id">Id of the role</param>
		/// <returns></returns>
		Role GetRoleById(int id);

		/// <summary>
		/// Creates new student
		/// </summary>
		/// <param name="student"></param>
		/// <returns></returns>
		bool CreateStudent(Student student);
		/// <summary>
		/// Creates new parent
		/// </summary>
		/// <param name="parent"></param>
		/// <returns></returns>
		bool CreateParent(Parent parent);

		/// <summary>
		/// Gets parent by id.
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		Parent GetParentById(int id);

		/// <summary>
		/// Gets teacher by id.
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		Teacher GetTeacherById(int id);

		/// <summary>
		/// Gets student by id.
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		Student GetStudentById(int id);

		/// <summary>
		/// Gets subject by id.
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		Subject GetSubjectById(int id);

		/// <summary>
		/// Create user.
		/// </summary>
		/// <param name="user"></param>
		/// <returns></returns>
		bool CreateUser(User user);

		/// <summary>
		/// Crate teacher.
		/// </summary>
		/// <param name="teacher"></param>
		/// <returns></returns>
		bool CreateTeacher(Teacher teacher);

		/// <summary>
		/// Create class teacher.
		/// </summary>
		/// <param name="classTeacher"></param>
		/// <returns></returns>
		bool CreateClassTeacher(ClassTeacher classTeacher);

		/// <summary>
		/// Create class techer with defined ID
		/// </summary>
		/// <param name="classTeacher"></param>
		/// <returns></returns>
		bool CreateClassTeacherWithId(ClassTeacher classTeacher);

		/// <summary>
		/// Create teacher with defined ID
		/// </summary>
		/// <param name="teacher"></param>
		/// <returns></returns>
		bool CreateTeacherWithId(Teacher teacher);

		/// <summary>
		/// Create user with defined ID
		/// </summary>
		/// <param name="user"></param>
		/// <returns></returns>
		bool CreateUserWithId(User user);

		/// <summary>
		/// Gets class by id.
		/// </summary>
		/// <param name="classId"></param>
		/// <returns></returns>
		Class GetClassById(int classId);

		/// <summary>
		/// Delete user
		/// </summary>
		/// <param name="dbUser"></param>
		bool DeleteUser(User dbUser);

		/// <summary>
		/// Delete teacher
		/// </summary>
		/// <param name="t"></param>
		bool DeleteTeacher(Teacher t);

		/// <summary>
		/// Delete class teacher
		/// </summary>
		/// <param name="ct"></param>
		bool DeleteClassTeacher(ClassTeacher ct);

		/// <summary>
		/// Delete student
		/// </summary>
		/// <param name="s"></param>
		bool DeleteStudent(Student s);

		/// <summary>
		/// Delete subject
		/// </summary>
		/// <param name="s"></param>
		bool DeleteSubject(Subject s);

		/// <summary>
		/// Delete parent
		/// </summary>
		/// <param name="p"></param>
		bool DeleteParent(Parent p);

		/// <summary>
		/// Update student
		/// </summary>
		/// <param name="student"></param>
		bool UpdateStudent(Student student);

		/// <summary>
		/// Update subject
		/// </summary>
		/// <param name="student"></param>
		bool UpdateSubject(Subject s);

		/// <summary>
		/// Update teacher
		/// </summary>
		/// <param name="teacher"></param>
		bool UpdateTeacher(Teacher teacher);

		/// <summary>
		/// Update class teacher
		/// </summary>
		/// <param name="classTeacher"></param>
		bool UpdateClassTeacher(ClassTeacher classTeacher);

		/// <summary>
		/// Update parent
		/// </summary>
		/// <param name="parent"></param>
		bool UpdateParent(Parent parent);

		/// <summary>
		/// Update user
		/// </summary>
		/// <param name="dbUser"></param>
		bool UpdateUser(User dbUser);

		/// <summary>
		/// Create new class
		/// </summary>
		/// <param name="c"></param>
		/// <returns></returns>
		bool CreateClass(Class c);

		/// <summary>
		/// Create new subject
		/// </summary>
		/// <param name="s"></param>
		/// <returns></returns>
		bool CreateSubject(Subject s);

		/// <summary>
		/// Update class
		/// </summary>
		/// <param name="c"></param>
		bool UpdateClass(Class c);

		/// <summary>
		/// Delete class
		/// </summary>
		/// <param name="c"></param>
		/// <returns></returns>
		bool DeleteClass(Class c);
	}
}
