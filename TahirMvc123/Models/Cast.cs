using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TahirMvc123.Models
{
    public class Cast
    {
        [Key]
        public int CastId { get; set; }
        public string CastName { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0: dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? Date { get; set; }
        public int VlilageId { get; set; }
        public Vlilage Vlilage { get; set; }
        public virtual ICollection<Family> Family { get; set; }
    }
}
