using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TahirMvc123.Models
{
    public class Family
    {
        [Key]
      
        public int Familyid { get; set; }
        public string FamilyName { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0: dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? Date { get; set; }
        public int CastId { get; set; }
        public Cast Cast { get; set; }
        public virtual ICollection<FamilyMember> FamilyMembers { get; set; }
    }
}
