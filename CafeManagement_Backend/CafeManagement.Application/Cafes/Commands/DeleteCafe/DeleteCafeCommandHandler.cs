using CafeManagement.Domain.IRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeManagement.Application.Cafes.Commands.DeleteCafe
{
    public class DeleteCafeCommandHandler : IRequestHandler<DeleteCafeCommand>
    {
        private readonly ICafeRepository _caferepository;

        public DeleteCafeCommandHandler(ICafeRepository caferepository)
        {
            _caferepository = caferepository;
        }

        public async Task Handle(DeleteCafeCommand request, CancellationToken cancellationToken)
        {
            await _caferepository.DeleteCafeAsync(request.Id);
        }
    }
}
