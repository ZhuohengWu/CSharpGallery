﻿namespace StaffTrackApp.Client.ApplicationStates;

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
    }
}
