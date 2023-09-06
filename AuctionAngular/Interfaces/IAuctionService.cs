﻿using AuctionAngular.Dtos;
using Database.Entities;

namespace AuctionAngular.Interfaces
{
    public interface IAuctionService
    {
        Task<bool> LiveAuctionAsync();
        Task<bool> StartedAuctionAsync();
        Task StartAuctionAsync();
        Task EndAuctionAsync();
        Task<IEnumerable<ViewVehicleDto>> LiveAuctionListAsync();
        Task<IEnumerable<ViewAuctionDto>> AuctionListAsync();
        ViewVehicleDto ViewVehicleDtoConvert(Vehicle vehicle, List<string> pictures);
        Task<ViewAuctionDto> ViewAuctionDtoConvert(Auction auction);
    }
}
