using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkforceHub.DataAccess.Entities
{
    public class Supervisor
    {
        public int SupervisorID { get; set; }
        public decimal AnnualSalary { get; set; }
        public Person Person { get; set; }
    }
}
