
namespace JSON_Dispatcher;

public partial class EditStudentsPage : ContentPage
{
    protected override bool OnBackButtonPressed()
    {
        ApplyChanges();
        Callback(Students);
        return base.OnBackButtonPressed();
    }

    private void Clear()
    {
        while (grid.Children.Count > 4)
        {
            grid.Children.RemoveAt(4);
        }

        while (grid.RowDefinitions.Count > 1)
        {
            grid.RowDefinitions.RemoveAt(1);
        }
    }

    private void ApplyChanges()
    {
        for (var i = 0; i < Students.Count; ++i)
        {
            Students[i].Name.FirstName = (grid.Children[i * 5 + 4] as Entry)?.Text ?? "";
            Students[i].Name.MiddleName = (grid.Children[i * 5 + 5] as Entry)?.Text ?? "";
            Students[i].Name.LastName = (grid.Children[i * 5 + 6] as Entry)?.Text ?? "";
            Students[i].Group = (grid.Children[i * 5 + 7] as Entry)?.Text ?? "";
        }
    }

    private void CreateEntry(int row, int column, string text)
    {
        var entry = new Entry
        {
            VerticalOptions = LayoutOptions.Center,
            HorizontalOptions = LayoutOptions.Center,
            Text = text
        };

        grid.SetRow(entry, row + 1);
        grid.SetColumn(entry, column);
        grid.Children.Add(entry);
    }

    private void CreateButton(int row, int column)
    {
        var button = new Button
        {
            VerticalOptions = LayoutOptions.Center,
            HorizontalOptions = LayoutOptions.Center,
            Text = "Delete"
        };

        button.Clicked += (object sender, EventArgs args) =>
        {
            ApplyChanges();
            Students.RemoveAt(row);
            Display();
        };

        grid.SetRow(button, row + 1);
        grid.SetColumn(button, column);
        grid.Children.Add(button);
    }

    private void Display()
    {
        Clear();

        for (var i = 0; i < Students.Count; ++i)
        {
            grid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            CreateEntry(i, 0, Students[i].Name.FirstName);
            CreateEntry(i, 1, Students[i].Name.MiddleName);
            CreateEntry(i, 2, Students[i].Name.LastName);
            CreateEntry(i, 3, Students[i].Group);
            CreateButton(i, 4);
        }
    }
}
