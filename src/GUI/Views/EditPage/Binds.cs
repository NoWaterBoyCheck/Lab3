
namespace JSON_Dispatcher;

public partial class EditPage : ContentPage
{
	private async void ShowButton_Clicked(object sender, EventArgs args)
	{
		await DisplayAlert("Students for the chosen class", MainPage.FormatStudents(Classes[Index].Students), "Ok");
	}

	private async void EditButton_Clicked(object sender, EventArgs args)
	{
		MyStudentsCallback callback = (IList<Student> students) =>
		{
			Classes[Index].Students = students;
			ApplyChanges();
			DisplayClass();
		};

		await Navigation.PushModalAsync(new EditStudentsPage(Classes[Index].Students, callback));
	}

	private void OnBackButtonClicked(object sender, EventArgs args)
	{
		OnBackButtonPressed();
	}
}
