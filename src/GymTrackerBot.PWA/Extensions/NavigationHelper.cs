using Microsoft.AspNetCore.Components;

namespace GymTrackerBot.PWA.Extensions;

public static class NavigationExtensions
{
    public static void NavigateToSession(this NavigationManager navigationManager, int sessionId)
    {
        navigationManager.NavigateTo($"sessions/{sessionId}");
    }
}