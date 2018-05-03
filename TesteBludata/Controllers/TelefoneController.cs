using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace TesteBludata.Controllers
{
    public class TelefoneController : Controller
    {
            BD_BLUDATAEntities db = new BD_BLUDATAEntities();
        // GET: Telefone
        public ActionResult ListarTelefones(int id) {

            var lista = db.Telefone.Where(a => a.id_pessoa == id);

            ViewBag.Pessoa = id;

            return PartialView(lista);
        }

        public ActionResult AddTelefone(int idPessoa, string num_tel)
        {
            var telefone = new Telefone()
            {
                id_pessoa = idPessoa,
                num_tel = num_tel  
            };

            db.Telefone.Add(telefone);
            db.SaveChanges();

            return Json(new { Resultado = telefone.id }, JsonRequestBehavior.AllowGet);
        }

        // GET: Telefone/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Telefone telefone = db.Telefone.Find(id);
            if (telefone == null)
            {
                return HttpNotFound();
            }
            return View(telefone);
        }

        // POST: Telefone/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "num_tel,id,id_pessoa")] Telefone telefone)
        {
            if (ModelState.IsValid)
            {
                db.Entry(telefone).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("../Pessoa/Edit/"+telefone.id_pessoa);
            }
            return View(telefone);
        }

        // GET: Telefone/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Telefone telefone = db.Telefone.Find(id);
            if (telefone == null)
            {
                return HttpNotFound();
            }
            return View(telefone);
        }

        // POST: Telefone/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Telefone telefone = db.Telefone.Find(id);
            db.Telefone.Remove(telefone);
            db.SaveChanges();
            return RedirectToAction("../Pessoa/Edit/"+telefone.id_pessoa);
        }

    }
}