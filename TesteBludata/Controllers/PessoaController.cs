using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TesteBludata;

namespace TesteBludata.Views
{
    public class PessoaController : Controller
    {
        private BD_BLUDATAEntities db = new BD_BLUDATAEntities();

        string uf = System.IO.File.ReadAllText("./config.ini");
        string msgErro;

        // GET: Pessoa
        public ActionResult Index()
        {
            return View(db.Pessoa.ToList());
        }

        // GET: Pessoa/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pessoa pessoa = db.Pessoa.Find(id);
            if (pessoa == null)
            {
                return HttpNotFound();
            }
            return View(pessoa);
        }

        // GET: Pessoa/Create
        public ActionResult Create()
        {
            ViewBag.msgErro = msgErro;
            return View();
        }

        // POST: Pessoa/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Pessoa pessoa)
        {
            DateTime agora = DateTime.Now.Date;

            if (pessoa.rg == null && uf == "SC")
            {
                msgErro = "O RG deve ser preenchido!";
                return Json(new { Resultado = pessoa.id }, JsonRequestBehavior.AllowGet);
            }
            else if (agora.Subtract(pessoa.data_nasc).TotalDays < (18 * 365) && uf == "PR")
            {
                msgErro = "Somente pessoas com mais de 18 anos podem ser cadastradas!";
                return Json(new { Resultado = pessoa.id }, JsonRequestBehavior.AllowGet);
            }
            if (ModelState.IsValid)
            {
                db.Pessoa.Add(pessoa);
                db.SaveChanges();
            }

            return Json(new { Resultado = pessoa.id},JsonRequestBehavior.AllowGet);
        }

        // GET: Pessoa/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pessoa pessoa = db.Pessoa.Find(id);
            if (pessoa == null)
            {
                return HttpNotFound();
            }
            ViewBag.msgErro = msgErro;
            return View(pessoa);
        }

        // POST: Pessoa/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,nome,cpf,data_cad,data_nasc,rg")] Pessoa pessoa)
        {
            DateTime agora = DateTime.Now.Date;
            if (pessoa.rg == null && uf == "SC")
            {
                msgErro = "O RG deve ser preenchido!";
                return View();
            }
            else if (agora.Subtract(pessoa.data_nasc).TotalDays < (18 * 365) && uf == "PR")
            {
                msgErro = "Somente pessoas com mais de 18 anos podem ser cadastradas!";
                return View();
            }
            if (ModelState.IsValid)
            {
                db.Entry(pessoa).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pessoa);
        }

        // GET: Pessoa/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pessoa pessoa = db.Pessoa.Find(id);
            if (pessoa == null)
            {
                return HttpNotFound();
            }
            return View(pessoa);
        }

        // POST: Pessoa/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Pessoa pessoa = db.Pessoa.Find(id);
            db.Telefone.RemoveRange(db.Telefone.Where(a => a.id_pessoa == id));
            db.Pessoa.Remove(pessoa);
            db.SaveChanges();
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
