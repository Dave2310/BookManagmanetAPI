
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Configurations
{
    public class ReviewConfiguration : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.ReviewerEmail)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(x => x.Rating)
                .IsRequired();
                
            builder.Property(x => x.Comment)
                .IsRequired()
                .HasMaxLength(200);

            builder.HasOne(x => x.Book)
                .WithMany(x => x.Reviews)
                .HasForeignKey(x => x.BookId);
        }
    }
}
