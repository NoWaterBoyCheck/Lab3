using NJsonSchema;

namespace JSON_Dispatcher;

public partial class MainPage : ContentPage
{
	private IList<Class> Classes;

	private FileResult ChosenFile;

	private readonly IFilePicker FileChooser;

	private JsonSchema4 ValidationSchema;

	public MainPage()
	{
		InitializeComponent();
		FileChooser = FilePicker.Default;
		Classes = new List<Class>();		
	}
}
