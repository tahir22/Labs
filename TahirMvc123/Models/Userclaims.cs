using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TahirMvc123.Models
{
    public class Userclaims: Entity
    {
        public string UserclaimsValues { get; set; }
        public int  RoleID { get; set; }
        public Role Role { get; set; }
        
    }
}
