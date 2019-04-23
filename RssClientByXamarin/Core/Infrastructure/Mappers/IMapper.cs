using JetBrains.Annotations;

namespace Core.Infrastructure.Mappers
{
    /// <typeparam name="T">To</typeparam>
    /// <typeparam name="TE">From</typeparam>
    public interface IMapper<in T, out TE>
    {
        [NotNull]
        TE Transform([CanBeNull] T model);
    }
}
