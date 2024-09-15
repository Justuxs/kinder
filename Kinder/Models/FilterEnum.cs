using System.ComponentModel.DataAnnotations;

namespace kinder_app.Models
{
    public enum FilterEnum
    {
        [Display(Name = "Show All")]
        All,
        [Display(Name = "Show Private")]
        Private,
        [Display(Name = "Show Public")]
        Public
    }
}