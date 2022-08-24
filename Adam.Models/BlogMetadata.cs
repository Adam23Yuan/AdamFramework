using System.ComponentModel.DataAnnotations.Schema;

namespace Adam.Models
{
    /// <summary>
    /// 从模型中通过特性排除以下类型
    /// </summary>
    [NotMapped]
    public class BlogMetadata
    {
        public DateTime LoadedFromDatabase { get; set; }
    }
}
