﻿using AuctionAngular.Dtos;
using Database.Entities;

namespace AuctionAngular.Interfaces
{
    public interface IAuctionService
    {
        Task<bool> LiveAuctionAsync();
        Task StartAuctionAsync();
        Task EndAuctionAsync();
        Task<IEnumerable<ViewVehicleDto>> LiveAuctionListAsync();
        Task<IEnumerable<Auction>> AuctionListAsync();
    }
}
