﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodsMart.Bl;

public class ProductEditDto
{
    public int Id { get; set; }
    [MaxLength(50)]
    [MinLength(5)]
    public string Name { get; set; } = string.Empty;
    [Range(0, 1000000, ErrorMessage = "The Price must be between {0} and {1}")]
    public double Price { get; set; }
}
