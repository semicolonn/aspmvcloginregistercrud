using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using testMvcApp.Models;

namespace testMvcApp.Controllers
{
    public class logincontController : Controller
    {

        mvcaspEntities db = new mvcaspEntities();

        // GET: logincont
        public ActionResult login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult login(authtable a)
        {

            if (ModelState.IsValid)
            {
                var data = db.authtables.Where(s => s.username.Equals(a.username) && s.password.Equals(a.password)).FirstOrDefault();
                if (data !=null)
                {
                    //add session
                    Session["FullName"] = a.username;
                    return RedirectToAction("dashboard");
                }
                else
                {
                    TempData["Msg"] = "Wrong user or password input!";
                    return RedirectToAction("login");
                }

            }
            return View();
        }

        public ActionResult dashboard()
        {
            return View();
        }

        public ActionResult logOut()
        {
            Session.Abandon();
            return RedirectToAction("login");
        }

        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(authtable autho)
        {

            if (ModelState.IsValid == true)
            {
                db.authtables.Add(autho);
                int a = db.SaveChanges();
                if (a > 0)
                {
                    TempData["Msg"] = "User created successfully!";
                    return RedirectToAction("Register");
                }
                else
                {
                    TempData["Msg"] = "Error to create user!";
                }
            }

            return View();


        }
    }
}