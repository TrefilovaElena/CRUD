using CRUD.BLL.Models.Foos;
using CRUD.DAL.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRUD.DAL.ModelConfigurations.Foos
{
    public class FooConfiguration : IEntityTypeConfiguration<Foo>
    {
        public void Configure(EntityTypeBuilder<Foo> builder)
        {
            builder.ToTable("Foos");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                   .ValueGeneratedOnAdd();

            builder.Property(e => e.Title).IsRequired();
            builder.Property(e => e.Title)
                .HasMaxLength(FieldConstants.NewsTitleMaxLength)
                .IsRequired();

            builder.Property(p => p.FooFooId)
                .IsRequired();

            builder.HasOne(e => e.FooFoo);

            builder.Property(p => p.LastModifiedDate)
                   .IsRequired();
        }
    }
}
