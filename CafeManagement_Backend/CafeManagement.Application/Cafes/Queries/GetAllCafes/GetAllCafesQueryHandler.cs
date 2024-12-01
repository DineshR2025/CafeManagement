using AutoMapper;
using CafeManagement.Application.DTOs;
using CafeManagement.Domain.IRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeManagement.Application.Cafes.Queries.GetAllCafes
{
    public class GetAllCafesQueryHandler : IRequestHandler<GetAllCafesQuery, List<CafeDto>>
    {
        private readonly ICafeRepository _cafeRepository;
        private readonly IMapper _mapper;

        public GetAllCafesQueryHandler(ICafeRepository cafeRepository, IEmployeeRepository employeeRepository, IMapper mapper) {
            _cafeRepository = cafeRepository;
            _mapper = mapper;
        }

        public async Task<List<CafeDto>> Handle(GetAllCafesQuery request, CancellationToken cancellationToken)
        {
            var cafes = string.IsNullOrWhiteSpace(request.Location)
            ? await _cafeRepository.GetAllCafesAsync()
            : await _cafeRepository.GetCafesbylocationAsync(request.Location);

            if (!cafes.Any()) return new List<CafeDto>();

            var cafeDtos = _mapper.Map<List<CafeDto>>(cafes);

            return cafeDtos.OrderByDescending(c => c.Employees).ToList();
        }
    }
}
