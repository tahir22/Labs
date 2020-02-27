using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TahirMvc123.Models
{
    public class User : Entity
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime? Date { get; set; }
        public bool IsActive { get; set; }
    }
}
