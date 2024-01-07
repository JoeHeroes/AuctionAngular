using System.ComponentModel.DataAnnotations;

namespace AuctionAngular.Enums
{
    public enum Damage
    {
        none,
        [Display(Name = "All Over")]
        All_Over,
        Burn,
        [Display(Name = " Burn Engine")]
        Burn_Engine,
        [Display(Name = "Front End")]
        Front_End,
        Hail,
        Mechanical,
        [Display(Name = "Minor Dents Scratch")]
        Minor_Dents_Scratch,
        [Display(Name = "Normal Wear")]
        Normal_Wear,
        [Display(Name = "Rear End")]
        Rear_End,
        Rollover,
        Side,
        [Display(Name = "Top Roof")]
        Top_Roof,
        Undercarriage,
        Unknown,
        Vandalism
    }
}
