using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeManagement.Application.Cafes.Commands.DeleteCafe
{
    public class DeleteCafeCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
