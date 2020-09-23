using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FundingDashboardAPI.Models
{
    public partial class Professions
    {


        [Key]
        [Column("ProfessionID")]
        public int ProfessionId { get; set; }
        [Required]
        [StringLength(80)]
        public string Name { get; set; }

        //[InverseProperty("Profession")]
        //public virtual ICollection<Skills> Skills { get; set; }
    }
}
