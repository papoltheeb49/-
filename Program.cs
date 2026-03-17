using System;
using System.Collections.Generic;
using System.Linq;

// ===== Abstract Class (Abstraction) =====
abstract class Person
{
    public string Name { get; set; }

    public Person(string name)
    {
        Name = name;
    }

    public abstract void Display();
}

// ===== Student Class (Inheritance) =====
class Student : Person
{
    public string StudentID { get; set; }
    private List<Subject> subjects = new List<Subject>(); // Encapsulation

    public Student(string name, string studentID) : base(name)
    {
        StudentID = studentID;
    }

    public void AddSubject(Subject subject)
    {
        subjects.Add(subject);
    }

    public List<Subject> GetSubjects()
    {
        return subjects;
    }

    public override void Display() // Polymorphism
    {
        Console.WriteLine($"ชื่อ: {Name}, รหัส: {StudentID}");
    }
}

// ===== Subject Class =====
class Subject
{
    public string SubjectName { get; set; }
    public string SubjectID { get; set; }
    public double Score { get; set; }

    public Subject(string name, string id, double score)
    {
        SubjectName = name;
        SubjectID = id;
        Score = score;
    }

    public string GetGrade()
    {
        if (Score >= 80) return "A";
        else if (Score >= 75) return "B+";
        else if (Score >= 70) return "B";
        else if (Score >= 65) return "C+";
        else if (Score >= 60) return "C";
        else if (Score >= 55) return "D+";
        else if (Score >= 50) return "D";
        else return "F";
    }

    public void Display()
    {
        Console.WriteLine($"{SubjectName} ({SubjectID}) - คะแนน: {Score} เกรด: {GetGrade()}");
    }
}

// ===== Main Program =====
class Program
{
    static List<Student> students = new List<Student>();

    static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("\n===== เมนู =====");
            Console.WriteLine("1. เพิ่มนักศึกษา");
            Console.WriteLine("2. เพิ่มวิชา");
            Console.WriteLine("3. แสดงข้อมูล");
            Console.WriteLine("4. ออกจากโปรแกรม");
            Console.Write("เลือก: ");
            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    AddStudent();
                    break;
                case 2:
                    AddSubject();
                    break;
                case 3:
                    ShowData();
                    break;
                case 4:
                    return;
                default:
                    Console.WriteLine("เลือกไม่ถูกต้อง");
                    break;
            }
        }
    }

    static void AddStudent()
    {
        Console.Write("ชื่อ: ");
        string name = Console.ReadLine();

        Console.Write("รหัสนักศึกษา: ");
        string id = Console.ReadLine();

        students.Add(new Student(name, id));
        Console.WriteLine("เพิ่มนักศึกษาแล้ว");
    }

    static Student FindStudent()
    {
        Console.Write("กรอกรหัสนักศึกษา: ");
        string id = Console.ReadLine();

        return students.FirstOrDefault(s => s.StudentID == id);
    }

    static void AddSubject()
    {
        var student = FindStudent();
        if (student == null)
        {
            Console.WriteLine("ไม่พบข้อมูล");
            return;
        }

        Console.Write("ชื่อวิชา: ");
        string name = Console.ReadLine();

        Console.Write("รหัสวิชา: ");
        string id = Console.ReadLine();

        Console.Write("คะแนน: ");
        double score = double.Parse(Console.ReadLine());

        student.AddSubject(new Subject(name, id, score));
        Console.WriteLine("เพิ่มวิชาแล้ว");
    }

    static void ShowData()
    {
        foreach (var student in students)
        {
            student.Display();

            var subjects = student.GetSubjects();
            if (subjects.Count == 0)
            {
                Console.WriteLine("  ไม่มีวิชา");
                continue;
            }

            foreach (var sub in subjects)
            {
                Console.Write("  ");
                sub.Display();
            }

            double max = subjects.Max(s => s.Score);
            double min = subjects.Min(s => s.Score);

            Console.WriteLine($"  คะแนนสูงสุด: {max}");
            Console.WriteLine($"  คะแนนต่ำสุด: {min}");
        }
    }
}