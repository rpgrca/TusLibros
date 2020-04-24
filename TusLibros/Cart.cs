using System.Collections.Generic;
using System.Collections;
using System;

namespace TusLibros.UnitTests
{
    public class Cart
    {
        private readonly List<object> _items = new List<object>();

        public List<object> GetBooks() => _items;

        public int Count => _items.Count;

        public bool IsEmpty() => Count == 0;

        public void Add(object isbn, int count)
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
    }
}
