using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Core.Entities.Abstract;

namespace Entities.Concrete
{
    public class UserDetail:Entity
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [Required]
        [StringLength(50)]
        public string Surname { get; set; }
        [Required]
        [StringLength(11)]
        public string IdentityNumber { get; set; }
        [Required]
        [StringLength(20)]
        public string Phone { get; set; }
        [Required]
        public string EMail { get; set; }

        public string Address { get; set; }
        public string Description { get; set; }
        public List<Rental> Rentals { get; set; }
        public User User { get; set; }
    }
}
