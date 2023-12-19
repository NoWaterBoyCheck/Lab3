using System.Text;

namespace JSON_Dispatcher;

public class FullName
{
    public string FirstName { get; set; }

    public string MiddleName { get; set; }

    public string LastName { get; set; }
    public FullName()
    {
        FirstName = "";
        MiddleName = "";
        LastName = "";
    }

    public override string ToString() => new StringBuilder()
        .Append(FirstName)
        .Append(' ')
        .Append(MiddleName)
        .Append(' ')
        .Append(LastName)
        .ToString();
}
