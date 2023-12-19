using NJsonSchema;

namespace JSON_Dispatcher;

public delegate void MyCallback();

public partial class EditPage : ContentPage
{
	private IList<Class> Classes;

	private readonly MyCallback Callback;

	private readonly int Index;

	public EditPage(IList<Class> classes, int index, MyCallback callback)
	{
		InitializeComponent();
		Classes = classes;
		Callback = callback;
		Index = index;
		DisplayClass();
	}
}
