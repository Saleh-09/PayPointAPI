using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayPointAPI.Models.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; } = default!;
        public string EmployeeEmail { get; set; } = default!;
        public double EmployeeStore { get; set; }
        public double EmployeePhone { get; set; }

        public int StoreId { get; set; }
        public Store Store { get; set; }= default!;
    }
}
