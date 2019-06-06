using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Account.Models;
using System.Linq;
using System;

namespace Account.Models
{
    public class AccountContext : DbContext
    {
        public AccountContext(DbContextOptions options) : base(options) { }

        public DbSet<User> Users {get;set;}

        public int Create(User user)
        {
            PasswordHasher<User> Hasher = new PasswordHasher<User>();
            user.Password = Hasher.HashPassword(user, user.Password);
            Add(user);
            SaveChanges();
            return user.UserId;
        }

    }
}