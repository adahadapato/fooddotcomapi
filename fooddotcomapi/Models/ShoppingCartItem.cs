using System;
using System.ComponentModel.DataAnnotations;

namespace fooddotcomapi.Models
{
    public class ShoppingCartItem
    {
        [Key]
        public int Id { get; set; }
        public double Price { get; set; }
        public int Qty { get; set; }
        public double TotalAmount { get; set; }
        public int ProductId { get; set; }
        public Guid CustomerId { get; set; }
    }
}

