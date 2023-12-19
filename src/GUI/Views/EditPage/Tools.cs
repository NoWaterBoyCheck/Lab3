
namespace JSON_Dispatcher;

public partial class EditPage : ContentPage
{
	protected override bool OnBackButtonPressed()
	{
		ApplyChanges();
		Callback();
		return base.OnBackButtonPressed();
	}

    private void ApplyChanges()
	{
		Classes[Index].Person.Name.FirstName = EntryForFirstName.Text ?? "";
        Classes[Index].Person.Name.MiddleName = EntryForMiddleName.Text ?? "";
        Classes[Index].Person.Name.LastName = EntryForLastName.Text ?? "";
        Classes [Index].Person.Faculty = EntryForFaculty.Text ?? "";
        Classes[Index].Person.Chair = EntryForChair.Text ?? "";

        Classes[Index].Date.Day = EntryForDay.Text ?? "";
        Classes[Index].Date.Time = EntryForTime.Text ?? "";

        Classes[Index].Subject = EntryForSubject.Text ?? "";
        Classes[Index].Audience = EntryForAudience.Text ?? "";
	}

    private void DisplayClass()
    {
        var cl = Classes[Index];
        EntryForFirstName.Text = cl.Person.Name.FirstName;
        EntryForMiddleName.Text = cl.Person.Name.MiddleName;
        EntryForLastName.Text = cl.Person.Name.LastName;
        EntryForFaculty.Text = cl.Person.Faculty;
        EntryForChair.Text = cl.Person.Chair;

        EntryForDay.Text = cl.Date.Day;
        EntryForTime.Text = cl.Date.Time;

        EntryForAudience.Text = cl.Audience;
        EntryForSubject.Text = cl.Subject;
    }
}
