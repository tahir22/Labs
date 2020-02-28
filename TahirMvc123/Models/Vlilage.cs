using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TahirMvc123.Models
{
    public class Vlilage : Entity
    {
        public string VlilageName { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0: dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? Date { get; set; }

        public virtual ICollection<Cast> Casts{ get; set; }
}
}
