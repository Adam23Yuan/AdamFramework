using Adam.ModelConfigurations;
using Adam.Models;
using Microsoft.EntityFrameworkCore;

namespace Adam.WebApi.Context
{
    /// <summary>
    /// 
    /// </summary>
    public class ApplicationDbContext : DbContext
    {
        /// <summary>
        /// 共享类型实体类型
        /// </summary>
        public DbSet<Dictionary<string, object>> BlogsCommon => Set<Dictionary<string, object>>("Blog");

        /// <summary>
        /// 
        /// </summary>
        public DbSet<Blog> Blogs { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public ApplicationDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging()
                .UseSqlServer(@"Server=localhost;Database=dev", options =>
                {
                    options.EnableRetryOnFailure();
                });
            base.OnConfiguring(optionsBuilder);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            #region 实体类型相关
            {
                // 集中配置 分组配置 选择一种即可
                // 1、集中配置
                //modelBuilder.Entity<Blog>().Property(b => b.Url).IsRequired();
                // 2、分组配置【为了减小OnModelCreate方法的大小】
                new BlogEntityTypeConfiguration().Configure(modelBuilder.Entity<Blog>());
                // 3、在实体属性上通过特性配置

                // 4、动态添加模型
                modelBuilder.Entity<AuditEntry>();
                // 映射表名称+架构名 或者通过在实体类的特性上指定
                modelBuilder.Entity<Blog>().ToTable("Blogs", schema: "dbo").HasComment("Blogs managed on the website");

                // 5、视图映射 
                modelBuilder.Entity<Blog>().ToView("blogsView", schema: "dbo");

                // 表值函数映射 
                // note 若要将实体映射到表值函数，函数必须是无参数的。
                modelBuilder.Entity<BlogWithMultiplePosts>().HasNoKey().ToFunction("BlogsWithMultiplePosts");

                // 6、共享类型实体类型
                modelBuilder.SharedTypeEntity<Dictionary<string, object>>(
                "Blog", bb =>
                {
                    bb.Property<int>("BlogId");
                    bb.Property<string>("Url");
                    bb.Property<DateTime>("LastUpdated");
                });
                // 7、主键配置
                modelBuilder.Entity<Blog>().HasKey(o => o.BlogId);
            }
            #endregion

            #region 实体属性相关
            {
                // 1、排除实体属性 将不和数据库列映射，也可以通过实体特性特性指定 [NotMapped]
                modelBuilder.Entity<Blog>().Ignore(o => o.LoadedFromDatabase);

                // 2、列名映射 也可以通过实体特性特性指定 [Column("blog_id")]
                modelBuilder.Entity<Blog>().Property(o => o.BlogId).HasColumnName("blog_id");

                // 3、数据库类型 也可以通过实体特性特性指定 [Column("blog_id")]
                modelBuilder.Entity<Blog>().Property(o => o.Rating).HasColumnType("decimal(5, 2)");
                modelBuilder.Entity<Blog>(eb =>
                {
                    // 列类型
                    eb.Property(o => o.Rating).HasColumnType("decimal(5, 2)");
                    // 最大长度，注释，列排序 
                    eb.Property(o => o.Url).HasMaxLength(500).HasComment("the url of the blog").HasColumnOrder(2);
                    // 列数值精度
                    eb.Property(o => o.Score).HasPrecision(14, 2);
                    // 默认值 
                    eb.Property(o => o.Rating).HasDefaultValue(1);
                    eb.Property(o => o.Created).HasDefaultValueSql("getdate()");
                });


            }
            #endregion
        }
    }
}
