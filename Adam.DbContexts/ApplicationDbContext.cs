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
            // 集中配置 分组配置 选择一种即可
            // 1、集中配置
            //modelBuilder.Entity<Blog>().Property(b => b.Url).IsRequired();
            // 2、分组配置【为了减小OnModelCreate方法的大小】
            new BlogEntityTypeConfiguration().Configure(modelBuilder.Entity<Blog>());
            // 3、在实体属性上通过特性配置

            // 动态添加模型
            modelBuilder.Entity<AuditEntry>();
            // 映射表名称+架构名 或者通过在实体类的特性上指定
            modelBuilder.Entity<Blog>().ToTable("Blogs", schema: "dbo").HasComment("Blogs managed on the website");

            // 视图映射 
            modelBuilder.Entity<Blog>().ToView("blogsView", schema: "dbo");

            // 表值函数映射 
            // note 若要将实体映射到表值函数，函数必须是无参数的。
            modelBuilder.Entity<BlogWithMultiplePosts>().HasNoKey().ToFunction("BlogsWithMultiplePosts");

            // 共享类型实体类型
            modelBuilder.SharedTypeEntity<Dictionary<string, object>>(
            "Blog", bb =>
            {
                bb.Property<int>("BlogId");
                bb.Property<string>("Url");
                bb.Property<DateTime>("LastUpdated");
            });
        }
    }
}
