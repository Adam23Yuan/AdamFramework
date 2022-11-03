using System.ComponentModel.DataAnnotations;

namespace WebApp.Utility
{
    public class QQNubmerAttribute : RegularExpressionAttribute
    {
        public QQNubmerAttribute() : base(@"^\d{5-10}$")
        {
            this.ErrorMessage = "QQ号格式不正常[{0}]";
        }
    }
}
