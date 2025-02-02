namespace StaffTrackApp.Client.ApplicationStates;

public class AllState
{
    public Action? Action {  get; set; }
    public bool ShowGeneralDepartment { get; set; }
    public void GeneralDepartmentClicked()
    {
        RessetAll();
        ShowGeneralDepartment = true;
        Action?.Invoke();
    }

    public bool ShowDepartment { get; set; }
    public void DepartmentClicked()
    {
        RessetAll();
        ShowDepartment = true;
        Action?.Invoke();
    }

    public bool ShowBranch { get; set; }
    public void BranchClicked()
    {
        RessetAll();
        ShowBranch = true;
        Action?.Invoke();
    }

    public bool ShowCountry { get; set; }
    public void CountryClicked()
    {
        RessetAll();
        ShowCountry = true;
        Action?.Invoke();
    }

    public bool ShowCity { get; set; }
    public void CityClicked()
    {
        RessetAll();
        ShowCity = true;
        Action?.Invoke();
    }

    public bool ShowTown { get; set; }
    public void TownClicked()
    {
        RessetAll();
        ShowTown = true;
        Action?.Invoke();
    }

    public bool ShowUser { get; set; }
    public void UserClicked()
    {
        RessetAll();
        ShowUser = true;
        Action?.Invoke();
    }

    public bool ShowEmployee { get; set; } = true;
    public void EmployeeClicked()
    {
        RessetAll();
        ShowEmployee = true;
        Action?.Invoke();
    }

    public bool ShowHealth { get; set; }
    public void HealthClicked()
    {
        RessetAll();
        ShowHealth = true;
        Action?.Invoke();
    }

    public bool ShowOvertime { get; set; }
    public void OvertimeClicked()
    {
        RessetAll();
        ShowOvertime = true;
        Action?.Invoke();
    }
    public bool ShowOvertimeType { get; set; }
    public void OvertimeTypeClicked()
    {
        RessetAll();
        ShowOvertimeType = true;
        Action?.Invoke();
    }

    public bool ShowSanction { get; set; }
    public void SanctionClicked()
    {
        RessetAll();
        ShowSanction = true;
        Action?.Invoke();
    }
    public bool ShowSanctionType { get; set; }
    public void SanctionTypeClicked()
    {
        RessetAll();
        ShowSanctionType = true;
        Action?.Invoke();
    }

    public bool ShowVacation { get; set; }
    public void VacationClicked()
    {
        RessetAll();
        ShowVacation = true;
        Action?.Invoke();
    }
    public bool ShowVacationType { get; set; }
    public void VacationTypeClicked()
    {
        RessetAll();
        ShowVacationType = true;
        Action?.Invoke();
    }

    public bool ShowUserProfile { get; set; }
    public void UserProfileClicked()
    {
        RessetAll();
        ShowUserProfile = true;
        Action?.Invoke();
    }

    private void RessetAll()
    {
        ShowGeneralDepartment = false;
        ShowDepartment = false;
        ShowBranch = false;

        ShowCountry = false;
        ShowCity = false;
        ShowTown = false;

        ShowUser = false;
        ShowEmployee = false;
        ShowHealth = false;
        ShowOvertime = false;
        ShowOvertimeType = false;
        ShowSanction = false;
        ShowSanctionType = false;
        ShowVacation = false;
        ShowVacationType = false;

        ShowUserProfile = false;
    }
}
