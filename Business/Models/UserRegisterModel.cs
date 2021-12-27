using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Core.Entities.Abstract;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Business.Models
{
    public class UserRegisterModel:Entity
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
        [Required]
        [MinLength(5)]
        [MaxLength(15)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        public UserDetailModel UserDetail { get; set; }


    }
}
