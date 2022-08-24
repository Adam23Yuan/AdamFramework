using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Adam.Models
{
    /// <summary>
    /// 特性说明
    /// <para>1.Table 使用特性指定表名加 框架名 也可以在 OnModelCreating 使用 ToTable 中指定</para>
    /// <para>2.Comment 指定表注释 也可以在 OnModelCreating 中使用 HasComment 指定</para>
    /// </summary>
    [Table("Blogs", Schema = "dbo")]
    [Comment("Blogs managed on the website")]
    public class Blog
    {
        /// <summary>
        /// 
        /// <para>映射数据库列名</para>
        /// <para>1.也可以在  OnModelCreating 方法中 通过属性指定 HasColumnName</para>
        /// </summary>
        [Column("blog_id")]
        public int BlogId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public string Url { get; set; }

        /// <summary>
        /// <para>数据库类型</para>
        /// <para>1.也可以在  OnModelCreating 方法中 通过属性指定 </para>
        /// </summary>
        [Column(TypeName = "decimal(5, 2)")]
        public decimal Rating { get; set; }

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
}
