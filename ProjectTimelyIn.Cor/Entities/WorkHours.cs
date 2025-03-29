using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTimelyIn.Core.Entities
{
    public class WorkHours
    {
        public int Id { get; set; }
       
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; } 
        public int EmployeeId { get; set; }
        public  Employee Employee { get; set; }
    }
}
