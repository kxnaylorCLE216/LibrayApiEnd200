using System;
using Xunit;

namespace LibraryApiIntegrationTests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            int a = 10, b = 20, answer;

            answer = a + b;

            Assert.Equal(30, answer);
        }

        [Theory]
        [InlineData(2,2,4)]
        [InlineData(2,3,5)]
        [InlineData(10,2,12)]
        public void CanAddTwoNumbers(int a, int b, int expected)
        {
            var answer = a + b;

            Assert.Equal(expected, answer);
        }
    }
}
