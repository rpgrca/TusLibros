using System.Runtime.InteropServices;
using System;
using Xunit;

namespace TusLibros.UnitTests
{
    public class CartShould
    {
        [Fact]
        public void WhenCreatingANewCart_ThenTheCartHasZeroItems()
        {
            var cart = new Cart();
            Assert.Equal(0, cart.Count);
        }

        [Fact]
        public void GivenANewCart_WhenAddingAnItem_ThenTheCartHasOneItem()
        {
            var cart = new Cart();
            cart.Add(new object(), 1);
            Assert.Equal(1, cart.Count);
        }

        [Fact]
        public void GivenANewCart_WhenAddingTwoItems_ThenTheCartHasTwoItems()
        {
            var cart = new Cart();
            cart.Add(new object(), 1);
            cart.Add(new object(), 1);
            Assert.Equal(2, cart.Count);
        }

        [Fact]
        public void WhenCreatingANewCart_ThenTheCartHasNoBooks()
        {
            var cart = new Cart();
            Assert.Empty(cart.GetBooks());
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void GivenANewCart_WhenTryingToAddInvalidCountOfItems_ThenAnExceptionIsThrown(int count)
        {
            var cart = new Cart();
            Assert.Throws<ArgumentException>(() => cart.Add(new object(), count));
        }

        [Fact]
        public void GivenANewCart_WhenAskedIsEmpty_ThenItShouldReturnTrue()
        {
            var cart = new Cart();
            Assert.True(cart.IsEmpty());
        }

        [Fact]
        public void GivenACartWithOneItem_WhenAskedIsEmpty_ThenItShouldReturnFalse()
        {
            var cart = new Cart();
            cart.Add(new object(), 1);
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
