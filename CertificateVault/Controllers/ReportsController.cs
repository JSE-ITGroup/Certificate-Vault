using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CertificateVault.Models.ViewModels;
using CertificateVault.Models.ADOModels;
using AutoMapper;
using log4net;
using System.Collections;
using CrystalDecisions.CrystalReports.Engine;

namespace CertificateVault.Controllers
{
    public class ReportsController : Controller
    {
        private CertificateVaultEntities db = new CertificateVaultEntities();
        static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        // GET: Reports
        public ActionResult Index()
        {
            GetDropDownViewData();
            return View();
        }



        private void GetDropDownViewData()
        {
            ViewBag.Symbols = new SelectList(db.usp_GetSymbols().ToList(),"ISIN_CODE", "ISIN_SHORT_NAME");
            ViewBag.Client = new SelectList(db.Clients.ToList(),"Name","Name");
            ViewBag.Status = new SelectList(db.Status.ToList(),"StatusName","StatusName");
            ViewBag.CertificateNumber = new SelectList(db.Certificates.ToList(), "CertificateNumber", "CertificateNumber");
            ViewBag.Vaults = new SelectList(db.Vaults.ToList(),"Name","Name");           
        }

        [HttpPost]
        public ActionResult DownloadJCSDCertificateVaultReport(string Symbol, string ClientName, string Status, string CertificateNumber, string VaultName)
        {
            ReportDocument rd = new ReportDocument();
            rd.Load(Server.MapPath("~/Rpts/JCSD Certificate Vault Summary.rpt"));
            rd.SetParameterValue("@CertificateNumber", string.IsNullOrEmpty(CertificateNumber) ? DBNull.Value : (object)CertificateNumber);
            rd.SetParameterValue("@SymbolIsin", string.IsNullOrEmpty(Symbol) ? DBNull.Value : (object)Symbol);
            rd.SetParameterValue("@ClientName", string.IsNullOrEmpty(ClientName) ? DBNull.Value : (object)ClientName);
            rd.SetParameterValue("@VaultName", string.IsNullOrEmpty(VaultName) ? DBNull.Value : (object)VaultName);
            rd.SetParameterValue("@Status", string.IsNullOrEmpty(Status) ? DBNull.Value : (object)Status);
            rd.SetDatabaseLogon("dependapp", "P@ssw0rd", "10.240.18.204", "CertificateVault");
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            return File(stream, "application/pdf", "Cert Vault Report - "+ClientName+".pdf");
        }
    }
}
