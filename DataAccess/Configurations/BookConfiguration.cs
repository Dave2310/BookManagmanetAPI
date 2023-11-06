using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Configurations
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Title)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(x => x.Author)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(x => x.Content) 
                .IsRequired()
                .HasMaxLength(150);

            builder.HasOne(x => x.Category)
                .WithMany(x => x.Books)
                .HasForeignKey(x => x.CategoryId);

            builder.HasMany(x => x.Reviews)
                .WithOne(x => x.Book)
                .HasForeignKey(x => x.BookId);
        }
    }
}
