namespace JSON_Dispatcher;

public class Class
{
    public string Subject { get; set; }

    public ClassDate Date { get; set; }

    public Person Person { get; set; }

    public string Audience { get; set; }

    public IList<Student> Students { get; set; }

    public Class()
    {
        Subject = "";
        Date = new();
        Person = new();
        Audience = "";
        Students = new List<Student>();
    }
}