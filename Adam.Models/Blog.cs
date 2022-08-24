using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Adam.Models
{
    /// <summary>
    /// 特性说明
    /// <para>Table 使用特性指定表名加 框架名 也可以在 OnModelCreateing使用 ToTable 中指定</para>
    /// <para>Comment 指定表注释 也可以在 OnModelCreateing中使用 HasComment 指定</para>
    /// </summary>
    [Table("Blogs", Schema = "dbo")]
    [Comment("Blogs managed on the website")]
    public class Blog
    {
        /// <summary>
        /// 
        /// </summary>
        public int BlogId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public string Url { get; set; }

        public List<Post> Posts { get; set; }
    }
}
