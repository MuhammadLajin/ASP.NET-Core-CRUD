using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using day1core.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace day1core.Controllers
{
    public class StudentController : Controller
    {
        ITIContext db;

        public StudentController(ITIContext db)
        {
            this.db = db;
        }
        public IActionResult Index()
        {

            ViewBag.dept = db.Departments.ToList();
            return View(db.Students.ToList());
        }

        public IActionResult Create()
        {

            ViewBag.dept = db.Departments.ToList();
            return View();
        }

        [HttpPost]
        public ActionResult Create(Student s)
        {
            //add data to db and save changes
            db.Students.Add(s);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            Student s = db.Students.Where(n => n.StId == id).SingleOrDefault();
            ViewBag.dept = db.Departments.ToList();

            return View(s);
        }

        [HttpPost]
        public ActionResult Edit(Student student)
        {
            Student s = db.Students.Where(n => n.StId == student.StId).SingleOrDefault();
            s.StFname = student.StFname;
            s.StLname = student.StLname;
            s.StAge = student.StAge;
            s.StAddress = student.StAddress;
            s.DeptId = student.DeptId;


            db.SaveChanges();


            return RedirectToAction("index");
        }
        public ActionResult Delete(int id)
        {
            Student s = db.Students.Where(n => n.StId == id).FirstOrDefault();

            db.Students.Remove(s);
            db.SaveChanges();
            return RedirectToAction("index");

        }
    }
}
