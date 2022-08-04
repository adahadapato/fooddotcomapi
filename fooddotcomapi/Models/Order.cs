using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace fooddotcomapi.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        [StringLength(60)]
        public string FullName { get; set; }
        [StringLength(100)]
        public string Address { get; set; }
        [StringLength(30)]
        public string Phone { get; set; }
        public double OrderTotal { get; set; }
        public DateTime OrderPlaced { get; set; }
        public bool IsOrderCompleted { get; set; }
        public Guid UserId { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
}

