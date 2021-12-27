using System;
using System.Collections.Generic;
using System.Text;
using Core.Entities.Abstract;

namespace Entities.Concrete
{
    public class Rental:Entity
    {
        public int CarId { get; set; }
        public int UserDetailId { get; set; }
        public DateTime RentDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public Car Car { get; set; }
        public UserDetail UserDetail { get; set; }
    }
}
