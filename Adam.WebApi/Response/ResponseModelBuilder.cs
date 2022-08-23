namespace Adam.WebApi.Response
{
    /// <summary>
    /// 
    /// </summary>
    public class ResponseModelBuilder
    {
        #region method

        /// <summary>
        /// return success
        /// </summary>
        /// <param name="msg">msg</param>
        /// <param name="data">data</param>
        /// <returns></returns>
        public static ResponseModel<T> Success<T>(T data, string msg = "OK")
        {
            return Build(200, msg, data);
        }

        /// <summary>
        /// return fail
        /// </summary>
        /// <param name="msg">msg</param>
        /// <param name="data">data</param>
        /// <returns></returns>
        public static ResponseModel<T> Fail<T>(string msg = "Error", T data = default)
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
        public static ResponseModel<T> Build<T>(int code, string msg, T data)
        {
            return new ResponseModel<T>() { Code = code, Message = msg, Data = data };
        }

        #endregion

        #region method async

        /// <summary>
        /// return success
        /// </summary>
        /// <param name="msg">msg</param>
        /// <param name="data">data</param>
        /// <returns></returns>
        public static async Task<ResponseModel<T>> SuccessAsync<T>(T data, string msg = "OK")
        {
            return await BuildAsync(200, msg, data);
        }

        /// <summary>
        /// return fail
        /// </summary>
        /// <param name="msg">msg</param>
        /// <param name="data">data</param>
        /// <returns></returns>
        public static async Task<ResponseModel<T>> FailAsync<T>(string msg = "Error", T data = default)
        {
            return await BuildAsync(500, msg, data);
        }

        /// <summary>
        /// return msg
        /// </summary>
        /// <param name="code"></param>
        /// <param name="msg">msg</param>
        /// <param name="data">data</param>
        /// <returns></returns>
        public static async Task<ResponseModel<T>> BuildAsync<T>(int code, string msg, T data)
        {
            await Task.Delay(0);
            var responseModel = new ResponseModel<T>
            {
                Code = code,
                Message = msg,
                Data = data
            };
            return responseModel;
        }

        #endregion

    }
}
