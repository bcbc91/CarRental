using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Core.Entities.Abstract;

namespace Entities.Concrete
{
    public class Color:Entity
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public List<Car> Cars { get; set; }
    }
}
