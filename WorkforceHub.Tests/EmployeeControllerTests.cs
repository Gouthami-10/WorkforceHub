using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using WorkforceHubAPI.Controllers;
using WorkforceHubAPI.BusinessLogic.Interfaces;
using WorkforceHub.Models;

namespace WorkforceHubAPI.Tests
{
    public class EmployeeControllerTests
    {
        private readonly Mock<IEmployeeService> _mockService;
        private readonly EmployeeController _controller;

        public EmployeeControllerTests()
        {
            _mockService = new Mock<IEmployeeService>();
            _controller = new EmployeeController(_mockService.Object);
        }

        [Fact]
        public void AddEmployee_ValidEmployee_ReturnsOk()
        {
            // Arrange
            var employeeCreateDto = new EmployeeCreateDto
            {
                FirstName = "John",
                LastName = "Doe",
                Address = "123 Main St",
                PayPerHour = 20.0m,
                Role = "Employee"
            };

            // Act
            var result = _controller.AddEmployee(employeeCreateDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("Employee added successfully.", okResult.Value);
            _mockService.Verify(s => s.AddEmployee(It.IsAny<EmployeeDto>()), Times.Once);
        }

        [Fact]
        public void AddEmployee_InvalidRole_ReturnsBadRequest()
        {
            // Arrange
            var employeeCreateDto = new EmployeeCreateDto
            {
                FirstName = "Jane",
                LastName = "Smith",
                Address = "456 Elm St",
                Role = "" // Invalid role
            };

            // Act
            var result = _controller.AddEmployee(employeeCreateDto);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Role must be specified.", badRequestResult.Value);
            _mockService.Verify(s => s.AddEmployee(It.IsAny<EmployeeDto>()), Times.Never);
        }

        [Fact]
        public void AddEmployee_InvalidPayPerHourForEmployee_ReturnsBadRequest()
        {
            // Arrange
            var employeeCreateDto = new EmployeeCreateDto
            {
                FirstName = "Alice",
                LastName = "Johnson",
                Address = "789 Oak St",
                PayPerHour = -10.0m, // Invalid pay
                Role = "Employee"
            };

            // Act
            var result = _controller.AddEmployee(employeeCreateDto);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("PayPerHour must be greater than 0 for an Employee.", badRequestResult.Value);
            _mockService.Verify(s => s.AddEmployee(It.IsAny<EmployeeDto>()), Times.Never);
        }

        [Fact]
        public void AddEmployee_InvalidAnnualSalaryForSupervisor_ReturnsBadRequest()
        {
            // Arrange
            var employeeCreateDto = new EmployeeCreateDto
            {
                FirstName = "Bob",
                LastName = "Brown",
                Address = "987 Pine St",
                AnnualSalary = 0.0m, // Invalid salary
                Role = "Supervisor"
            };

            // Act
            var result = _controller.AddEmployee(employeeCreateDto);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("AnnualSalary must be greater than 0 for a Supervisor.", badRequestResult.Value);
            _mockService.Verify(s => s.AddEmployee(It.IsAny<EmployeeDto>()), Times.Never);
        }
    }
}
