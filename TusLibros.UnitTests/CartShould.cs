using System.Runtime.InteropServices;
using System;
using Xunit;
using System.Collections.Generic;

namespace TusLibros.UnitTests
{
    public class CartShould
    {
        private readonly object VALID_ITEM = new object();
        private readonly object ANOTHER_VALID_ITEM = new object();
        private readonly object INVALID_ITEM = new object();

        private List<object> GetCatalogWithValidItem() => new List<object>() { VALID_ITEM };
        private List<object> GetCatalogWithTwoValidItems() => new List<object>() { VALID_ITEM, ANOTHER_VALID_ITEM };
        private Cart GetCartWithACatalogWithValidItem() => new Cart(GetCatalogWithValidItem());
        private Cart GetCartWithACatalogWithTwoValidItems() => new Cart(GetCatalogWithTwoValidItems());
        private Cart GetCartWithEmptyCatalog() => new Cart(new List<object>());

        [Fact]
        public void GivenACartWithACatalogWithOneItem_WhenAnItemThatIsNotInCatalogIsAdded_ThenAnExceptionIsThrown()
        {
            var cart = GetCartWithACatalogWithValidItem();
            var exception = Assert.Throws<Exception>(() => cart.Add(INVALID_ITEM, 1));
            Assert.Contains("No existe", exception.Message);
        }

        [Fact]
        public void GivenACartWithACatalogWithOneItem_WhenAnItemThatIsNotInCatalogIsAdded_ThenTheCartStaysEmpty()
        {
            var cart = GetCartWithACatalogWithValidItem();

            try
            {
                cart.Add(INVALID_ITEM, 1);
            }
            catch (Exception)
            {
                Assert.True(cart.IsEmpty());
            }
        }

        [Fact]
        public void WhenCreatingANewCart_ThenTheCartHasZeroItems()
        {
            var cart = GetCartWithEmptyCatalog();
            Assert.Equal(0, cart.Count);
        }

        [Fact]
        public void GivenANewCart_WhenAddingAnItem_ThenTheCartHasOneItem()
        {
            var cart = GetCartWithACatalogWithValidItem();
            cart.Add(VALID_ITEM, 1);
            Assert.Equal(1, cart.Count);
        }

        [Fact]
        public void GivenANewCart_WhenAddingTwoItems_ThenTheCartHasTwoItems()
        {
            var cart = GetCartWithACatalogWithTwoValidItems();
            cart.Add(VALID_ITEM, 1);
            cart.Add(ANOTHER_VALID_ITEM, 1);
            Assert.Equal(2, cart.Count);
        }

        [Fact]
        public void WhenCreatingANewCart_ThenTheCartHasNoBooks()
        {
            var cart = GetCartWithEmptyCatalog();
            Assert.Empty(cart.GetBooks());
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void GivenANewCart_WhenTryingToAddInvalidCountOfItems_ThenAnExceptionIsThrown(int count)
        {
            var cart = GetCartWithACatalogWithValidItem();
            Assert.Throws<ArgumentException>(() => cart.Add(VALID_ITEM, count));
        }

        [Fact]
        public void GivenANewCart_WhenAskedIsEmpty_ThenItShouldReturnTrue()
        {
            var cart = GetCartWithEmptyCatalog();
            Assert.True(cart.IsEmpty());
        }

        [Fact]
        public void GivenACartWithOneItem_WhenAskedIsEmpty_ThenItShouldReturnFalse()
        {
            var cart = GetCartWithACatalogWithValidItem();
            cart.Add(VALID_ITEM, 1);
            Assert.False(cart.IsEmpty());
        }
    }
}


// Iteracion 2
// TODO: Cashier tratando de checkout carrito vacio
// TODO: checkout de un carrito con un elemento
// TODO: Tener precio
// TODO: Calcular total precio producto
//
// TODO: Tarjeta vencida
// TODO: Tarjeta valida
// TODO: Cashier.Debit(total a cobrar)

// Iteracion 3
// TODO: Merchant
