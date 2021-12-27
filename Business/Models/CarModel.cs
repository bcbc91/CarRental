using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Core.Entities.Abstract;
using Entities.Concrete;
using Entities.Concrete.Enums;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Business.Models
{
    public class CarModel:Entity
    {
        [Required]
        [StringLength(200)]
        public string Name { get; set; }
        [DisplayName("Brand")]
        public int BrandId { get; set; }
        [DisplayName("Color")]
        public int ColorId { get; set; }
        [DisplayName("Model Year")]
        public int ModelYear { get; set; }
        [DisplayName("Daily Price")]
        public double DailyPrice { get; set; }
        [DisplayName("Daily Price")]
        public string DailyPriceText { get; set; }
        [StringLength(500)] public string Description { get; set; }

        [DisplayName("Gear Type")]
        public GearType GearType { get; set; }
        [DisplayName("Fuel Type")]
        public FuelType FuelType { get; set; }
        public BrandModel Brand { get; set; }
        public ColorModel Color { get; set; }
        [DisplayName("Image")]
        public string ImagePath { get; set; }
      
    }
}
