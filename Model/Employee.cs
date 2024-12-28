using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace WebAPICoreSQL.Model
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Department { get; set; }
        [NotMapped]
        public Location BranchLocation { get; set; }
        [JsonIgnore]
        public string City { get; set; }
        [JsonIgnore]
        public string State { get; set; }
        public string FormattedName { get; set; }

        public Employee(int id, string firstname, string lastName, string department, Location location) { 
        
            Id=id;
            FirstName= firstname;
            LastName= lastName;
            Department=department;
            BranchLocation=location;
            City=location.City;
            State=location.State;  
        }
        public Employee() { }
    }
}
