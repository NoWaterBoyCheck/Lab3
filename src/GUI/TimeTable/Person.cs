namespace JSON_Dispatcher;

public class Person
{
    public Person()
    {
        Name = new();
        Faculty = "";
        Chair = "";
    }
    public FullName Name { get; set; }

    public string Faculty { get; set; }

    public string Chair { get; set; }
}
