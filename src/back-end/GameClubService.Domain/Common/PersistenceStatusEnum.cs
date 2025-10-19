namespace GameClubService.Domain.Common;

public enum PersistenceStatusEnum
{
    /// <summary>
    /// Operation completed successfully.
    /// </summary>
    Success = 0,

    /// <summary>
    /// The requested entity was not found.
    /// </summary>
    NotFound = 1,

    /// <summary>
    /// The entity already exists and cannot be duplicated.
    /// </summary>
    Conflict = 2
}
