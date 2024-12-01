using CafeManagement.Domain.Entity;
using CafeManagement.Domain.Models;

namespace CafeManagement.Domain.IRepository
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<EmployeeViewModel>> GetAllEmployeesAsync();
        Task<IEnumerable<EmployeeViewModel>> GetEmployeesByCafeAsync(string cafe);
        Task AddEmployeeAsync(Employee employee);
        Task UpdateEmployeeAsync(Employee employee);
        Task DeleteEmployeeAsync(string employeeId);
    }
}
