using CRUD.BLL.Models.Foos;
using CRUD.DAL.ModelConfigurations.Foos;
using Microsoft.EntityFrameworkCore;

namespace CRUD.DAL.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        { }

        public DbSet<Foo> Foos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new FooConfiguration());
            modelBuilder.ApplyConfiguration(new FooFooConfiguration());

            #region Seed some test data
            modelBuilder.Entity<FooFoo>().HasData(TestData.FooFoos);
            modelBuilder.Entity<Foo>().HasData(TestData.Foos);

            #endregion Seed some test data
            base.OnModelCreating(modelBuilder);
        }
    }
}
