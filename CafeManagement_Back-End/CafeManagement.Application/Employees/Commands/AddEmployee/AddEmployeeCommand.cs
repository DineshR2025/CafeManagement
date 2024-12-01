using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeManagement.Application.Employees.Commands.AddEmployee
{
    public class AddEmployeeCommand : IRequest<string>
    {
        public string Name { get; set; } 
        public string Email { get; set; }
        public string PhoneNumber { get; set; } 
        public string Gender { get; set; }
        public Guid AssignedCafeId { get; set; }
    }
}
