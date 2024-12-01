
using CafeManagement.Domain.Entity;
using CafeManagement.Domain.IRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeManagement.Application.Cafes.Commands.AddCafe
{
    public class AddCafeCommandHandler : IRequestHandler<AddCafeCommand, Guid>
    {
        private readonly ICafeRepository _cafeRepository;
        public AddCafeCommandHandler(ICafeRepository cafeRepository)
        {
            _cafeRepository = cafeRepository;
        }
        public async Task<Guid> Handle(AddCafeCommand request, CancellationToken cancellationToken)
        {
            var cafe = new Cafe() { Name = request.Name,
                                    Description = request.Description,
                                    Location = request.Location,
                                    Logo = Convert.FromBase64String(request.Logo?? string.Empty)
                                    };
            await _cafeRepository.AddCafeAsync(cafe);
            return cafe.Id;
        }
    }
}
