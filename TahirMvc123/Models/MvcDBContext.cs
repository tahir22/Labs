﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
 
using System.Linq;
using System.Web;
using TahirMvc123.Models;

namespace TahirMvc123.Models
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
        public DbSet<Role> Role { get; set; }
        public DbSet<UesrRole> UesrRole { get; set; }
        public DbSet<Userclaims> Userclaims { get; set; }
        //protected override void OnModelCreating(ModelBuilder builder)E:\ProjectNew2019\AllProject2020\Labs\TahirMvc123\Models\Role.cs
        //{
        //    base.OnModelCreating(builder);
        //}






    }
}
