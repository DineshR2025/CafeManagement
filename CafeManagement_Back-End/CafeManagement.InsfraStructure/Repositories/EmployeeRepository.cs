using CafeManagement.Application.DTOs;
using CafeManagement.Domain.Entity;
using CafeManagement.Domain.IRepository;
using CafeManagement.Domain.Models;
using CafeManagement.Insfrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CafeManagement.Insfrastructure.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private CafeManagementContext dbContext;

        public EmployeeRepository(CafeManagementContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<EmployeeViewModel>> GetAllEmployeesAsync()
        {
            return await (from emp in dbContext.Employees
                          join cf in dbContext.Cafes
                          on emp.CafeId equals cf.Id
                          select new EmployeeViewModel()
                          {
                              Name = emp.Name,
                              Id = emp.Id,
                              Email = emp.EmailAddress,
                              PhoneNumber = emp.PhoneNumber,
                              Gender = emp.Gender,
                              Cafe = cf.Name,
                              AssignedCafeId = cf.Id,
                              DaysWorked = (DateTime.UtcNow - emp.StartDate.ToDateTime(TimeOnly.MinValue)).Days,
                          }).ToListAsync();
        }

        public async Task<IEnumerable<EmployeeViewModel>> GetEmployeesByCafeAsync(string cafeId)
        {
            return await (from emp in dbContext.Employees
                          join cf in dbContext.Cafes
                                      on emp.CafeId equals cf.Id
                          where emp.CafeId.ToString() == cafeId
                          select new EmployeeViewModel()
                          {
                              Name = emp.Name,
                              Id = emp.Id,
                              Email = emp.EmailAddress,
                              PhoneNumber = emp.PhoneNumber,
                              Gender = emp.Gender,
                              Cafe = cf.Name,
                              AssignedCafeId = cf.Id,
                              DaysWorked = (DateTime.UtcNow - emp.StartDate.ToDateTime(TimeOnly.MinValue)).Days,
                          }).ToListAsync();
        }

        public async Task AddEmployeeAsync(Employee employee)
        {
            await dbContext.Employees.AddAsync(employee);
            await dbContext.SaveChangesAsync();
        }
        public async Task UpdateEmployeeAsync(Employee employee)
        {
            var updEmp = await dbContext.Employees.FirstOrDefaultAsync(x => x.Id == employee.Id);
            if (updEmp != null)
            {
                updEmp.Name = employee.Name;
                updEmp.EmailAddress = employee.EmailAddress;
                updEmp.PhoneNumber = employee.PhoneNumber;
                updEmp.Gender = employee.Gender;
                updEmp.CafeId = employee.CafeId;

                await dbContext.SaveChangesAsync();
            }
        }
        public async Task DeleteEmployeeAsync(string employeeId)
        {
            var emp = await dbContext.Employees.FirstOrDefaultAsync(x => x.Id == employeeId);

            if (emp != null)
            {
                dbContext.Employees.Remove(emp);

                await dbContext.SaveChangesAsync();
            }
        }
    }
}
