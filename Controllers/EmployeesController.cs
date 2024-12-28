using Microsoft.AspNetCore.Mvc;
using WebAPICoreSQL.Data;
using WebAPICoreSQL.Extensions;
using WebAPICoreSQL.Model;

namespace WebAPICoreSQL.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        public EmployeesController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
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

    }
}
