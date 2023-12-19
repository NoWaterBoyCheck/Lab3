using NJsonSchema;

namespace JSON_Dispatcher;

public partial class MainPage : ContentPage
{

	private async void ExitButton_Clicked(object sender, EventArgs e)
	{
		var option = await DisplayAlert("Confirm exit", "Are you sure tou want to exit the program ?", "Yes", "No");
		if (option)
		{
			System.Environment.Exit(0);
		}
	}

	private async void OpenButton_Clicked(object sender, EventArgs e)
	{
		ValidationSchema ??= await JsonSchema4.FromTypeAsync(typeof(List<Class>));

		var customFileType = new FilePickerFileType(
				new Dictionary<DevicePlatform, IEnumerable<string>>
				{
					{ DevicePlatform.WinUI, new[] { ".json" } },
				});
		var options = new PickOptions() { PickerTitle = "Select json file", FileTypes = customFileType };
		ChosenFile = await FileChooser.PickAsync(options);

		if (!await ProcessFile())
		{
			return;
		}

		Title = "JSON Dispatcher - " + ChosenFile.FileName;
		MakeSearch();
	}

	private async void AboutButton_Clicked(object sender, EventArgs args)
	{
		await DisplayAlert("Information", "Author: Kulyk Oleksandr Vasylyovych, 2nd course, K-27\n\nThe program was created in 2023\n\nThis program allows you to read TimeTable JSON Documents, filter and edit them", "Ok");
	}

	private async void FindButton_Clicked(object sender, EventArgs e)
	{
		if (ChosenFile == null)
		{
			await DisplayAlert("Error", "Input file is not chosen", "Ok");
			return;
		}

		MakeSearch();
	}

	private void ClearFiltersButton_Clicked(object sender, EventArgs e)
	{
		ClearFilters();
	}

	private void ClearResultsButton_Clicked(object sender, EventArgs e)
	{
		ClearResults();
	}

	private void OnFilterChanged(object sender, EventArgs args)
	{
		if (ChosenFile is null)
		{
			return;
		}

		MakeSearch();
	}

	private async void SaveButton_Clicked(object sender, EventArgs args)
	{
		if (ChosenFile is null)
		{
			await DisplayAlert("Error", "Input file is not chosen", "Ok");
			return;
		}

		Save();
	}

	private async void AddButton_Clicked(object sender, EventArgs args)
	{
		if (ChosenFile is null)
		{
			await DisplayAlert("Error", "Input file is not chosen", "Ok");
			return;
		}

		Classes.Add(new());
		await Navigation.PushModalAsync(new EditPage(Classes, Classes.Count - 1, MakeSearch));
	}
}
