namespace GymTrackerBot.Domain.Versioning
{
    /// <summary>
    /// Interface for models that support versioning
    /// </summary>
    public interface IVersionedModel
    {
        /// <summary>
        /// Gets the schema version of this model
        /// </summary>
        int SchemaVersion { get; }
        
        /// <summary>
        /// Upgrades this model instance to the latest schema version
        /// </summary>
        /// <returns>An upgraded instance of the model</returns>
        IVersionedModel UpgradeToLatest();
    }
}