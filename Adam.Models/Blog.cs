using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Adam.Models
{
    /// <summary>
    /// 特性说明
    /// <para>1.Table 使用特性指定表名加 框架名 也可以在 OnModelCreating 使用 ToTable 中指定</para>
    /// <para>2.Comment 指定表注释 也可以在 OnModelCreating 中使用 HasComment 指定</para>
    /// <para>3.Index 指定表索引 也可以在 OnModelCreating 中使用 HasIndex(p=>p.Url).IsUnique().HasDatabaseName 指定</para>
    /// </summary>
    [Table("Blogs", Schema = "dbo")]
    [Comment("Blogs managed on the website")]
    [Index(nameof(Url), IsUnique = true, Name = "Index_Url")]
    public class Blog
    {
        /// <summary>
        /// 
        /// <para>映射数据库列名</para>
        /// <para>1.也可以在  OnModelCreating 方法中 通过属性指定 HasColumnName</para>
        /// </summary>
        [Key]
        [Column("blog_id")]
        public int BlogId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Required]
        [MaxLength(500)]
        [Comment("the url of the blog")]
        [Column(Order = 2)]
        public string Url { get; set; }

        /// <summary>
        /// <para>数据库类型</para>
        /// <para>1.也可以在  OnModelCreating 方法中 通过属性指定 </para>
        /// </summary>
        [Column(TypeName = "decimal(5, 2)")]
        public decimal Rating { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Precision(14, 2)]
        public decimal Score { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string LicensePlate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime Created { get; set; }

        /// <summary>
        /// 
        /// <para>Timestamp/rowversion 是每次插入行或更新行时数据库自动生成新值的属性。 该属性也被视为并发令牌，确保在查询后要更新的行发生更改时得到异常。 具体的详细信息取决于所使用的数据库提供程序；对于 SQL Server，通常使用 byte[] 属性，该属性将设置为数据库中的 ROWVERSION 列。</para>
        /// <para>可以将属性配置为 timestamp/ rowversion</para>
        /// </summary>
        [Timestamp]
        public byte[] Timestamp { get; set; }

        /// <summary>
        /// 排除属性
        /// <para>1.也可以在  OnModelCreating 方法中 排除</para>
        /// </summary>
        [NotMapped]
        public DateTime LoadedFromDatabase { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<Post> Posts { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public enum abc
    {
        a = 0,
        b = 1,
    }
}
