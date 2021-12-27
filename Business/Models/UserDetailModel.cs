using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Core.Entities.Abstract;
using Entities.Concrete;

namespace Business.Models
{
    public class UserDetailModel:Entity
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [Required]
        [StringLength(50)]
        public string Surname { get; set; }
        [Required]
        [StringLength(11)]
        [DisplayName("Identity Number")]
        public string IdentityNumber { get; set; }
        [Required]
        [StringLength(20)]
        [Phone]
        public string Phone { get; set; }
        [Required]
        [EmailAddress]
        [DisplayName("E-Mail")]
        public string EMail { get; set; }

        public string Address { get; set; }
        
        
       
    }
}
