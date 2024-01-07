﻿
namespace Database.Entities
{
    public class Opinion
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Origin { get; set; }
        public string Valuation { get; set; }
        public int Condition { get; set; }
        public string DescriptionConditionInside { get; set; }
        public string DescriptionConditionBodywork { get; set; }
        public bool ComplianceWithVIN { get; set; }
        public int VehicleId { get; set; }
    }
}