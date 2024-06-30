using Microsoft.EntityFrameworkCore;
using Serv_CouponAPI.Models;

namespace Serv_CouponAPI.DATABASE
{
    public class ApplicationDBContext: DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> option): base(option)
        {
            
        }
        public DbSet<Coupon> Coupons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Coupon>().HasData(new Coupon
            {
                CouponId = 1,
                CouponCode="10OFF",
                DiscountAmount= 10,
                MinAmount=20
               

            });

            modelBuilder.Entity<Coupon>().HasData(new Coupon
            {
                CouponId = 2,
                CouponCode="20OFF",
                DiscountAmount= 30,
                MinAmount=10


            });
        }
    }
}
