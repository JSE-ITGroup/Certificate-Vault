using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CertificateVault.Models.ViewModels
{
    public partial class usp_GetSymbols_ResultVM
    {
        public int ISIN_ID { get; set; }
        public string ISIN_CODE { get; set; }
        public string ISIN_CCY { get; set; }
        public Nullable<int> ISIN_ISSUER { get; set; }
        public string ISIN_FULL_NAME { get; set; }
        public string ISIN_SHORT_NAME { get; set; }
        public string ISIN_CSD { get; set; }
    }
}