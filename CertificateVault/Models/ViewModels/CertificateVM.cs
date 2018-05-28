using CertificateVault.Models.ADOModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CertificateVault.Models.ViewModels
{
    public class CertificateVM
    {
        [Key]
        [Column(Order = 0)]
        [Required]
        public string CertificateNumber { get; set; }

        [Key]
        [Column(Order = 1)]
        [Required]
        public int VaultId { get; set; }

        [Required]
        public int MemberID { get; set; }

        [Key]
        [Column(Order = 2)]
        [Required]
        [Display(Name = "Symbol ISIN")]
        public string SymbolIsin { get; set; }

        [Required]
        public long Volume { get; set; }

        [Required]
        public string Status { get; set; }

        [Required]
        [Display(Name = "Created At")]
        public System.DateTime CreatedAt { get; set; }

        [Required]
        [Display(Name = "Created By")]
        public string CreatedBy { get; set; }

        [Required]
        [Display(Name = "Apporved At")]
        public Nullable<System.DateTime> ApprovedAt { get; set; }

        [Required]
        [Display(Name = "Approved By")]
        public string ApprovedBy { get; set; }

        [Required]
        [Display(Name = "Withdraw Request At")]
        public Nullable<System.DateTime> WithdrawRequestAt { get; set; }

        [Required]
        [Display(Name = "Withdraw Request By")]
        public string WithdrawRequestBy { get; set; }

        [Required]
        [Display(Name = "Withdraw Apporved At")]
        public Nullable<System.DateTime> WithdrawApprovedAt { get; set; }

        [Required]
        [Display(Name = "Withdraw Apporved By")]
        public string WithdrawApprovedBy { get; set; }

        [Required]
        [Display(Name = "Updated At")]
        public System.DateTime UpdatedAt { get; set; }

        [Required]
        public string Client { get; set; }
        public Nullable<int> ClientId { get; set; }

       // public virtual Vault Vault { get; set; }
    }
}