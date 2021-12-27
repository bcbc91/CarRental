using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities.Abstract
{
    public abstract class Entity
    {
        public int Id { get; set; }
        public string  GuId { get; set; }
    }
}
