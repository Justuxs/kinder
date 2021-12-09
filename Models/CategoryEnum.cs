using System.ComponentModel.DataAnnotations;

namespace kinder_app.Models
{
    public enum CategoryEnum
    {
        [Display(Name = "Furniture")]
        Furniture,
        [Display(Name = "Transport")]
        Transport,
        [Display(Name = "Technology")]
        Technology,
        [Display(Name = "Education")]
        Education
    }
}