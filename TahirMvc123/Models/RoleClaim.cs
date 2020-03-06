using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TahirMvc123.Models
{
    public class RoleClaim : Entity
    {
        public string Value { get; set; }

        public int RoleId { get; set; }
        public Role Role { get; set; }
    }
}
