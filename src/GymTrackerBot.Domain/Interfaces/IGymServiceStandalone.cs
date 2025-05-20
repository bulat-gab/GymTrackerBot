using GymTrackerBot.Domain.Models;

namespace GymTrackerBot.Domain.Interfaces;
public interface IGymServiceStandalone
{
    /// <summary>
    /// Gets all gym sessions
    /// </summary>
    Task<List<GymSession>> GetSessionsAsync();

    /// <summary>
    /// Gets a specific gym session by ID
    /// </summary>
    /// <param name="id">The ID of the session to retrieve</param>
    Task<GymSession?> GetSessionAsync(int id);

    /// <summary>
    /// Gets all dates in a specific month that have gym sessions
    /// </summary>
    /// <param name="month">The month to check for sessions</param>
    Task<List<DateTime>> GetSessionDatesInMonthAsync(DateTime month);

    /// <summary>
    /// Gets all gym sessions for a specific date
    /// </summary>
    /// <param name="date">The date to retrieve sessions for</param>
    Task<List<GymSession>> GetSessionsForDateAsync(DateTime date);

    /// <summary>
    /// Creates a new gym session
    /// </summary>
    /// <param name="session">The session to create</param>
    Task<GymSession> CreateSessionAsync(GymSession session);

    /// <summary>
    /// Updates an existing gym session
    /// </summary>
    /// <param name="session">The session with updated information</param>
    Task<GymSession> UpdateSessionAsync(GymSession session);

    /// <summary>
    /// Deletes a gym session by ID
    /// </summary>
    /// <param name="id">The ID of the session to delete</param>
    Task<bool> DeleteSessionAsync(int id);

    /// <summary>
    /// Marks a gym session as finished
    /// </summary>
    /// <param name="id">The ID of the session to finish</param>
    /// <param name="endTime">The end time of the session</param>
    /// <param name="notes">Optional notes to add to the session</param>
    Task<GymSession> FinishSessionAsync(int id, DateTime endTime, string? notes = null);
}
