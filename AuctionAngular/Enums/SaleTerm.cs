using System.ComponentModel.DataAnnotations;

namespace AuctionAngular.Enums
{
    public enum SaleTerm
    {
        none,
        [Display(Name = "Conditional repair")]
        Conditional_repair,
        [Display(Name = "Used vehicle")]
        Used_vehicle,
        [Display(Name = "To be dismantled")]
        To_be_dismantled,
        Classic
    }
}
