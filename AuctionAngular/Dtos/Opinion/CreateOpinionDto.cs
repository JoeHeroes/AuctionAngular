﻿namespace AuctionAngular.Dtos.Opinion
{
    public class CreateOpinionDto
    {
        public string Description { get; set; }
        public string Origin { get; set; }
        public string Valuation { get; set; }
        public int Condition { get; set; }
        public string DescriptionInside { get; set; }
        public string DescriptionBodywork { get; set; }
        public bool ComplianceWithVIN { get; set; }
        public int VehicleId { get; set; }
    }
}
