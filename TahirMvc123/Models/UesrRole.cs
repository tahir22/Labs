using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TahirMvc123.Models
{
    public class UesrRole : Entity
    {

        public int UserId { get; set; }
        public User Users { get; set; }
        public int RoleId { get; set; }
        public Role Roles { get; set; }
    }
}
