using CafeManagement.Application.Cafes.Commands.AddCafe;
using CafeManagement.Application.DTOs;
using CafeManagement.Domain.Entity;
using CafeManagement.Domain.IRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeManagement.Application.Employees.Commands.AddEmployee
{
    public class AddEmployeeCommandHandler : IRequestHandler<AddEmployeeCommand, string>
    {
        private readonly IEmployeeRepository _employeeRepository;
        public AddEmployeeCommandHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        public async Task<string> Handle(AddEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employee = new Employee()
            {
                Id = "UI", //Actual value will be set by DB trigger
                CafeId = request.AssignedCafeId,
                Name = request.Name,
                EmailAddress = request.Email,
                PhoneNumber = request.PhoneNumber,
                StartDate = DateOnly.FromDateTime(DateTime.Now),
                Gender = request.Gender,
            };
            await _employeeRepository.AddEmployeeAsync(employee);
            return employee.Id;
        }
    }
}
