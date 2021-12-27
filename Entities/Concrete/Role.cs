using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Core.Entities.Abstract;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Entities.Concrete
{
    public class Role:Entity
    {
        [Required]
        public string Name { get; set; }
        public List<User> Users { get; set; }
    }
}
