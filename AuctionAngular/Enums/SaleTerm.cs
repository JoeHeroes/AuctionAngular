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
        [Display(Name = "To be desmantled")]
        To_be_desmantled,
        Classic
    }
}
