using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FundingDashboardAPI.Models
{
    public partial class Funding
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [Required]
        public string Region { get; set; }
        [Required]
        [StringLength(100)]
        public string ServiceLine { get; set; }
        [Required]
        public string MinimumCriteria { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string HowToApply { get; set; }
        [Required]
        public string WhenToApply { get; set; }
        [Required]
        public string ApprovalProcess { get; set; }
        [Required]
        public string FundingCollection { get; set; }
        [Required]
        [StringLength(50)]
        public string AddedBy { get; set; }
        [Column(TypeName = "date")]
        public DateTime AddedOn { get; set; }
        public bool Archived { get; set; }
    }
}
