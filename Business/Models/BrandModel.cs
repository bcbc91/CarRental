using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Core.Entities.Abstract;

namespace Business.Models
{
    public class BrandModel:Entity
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public int CarCount { get; set; }

        


    }
}
