namespace WorkforceHub.Models
{
    public class EmployeeDto
    {
        public int EmployeeID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public decimal PayPerHour { get; set; } // Only for Employees
        public decimal AnnualSalary { get; set; } // Only for Supervisors and Managers
        public decimal MaxExpenseAmount { get; set; } // Only for Managers
        public string Role { get; set; } // Identifies the type (Employee, Supervisor, Manager)
    }
}
