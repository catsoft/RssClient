using JetBrains.Annotations;

namespace Core.Database
{
    public interface IHaveId
    {
        [CanBeNull] string Id { get; set; }
    }
}
