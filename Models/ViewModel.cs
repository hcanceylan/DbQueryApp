using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DbQueryApp.Models
{
    public class ViewModel
    {
        public List<Employees> Employees { get; set; } = new List<Employees>();
        public EmployeeSearchModel EmployeeSearch { get; set; } = new EmployeeSearchModel();
        public AlertModel alertModel { get; set; } = new AlertModel();
    }
}
