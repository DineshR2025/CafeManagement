using CafeManagement.Application.Cafes.Commands.AddCafe;
using CafeManagement.Domain.Entity;
using CafeManagement.Domain.IRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeManagement.Application.Cafes.Commands.UpdateCafe
{
    public class UpdateCafeCommandHandler : IRequestHandler<UpdateCafeCommand>
    {
        private readonly ICafeRepository _cafeRepository;
        public UpdateCafeCommandHandler(ICafeRepository cafeRepository)
        {
            _cafeRepository = cafeRepository;
        }
        public async Task Handle(UpdateCafeCommand request, CancellationToken cancellationToken)
        { 
            var cafe = new Cafe()
            {
                Name = request.Name,
                Description = request.Description,
                Location = request.Location,
                Logo = Convert.FromBase64String(request.Logo ?? string.Empty)
            };

            await _cafeRepository.UpdateCafeAsync(cafe);
        }

    }
}
