using AdmissionSystemAPI.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdmissionSystemAPI.Models
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            var saId = Guid.NewGuid();
            var aId = Guid.NewGuid();
            var scId = Guid.NewGuid();
            var sdId = Guid.NewGuid();
            var suaid = Guid.NewGuid();
            var aid = Guid.NewGuid();
            modelBuilder.Entity<Role>().HasData(
                new Role
                {
                    Id= saId,
                    Name = "Super Admin",
                    NormalizedName="SUPER ADMIN"

                },
                    new Role
                    {
                        Id = aId,
                        Name = "Admin",
                        NormalizedName="ADMIN"

                    },
                    new Role
                    {
                        Id = scId,
                        Name = "Schools",
                        NormalizedName="SCHOOLS"
                    },
                    new Role
                    {
                        Id = sdId,
                        Name = "Students",
                        NormalizedName= "STUDENTS"
                    }
                );
            var passwordHaser = new PasswordHasher();

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id=suaid,
                    UserName = "Super Admin",
                    Email = "superadmin@admin.com",
                    PasswordHash = passwordHaser.HashPassword("Test@123")

                }, new User
                {
                    Id= aid,
                    UserName = "Admin",
                    Email = "admin@admin.com",
                    PasswordHash = passwordHaser.HashPassword("Test@1234")

                }
                );
            modelBuilder.Entity<UserRole>().HasData(
                new UserRole
                {
                    RoleId= saId,
                    UserId= suaid

                }, new UserRole
                {
                    RoleId = aId,
                    UserId = aid

                }
                );
            modelBuilder.Entity<EducationSystem>().HasData(
                new EducationSystem
                {
                    Id=1,
                    Type = "Boys"

                }, new EducationSystem
                {
                    Id = 2,
                    Type = "Girls"

                }, new EducationSystem
                {
                    Id = 3,
                    Type = "Co-Education"

                }
                );
        }
    }
}
