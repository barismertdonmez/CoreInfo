using CoreInfo.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CoreInfo.Controllers
{
    public class EmployeeController : Controller
    {
        private CustomerDbContext _employeeDbContext;
        public EmployeeController(CustomerDbContext customerDbContext)
        {
            _employeeDbContext = customerDbContext;
        }
        public async Task<IActionResult> Index()
        {
            var list = await _employeeDbContext.Employees.ToListAsync();
            return View();
        }
    }
}
