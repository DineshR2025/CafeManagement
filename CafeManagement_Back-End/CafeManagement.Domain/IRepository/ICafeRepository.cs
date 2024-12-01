using CafeManagement.Domain.Entity;
using CafeManagement.Domain.Models;

namespace CafeManagement.Domain.IRepository
{
    public interface ICafeRepository
    {
        Task<IEnumerable<CafeViewModel>> GetAllCafesAsync();
        Task<IEnumerable<CafeViewModel>> GetCafesbylocationAsync(string location);
        Task AddCafeAsync(Cafe cafe);
        Task UpdateCafeAsync(Cafe cafeDto);
        Task DeleteCafeAsync(Guid cafeId);
    }
}
