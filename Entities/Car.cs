using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DemoIndentityCore.Entities
{
    public class Car
    {
            [Key]
            public int CarId { get; set; }

            public String CarName { get; set; }

            public double Price { get; set; }

            public String Photo { get; set; }

            public int Quantity { get; set; }

            public ICollection<Order> Orders { get; set; }
        
    }
}
