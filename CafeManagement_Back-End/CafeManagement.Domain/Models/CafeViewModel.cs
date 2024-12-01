using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeManagement.Domain.Models
{
    public class CafeViewModel
    {
        public Guid? Id { get; set; } = null;
        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string Logo { get; set; }
        public int? Employees { get; set; } = 0;
    }
}
