using Microsoft.EntityFrameworkCore;
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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }


       

     

}
}
