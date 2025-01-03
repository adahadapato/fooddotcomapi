﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace fooddotcomapi.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Detail { get; set; }
        public string ImageUrl { get; set; }
        public double Price { get; set; }
        public bool IsPopularProduct { get; set; }
        public int CategoryId { get; set; }

        [NotMapped]
        public IFormFile Image { get; set; }

        [JsonIgnore]
        public ICollection<OrderDetail> OrderDetails { get; set; }

        [JsonIgnore]
        public ICollection<ShoppingCartItem> ShoppingCartItems { get; set; }
    }
}

