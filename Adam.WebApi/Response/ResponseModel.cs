using System.Text.Json.Serialization;

namespace Adam.WebApi.Response
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class ResponseModel<T>
    {
        #region ctor

        #endregion

        #region property

        /// <summary>
        /// code 
        /// <para>200:success</para>
        /// <para>500:error</para>
        /// </summary>
        [JsonPropertyName("code")]
        public int Code { get; set; } = 200;

        /// <summary>
        /// message
        /// </summary>
        [JsonPropertyName("message")]
        public string Message { get; set; } = "";

        /// <summary>
        /// data
        /// </summary>
        [JsonPropertyName("data")]
        public T Data { get; set; }

        #endregion

        #region method

        /// <summary>
        /// return success
        /// </summary>
        /// <param name="msg">msg</param>
        /// <param name="data">data</param>
        /// <returns></returns>
        public static ResponseModel<T> Success(T data, string msg = "OK")
        {
            return Build(200, msg, data);
        }

        /// <summary>
        /// return fail
        /// </summary>
        /// <param name="msg">msg</param>
        /// <param name="data">data</param>
        /// <returns></returns>
        public static ResponseModel<T> Fail(string msg = "Error", T data = default)
        {
            return Build(500, msg, data);
        }

        /// <summary>
        /// return msg
        /// </summary>
        /// <param name="code"></param>
        /// <param name="msg">msg</param>
        /// <param name="data">data</param>
        /// <returns></returns>
        public static ResponseModel<T> Build(int code, string msg, T data)
        {
            return new ResponseModel<T>() { Code = code, Message = msg, Data = data };
        }

        #endregion
    }
}
