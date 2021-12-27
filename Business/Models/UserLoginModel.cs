using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Core.Entities.Abstract;

namespace Business.Models
{
    public class UserLoginModel:Entity
    {
        [Required]
        [MinLength(3)]
        [MaxLength(30)]
        [DisplayName("User Name")]
        public string UserName { get; set; }
        [Required]
        [MinLength(5)]
        [MaxLength(15)]
        public string Password { get; set; }

        public string RoleName { get; set; }
    }
}
