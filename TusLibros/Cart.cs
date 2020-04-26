using System.Linq;
using System.Collections.Generic;
using System;

namespace TusLibros
{
    public class Cart
    {
        private const string NOT_IN_CATALOG_ERROR = "No existe en el catálogo";

        private readonly List<object> _items;
        private readonly List<object> _catalog;

        public List<object> GetBooks()
        {
            var itemsToReturn = new List<object>();
            _items.ForEach(p => itemsToReturn.Add(p));
            return itemsToReturn;
        }

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

        public int GetCount(object item) => _items.Count(p => p == item);

        public bool Contains(object item) => _items.Contains(item);
    }
}
