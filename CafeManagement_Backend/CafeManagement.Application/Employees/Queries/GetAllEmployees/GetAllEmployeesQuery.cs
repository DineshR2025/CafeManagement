using CafeManagement.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeManagement.Application.Employees.Queries.GetAllEmployees
{
    public class GetAllEmployeesQuery : IRequest<List<EmployeeDto>>
    {
        public string? CafeId { get; set; }
    }
}
