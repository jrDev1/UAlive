using Ludiq;

namespace Lasm.UAlive
{
    /// <summary>
    /// An enum of Linq query operations.
    /// </summary>
    [RenamedFrom("Lasm.BoltExtensions.QueryOperation")]
    public enum QueryOperation
    {
        Any,
        AnyWithCondition,
        First,
        FirstOrDefault,
        OrderBy,
        OrderByDescending,
        Single,
        Where
    }
}