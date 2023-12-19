
namespace JSON_Dispatcher;

public partial class EditStudentsPage : ContentPage
{
	private void OnAddButtonClicked(object sender, EventArgs args)
	{
		ApplyChanges();
		Students.Add(new());
		Display();
	}

	private void OnBackButtonClicked(object sender, EventArgs args)
	{
		OnBackButtonPressed();
	}
}
