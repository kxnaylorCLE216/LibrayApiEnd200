using System;
using System.Collections.Generic;

namespace LibraryApi
{
    public class CatalogModel
    {
        public DateTime CreatedAt { get; set; }
        public List<string> Items { get; set; }
    }
}