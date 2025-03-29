using ProjectTimelyIn.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTimelyIn.Core.DTOS
{
   public class VacationRequestDTO
    {
      public int Id { get; set; }
      public int EmployeeId { get; set; }
      public DateTime StartDate { get; set; }
      public DateTime EndDate { get; set; }
      public string Reason { get; set; }
        public Employee Employee { get; internal set; }
    }
}
