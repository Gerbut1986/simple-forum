using System;
using System.Collections.Generic;
using System.Linq;
using My_Forum_Web.Models;
using System.Web.Mvc;

namespace My_Forum_Web.Controllers
{
    public class EditController : Controller
    {
        MyContext db;

        public EditController() => db = new MyContext();

        public ActionResult EditMsg(int? id)
        {
            ForumMsg upd = db.ForumMsgs.Find(id);
            if (id == null) return HttpNotFound();
            ViewBag.id = id;
            return View(upd);
        }

        [HttpPost]
        public ActionResult EditMsg(ForumMsg entity)
        {
            if (entity != null)
            {
                entity.Date_Added = DateTime.Now;
                entity.UserId = AccountController.CurrentUser;
                db.Entry(entity).State = System.Data.Entity.EntityState.Modified;
                int res = db.SaveChanges();
                return RedirectToAction("../Home/Index");
            }
            return View();
        }
    }
}