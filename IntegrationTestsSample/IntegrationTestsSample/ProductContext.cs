﻿using IntegrationTestsSample.Models;
using Microsoft.EntityFrameworkCore;

namespace IntegrationTestsSample
{
    public class ProductContext : DbContext
    {
        public ProductContext(DbContextOptions options) : base(options) { }
        public DbSet<Product> Products { get; set; }
    }
}
