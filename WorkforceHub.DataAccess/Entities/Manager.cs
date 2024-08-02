using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkforceHub.DataAccess.Entities
{
    public class Manager
    {
        public int ManagerID { get; set; }
        public decimal AnnualSalary { get; set; }
        public decimal? MaxExpenseAmount { get; set; }
        public Person Person { get; set; }
    }
}
