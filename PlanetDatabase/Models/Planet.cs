using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PlanetDatabase.Models
{
    public class Planet : BaseEntity
    {
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Name { get; set; }

        [Required] 
        public decimal KmFromSun { get; set; }
    }
}