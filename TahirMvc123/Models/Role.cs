using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TahirMvc123.Models
{
    public class Role : Entity
    {
        public string   Name { get; set; }
        public virtual ICollection<UesrRole> UesrRoles { get; set; }
        public virtual ICollection<Userclaims> Userclaims { get; set; }
    }
}
