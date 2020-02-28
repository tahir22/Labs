﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TahirMvc123.Models
{
    public class FamilyMember : Entity
    {
        public string MemberName { get; set; }
        public bool MarriedStatus { get; set; }
        public int? Children { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0: dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? Date { get; set; }
         
        [ForeignKey("Parent")]
        public int? ParentId { get; set; }
        public FamilyMember Parent { get; set; }

        public int FamilyId { get; set; }
        public Family Family { get; set; }
        
        public virtual ICollection<FamilyMember> ChildrenList { get; set; }
        
        [NotMapped]
        public string ChildrenName { get; set; }
    }
}
