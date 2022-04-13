using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azmoon.Domain.Entities;

namespace Azmoon.Persistence.Contexts
{
    public static class ModelBuilderExtension
    {
        public static void TicketSeed(this ModelBuilder modelBuilder)
        {

            var userr = new User
            {
                Id = Guid.NewGuid().ToString(),
                FirstName = "Kardin",
                LastName = "BestGroup",
                Phone = "32458",
                UserName = "13971397",
                NormalizedUserName = "13971397",
                NormalizedEmail = "Kardin@email.com",
                Email = "Kardin@email.com",
               // NormalizedEmail = "Kardin@email.com",
                EmailConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = Guid.NewGuid().ToString(),
                GroupId = 1
            };
            var password = new PasswordHasher<User>();
            var hashed = password.HashPassword(userr, "K@rd!N1399");
            userr.PasswordHash = hashed;
            modelBuilder.Entity<User>().HasData(userr);
     
            var Public = new Role
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Public",
                NormalizedName = "Public",
                Description = "عمومی",

            };
     
            var Registered = new Role
            {
                Id = Guid.NewGuid().ToString(),
                ParentId = Public.Id,
                Name = "Registered",
                NormalizedName = "Registered",
                Description = "کاربر عضو",

            };
            var Writer = new Role
            {
                Id = Guid.NewGuid().ToString(),
                ParentId = Registered.Id,
                Name = "Writer",
                NormalizedName = "Writer",
                Description = "نویسنده",

            };
            var Editor = new Role
            {
                Id = Guid.NewGuid().ToString(),
                ParentId = Writer.Id,
                Name = "Editor",
                NormalizedName = "Editor",
                Description = "ویراشگر",

            };
             var Publisher = new Role
            {
                Id = Guid.NewGuid().ToString(),
                ParentId = Editor.Id,
                Name = "Publisher",
                NormalizedName = "Publisher",
                Description = "منتشرکننده",

            };
            var Distributor = new Role
            {
                Id = Guid.NewGuid().ToString(),
                ParentId = Publisher.Id,
                Name = "Distributor",
                NormalizedName = "Distributor",
                Description = "توزیع کننده",

            };
         
            var Admin = new Role
            {
                Id = Guid.NewGuid().ToString(),
                ParentId = Distributor.Id,
                Name = "Admin",
                NormalizedName = "Admin",
                Description = "مدیر",

            };
            var SuperAdmin = new Role
            {
                Id = Guid.NewGuid().ToString(),
                ParentId = Admin.Id,
                Name = "SuperAdmin",
                NormalizedName = "SuperAdmin",
                Description = "مدیر ارشد سایت",

            };
            modelBuilder.Entity<Role>().HasData(Public, Registered, Writer, Editor, Distributor , Publisher, Admin , SuperAdmin);
            modelBuilder.Entity<Group>().HasData(
               new Group
               {
                   Id = 1,
                   Name = "فناوری اطلاعات و ارتباطات",
                   ParentId = null
               });
   
           
        }
    }
}
