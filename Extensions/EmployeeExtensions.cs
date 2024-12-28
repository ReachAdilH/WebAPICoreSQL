using WebAPICoreSQL.Model;

namespace WebAPICoreSQL.Extensions
{
    public static class EmployeeExtensions
    {
        public static string GetFormattedName(this Employee employee)
        {
            return $"{employee.LastName} , {employee.FirstName}";
        }

        public static string GetBriefInfo(this Employee employee)
        {
            return $"Employee: {employee.GetFormattedName()} |Department: {employee.Department} | Branch: {employee.BranchLocation}";
        }
    }
}
