using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TahirMvc123.Models
{
    public interface IEntity
    {
        int Id { get; set; }
    }

    public class Entity : IEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
