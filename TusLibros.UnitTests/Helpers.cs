using System.Collections.Generic;

namespace TusLibros.UnitTests
{
    public static class Helpers
    {
        public static readonly object VALID_ITEM = new object();
        public static readonly object ANOTHER_VALID_ITEM = new object();
        public static readonly object INVALID_ITEM = new object();

        public static List<object> GetCatalogWithValidItem() => new List<object>() { VALID_ITEM };
        public static System.Collections.Generic.List<object> GetCatalogWithTwoValidItems() => new List<object>() { VALID_ITEM, ANOTHER_VALID_ITEM };
        public static Cart GetCartWithACatalogWithValidItem() => new Cart(GetCatalogWithValidItem());
        public static Cart GetCartWithACatalogWithTwoValidItems() => new Cart(GetCatalogWithTwoValidItems());
        public static Cart GetCartWithEmptyCatalog() => new Cart(new List<object>());
    }
}