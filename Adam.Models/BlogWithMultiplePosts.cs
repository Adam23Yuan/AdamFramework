namespace Adam.Models
{
    /// <summary>
    /// 表值函数映射
    /// 
    /// </summary>
    public class BlogWithMultiplePosts
    {
        public string Url { get; set; }
        public int PostCount { get; set; }

        /*
         CREATE FUNCTION dbo.BlogsWithMultiplePosts()
RETURNS TABLE
AS
RETURN
(
    SELECT b.Url, COUNT(p.BlogId) AS PostCount
    FROM Blogs AS b
    JOIN Posts AS p ON b.BlogId = p.BlogId
    GROUP BY b.BlogId, b.Url
    HAVING COUNT(p.BlogId) > 1
)
         */
    }
}
