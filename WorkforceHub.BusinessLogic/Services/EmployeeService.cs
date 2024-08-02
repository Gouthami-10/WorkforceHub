using WorkforceHubAPI.BusinessLogic.Interfaces;
using System.Data;
using Microsoft.Data.SqlClient;
using WorkforceHub.Models;

namespace WorkforceHubAPI.BusinessLogic.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly string _connectionString;

        public EmployeeService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<EmployeeDto> GetAllEmployees()
        {
            var employees = new List<EmployeeDto>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                // Fetch employees
                using (var command = new SqlCommand(@"
                    SELECT e.EmployeeID, p.FirstName, p.LastName, p.Address, e.PayPerHour
                    FROM Employees e
                    JOIN Person p ON e.EmployeeID = p.PersonID", connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        employees.Add(new EmployeeDto
                        {
                            EmployeeID = reader.GetInt32(0),
                            FirstName = reader.GetString(1),
                            LastName = reader.GetString(2),
                            Address = reader.GetString(3),
                            PayPerHour = reader.GetDecimal(4),
                            Role = "Employee"
                        });
                    }
                }

                // Fetch supervisors
                using (var command = new SqlCommand(@"
                    SELECT s.SupervisorID, p.FirstName, p.LastName, p.Address, s.AnnualSalary
                    FROM Supervisors s
                    JOIN Person p ON s.SupervisorID = p.PersonID", connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        employees.Add(new EmployeeDto
                        {
                            EmployeeID = reader.GetInt32(0),
                            FirstName = reader.GetString(1),
                            LastName = reader.GetString(2),
                            Address = reader.GetString(3),
                            AnnualSalary = reader.GetDecimal(4),
                            Role = "Supervisor"
                        });
                    }
                }

                // Fetch managers
                using (var command = new SqlCommand(@"
                    SELECT m.ManagerID, p.FirstName, p.LastName, p.Address, m.AnnualSalary, m.MaxExpenseAmount
                    FROM Managers m
                    JOIN Person p ON m.ManagerID = p.PersonID", connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        employees.Add(new EmployeeDto
                        {
                            EmployeeID = reader.GetInt32(0),
                            FirstName = reader.GetString(1),
                            LastName = reader.GetString(2),
                            Address = reader.GetString(3),
                            AnnualSalary = reader.GetDecimal(4),
                            MaxExpenseAmount = reader.GetDecimal(5),
                            Role = "Manager"
                        });
                    }
                }
            }

            return employees;
        }

        public void AddEmployee(EmployeeDto employeeDto)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                // Insert into Person table
                var personQuery = @"
                    INSERT INTO Person (FirstName, LastName, Address)
                    OUTPUT INSERTED.PersonID
                    VALUES (@FirstName, @LastName, @Address)";

                int personId;
                using (var personCommand = new SqlCommand(personQuery, connection))
                {
                    personCommand.Parameters.AddWithValue("@FirstName", employeeDto.FirstName);
                    personCommand.Parameters.AddWithValue("@LastName", employeeDto.LastName);
                    personCommand.Parameters.AddWithValue("@Address", employeeDto.Address);

                    personId = (int)personCommand.ExecuteScalar();
                }

                // Insert into the correct table based on role
                if (employeeDto.Role == "Employee")
                {
                    var employeeQuery = @"
                        INSERT INTO Employees (EmployeeID, PayPerHour)
                        VALUES (@EmployeeID, @PayPerHour)";
                    using (var employeeCommand = new SqlCommand(employeeQuery, connection))
                    {
                        employeeCommand.Parameters.AddWithValue("@EmployeeID", personId);
                        employeeCommand.Parameters.AddWithValue("@PayPerHour", employeeDto.PayPerHour);
                        employeeCommand.ExecuteNonQuery();
                    }
                }
                else if (employeeDto.Role == "Supervisor")
                {
                    var supervisorQuery = @"
                        INSERT INTO Supervisors (SupervisorID, AnnualSalary)
                        VALUES (@SupervisorID, @AnnualSalary)";
                    using (var supervisorCommand = new SqlCommand(supervisorQuery, connection))
                    {
                        supervisorCommand.Parameters.AddWithValue("@SupervisorID", personId);
                        supervisorCommand.Parameters.AddWithValue("@AnnualSalary", employeeDto.AnnualSalary);
                        supervisorCommand.ExecuteNonQuery();
                    }
                }
                else if (employeeDto.Role == "Manager")
                {
                    var managerQuery = @"
                        INSERT INTO Managers (ManagerID, AnnualSalary, MaxExpenseAmount)
                        VALUES (@ManagerID, @AnnualSalary, @MaxExpenseAmount)";
                    using (var managerCommand = new SqlCommand(managerQuery, connection))
                    {
                        managerCommand.Parameters.AddWithValue("@ManagerID", personId);
                        managerCommand.Parameters.AddWithValue("@AnnualSalary", employeeDto.AnnualSalary);
                        managerCommand.Parameters.AddWithValue("@MaxExpenseAmount", employeeDto.MaxExpenseAmount);
                        managerCommand.ExecuteNonQuery();
                    }
                }
            }
        }
    }
}
