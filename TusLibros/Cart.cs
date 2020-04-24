using System.Collections.Generic;
using System;

namespace TusLibros.UnitTests
{
    public class Cart
    {
        private const string NOT_IN_CATALOG_ERROR = "No existe en el catálogo";

        private readonly List<object> _items;
        private readonly List<object> _catalog;

        public List<object> GetBooks() => _items;

        public int Count => _items.Count;

        public bool IsEmpty() => _items.Count == 0;

        public Cart(List<object> catalog)
        {
            _catalog = catalog ?? throw new ArgumentException();
            _items = new List<object>();
        }

        public void Add(object item, int count)
        {
            if (count < 1)
            {
                throw new ArgumentException();
            }

            if (! _catalog.Contains(item))
            {
                throw new Exception(NOT_IN_CATALOG_ERROR);
            }

            for (int i = 0; i < count; i++)
            {
                _items.Add(item);
            }
        }
    }
}
