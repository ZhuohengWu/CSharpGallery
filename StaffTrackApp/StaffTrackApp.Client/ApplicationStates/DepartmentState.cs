namespace StaffTrackApp.Client.ApplicationStates;

public class DepartmentState
{
    public Action? GeneralDepartmentAction {  get; set; }
    public bool ShowGeneralDepartment { get; set; }
    public void GeneralDepartmentClicked()
    {
        RessetAllDepartments();
        ShowGeneralDepartment = true;
        GeneralDepartmentAction?.Invoke();
    }
    private void RessetAllDepartments()
    {
        ShowGeneralDepartment = false;
    }
}
