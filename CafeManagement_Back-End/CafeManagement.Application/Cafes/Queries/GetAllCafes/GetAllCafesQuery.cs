using CafeManagement.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeManagement.Application.Cafes.Queries.GetAllCafes
{
    public class GetAllCafesQuery : IRequest<List<CafeDto>>
    {
        public string? Location { get; set; }
    }
}
