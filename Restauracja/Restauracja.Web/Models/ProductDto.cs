﻿namespace Restauracja.Web.Models;

public class ProductDto
{
    public long Id { get; set; }

    public string Name { get; set; }

    public double Price { get; set; }

    public string? Description { get; set; }

    public string CategoryName { get; set; }

    public string? ImageUrl { get; set; }

    public int Count { get; set; }
}