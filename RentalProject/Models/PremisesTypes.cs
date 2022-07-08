using System.ComponentModel.DataAnnotations;

namespace RentalProject.Models
{
    public class PremisesTypes
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Premises Type")]
        public string PremisesType { get; set; }

    }
}
