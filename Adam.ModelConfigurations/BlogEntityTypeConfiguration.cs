using Adam.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.VisualBasic;

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

            //// 设置组合键。只能通过 FluentApi 进行配置
            // 主键名称
            // builder.HasKey(c => new { c.BlogId, c.LicensePlate }).HasName("PrimaryKey_BlogId");

            // Timestamp/rowversion 是每次插入行或更新行时数据库自动生成新值的属性。 该属性也被视为并发令牌，确保在查询后要更新的行发生更改时得到异常。 具体的详细信息取决于所使用的数据库提供程序；对于 SQL Server，通常使用 byte[] 属性，该属性将设置为数据库中的 ROWVERSION 列。
            // 可以将属性配置为 timestamp/ rowversion
            builder.Property(o => o.Timestamp).IsRowVersion();

            builder.HasMany(o => o.Posts).WithOne();

            // 索引|索引筛选|索引包含列
            builder.HasIndex(p => p.Url).IsUnique().HasDatabaseName("Index_Url").HasFilter("[Url] IS NOT NULL").IncludeProperties(p => new { p.Title });
            // 检查约束
#warning 
            //builder.HasCheckConstraint("CK_Prices", "[Price] > [DiscountedPrice]", c => c.HasName("CK_Product_Prices"));
        }
    }
}
