class LINQClass
{
    // 定義一個簡單的類別來表示學生
    public class Student
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public double Grade { get; set; }
        public string City { get; set; }
    }

    public LINQClass()
    {
        // 建立學生列表
        List<Student> students = new List<Student>
        {
            new Student { Name = "Alice", Age = 22, Grade = 85.5, City = "New York" },
            new Student { Name = "Bob", Age = 20, Grade = 90.2, City = "Los Angeles" },
            new Student { Name = "Charlie", Age = 23, Grade = 78.4, City = "New York" },
            new Student { Name = "David", Age = 21, Grade = 82.7, City = "Los Angeles" },
            new Student { Name = "Eve", Age = 22, Grade = 88.9, City = "Chicago" }
        };

        // LINQ 查詢範例

        // 1. 查詢所有學生的姓名
        var allNames = students.Select(s => s.Name);
        Console.WriteLine("All student names:");
        foreach (var name in allNames)
        {
            Console.WriteLine(name);
        }
        Console.WriteLine("===========================");

        // 2. 查詢年齡大於 21 歲的學生
        var studentsAbove21 = students.Where(s => s.Age > 21);
        Console.WriteLine("Students older than 21:");
        foreach (var student in studentsAbove21)
        {
            Console.WriteLine($"{student.Name}, Age: {student.Age}");
        }
        Console.WriteLine("===========================");

        // 3. 查詢學業成績大於 80 的學生，並按成績排序
        var topStudents = students.Where(s => s.Grade > 80).OrderByDescending(s => s.Grade);
        Console.WriteLine("Top students with grades above 80:");
        foreach (var student in topStudents)
        {
            Console.WriteLine($"{student.Name}, Grade: {student.Grade}");
        }
        Console.WriteLine("===========================");

        // 4. 分組查詢：根據城市分組學生
        var studentsByCity = students.GroupBy(s => s.City);
        Console.WriteLine("Students grouped by city:");
        foreach (var group in studentsByCity)
        {
            Console.WriteLine($"City: {group.Key}");
            foreach (var student in group)
            {
                Console.WriteLine($"  {student.Name}, Age: {student.Age}, Grade: {student.Grade}");
            }
        }
        Console.WriteLine("===========================");

        // 5. 使用 LINQ 查詢語法查詢學生的名字和年齡
        var querySyntax = from s in students
                          where s.Age > 21
                          select new { s.Name, s.Age };
        Console.WriteLine("Students older than 21 (Query Syntax):");
        foreach (var item in querySyntax)
        {
            Console.WriteLine($"{item.Name}, Age: {item.Age}");
        }
        Console.WriteLine("===========================");

        // 6. 使用 LINQ 查詢語法查詢並排序學生
        var sortedStudentsQuerySyntax = from s in students
                                        where s.Grade > 80
                                        orderby s.Grade descending
                                        select s;
        Console.WriteLine("Top students with grades above 80 (Query Syntax):");
        foreach (var student in sortedStudentsQuerySyntax)
        {
            Console.WriteLine($"{student.Name}, Grade: {student.Grade}");
        }
        Console.WriteLine("===========================");

        // 7. 查詢最年長學生的姓名和年齡
        var oldestStudent = students.OrderByDescending(s => s.Age).FirstOrDefault();
        if (oldestStudent != null)
        {
            Console.WriteLine($"Oldest student: {oldestStudent.Name}, Age: {oldestStudent.Age}");
        }
        Console.WriteLine("===========================");

        // 8. 計算學生的平均年齡
        var averageAge = students.Average(s => s.Age);
        Console.WriteLine($"Average age of students: {averageAge}");
        Console.WriteLine("===========================");

        // 9. 確定是否所有學生的成績都大於 75
        bool allAbove75 = students.All(s => s.Grade > 75);
        Console.WriteLine($"All students have grades above 75: {allAbove75}");
        Console.WriteLine("===========================");

        // 10. 查找是否存在任何學生來自 "Chicago"
        bool anyFromChicago = students.Any(s => s.City == "Chicago");
        Console.WriteLine($"Any student from Chicago: {anyFromChicago}");
    }
}
