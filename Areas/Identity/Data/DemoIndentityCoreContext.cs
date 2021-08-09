using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DemoIndentityCore.Areas.Identity.Data;
using DemoIndentityCore.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DemoIndentityCore.Data
{
    public class DemoIndentityCoreContext : IdentityDbContext<DemoIndentityCoreUser>
    {
        public DemoIndentityCoreContext(DbContextOptions<DemoIndentityCoreContext> options)
            : base(options)
        {
        }

        public DbSet<Car> Cars { get; set; }

        public DbSet<Order> Orders { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
            this.SeedRoles(builder);
            this.SeedUser(builder);
            this.SeedUserToRole(builder);
            this.SeedCars(builder);
        }

        // Tạo Roles
        private void SeedRoles(ModelBuilder builder)
        {
            builder.Entity<IdentityRole>().HasData(
                new IdentityRole()
                {
                    Id = "fab4fac1-c546-41de-aebc-a14da6895711",
                    Name = "Admin",
                    //ConcurrencyStamp = "1",
                    NormalizedName = "Admin"
                }
            );
        }

        // User
        private void SeedUser(ModelBuilder builder)
        {
            DemoIndentityCoreUser user = new DemoIndentityCoreUser()
            {
                Id = "b74ddd14-6340-4840-95c2-db12554843e5",
                UserName = "admin@abc.com",
                NormalizedUserName = "admin@abc.com",

                Email = "admin@abc.com",
                NormalizedEmail = "admin@abc.com",
                LockoutEnabled = false,
                PhoneNumber = "1234567890",

                SecurityStamp = Guid.NewGuid().ToString(),

            };

            PasswordHasher<DemoIndentityCoreUser> pHash = new PasswordHasher<DemoIndentityCoreUser>();
            user.PasswordHash = pHash.HashPassword(user, "Admin*123");
            user.EmailConfirmed = true;
            user.LockoutEnabled = false;


            builder.Entity<DemoIndentityCoreUser>().HasData(user);
        }

        // Gán User vô Role
        private void SeedUserToRole(ModelBuilder builder)
        {
            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>()
                {
                    RoleId = "fab4fac1-c546-41de-aebc-a14da6895711",
                    UserId = "b74ddd14-6340-4840-95c2-db12554843e5"
                }
            );
        }

        

        private void SeedCars(ModelBuilder builder)
        {
            builder.Entity<Car>().HasData(
                new Car() { CarId = 1, CarName = "Mercedes-benz C-Class Sedan", Price = 4299000000, Quantity = 5, Photo = "image1.png" },
                new Car() { CarId = 2, CarName = "Mercedes-benz E-Class Sedan", Price = 2050000000, Quantity = 5, Photo = "image2.png" },
                new Car() { CarId = 3, CarName = "Mercedes-benz GLB SUV", Price = 1999000000, Quantity = 5, Photo = "image3.png" },
                new Car() { CarId = 4, CarName = "Mercedes-benz GLE Coupe ", Price = 3000000, Quantity = 5, Photo = "image4.png" },
                new Car() { CarId = 5, CarName = "Mercedes-benz A-Class Sedan", Price = 2259000000, Quantity = 5, Photo = "image5.png" }
             );
        }
    }
}
