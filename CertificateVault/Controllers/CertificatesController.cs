using CertificateVault.Models.ADOModels;
using PagedList;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CertificateVault.Controllers
{
    public class CertificatesController : Controller
    {
        private CertificateVaultEntities db = new CertificateVaultEntities();

        // GET: Certificates
        public async Task<ActionResult> Index(int? page)
        {
            var certificates =  db.Certificates.Include(c => c.Vault);
            return View(certificates.ToList().ToPagedList(page ?? 1,10));
        }

        // GET: Certificates/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Certificate certificate = await db.Certificates.FindAsync(id);
            if (certificate == null)
            {
                return HttpNotFound();
            }
            return View(certificate);
        }

        // GET: Certificates/Create
        public ActionResult Create()
        {
            ViewBag.VaultId = new SelectList(db.Vaults, "VaultId", "Name");
            return View();
        }

        // POST: Certificates/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "CertificateNumber,VaultId,MemberID,SymbolIsin,Volume,Status,CreatedAt,CreatedBy,ApprovedAt,ApprovedBy,WithdrawRequestAt,WithdrawRequestBy,WithdrawApprovedAt,WithdrawApprovedBy,UpdatedAt,Client,ClientId")] Certificate certificate)
        {
            if (ModelState.IsValid)
            {
                db.Certificates.Add(certificate);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.VaultId = new SelectList(db.Vaults, "VaultId", "Name", certificate.VaultId);
            return View(certificate);
        }

        // GET: Certificates/Edit/5
        public async Task<ActionResult> Edit(string id,int id2,string id3)
        {
            if (id == null && id2 == -1 && id3 == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Certificate certificate = await db.Certificates.FindAsync(id,id2,id3);
            if (certificate == null)
            {
                return HttpNotFound();
            }
            ViewBag.VaultId = new SelectList(db.Vaults, "VaultId", "Name", certificate.VaultId);
            return View(certificate);
        }

        // POST: Certificates/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Authorize(Roles ="Clerk,Manager,Supervisor")]
        public async Task<ActionResult> Edit([Bind(Include = "CertificateNumber,VaultId,MemberID,SymbolIsin,Volume,Status,CreatedAt,CreatedBy,ApprovedAt,ApprovedBy,WithdrawRequestAt,WithdrawRequestBy,WithdrawApprovedAt,WithdrawApprovedBy,UpdatedAt,Client,ClientId")] Certificate certificate)
        {
            if (ModelState.IsValid)
            {
                db.Entry(certificate).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.VaultId = new SelectList(db.Vaults, "VaultId", "Name", certificate.VaultId);
            return View(certificate);
        }

        // GET: Certificates/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Certificate certificate = await db.Certificates.FindAsync(id);
            if (certificate == null)
            {
                return HttpNotFound();
            }
            return View(certificate);
        }

        // POST: Certificates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            Certificate certificate = await db.Certificates.FindAsync(id);
            db.Certificates.Remove(certificate);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
