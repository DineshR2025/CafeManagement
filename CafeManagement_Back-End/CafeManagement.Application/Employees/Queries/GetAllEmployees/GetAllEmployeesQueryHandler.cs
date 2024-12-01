using AutoMapper;
using CafeManagement.Application.DTOs;
using CafeManagement.Domain.Entity;
using CafeManagement.Domain.IRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeManagement.Application.Employees.Queries.GetAllEmployees
{
    internal class GetAllEmployeesQueryHandler : IRequestHandler<GetAllEmployeesQuery, List<EmployeeDto>>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;
        public GetAllEmployeesQueryHandler(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }   
        public async Task<List<EmployeeDto>> Handle(GetAllEmployeesQuery request, CancellationToken cancellationToken)
        {
            var employees = string.IsNullOrEmpty(request.CafeId == "null" ? null : request.CafeId)
            ? await _employeeRepository.GetAllEmployeesAsync()
                : await _employeeRepository.GetEmployeesByCafeAsync(request.CafeId);
            if (!employees.Any()) return new List<EmployeeDto>();

            var cafeDtos = _mapper.Map<List<EmployeeDto>>(employees);

            return cafeDtos.OrderByDescending(c => c.DaysWorked).ToList();
        }
    }
}
