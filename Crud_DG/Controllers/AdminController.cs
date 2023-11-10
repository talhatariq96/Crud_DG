using Crud_DG.Common;
using Crud_DG.Models;
using Crud_DG.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Crud_DG.Controllers
{
    public class AdminController : Controller
    {
        private readonly IEmployeeService _service;

        public AdminController(IEmployeeService service)
        {
            _service= service;
        }
        public async Task<IActionResult> Index()
        {
            ResultSet<IEnumerable<Employee>>  _getEmployees = new();
            List<Employee> _listOfEmployees = new List<Employee>();
            try
            {
                 _getEmployees =await _service.GetAllEmployeesAsync();
                if (_getEmployees.Data.ToList().Count() > 0)
                {
                    _listOfEmployees = _getEmployees.Data.ToList();
                }

            }
            catch (Exception)
            {

                throw;
            }
            return View(_listOfEmployees);
        }
        public async Task<IActionResult> _GetEmployeesPartial()
        {
            ResultSet<IEnumerable<Employee>> _getEmployees = new();
            List<Employee> _listOfEmployees = new List<Employee>();
            try
            {
                _getEmployees = await _service.GetAllEmployeesAsync();
                if (_getEmployees.Data.ToList().Count() > 0)
                {
                    _listOfEmployees = _getEmployees.Data.ToList();
                }

            }
            catch (Exception)
            {

                throw;
            }
            return PartialView(_listOfEmployees);
        }
        [HttpPost]
        public async Task<JsonResult> AddEmployee(UploadEmployeeModel employee)
        {
            if (ModelState.IsValid)
            {
                await _service.CreateOrUpdateEmployeeAsync(employee);
                return Json(new { success = true });

            }
            return Json(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors) });

        }
        [HttpPost]
        public async Task<JsonResult> DeleteEmployee(DeleteEmployeeModel employee)
        {
            if (ModelState.IsValid)
            {
                await _service.DeleteEmployeeAsync(employee);
                return Json(new { success = true });

            }
            return Json(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors) });

        }

    }
}
