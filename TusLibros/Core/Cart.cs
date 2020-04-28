using System.Linq;
using System.Collections.Generic;
using System;

namespace TusLibros
{
    public class Cart
    {
        public const string NOT_IN_CATALOG_ERROR = "No existe en el catálogo";
        public const string CATALOG_IS_NULL_ERROR = "El catalogo no puede ser nulo.";

        private readonly List<object> _items;
        private readonly List<object> _catalog;

        public List<object> GetItems() => new List<object>(_items);

        public int Count => _items.Count;

        public bool IsEmpty() => _items.Count == 0;

        public Cart(List<object> catalog)
        {
            _catalog = catalog ?? throw new ArgumentException(CATALOG_IS_NULL_ERROR);
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

        public int GetCount(object item) => _items.Count(p => p == item);

        public bool Contains(object item) => _items.Contains(item);
    }
}
