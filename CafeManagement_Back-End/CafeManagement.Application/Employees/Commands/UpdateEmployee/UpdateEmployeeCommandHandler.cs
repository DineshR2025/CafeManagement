using CafeManagement.Domain.Entity;
using CafeManagement.Domain.IRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeManagement.Application.Employees.Commands.UpdateEmployee
{
    public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand>
    {
        private readonly IEmployeeRepository _employeeRepository;
        public UpdateEmployeeCommandHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employee = new Employee()
            {
                Id = request.Id,
                CafeId = request.AssignedCafeId,
                Name = request.Name,
                EmailAddress = request.Email,
                PhoneNumber = request.PhoneNumber,
                Gender = request.Gender,
            };

            await _employeeRepository.UpdateEmployeeAsync(employee);
        }
    }
}
