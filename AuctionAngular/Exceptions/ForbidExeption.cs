namespace AuctionAngular.Services
{
    public class ForbidExeption : Exception
    {
        public ForbidExeption(string message) : base(message)
        {
        }
    }
}
