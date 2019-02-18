namespace Shared.Repository
{
    /// <typeparam name="T">Model</typeparam>
    /// <typeparam name="TE">Data</typeparam>
    public interface IMapper<in T, out TE>
    {
        TE Transform(T model);
    }
}