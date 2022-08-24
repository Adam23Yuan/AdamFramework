using Adam.Core;

namespace TestProject
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
            Console.WriteLine($"time:{DateTime.Now} =>test environment is already ok ");
        }

        /// <summary>
        /// 
        /// </summary>
        [TearDown]
        public void PrintTime()
        {
            Console.WriteLine($"time:{DateTime.Now}");
        }

        /// <summary>
        /// Test the service route.
        /// </summary>
        /// <returns>A task.</returns>
        [Test]
        public async Task IsTrue()
        {
            var task = UnitSource.PostService(string.Empty);
            Assert.IsTrue(await task);
        }

        /// <summary>
        /// Test the service route.
        /// </summary>
        /// <returns>A task.</returns>
        [Test]
        public async Task IsFalse()
        {
            var task = UnitSource.PostService(string.Empty);
            Assert.IsFalse(await task);
        }
    }
}
