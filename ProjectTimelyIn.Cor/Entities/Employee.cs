using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTimelyIn.Core.Entities
{
    public class Employee
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; } // "Admin" or "Employee"
        public  List<WorkHours> WorkHours { get; set; }
        public  List<Vacation> Vacations { get; set; }
     

    }
}
