using DbQueryApp.DbServices;
using DbQueryApp.DbServicesCore;
using DbQueryApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace DbQueryApp.Controllers
{
   
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IEmployeeServices _employeeService;

        public HomeController(ILogger<HomeController> logger,IEmployeeServices EmployeeService)
        {
            _employeeService = EmployeeService;
            _logger = logger;
        }

        public IActionResult Index()
        {
            var Result = _employeeService.LoadEmployees("select EmployeeID,LastName,FirstName,BirthDate from dbo.Employees",null);
            return View(new ViewModel() { Employees = Result});
        }
        
        [HttpPost]
        public IActionResult InsertUpdateEmployee(EmployeeSearchModel model, string InsertOrUpdate)
        {
            string Query = string.Empty;
            string Message = string.Empty;

            if(InsertOrUpdate == "Insert")
            {
                Query = "insert into dbo.Employees (LastName,FirstName,BirthDate) values (@LastName,@FirstName,@BirthDate)";
                Message = "Insertion";
            }
            else if (InsertOrUpdate == "Update")
            {
                Query = "update dbo.Employees set LastName = @LastName , FirstName = @FirstName , BirthDate = @BirthDate where LastName = @LastName";
                Message = "Update";
            }

            if (!string.IsNullOrEmpty(InsertOrUpdate) && !string.IsNullOrEmpty(Query))
            {
                try
                {
                    var Result = _employeeService.ManipulateEmployee(Query, new { LastName = model.LastName, FirstName = model.FirstName, BirthDate = model.BirthDate });
                    return View("Index", new ViewModel()
                    {
                        alertModel = new AlertModel()
                        {
                            Alert = Result >= 1 ? 1 : -1,
                            Message =
                        Result >= 1 ? $"{Message} Success" : $"{Message} Failed"
                        }
                    });
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    return View("Index", new ViewModel() { alertModel = new AlertModel() { Alert = -1, Message = $"{Message} Failed" } });
                }
            }
            return View("Index", new ViewModel() { alertModel = new AlertModel() { Alert = -1, Message = $"{Message} Failed" } });

        }

        [HttpPost]
        public IActionResult UpdateEmployee(EmployeeSearchModel model)
        {
            try
            {
                var Result = _employeeService.ManipulateEmployee("insert into dbo.Employees (LastName,FirstName,BirthDate) values (@LastName,@FirstName,@BirthDate)", new { LastName = model.LastName, FirstName = model.FirstName, BirthDate = model.BirthDate });
                return View("Index", new ViewModel()
                {
                    alertModel = new AlertModel()
                    {
                        Alert = Result >= 1 ? 1 : -1,
                        Message = Result >= 1 ? "Insertion Success" : "Insertion Failed"
                    }
                });
            }
            catch (Exception)
            {
                return View("Index", new ViewModel() { alertModel = new AlertModel() { Alert = -1, Message = "Insertion Failed" } });
            }
        }

        [HttpPost]
        public IActionResult DeleteEmployee(EmployeeSearchModel model)
        {
            try
            {
                var Result = _employeeService.ManipulateEmployee("delete from dbo.Employees where LastName = @LastName", new { LastName = model.LastName});

                return View("Index", new ViewModel()
                {
                    alertModel = new AlertModel()
                    {
                        Alert = Result >= 1 ? 1 : -1,
                        Message = Result >= 1 ? "Delete Success" : "Delete Failed"
                    }
                });
            }
            catch (Exception)
            {
                return View("Index", new ViewModel() { alertModel = new AlertModel() { Alert = -1, Message = "Delete Failed" } });
            }
        }

        [HttpGet]
        public IActionResult SearchEmployee(EmployeeSearchModel model)
        {
            string query = string.Empty;

            if(model.BirthDate == DateTime.MinValue || model.BirthDate == DateTime.MaxValue)
            {
                query = "select EmployeeID,LastName,FirstName,BirthDate from dbo.Employees where LastName = @LastName or FirstName = @FirstName";
            }
            else
            {
                query = "select EmployeeID,LastName,FirstName,BirthDate from dbo.Employees where LastName = @LastName or FirstName = @FirstName or BirthDate = @BirthDate";
            }

           
            var Result = _employeeService.LoadEmployees(query, new {LastName=model.LastName,FirstName=model.FirstName,BirthDate=model.BirthDate});
            return View("Index", new ViewModel() { Employees = Result });
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
