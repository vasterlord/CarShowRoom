using CarShowRoom.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarShowRoom.DataSource
{
    public class Context : DbContext
    {
        public Context() : base("DBConnection")
        {

        } 

        public DbSet<CarBrand> CarBrands { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Client> Clients { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<DecimalPropertyConvention>();
            modelBuilder.Conventions.Add(new DecimalPropertyConvention(15, 2));
            base.OnModelCreating(modelBuilder);
        }
    }
}
