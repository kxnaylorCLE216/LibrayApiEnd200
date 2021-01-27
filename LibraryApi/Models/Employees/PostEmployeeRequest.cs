using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApi.Models.Employees
{
    public class PostEmployeeRequest
    {
    
        public string  Name { get; set; }
        public string  Department { get; set; }
        public decimal StartingSalary { get; set; }
    }


}
