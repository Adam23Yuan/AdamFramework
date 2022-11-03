using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text;

namespace WebApp.Utility
{
    public static class MVCHelper
    {
        public static string GetValidMessage(ModelStateDictionary valuePairs)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var key in valuePairs.Keys) {
                if (valuePairs[key].Errors.Count <= 0) {
                    continue;
                }

                sb.Append($"属性：【{key}】");
                sb.Append($"错误");
                foreach (var modelError in valuePairs[key].Errors)
                {
                    sb.Append(modelError.ErrorMessage);
                }
            }

            return sb.ToString();
        }
    }
}
