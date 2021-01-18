using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using testMvcApp.Models;

namespace testMvcApp.Controllers
{
    public class DefaultController : Controller
    {

        mvcaspEntities db = new mvcaspEntities();
        // GET: Default
        public ActionResult Index()
        {
            Session["user"] = "Hi Dear User!";

            if (Session["user"].Equals(""))
            {
                return RedirectToAction("errorpage");
            }

            var data = db.students.ToList();

            return View(data);
        }

        // GET: Default/Details/5
        public ActionResult Details(int id)
        {
            var row = db.students.Where(model => model.id == id).FirstOrDefault();

            return View(row);
        }

        // GET: Default/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Default/Create
        [HttpPost]
        public ActionResult Create(student s)
        {
        
                if (ModelState.IsValid == true)
                {
                    db.students.Add(s);
                    int a = db.SaveChanges();
                    if (a > 0)
                    {
                        TempData["Msg"] = "<script>alert('Inserted!')</script>";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["Msg"] = "<script>alert('NOT Inserted!')</script>";
                    }
                }
  
                return View();

            
        }

        // GET: Default/Edit/5
        public ActionResult Edit(int id)
        {
            var row = db.students.Where(model => model.id == id).FirstOrDefault();
            
            return View(row);
        }

        // POST: Default/Edit/5
        [HttpPost]
        public ActionResult Edit(student s)
        {
            if (ModelState.IsValid == true)
            {
                db.Entry(s).State = System.Data.Entity.EntityState.Modified;
                int a = db.SaveChanges();
                if (a > 0)
                {
                    TempData["Msg"] = "<script>alert('Updated!')</script>";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["Msg"] = "<script>alert('NOT Updated!')</script>";
                }
            }
            return View();
        }

        // GET: Default/Delete/5
        
        // POST: Default/Delete/5
        public ActionResult Delete(int id)
        {
            if (id >0 )
            {
                var row = db.students.Where(model => model.id == id).FirstOrDefault();

                    db.Entry(row).State = System.Data.Entity.EntityState.Deleted;
                    int a = db.SaveChanges();
                    if (a > 0)
                    {
                        TempData["Msg"] = "<script>alert('Deleted!')</script>";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["Msg"] = "<script>alert('NOT Deleted!')</script>";
                    }
                }

            return View();
           
        }



        public ActionResult Home()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Home(HttpPostedFileBase files)
        {
            if (files !=null && files.ContentLength>0)
            {
                var fileName = Path.GetFileName(files.FileName);
                var path = Path.Combine(Server.MapPath("~/uploadFiles"), fileName);

                files.SaveAs(path);
            }
            TempData["Msg"] = "Uploaded successfully!";
            return RedirectToAction("/");
        }



        public ActionResult anotherpage()
        {

            var data = db.books.ToList();

            return View(data);
        }

        public ActionResult cart(int id)
        {
            var row = db.books.Where(model => model.id == id).FirstOrDefault();
            return View();
        }

        public ActionResult errorpage()
        {
            return View();
        }

    }
}
