namespace AuctionAngular.Dtos.Opinion
{
    public class ViewOpinionDto
    {
        public string Description { get; set; }
        public string Origin { get; set; }
        public string Valuation { get; set; }
        public int Condition { get; set; }
        public string DescriptionConditionInside { get; set; }
        public string DescriptionConditionBodywork { get; set; }
        public bool ComplianceWithVIN { get; set; }
    }
}
