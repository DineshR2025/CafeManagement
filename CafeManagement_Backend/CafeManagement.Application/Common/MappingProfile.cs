using AutoMapper;
using CafeManagement.Application.Cafes.Commands.AddCafe;
using CafeManagement.Application.Cafes.Commands.UpdateCafe;
using CafeManagement.Application.DTOs;
using CafeManagement.Domain.Entity;
using CafeManagement.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeManagement.Application.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Cafe Mappings
            CreateMap<CafeViewModel, CafeDto>();

            // Employee Mappings
            CreateMap<EmployeeViewModel, EmployeeDto>();
        }
    }
}
