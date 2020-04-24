using System.Reflection;
using TusLibros;
using Xunit;

namespace TusLibros.UnitTests
{
    public class CashierShould
    {
        [Fact]
        public void Test1()
        {
            var cashier = new Cashier();
            Assert.NotNull(cashier);
        }
    }
}