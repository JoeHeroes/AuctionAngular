namespace AuctionAngular.Dtos.Opinion
{
    public class CreateOpinionDto
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Origin { get; set; }
        public string Valuation { get; set; }
        public int assessConditionInside { get; set; }
        public int assessConditionOutsite { get; set; }
        public int VehicleId { get; set; }
    }
}
