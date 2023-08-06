namespace AuctionAngular.Interfaces
{
    public interface IProcessingService
    {
        Task DoWorkAsync(CancellationToken stoppingToken);
    }
}
