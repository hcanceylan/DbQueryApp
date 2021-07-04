using DbQueryApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DbQueryApp.DbServices
{
    public interface IEmployeeServices
    {
        List<Employees> LoadEmployees(string query, dynamic Parameters);
        int ManipulateEmployee(string query, dynamic Parameters);
    }
}
