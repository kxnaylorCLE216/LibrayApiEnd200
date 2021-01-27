using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApi.Services
{
    public class FormalFormatting : IFormatNames
    {
        public string FormatName(string first, string last)
        {
            return $"{last}, {first}";
        }
    }
}
