using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApi.Models.Options
{
    public class MessagesOptions
    {
        public string SectionName = "Messages";
        public string CacheMessage { get; set; }
        public string SomeOtherMessage { get; set; }
    }

}
