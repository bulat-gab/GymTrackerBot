using GymTrackerBot.Domain.Models;

namespace GymTrackerBot.Infrastructure;

public static class UIHelper
{
    /// <summary>
    /// Returns a formatted date like "28 May 2025, 16:44"
    /// </summary>
    public static string GetLongDateTime(DateTime dateTime) =>
        dateTime.ToString("d MMM yyyy, HH:mm");

    /// <summary>
    /// Returns a mobile-friendly short format like "28 May, 16:44"
    /// </summary>
    public static string GetShortDateTime(DateTime dateTime) =>
        dateTime.ToString("d MMM, HH:mm");

    /// <summary>
    /// Safe wrapper for nullable date time with long format
    /// </summary>
    public static string GetLongDateTime(DateTime? dateTime) =>
        dateTime.HasValue ? GetLongDateTime(dateTime.Value) : "-";

    /// <summary>
    /// Safe wrapper for nullable date time with short format
    /// </summary>
    public static string GetShortDateTime(DateTime? dateTime) =>
        dateTime.HasValue ? GetShortDateTime(dateTime.Value) : "-";
    
    public static string GetSessionTypeDisplay(GymSessionType types)
    {
        if (types == GymSessionType.None) return "-";
        return string.Join(", ",
            Enum.GetValues<GymSessionType>()
                .Where(t => t != GymSessionType.None && types.HasFlag(t))
                .Select(t => t.ToString()));
    }
}