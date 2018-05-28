using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CertificateVault.Models.ADOModels;
using AutoMapper;
using CertificateVault.Models.ViewModels;
using log4net;
using PagedList;

namespace CertificateVault.Controllers
{
    public class ClientsController : Controller
    {
        private CertificateVaultEntities db = new CertificateVaultEntities();
        static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        // GET: Clients
        public async Task<ActionResult> Index(int ? page)
        {
            var clients = db.Clients;
            return View(clients.ToList().ToPagedList(page ?? 1,10));
        }

        // GET: Clients/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var client = await db.Clients.FindAsync(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // GET: Clients/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Clients/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ClientId,Name,Description,CreatedBy,CreatedAt,UpdatedBy,UpdatedAt")] Client client)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Clients.Add(client);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                catch(Exception exp)
                {
                    ViewData["Message"] = "Error Adding Client Data.";
                    ModelState.AddModelError("Error", "An error has occured please contact Administrator");
                    log.Error(exp);
                    return View(client);
                }
            }

            return View(client);
        }

        // GET: Clients/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = await db.Clients.FindAsync(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // POST: Clients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ClientId,Name,Description,CreatedBy,CreatedAt,UpdatedBy,UpdatedAt")] Client client)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(client).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                catch(Exception exp)
                {
                    ViewData["Message"] = "Error Editing Client Data.";
                    ModelState.AddModelError("Error", "An error has occured please contact Administrator");
                    log.Error(exp);
                    return View(client);
                }
            }
            return View(client);
        }

        // GET: Clients/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = await db.Clients.FindAsync(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // POST: Clients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Client client = await db.Clients.FindAsync(id);
            try
            {
                db.Clients.Remove(client);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            catch(Exception exp)
            {
                ViewData["Message"] = "Error Deleting Client Data.";
                ModelState.AddModelError("Error", "An error has occured please contact Administrator");
                log.Error(exp);
                return View(client);
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
