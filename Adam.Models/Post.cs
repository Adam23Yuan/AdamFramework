using System.ComponentModel.DataAnnotations.Schema;

namespace Adam.Models
{
    public class Post
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public int BlogForeignKey { get; set; }

        /// <summary>
        /// 数据注解外键
        /// </summary>
        [ForeignKey("BlogForeignKey")]
        public Blog Blog { get; set; }

        public int BlogId { get; set; }
    }
}
