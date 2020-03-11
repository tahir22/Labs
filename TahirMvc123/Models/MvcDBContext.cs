using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Web;
using TahirMvc123.Models;

namespace TahirMvc123
{
    public class MvcDBContext : DbContext
    {
        public MvcDBContext(DbContextOptions<MvcDBContext> options) : base(options) { }

        public DbSet<Customers> Customers { get; set; }

        public DbSet<Vlilage> Vlilage { get; set; }
        public DbSet<Cast> Cast { get; set; }
        public DbSet<Family> Family { get; set; }
        public DbSet<FamilyMember> FamilyMember { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<RoleClaim> RoleClaims { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

         
            builder.Entity<UserRole>().HasIndex(x => new {
                x.UserId,
                x.RoleId
            }).IsUnique();
        } 

    
    }
}
