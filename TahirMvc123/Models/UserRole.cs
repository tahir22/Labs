﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TahirMvc123.Models
{
    public class UserRole : Entity
    {
     
        public int UserId { get; set; }
        
        public int RoleId { get; set; }

        public User User { get; set; }
        public Role Role { get; set; }
    }
}
