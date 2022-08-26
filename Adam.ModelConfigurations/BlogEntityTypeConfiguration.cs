using Adam.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Adam.ModelConfigurations
{
    public class BlogEntityTypeConfiguration : IEntityTypeConfiguration<Blog>
    {
        public void Configure(EntityTypeBuilder<Blog> builder)
        {
            builder
            .Property(b => b.Url)
            .IsRequired()
            .HasMaxLength(500)
            .HasComment("the url of the blog")
            .HasColumnOrder(2);
            builder
            .Property(b => b.Score)
            .HasPrecision(14, 2);
        }
    }
}
