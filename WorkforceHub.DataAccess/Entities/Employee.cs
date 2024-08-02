using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkforceHub.DataAccess.Entities
{
    public class Employee
    {
        public int EmployeeID { get; set; }
        public decimal PayPerHour { get; set; }
        public Person Person { get; set; }
    }
}
