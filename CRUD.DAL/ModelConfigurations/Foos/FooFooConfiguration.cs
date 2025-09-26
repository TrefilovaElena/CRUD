using CRUD.BLL.Models.Foos;
using CRUD.DAL.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRUD.DAL.ModelConfigurations.Foos
{
    public class FooFooConfiguration : IEntityTypeConfiguration<FooFoo>
    {
        public void Configure(EntityTypeBuilder<FooFoo> builder)
        {
            builder.ToTable("FooFoos");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                   .ValueGeneratedOnAdd();

            builder.Property(p => p.Value)
                   .IsRequired();
        }
    }
}
