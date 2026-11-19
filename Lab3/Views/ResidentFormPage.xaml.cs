using DormitoryJsonManager.Models;

namespace DormitoryJsonManager.Views;

public partial class ResidentFormPage : ContentPage
{
    private readonly TaskCompletionSource<Resident?> _tcs = new();
    private readonly Resident? _existingResident;

    public ResidentFormPage(Resident? resident = null)
    {
        InitializeComponent();
        _existingResident = resident;

        if (_existingResident is not null)
        {
            FormTitleLabel.Text = "Редагування даних мешканця";
            FullNameEntry.Text = _existingResident.FullName;
            FacultyEntry.Text = _existingResident.Faculty;
            DepartmentEntry.Text = _existingResident.Department;
            CourseEntry.Text = _existingResident.Course.ToString();
            BuildingEntry.Text = _existingResident.Accommodation.Building;
            RoomEntry.Text = _existingResident.Accommodation.Room;
            CheckInEntry.Text = _existingResident.Accommodation.CheckInDate;
            CheckOutEntry.Text = _existingResident.Accommodation.CheckOutDate;
            BenefitEntry.Text = _existingResident.Benefit;
            PhoneEntry.Text = _existingResident.Phone;
        }
        else
        {
            CheckInEntry.Text = "2026-09-01";
            CheckOutEntry.Text = "2027-06-30";
        }
    }

    public Task<Resident?> GetResultAsync() => _tcs.Task;

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(FullNameEntry.Text) ||
            string.IsNullOrWhiteSpace(FacultyEntry.Text) ||
            string.IsNullOrWhiteSpace(DepartmentEntry.Text) ||
            string.IsNullOrWhiteSpace(BuildingEntry.Text) ||
            string.IsNullOrWhiteSpace(RoomEntry.Text))
        {
            await DisplayAlert("Помилка валідації", "Заповніть усі обов'язкові поля (*).", "ОК");
            return;
        }

        if (!int.TryParse(CourseEntry.Text, out int course) || course < 1 || course > 6)
        {
            await DisplayAlert("Помилка валідації", "Курс має бути цілим числом від 1 до 6.", "ОК");
            return;
        }

        Resident result = new()
        {
            FullName = FullNameEntry.Text.Trim(),
            Faculty = FacultyEntry.Text.Trim(),
            Department = DepartmentEntry.Text.Trim(),
            Course = course,
            Benefit = string.IsNullOrWhiteSpace(BenefitEntry.Text) ? null : BenefitEntry.Text.Trim(),
            Phone = string.IsNullOrWhiteSpace(PhoneEntry.Text) ? null : PhoneEntry.Text.Trim(),
            Accommodation = new Accommodation
            {
                Building = BuildingEntry.Text.Trim(),
                Room = RoomEntry.Text.Trim(),
                CheckInDate = CheckInEntry.Text?.Trim() ?? string.Empty,
                CheckOutDate = CheckOutEntry.Text?.Trim() ?? string.Empty
            }
        };

        await Navigation.PopModalAsync();
        _tcs.TrySetResult(result);
    }

    private async void OnCancelClicked(object sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
        _tcs.TrySetResult(null);
    }
}
