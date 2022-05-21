using System.ComponentModel.DataAnnotations;

namespace kinder_app.Models
{
    public enum CategoryEnum
    {
        [Display(Name = "Furniture")]
        Furniture,
        [Display(Name = "Clothing")]
        Clothing,
        [Display(Name = "Electronics")]
        Electronics
    }
}