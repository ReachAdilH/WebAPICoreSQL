using WebAPICoreSQL.Data;
using WebAPICoreSQL.DelegateFilter;
using WebAPICoreSQL.Model;
using static WebAPICoreSQL.Model.Employee;

namespace WebAPICoreSQL.Service
{
    public class EmployeeFilterService
    {
        private readonly AppDbContext _appDbContext;
        public EmployeeFilterService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public List<Employee> GetFilteredEmployees (EmployeeFilter filter)
        {
            
           var employees = _appDbContext.Employee.Select(x => new Employee(x.Id, x.FirstName, x.LastName, x.Department, new Location( x.City,x.State))).ToList();
           return employees.Where(e => filter(e)).ToList();
        } 

    }
}
