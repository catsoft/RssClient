using JetBrains.Annotations;

namespace Shared.Database
{
    public interface IHaveId
    {
        [CanBeNull] string Id { get; set; }
    }
}
