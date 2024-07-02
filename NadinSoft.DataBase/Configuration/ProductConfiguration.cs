using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NadinSoft.Domain.Entities.Products;

namespace NadinSoft.DataBase.Configuration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasOne(x => x.Creator)
                .WithMany()
                .HasForeignKey(fk => fk.CreaetorId);

            builder.Property(x => x.Name)
                .HasMaxLength(150);

            builder.Property(x => x.ProduceDate)
                .HasColumnType("datetime(0)");

            builder.Property(x => x.ManufactureEmail)
                .HasMaxLength(250);

            builder.Property(x => x.ManufacturePhone)
                .HasMaxLength(11);

            builder.HasIndex(x => x.ManufactureEmail)
                .IsUnique();

            builder.HasIndex(x => x.ProduceDate)
                .IsUnique();

            builder.Property(x => x.CreatedAt)
                .HasColumnType("datetime(0)");

            builder.Property(x => x.ModifiedAt)
                .HasColumnType("datetime(0)");

            builder.Property(x => x.RemovedAt)
                .HasColumnType("datetime(0)");
        }
    }
}
