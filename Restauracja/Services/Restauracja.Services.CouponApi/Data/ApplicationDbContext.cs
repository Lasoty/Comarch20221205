﻿using Microsoft.EntityFrameworkCore;
using Restauracja.Services.CouponApi.Model;

namespace Restauracja.Services.CouponApi.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {

    }


    public DbSet<Coupon> Coupons { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);


        modelBuilder.Entity<Coupon>().HasData(new Coupon
        {
            Id = 1,
            CouponCode = "10OFF",
            DiscountAmount = 10
        });
        modelBuilder.Entity<Coupon>().HasData(new Coupon
        {
            Id = 2,
            CouponCode = "20OFF",
            DiscountAmount = 20
        });
    }
}
