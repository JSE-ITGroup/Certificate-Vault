using CertificateVault.Models.ADOModels;
using log4net;
using PagedList;
using System;
using System.Collections;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CertificateVault.Controllers
{
    public class VaultsController : Controller
    {
        private readonly CertificateVaultEntities db = new CertificateVaultEntities();
        static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private void  GetDropDownViewData()
        {
            IEnumerable vault = db.AspNetUsers.ToList().Select(v => new SelectListItem
            {
                Text = v.UserName.ToString(),
                Value = v.UserName.ToString()
            });
            ViewBag.Vaults = vault;
        }

    

        // GET: Vaults
        public async Task<ActionResult> Index(int? page)
        {
            var vaults = db.Vaults;
            return View(vaults.ToList().ToPagedList(page ?? 1,10));
        }

        // GET: Vaults/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vault vault = await db.Vaults.FindAsync(id);
            if (vault == null)
            {
                return HttpNotFound();
            }
            return View(vault);
        }

        // GET: Vaults/Create
        [Authorize]
        public ActionResult Create()
        {
            GetDropDownViewData();
            return View();
        }

        // POST: Vaults/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<ActionResult> Create([Bind(Include = "VaultId,Name,CreatedBy,CreatedAt,UpdatedBy,UpdatedAt")] Vault vault)
        {
            GetDropDownViewData();
            if (ModelState.IsValid)
            {
                try
                {
                    db.Vaults.Add(vault);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                catch(Exception exp)
                {
                    ViewData["Message"] = "Error Adding Vault Data.";
                    ModelState.AddModelError("Error", "An error has occured please contact Administrator");
                    log.Error(exp);
                    return View(vault);
                }
            }
            return View(vault);
        }

        // GET: Vaults/Edit/5
        [Authorize]
        public async Task<ActionResult> Edit(int? id)
        {
            GetDropDownViewData();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            } 
            Vault vault =  await db.Vaults.FindAsync(id);
            if (vault == null)
            {
                return HttpNotFound();
            }
           
            return View(vault);
        }

        // POST: Vaults/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<ActionResult> Edit([Bind(Include = "VaultId,Name,CreatedBy,CreatedAt,UpdatedBy,UpdatedAt")] Vault vault)
        {
            GetDropDownViewData();
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(vault).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                catch(Exception exp)
                {
                    ViewData["Message"] = "Error Adding Vault Data.";
                    ModelState.AddModelError("Error", "An error has occured please contact Administrator");
                    log.Error(exp);
                    return View(vault);
                }
            }
            return View(vault);
        }

        // GET: Vaults/Delete/5
        [Authorize]
        public async Task<ActionResult> Delete(int? id)
        {
            GetDropDownViewData();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vault vault = await db.Vaults.FindAsync(id);
            if (vault == null)
            {
                return HttpNotFound();
            }
            return View(vault);
        }

        // POST: Vaults/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Vault vault = await db.Vaults.FindAsync(id);
            try
            {
                db.Vaults.Remove(vault);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            catch(Exception exp)
            {
                ViewData["Message"] = "Error Deleting Vault Data.";
                ModelState.AddModelError("Error", "An error has occured please contact Administrator");
                log.Error(exp);
                return View(vault);
            }
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
