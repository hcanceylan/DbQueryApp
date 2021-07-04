using DbQueryApp.DbServicesCore;
using DbQueryApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DbQueryApp.DbServices
{
    public class EmployeeServices : IEmployeeServices
    {
        private ISqlAccess _sqlAccess;
        public EmployeeServices(ISqlAccess SqlAccess)
        {
            //This service is for fetching - manipulating employee data in database, which uses core sql access class.
            _sqlAccess = SqlAccess;
            //Sql access field gets the access reference from core sql access when the dependency of EmployeeServices injected.
        }
        public List<Employees> LoadEmployees(string query,dynamic Parameters)
        {
            return _sqlAccess.LoadData<Employees, dynamic>(query,Parameters); //Returns core service for fetching data query
        }
        public int ManipulateEmployee(string query, dynamic Parameters)
        {
            return _sqlAccess.ManipulateData<Employees, dynamic>(query, Parameters); //Returns core service for manipulation of datas in database
        }
        
    }
}
