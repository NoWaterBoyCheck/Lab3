using NJsonSchema;

namespace JSON_Dispatcher;

public delegate void MyStudentsCallback(IList<Student> students);

public partial class EditStudentsPage : ContentPage
{
	private IList<Student> Students;

	private readonly MyStudentsCallback Callback;

	public EditStudentsPage(IList<Student> students, MyStudentsCallback callback)
	{
		InitializeComponent();
		Students = students;
		Callback = callback;
		Display();
	}
}
