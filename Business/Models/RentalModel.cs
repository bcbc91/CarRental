using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Core.Entities.Abstract;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Business.Models
{
    public class RentalModel:Entity
    {
        public int CarId { get; set; }
        public int UserDetailId { get; set; }
        [DisplayName("Car")]
        public string CarName { get; set; }
        [DisplayName("Daily Price")]
        public double DailyPrice { get; set; }

        public DateTime RentDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public CarModel Car { get; set; }
        public UserDetailModel UserDetailModel { get; set; }
    }
}
