namespace Shared.Infrastructure.Mappers
{
    /// <typeparam name="T">To</typeparam>
    /// <typeparam name="TE">From</typeparam>
    public interface IMapper<in T, out TE>
    {
        TE Transform(T model);
    }
}