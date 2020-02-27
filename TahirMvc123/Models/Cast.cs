using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks; 

namespace TahirMvc123.Models
{
    public class Cast: Entity
    { 
        public string CastName { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0: dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? Date { get; set; }
        public int VlilageId { get; set; }
        public Vlilage Vlilage { get; set; }
        public virtual ICollection<Family> Families { get; set; }
        
    }
}
