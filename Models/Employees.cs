using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DbQueryApp.Models
{
    public class Employees
    {
        public int EmployeeID { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
