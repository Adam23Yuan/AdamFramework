namespace Adam.Core
{
    /// <summary>
    /// UnitSource.
    /// </summary>
    public class UnitSource
    {
        /// <summary>
        /// PostService.
        /// </summary>
        /// <param name="input">intput vlaue.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        public static async Task<bool> PostService(string input)
        {
            await Task.Delay(1);
            return DateTime.Now.Second % 2 == 0;
        }
    }
}
