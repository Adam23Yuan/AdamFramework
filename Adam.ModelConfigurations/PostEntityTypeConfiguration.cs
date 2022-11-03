using Adam.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.VisualBasic;

namespace Adam.ModelConfigurations
{
    public class PostEntityTypeConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            // 若要在 Fluent API 中配置关系，首先应标识构成关系的导航属性。
            // HasOne 或 HasMany 标识要开始配置的实体类型的导航属性。 然后，将调用链接到
            // WithOne 或 WithMany 以标识反向导航。 
            // HasOne/WithOne 用于引用导航属性，
            // HasMany/WithMany 用于集合导航属性
            // 外键指定方式 [外键 or 影子 外键]
            builder.HasOne(o => o.Blog).WithMany(o => o.Posts)
                .OnDelete(DeleteBehavior.Cascade)
                .HasForeignKey(o => o.BlogForeignKey).HasConstraintName("ForeignKey_Post_Blog");
            // 无导航属性
            builder.HasOne<Blog>().WithMany().HasForeignKey(p => p.BlogId);
        }
    }
}
