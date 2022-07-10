using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace RentalProject.Models
{
    public class Premises
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]

        public decimal Price { get; set; }
        public string Image { get; set; }
        [Display(Name = "Premises District")]
        public string PremisesDistrict { get; set; }
        [Required]
        [Display(Name = "Availability of equipment")]
        public bool IsEquipAvailable { get; set; }

        [Display(Name = "Premises Type")]
        [Required]
        public int PremisesTypeId { get; set; }
        [ForeignKey("PremisesTypeId")]
        public virtual PremisesTypes PremisesTypes { get; set; }
        [Display(Name = "Special Tag")]
        [Required]
        public int SpecialTagId { get; set; }
        [ForeignKey("SpecialTagId")]
        public virtual SpecialTag SpecialTag { get; set; }
    }
}
