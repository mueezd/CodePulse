using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CodePulse.API.Data
{
    public class AuthDbContext : IdentityDbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var readerRoleId = "518277b6-1a5f-4c40-a581-50f57b2f1fa2";
            var writerRoleId = "5ab70174-7e3b-4c3b-a708-0203c440b600";

            // Create Reader ANd Write Role

            var roles = new List<IdentityRole>
            {
                new IdentityRole()
                {
                    Id = readerRoleId,
                    Name = "Reader",
                    NormalizedName = "Reader".ToUpper(),
                    ConcurrencyStamp =  readerRoleId
                },
                new IdentityRole()
                {
                    Id=writerRoleId,
                    Name = "Writer",
                    NormalizedName = "Writer".ToUpper(),
                    ConcurrencyStamp = writerRoleId
                }
            };

            //Seed Role

            builder.Entity<IdentityRole>().HasData(roles);

            //Create an Admin User
            var adminUserId = "0ec20de9-1900-4d55-a7fc-ea681960e3b8";

            var admin = new IdentityUser()
            {
                Id = adminUserId,
                UserName = "deepro@deepro.com",
                Email = "deepro@deepro.com",
                NormalizedEmail = "deepro@deepro.com".ToUpper(),
                NormalizedUserName = "deepro@deepro.com".ToUpper(),
            };

            admin.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(admin, "Admin@123");

            builder.Entity<IdentityUser>().HasData(admin);

            //Give Roles to admin

            var adminRoles = new List<IdentityUserRole<string>>()
            {
                new()
                {
                    UserId = adminUserId,
                    RoleId = readerRoleId
                },
                  new()
                {
                    UserId = adminUserId,
                    RoleId = writerRoleId
                }
            };

            builder.Entity<IdentityUserRole<string>>().HasData(adminRoles);
        }
    }
}
