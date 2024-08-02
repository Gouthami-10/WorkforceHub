using WorkforceHub.Models;

namespace WorkforceHubAPI.BusinessLogic.Interfaces
{
    public interface IEmployeeService
    {
        List<EmployeeDto> GetAllEmployees();
        void AddEmployee(EmployeeDto employeeDto);
    }
}
