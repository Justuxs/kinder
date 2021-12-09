using System.ComponentModel.DataAnnotations;

namespace kinder_app.Models
{
    public enum ConditionEnum
    {
        [Display(Name = "Mint")] 
        Mint,
        [Display(Name = "Near mint")] 
        Near_mint,
        [Display(Name = "Very good")] 
        Very_good,
        [Display(Name = "Good")] 
        Good,
        [Display(Name = "Fair")] 
        Fair,
        [Display(Name = "Poor")] 
        Poor,
        [Display(Name = "Very poor")] 
        Very_poor,
        [Display(Name = "Broken")]
        Broken
    }
}