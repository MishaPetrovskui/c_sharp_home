using System.Text;
using System.Threading;
using Game;

class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        // Створення об'єктів класу Student
        Student student1 = new Student("Олексій", 2004, "P-35", 4.5);
        Student student2 = new Student("Марина", 2003, "P-33", 4.8);

        Console.WriteLine("Студент 1:");
        student1.Print();
        Console.WriteLine($"Вік: {student1.GetAge()} років\n");

        Console.WriteLine("Студент 2:");
        student2.Print();
        Console.WriteLine($"Вік: {student2.GetAge()} років\n");
    }
}



// VTORAYA CHAST


namespace Game
{
    class Student
    {
        string name;
        int birthYear;
        string group;
        double averageMark;

        public Student(string name, int birthYear, string group, double averageMark)
        {
            this.name = name;
            this.birthYear = birthYear;
            this.group = group;
            this.averageMark = averageMark;
        }

        public void Print()
        {
            Console.WriteLine($"Ім'я: {name}");
            Console.WriteLine($"Рік народження: {birthYear}");
            Console.WriteLine($"Група: {group}");
            Console.WriteLine($"Середній бал: {averageMark}");
        }

        public int GetAge()
        {
            return DateTime.Now.Year - this.birthYear;
        }
    }
}
