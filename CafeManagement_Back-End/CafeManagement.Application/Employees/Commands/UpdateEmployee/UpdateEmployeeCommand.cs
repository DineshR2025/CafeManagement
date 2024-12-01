using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeManagement.Application.Employees.Commands.UpdateEmployee
{
    public class UpdateEmployeeCommand : IRequest
    {
        public string Id { get; set; }
        public string Name { get; set; } 
        public string Email { get; set; } 
        public string PhoneNumber { get; set; }
        public string Gender { get; set; } 
        public Guid AssignedCafeId { get; set; }
    }
}
