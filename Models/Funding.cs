﻿using System;
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
        //[Required]
        //public string Region { get; set; }

        [Required]
        [StringLength(20)]
        [Display(Name = "CSP")]
        public string CSP { get; set; }


        [Required]
        [StringLength(100)]
        [Display(Name = "Service Line")]
        public string ServiceLine { get; set; }



        [Required]

        public string Description { get; set; }

        [Required]
        [Display(Name = "Minimum Criteria")]
        public string MinimumCriteria { get; set; }

        [Required]
        [Display(Name = "Funding Value")]
        public string FundingValue { get; set; }

        [Required]
        [Display(Name = "Supporting Documentation")]
        public string SupportingDocumentation { get; set; }



        [Required]
        [Display(Name = "How To Apply")]
        public string HowToApply { get; set; }

        [Required]
        [Display(Name = "When To Apply")]
        public string WhenToApply { get; set; }

        [Required]
        [Display(Name = "Approval Process")]
        public string ApprovalProcess { get; set; }

        [Required]
        [Display(Name = "Funding Claim Process")]
        public string FundingClaimProcess { get; set; }

        [Required]
        [Display(Name = "Funding Collection")]
        public string FundingCollection { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Added By")]
        public string AddedBy { get; set; }

        [Column(TypeName = "datetime2")]
        [Display(Name = "Last Updated")]
        public DateTime AddedOn { get; set; }

        public bool Archived { get; set; }
        public bool UK { get; set; }
        public bool USA { get; set; }
        public bool EUR { get; set; }
        public bool ASIA { get; set; }




    }


}
