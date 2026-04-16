using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PustokApp.Models;

namespace PustokApp.Data.Configurations
{
    public class BookImageConfiguration : IEntityTypeConfiguration<BookImage>
    {
        public void Configure(EntityTypeBuilder<BookImage> builder)
        {
            builder.Property(x => x.Id)
                   .HasDefaultValueSql("NEWID()"); // <-- SQL сам генерирует Guid
        }
    }
}