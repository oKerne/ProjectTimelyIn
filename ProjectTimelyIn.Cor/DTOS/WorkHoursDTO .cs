using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTimelyIn.Core.DTOS
{
   public class WorkHoursDTO
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public DateTime WorkDate { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public double TotalHours { get; set; } // חישוב שעות העבודה
    }
}
