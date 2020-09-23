using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FundingDashboardAPI.Models
{
    public partial class Skills
    {
        [Key]
        [Column("SkillID")]
        public int SkillId { get; set; }
        [Required]
        [Column("SkillCode")]
        public string SkillCode { get; set; }
        [Column("ProfessionID")]
        public int ProfessionId { get; set; }
        [Required]
        public string SkillText { get; set; }
        [Required]
        public int SkillLevel { get; set; }

        //[ForeignKey(nameof(ProfessionId))]
        //[InverseProperty(nameof(Professions.Skills))]
        //public virtual Professions Profession { get; set; }
    }
}
