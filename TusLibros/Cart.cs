using System.Collections.Generic;
using System.Collections;
using System;

namespace TusLibros.UnitTests
{
    public class Cart
    {
        private List<object> _items;
        public string Id { get; }

        public Cart()
        {
            _items = new List<object>();
            Id = Guid.NewGuid().ToString();
        }

        public int Count
        {
            get { return _items.Count; }
        }

        public void Add(string isbn, int count)
        {
            if (count < 1)
            {
                throw new ArgumentException();
            }

            for (int i = 0; i < count; i++)
            {
                _items.Add(isbn);
            }
        }

        public List<object> GetBooks()
        {
            return _items;
        }
    }
}
