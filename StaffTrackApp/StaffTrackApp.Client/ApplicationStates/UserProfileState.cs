using BaseLibrary.DTOs;

namespace StaffTrackApp.Client.ApplicationStates;

public class UserProfileState
{
    public Action? Action { get; set; }
    public UserProfile UserProfile { get; set; } = new();
    public void ProfileUpdated() => Action!.Invoke();
}
