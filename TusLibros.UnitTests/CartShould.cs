using System;
using Xunit;

namespace TusLibros.UnitTests
{
    public class CartShould
    {
        // Given When Then
        [Fact]
        public void WhenCreatingANewCart_ThenTheCartHasZeroItems()
        {
            var cart = new Cart();
            Assert.Equal(0, cart.Count);
        }

        [Fact]
        public void GivenANewCart_WhenAddingAnIsbn_ThenTheCartHasOneItem()
        {
            var cart = new Cart();
            var isbn = "1";
            cart.Add(isbn, 1);
            Assert.Equal(1, cart.Count);
        }

        [Fact]
        public void GivenANewCart_WhenAddingTwoIsbns_ThenTheCartHasTwoItems()
        {
            var cart = new Cart();
            var firstIsbn = "1";
            var secondIsbn = "2";
            cart.Add(firstIsbn, 1);
            cart.Add(secondIsbn, 1);
            Assert.Equal(2, cart.Count);
        }

        [Fact]
        public void WhenCreatingANewCart_ThenTheCartHasNoBooks()
        {
            var cart = new Cart();
            Assert.Empty(cart.GetBooks());
        }

        [Fact]
        public void WhenCreatingANewCart_ThenTheCartHasAnId()
        {
            var cart = new Cart();
            Assert.NotNull(cart.Id);
        }

        [Fact]
        public void GivenTwoCarts_WhenComparingIds_ThenTheIdsAreDifferent()
        {
            var firstCart = new Cart();
            var secondCart = new Cart();
            Assert.NotEqual(firstCart.Id, secondCart.Id);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void GivenANewCart_WhenTryingToInvalidCountOfItems_ThenAnExceptionIsThrown(int count)
        {
            var cart = new Cart();
            var isbn = "1";

            Assert.Throws<ArgumentException>(() => cart.Add(isbn, count));
        }
    }
}

// FIXME: Modificar la lista de GetBooks para regresar un IEnumerator
// TODO: Agregar IsEmpty a Cart
// TODO: Agregar Catalog a Cart
// TODO: Add debe verificar que solo acepta isbn de Catalog

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
