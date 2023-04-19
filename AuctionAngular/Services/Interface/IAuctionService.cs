using AuctionAngular.DTO;
using AuctionAngular.Models;
using Microsoft.AspNetCore.Mvc;

namespace AuctionAngular.Services.Interface
{
    public interface IAuctionService
    {
        Task<bool> LiveAuction();
        Task<IEnumerable<ViewVehicleDto>> LiveAuctionList();
    }
}
