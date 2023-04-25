using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheITBlog.Data;
using TheITBlog.Enums;
using TheITBlog.Models;

namespace TheITBlog.Services
{
    public class DataService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<BlogUser> _userManager;

        public DataService(ApplicationDbContext dbContext,
                           RoleManager<IdentityRole> roleManager,
                           UserManager<BlogUser> userManager)
        {
            _dbContext = dbContext;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task ManageDataAsync()
        {
            //Create the Database from the Migrations
            await _dbContext.Database.MigrateAsync();

            //Task 1: Seed some Roles into the system Method
            await SeedRolesAsync();

            //Task 2: Seed some Users into the system Method
            await SeedUsersAsync();
        }

        //Task 1
        private async Task SeedRolesAsync()
        {
            //If there are already Roles in the system, do nothing.
            if (_dbContext.Roles.Any())
            {
                return;
            }

            //Otherwise create some Roles
            foreach (var role in Enum.GetNames(typeof(BlogRole)))
            {
                //I need to use the Role Manager to create roles.
                await _roleManager.CreateAsync(new IdentityRole(role));
            }
        }

        //Task 2
        private async Task SeedUsersAsync()
        {
            //If there are already Users in the system, do nothing.
            if (_dbContext.Users.Any())
            {
                return;
            }

            //Step 1: Create a new instance of BlogUser
            var adminUser = new BlogUser()
            {
                Email = "DaveT@email.com",
                UserName = "DaveT@email.com",
                FirstName = "Dave",
                LastName = "Thompson",
                DisplayName = "The Master",
                PhoneNumber = "(800) 555 1212",
                EmailConfirmed = true
            };

            //Step 2: Use the UserManager to create a new user that is defined by the adminUser variable
            await _userManager.CreateAsync(adminUser, "Abc123!");

            //Step 3: Add this new user to the Administrator role
            await _userManager.AddToRoleAsync(adminUser, BlogRole.Administrator.ToString());

            //Step 1 Repeat: Create the Moderator user
            var modUser = new BlogUser()
            {
                Email = "JonT@email.com",
                UserName = "JonT@email.com",
                FirstName = "Jon",
                LastName = "Thomas",
                DisplayName = "The Student",
                PhoneNumber = "(800) 555 1212",
                EmailConfirmed = true
            };

            await _userManager.CreateAsync(modUser, "Abc123!");
            await _userManager.AddToRoleAsync(modUser, BlogRole.Moderator.ToString());
        }
    }
}
