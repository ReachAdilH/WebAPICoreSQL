using Microsoft.AspNetCore.Authorization;
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
        private readonly ILoggerService _loggerService;
        public EmployeesController(AppDbContext appDbContext, EmployeeFilterService employeeFilterService, ILoggerService loggerService)
        {
            _appDbContext = appDbContext;
            _employeeFilterService = employeeFilterService;
            _loggerService = loggerService;
        }

        [HttpGet]
        [Authorize]
        [Route("all")]
        public IActionResult GetAllEmployees()
        {
            var employee = _appDbContext.Employee.Select(x => new Employee(x.Id, x.FirstName, x.LastName, x.Department, new Location(x.City, x.State))).ToList();

            return Ok(employee);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetEmployees(int id)
        {
            var correlationId = Guid.NewGuid();
            try
            {
                await _loggerService.LogAsync(correlationId, "EmployeeAPI", "GetEmployees", "Fetching employee data.");

                var employee = _appDbContext.Employee.Where(x => x.Id == id)
                    .Select(x => new Employee(x.Id, x.FirstName, x.LastName, x.Department, new Location(x.City, x.State))).FirstOrDefault();
                if (employee == null)
                {
                    await _loggerService.LogAsync(correlationId, "EmployeeAPI", "GetEmployees", "An error occurred while fetching employees.");
                    return NotFound();
                }
                else
                {
                    var formattedName = employee.GetFormattedName();
                    var briefInfo = employee.GetBriefInfo();
                    employee.FormattedName = formattedName;
                    await _loggerService.LogAsync(correlationId, "EmployeeAPI", "GetEmployees", "Employee data fetched successfully.");
                }
                return Ok(employee);
            }
            catch (Exception ex)
            {
                await _loggerService.LogAsync(correlationId, "EmployeeAPI", "GetEmployees", "An error occurred while fetching employees.", ex.Message);
                return NotFound();
            }
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
