using Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Text;
using Entities.Concrete.Enums;

namespace Entities.Concrete
{
    public class Car : Entity
    {
        [Required] 
        [StringLength(200)] 
        public string Name { get; set; }

        public int BrandId { get; set; }
        public int ColorId { get; set; }
        public int ModelYear { get; set; }
        public double DailyPrice { get; set; }
        [StringLength(500)] public string Description { get; set; }
        public GearType GearType { get; set; }
        public FuelType FuelType { get; set; }

        public Brand Brand { get; set; }
        public Color Color { get; set; }
        
        public List<Rental> Rentals { get; set; }

        public string ImagePath { get; set; }
    }

}
