﻿using System.ComponentModel.DataAnnotations;

namespace Web.APIs.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public decimal Price { get; set; }

        public string? Description { get; set; }

        public int? CategoryId { get; set; }

        public Category Category { get; set; }
    }
}
