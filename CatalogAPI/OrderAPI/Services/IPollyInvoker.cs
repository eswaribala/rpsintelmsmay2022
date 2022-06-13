namespace OrderAPI.Services
{
    public interface IPollyInvoker
    {
        Task<String> EnsureCancellation(CancellationToken cancellationToken);
    }
}
