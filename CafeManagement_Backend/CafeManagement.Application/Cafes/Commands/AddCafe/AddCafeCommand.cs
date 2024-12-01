using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CafeManagement.Application.Cafes.Commands.AddCafe
{
    public class AddCafeCommand: IRequest<Guid>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string? Logo { get; set; } = null;
    }
}
