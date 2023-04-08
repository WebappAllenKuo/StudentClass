using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp14
{
	internal class Program
	{
		static void Main(string[] args)
		{
			Student allen = new Student("Allen", 18
										, new StudentSubject("國文",80)
										, new StudentSubject("英文", 90)
										, new StudentSubject("數學", 10)
										);
			
			Console.WriteLine(allen.GetInfo());
		}
	}

	/// <summary>
	/// 單一科目的名稱及分數
	/// </summary>
	public class StudentSubject
	{
		public string SubjectName { get; set; }
		public int Grade { get; private set; }

		public StudentSubject(string subjectName, int grade)
		{
			// subjectName 必填
			if (string.IsNullOrEmpty(subjectName))
			{
				throw new ArgumentNullException("subjectName", "科目名稱必填");
			}
			// grade 必需介於0~100
			if (grade < 0 || grade > 100)
			{
				throw new ArgumentOutOfRangeException("grade", $"{subjectName} 成績必需介於0~100");
			}

			SubjectName = subjectName;
			Grade = grade;
		}

		public override string ToString()
		{
			return $"{SubjectName}成績是{Grade}";
		}
	}

	/// <summary>
	/// 用來存放多科成績的類別
	/// </summary>
	public class SubjectsCollection
	{
		private List<StudentSubject> Subjects = new List<StudentSubject>();
		public SubjectsCollection(StudentSubject[] subjects)
		{
			Subjects = subjects.ToList();
		}

		public bool IsPass
		{
			get
			{
				double average = this.Subjects.Count == 0 ? 0 : Subjects.Average(x => x.Grade);
				return (average >= 60.0);
			}
		}

		public string GetSubjectsText()
		{
			return this.Subjects.Count > 0
				? this.Subjects.Select(x => x.ToString()).Aggregate((acc, next) => acc += ", " + next)
				: "沒有學科成績";
		}
	}

	public class Student
	{
		public string Name { get; set; }
		public int Age { get; set; }

		public SubjectsCollection Subjects { get; set; }
		
		public Student(string name, int age, params StudentSubject[] subjects)
		{
			Name = name;
			Age = age;

			this.Subjects = new SubjectsCollection(subjects);
		}

		public string GetInfo()
		{
			return this.Subjects.IsPass ? PassMessage() : FailMessage();
			
		}

		private string PassMessage()
		{
			string result = "本次考試及格";
			return GenerateMessage(result);
		}

		private string FailMessage()
		{
			string result = "本次考試沒有及格";
			return GenerateMessage(result);
		}

		private string GenerateMessage(string result)
		{
			string template = "您好，我是{0}，我的年齡是{1}，{2}，{3}";

			string message = string.Format(template, Name, Age, this.Subjects.GetSubjectsText(), result);
			return message;
		}
	}
}
