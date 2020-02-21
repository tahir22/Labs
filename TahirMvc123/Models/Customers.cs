using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TahirMvc123.Models
{
    public class Customers
    {
        [Key]
        public int customerid { get; set; }
        public string   Name  { get; set; }
        public string profilePic { get; set; }


    }
}
