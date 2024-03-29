﻿namespace AuctionAngular.Dtos.Vehicle
{
    public class ViewAdminVehiclesDto
    {
        public int LotNumber { get; set; }
        public int AuctionNumber { get; set; }
        public string Producer { get; set; }
        public string ModelSpecifer { get; set; }
        public string ModelGeneration { get; set; }
        public int RegistrationYear { get; set; }
        public long MeterReadout { get; set; }
        public DateTime DateTime { get; set; }
        public int CurrentBid { get; set; }
        public bool isSold { get; set; }
        public bool isConfirm { get; set; }
    }
}
