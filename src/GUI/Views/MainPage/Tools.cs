using System.Text;
using System.Diagnostics;
using System.Collections.Generic;
using System.Text.Json;

namespace JSON_Dispatcher;

public partial class MainPage : ContentPage
{
	private Filters CollectFilters()
	{
		var filters = new Filters();
		if (NameCheckbox.IsChecked)
		{
			filters.Name = NameEntry.Text ?? "";
		}

		if (FacultyCheckbox.IsChecked)
		{
			filters.Faculty = FacultyEntry.Text ?? "";
		}

		if (ChairCheckbox.IsChecked)
		{
			filters.Chair = ChairEntry.Text ?? "";
		}

		if (SubjectCheckBox.IsChecked)
		{
			filters.Subject = SubjectEntry.Text ?? "";
		}

		if (DateCheckBox.IsChecked)
		{
			filters.Date = DateEntry.Text ?? "";
		}

		if (GroupCheckBox.IsChecked)
		{
			filters.Group = GroupEntry.Text ?? "";
		}

		if (AudienceCheckBox.IsChecked)
		{
			filters.Audience = AudienceEntry.Text ?? "";
		}

		return filters;
	}

	private void ClearFilters()
	{
		NameEntry.Text = "";
		NameCheckbox.IsChecked = false;
		ChairEntry.Text = "";
		ChairCheckbox.IsChecked = false;
		FacultyEntry.Text = "";
		FacultyCheckbox.IsChecked = false;
		SubjectEntry.Text = "";
		SubjectCheckBox.IsChecked = false;
		DateEntry.Text = "";
		DateCheckBox.IsChecked = false;
		GroupEntry.Text = "";
		GroupCheckBox.IsChecked = false;
		AudienceEntry.Text = "";
		AudienceCheckBox.IsChecked = false;
	}

	private async Task<bool> ProcessFile()
	{
		if (ChosenFile is null)
		{
			return false;
		}

		var StringJSON = await ReadFile();

		var errors = ValidationSchema.Validate(StringJSON);
		if (errors.Count > 0)
		{
			foreach (var err in errors)
			{
				Debug.WriteLine(err);
			}

			Title = "JSON Dispatcher - File is not chosen";
			ChosenFile = null;
			await DisplayAlert("Invalid file", "The file content is in ivalid format", "Ok");
			return false;
		}

		Classes = JsonSerializer.Deserialize<List<Class>>(StringJSON);

		return true;
	}

	private async Task<string> ReadFile()
	{
		StringBuilder builder = new();
		using (var stream = await ChosenFile.OpenReadAsync())
		{
			int b = stream.ReadByte();
			while (b != -1)
			{
				builder.Append((char)b);
				b = stream.ReadByte();
			}
		}

		return builder.ToString();
	}

	private void ClearResults()
	{
		while (GridForResults.Children.Count > 6)
		{
			GridForResults.Children.RemoveAt(6);
		}

		while (GridForResults.RowDefinitions.Count > 1)
		{
			GridForResults.RowDefinitions.RemoveAt(1);
		}
	}

	private void MakeSearch()
	{
		var filterOptions = CollectFilters();
		var results = (from cl in Classes where filterOptions.ValidateClass(cl) select cl).ToList();

		ClearResults();
		DisplayResults(results);
	}

	private void DisplayResults(IList<Class> results)
	{
		for (var i = 0; i < results.Count; ++i)
		{
			var result = results[i];

			GridForResults.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

			CreateLabel(i, 0, result.Person.Name.ToString());
			CreateLabel(i, 1, result.Person.Faculty);
			CreateLabel(i, 2, result.Person.Chair);
			CreateLabel(i, 3, result.Date.ToString());
			CreateLabel(i, 4, result.Audience);

			var ShowButton = CreateButton(i, 5, "Show");
			ShowButton.Clicked += async (object sender, EventArgs args) => await DisplayAlert($"Students for {result.Person.Name}, {result.Subject} on {result.Date}", FormatStudents(result.Students), "Ok");

			var EditButton = CreateButton(i, 6, "Edit");
			EditButton.Clicked += async (object sender, EventArgs args) => await Navigation.PushModalAsync(new EditPage(Classes, Classes.IndexOf(result), MakeSearch));

			var DeleteButton = CreateButton(i, 7, "Remove");
			DeleteButton.Clicked += (object sender, EventArgs args) =>
			{
				Classes.Remove(result);
				MakeSearch();
			};
		}
	}

	private void CreateLabel(int row, int column, string text)
	{
		var label = new Label
		{
			HorizontalOptions = LayoutOptions.Center,
			VerticalOptions = LayoutOptions.Center,
			Text = text
		};

		GridForResults.SetRow(label, row + 1);
		GridForResults.SetColumn(label, column);
		GridForResults.Children.Add(label);
	}

	private Button CreateButton(int row, int column, string text)
	{
		var button = new Button
		{
			HorizontalOptions = LayoutOptions.Center,
			VerticalOptions = LayoutOptions.Center,
			Text = text,
		};

		GridForResults.SetRow(button, row + 1);
		GridForResults.SetColumn(button, column);
		GridForResults.Children.Add(button);

		return button;
	}

	public static string FormatStudents(IList<Student> students)
	{
		StringBuilder b = new();

		for (var i = 0; i < students.Count; ++i)
		{
			b.Append(students[i].Name.ToString());
			b.Append(", ");
			b.Append(students[i].Group);

			if (i != students.Count - 1)
			{
				b.Append('\n');
			}
		}

		return b.ToString();
	}

	private void Save()
	{
		File.WriteAllText(ChosenFile.FullPath, JsonSerializer.Serialize(Classes));
	}
}
