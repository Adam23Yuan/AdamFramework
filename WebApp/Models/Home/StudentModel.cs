using System.ComponentModel.DataAnnotations;
using WebApp.Utility;

namespace WebApp.Models.Home
{
    public class StudentModel:ModelBase
    {
        [Required]
        public int Age { get; set; }

        [Required]
        public string Name { get; set; }

        [QQNubmer]
        public string QQNumber { get; set; }
    }
}
