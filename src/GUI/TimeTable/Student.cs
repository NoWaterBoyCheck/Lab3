namespace JSON_Dispatcher;

public class Student
{
    public FullName Name { get; set; }

    public string Group { get; set; }

    public Student()
    {
        Name = new();
        Group = "";
    }
}