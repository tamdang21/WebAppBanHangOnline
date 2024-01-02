using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebAppBanHangOnlineNhomNBTPQ.Controllers
{
    public class RegisterController : Controller
    {
        // GET: Register
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register_Account(tbKHACHHANG Account_user)
        {
            if (ModelState.IsValid)
            {
                using (dbQUANLYBANQUANAOEntities db = new dbQUANLYBANQUANAOEntities())
                {
                    if (!db.tbKHACHHANGs.Any(user => user.EMAIL.Equals(Account_user.EMAIL)))
                    {
                        String hoten = Account_user.TAIKHOAN;
                        var acc = new tbKHACHHANG
                        {
                            EMAIL = Account_user.EMAIL,
                            HOTEN = hoten,
                            TAIKHOAN = hoten,
                            MATKHAU = Account_user.MATKHAU
                        };
                        db.tbKHACHHANGs.Add(acc);
                        db.SaveChanges();
                        return RedirectToAction("Index", "SignIn/Login");
                    }
                    else
                    {
                        ViewBag.ErrorMessageRegister = "Email has already been used";
                    }
                }
            }

            return View("Register");
        }
    }
}