﻿namespace ProjectTimelyIn.Api.Models
{
    public class VacationPostModel
    {

            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }
            public string Reason { get; set; }
            public int EmployeeId { get; set; }
        
    }
}
