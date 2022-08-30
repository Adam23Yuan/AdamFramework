namespace Adam.Core
{
    public class UnitSource
    {
        public async static Task<bool> PostService(string input)
        {
            await Task.Delay(1);
            return DateTime.Now.Second % 2 == 0;
        }
    }
}
