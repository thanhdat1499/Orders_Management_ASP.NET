using DemoIndentityCore.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DemoIndentityCore.Entities
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }


        public String CarName { get; set; }

        public double Price { get; set; }

        public String Photo { get; set; }

        public int Quantity { get; set; }


        public DateTime? CreateDate { get; set; }


        [ForeignKey("car")]
        public int CarId { get; set; }

        public Car car { get; set; }


        public String UserId { get; set; }

        [ForeignKey("UserId")]
        public DemoIndentityCoreUser User { get; set; }
    }
}
