using Microsoft.AspNetCore.Mvc;
using WebAPICoreSQL.Data;
using WebAPICoreSQL.DelegateFilter;
using WebAPICoreSQL.Extensions;
using WebAPICoreSQL.Model;
using WebAPICoreSQL.Service;

namespace WebAPICoreSQL.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        private readonly EmployeeFilterService _employeeFilterService;
        public EmployeesController(AppDbContext appDbContext, EmployeeFilterService employeeFilterService)
        {
            _appDbContext = appDbContext;
            _employeeFilterService = employeeFilterService;
        }

        [HttpGet]
        [Route("all")]
        public IActionResult GetAllEmployees()
        {
            var employee = _appDbContext.Employee.Select(x => new Employee(x.Id, x.FirstName, x.LastName, x.Department, new Location(x.City, x.State))).ToList();

            return Ok(employee);
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetEmployees(int id)
        {
            var employee = _appDbContext.Employee.Where(x=> x.Id == id)
                .Select(x=> new Employee(x.Id, x.FirstName, x.LastName, x.Department,  new Location(x.City, x.State))).FirstOrDefault();
            var formattedName = employee.GetFormattedName();
            var briefInfo = employee.GetBriefInfo();
            employee.FormattedName = formattedName;
            if (employee == null)
            {
                return NotFound();
            }
            return Ok(employee);
        }
        [HttpGet("filter-by-department")]
        public IActionResult GetEmployeeByDepartment([FromQuery] string department)
        {
            EmployeeFilter employeeFilter = e => e.Department.Equals(department, StringComparison.OrdinalIgnoreCase);
            var filteredEmployee = _employeeFilterService.GetFilteredEmployees(employeeFilter);
            return Ok(filteredEmployee);    
        }
        [HttpGet("filter-by-City")]
        public IActionResult GetEmployeeByCity([FromQuery] string city)
        {
            EmployeeFilter employeeFilter = e => e.City.Equals(city, StringComparison.OrdinalIgnoreCase);
            var filteredEmployee = _employeeFilterService.GetFilteredEmployees(employeeFilter);
            return Ok(filteredEmployee);
        }
    }
}
