using galleryprojectb.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace galleryprojectb.DAL
{
    public class galleryprojectbcontext : DbContext
    {

        public galleryprojectbcontext() : base("SchoolContext")
        {
        }

        public DbSet<User> User { get; set; }
        public DbSet<photo> photo { get; set; }

        public DbSet<Galleries> Gallery { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}