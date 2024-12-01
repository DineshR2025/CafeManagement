using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeManagement.Domain.Models
{
    public class EmployeeViewModel
    {
        public string? Id { get; set; } = null;
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Gender { get; set; }
        public int? DaysWorked { get; set; } = 0;
        public string? Cafe { get; set; } = null;
        public Guid AssignedCafeId { get; set; }
    }
}
