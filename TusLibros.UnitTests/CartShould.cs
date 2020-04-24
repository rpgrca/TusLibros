using System;
using Xunit;
using System.Collections.Generic;
using static TusLibros.UnitTests.Helpers;

namespace TusLibros.UnitTests
{
    public class CartShould
    {
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

        [Fact]
        public void GivenANewCart_WhenInitializedWithNullCatalog_ThenAnExceptionIsThrown()
        {
            Assert.Throws<ArgumentException>(() => new Cart(null));
        }

        [Fact]
        public void GivenANewCartAndACopyOfItems_WhenAddingAnItemToCart_ThenTheCopyOfItemsDidNotAddTheItem()
        {
            var cart = GetCartWithACatalogWithValidItem();
            var items = cart.GetBooks();
            cart.Add(VALID_ITEM, 1);

            Assert.NotEqual(items.Count, cart.Count);
        }

        [Fact]
        public void GivenANewCartWithAnItem_WhenChangingTheItemInTheCopyOfItems_ThenTheCartDidNotChange()
        {
            var cart = GetCartWithACatalogWithValidItem();
            cart.Add(VALID_ITEM, 1);

            var items = cart.GetBooks();
            items[0] = INVALID_ITEM;
            Assert.Single(cart.GetBooks(), VALID_ITEM);
        }

        [Fact]
        public void WithANewCart_WhenCountingInexistantItems_ThenReturnsZero()
        {
            var cart = GetCartWithEmptyCatalog();
            var count = cart.GetCount(VALID_ITEM);

            Assert.Equal(0, count);
        }

        [Fact]
        public void WithANewCartWithOneItem_WhenCountingThatItem_ThenReturnsOne()
        {
            var cart = GetCartWithACatalogWithValidItem();
            cart.Add(VALID_ITEM, 2);
            var count = cart.GetCount(VALID_ITEM);

            Assert.Equal(2, count);
        }

        [Fact]
        public void WithANewCartWithOneItem_WhenCountingAnotherItem_ThenReturnsZero()
        {
            var cart = GetCartWithACatalogWithValidItem();
            cart.Add(VALID_ITEM, 1);
            var count = cart.GetCount(ANOTHER_VALID_ITEM);

            Assert.Equal(0, count);
        }

        [Fact]
        public void WithANewCart_WhenAddingTheSameItemTwice_ThenReturnsSum()
        {
            var cart = GetCartWithACatalogWithValidItem();
            cart.Add(VALID_ITEM, 4);
            cart.Add(VALID_ITEM, 6);
            var count = cart.GetCount(VALID_ITEM);

            Assert.Equal(10, count);
        }

        [Fact]
        public void WithANewCart_WhenAddingDifferentItemsTwice_ThenReturnsDifferentCounts()
        {
            var cart = GetCartWithACatalogWithTwoValidItems();
            cart.Add(VALID_ITEM, 4);
            cart.Add(ANOTHER_VALID_ITEM, 6);

            Assert.Equal(4, cart.GetCount(VALID_ITEM));
            Assert.Equal(6, cart.GetCount(ANOTHER_VALID_ITEM));
        }

        [Fact]
        public void WithANewCart_WhenCheckingForInexistantItems_ThenReturnsFalse()
        {
            var cart = GetCartWithEmptyCatalog();
            Assert.False(cart.Contains(VALID_ITEM));
        }

        [Fact]
        public void WithANewCartWithOneItem_WhenCheckingForThatItem_ThenReturnsTrue()
        {
            var cart = GetCartWithACatalogWithValidItem();
            cart.Add(VALID_ITEM, 1);
            Assert.True(cart.Contains(VALID_ITEM));
        }

        [Fact]
        public void WithANewCartWithOneItem_WhenCheckingAnotherItem_ThenReturnsFalse()
        {
            var cart = GetCartWithACatalogWithValidItem();
            cart.Add(VALID_ITEM, 1);
            Assert.False(cart.Contains(ANOTHER_VALID_ITEM));
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
