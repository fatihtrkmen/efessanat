using efessanat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace efessanat.Controllers
{
    public class AdminController : Controller
    {
        //Test
        efessanatdb db = new efessanatdb();
        public ActionResult Index()
        {
            return View();
        }
        [ValidateAntiForgeryToken]
        public ActionResult checkUser(Users _user)
        {
            if (ModelState.IsValid)
            {
                var res = db.user.Where(m=>m.Username==_user.Username && m.Password==_user.Password).ToList();
                if (res.Count > 0)
                {
                    Session["uid"] = res[0].Id;
                    return Redirect("/Panel/");
                }
                else
                {
                    ModelState.AddModelError("", "Geçersiz kullanıcı bilgileri.");
                    return View("Index", _user);
                }
            }
            else
            {
                ModelState.AddModelError("", "Lütfen tüm alanları doldurun.");
                return View("Index", _user);
            }

        }
    }
}