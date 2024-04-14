using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RealTimeStockExchange.DAL.Entities;
using RealTimeStockExchange.Helpers.Enums;

namespace RealTimeStockExchange.DAL.Context
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            SeedUsersData(builder);
            SeedRolesData(builder);
            SeedUserRolesData(builder);
        }


        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Order> Orders { get; set; }


        private void SeedUsersData(ModelBuilder builder)
        {
            ApplicationUser AdminUser = new ApplicationUser()
            {
                Id = "b74ddd14-6340-4840-95c2-db12554843e5",
                UserName = "Admin",
                NormalizedUserName = "ADMIN",
                Email = "admin@gmail.com",
                NormalizedEmail = "ADMIN@GMAIL.COM",
                EmailConfirmed = true,
                LockoutEnabled = false,
                PhoneNumber = "1234567890"
            };
            ApplicationUser CustomerUser1 = new ApplicationUser()
            {
                Id = "b74ddd14-6340-4840-95c2-db1255484rr5",
                UserName = "Customer1",
                NormalizedUserName = "CUSTOMER1",
                Email = "customer1@gmail.com",
                NormalizedEmail = "CUSTOMER1@GMAIL.COM",
                EmailConfirmed = true,
                LockoutEnabled = false,
                PhoneNumber = "123456789"
            };

            ApplicationUser CustomerUser2 = new ApplicationUser()
            {
                Id = "b74ddd14-6340-4840-95c2-db1255484rt2",
                UserName = "CUSTOMER2",
                NormalizedUserName = "",
                Email = "customer2@gmail.com",
                NormalizedEmail = "CUSTOMER2@GMAIL.COM",
                EmailConfirmed = true,
                LockoutEnabled = false,
                PhoneNumber = "123456789"
            };


            PasswordHasher<ApplicationUser> passwordHasher = new PasswordHasher<ApplicationUser>();

            AdminUser.PasswordHash = passwordHasher.HashPassword(AdminUser, "Admin_123");
            CustomerUser1.PasswordHash = passwordHasher.HashPassword(CustomerUser1, "Customer_1");
            CustomerUser2.PasswordHash = passwordHasher.HashPassword(CustomerUser2, "Customer_2");

            builder.Entity<ApplicationUser>().HasData(AdminUser);
            builder.Entity<ApplicationUser>().HasData(CustomerUser1);
            builder.Entity<ApplicationUser>().HasData(CustomerUser2);

        }
        private void SeedRolesData(ModelBuilder builder)
        {
            builder.Entity<IdentityRole>()
                .HasData(
                    new IdentityRole {
                        Id = "fab4fac1-c546-41de-aebc-a14da6895711",
                        Name = nameof(UserTypes.Admin),
                        NormalizedName = nameof(UserTypes.Admin),
                        ConcurrencyStamp = "1"
                    },
                    new IdentityRole {
                        Id = "c7b013f0-5201-4317-abd8-c211f91b7330", 
                        Name = nameof(UserTypes.Customer),
                        NormalizedName = nameof(UserTypes.Customer),
                        ConcurrencyStamp = "2"
                    });
        }
        private void SeedUserRolesData(ModelBuilder builder)
        {
            builder.Entity<IdentityUserRole<string>>()
                .HasData(
                    new IdentityUserRole<string>() {
                        RoleId = "fab4fac1-c546-41de-aebc-a14da6895711",
                        UserId = "b74ddd14-6340-4840-95c2-db12554843e5"
                    },
                    new IdentityUserRole<string>()
                    {
                        RoleId = "c7b013f0-5201-4317-abd8-c211f91b7330",
                        UserId = "b74ddd14-6340-4840-95c2-db1255484rr5"
                    }, new IdentityUserRole<string>()
                    {
                        RoleId = "c7b013f0-5201-4317-abd8-c211f91b7330",
                        UserId = "b74ddd14-6340-4840-95c2-db1255484rt2"
                    }
                );
        }
    }
}
