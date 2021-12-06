using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using web_app_asp_net_mvc_database_first.Models;
using web_app_asp_net_mvc_database_first.Models.Entities;

namespace web_app_asp_net_mvc_database_first.Controllers
{
    public class TeachersController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            var db = new TimetableEntities();
            var teachers = MappingTeachers(db.Teachers.ToList());

            return View(teachers);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var teacher = new TeacherViewModel();
            return View(teacher);
        }

        [HttpPost]
        public ActionResult Create(TeacherViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var db = new TimetableEntities();


            var teacher = new Teachers();
            MappingTeacher(model, teacher,db);
            db.Teachers.Add(teacher);
            db.SaveChanges();

            return RedirectPermanent("/Teachers/Index");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var db = new TimetableEntities();
            var teacher = db.Teachers.FirstOrDefault(x => x.Id == id);
            if (teacher == null)
                return RedirectPermanent("/Teachers/Index");

            db.Teachers.Remove(teacher);
            db.SaveChanges();

            return RedirectPermanent("/Teachers/Index");
        }


        [HttpGet]
        public ActionResult Edit(int id)
        {
            var db = new TimetableEntities();
            var teacher = MappingTeachers(db.Teachers.Where(x => x.Id == id).ToList()).FirstOrDefault(x => x.Id == id);
            if (teacher == null)
                return RedirectPermanent("/Teachers/Index");

            return View(teacher);
        }

        [HttpPost]
        public ActionResult Edit(TeacherViewModel model)
        {
            var db = new TimetableEntities();
            var teacher = db.Teachers.FirstOrDefault(x => x.Id == model.Id);
            if (teacher == null)
                ModelState.AddModelError("Id", "Преподаватель не найден");

            if (!ModelState.IsValid)
                return View(model);

            MappingTeacher(model, teacher, db);

            db.Entry(teacher).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectPermanent("/Teachers/Index");
        }

        private void MappingTeacher(TeacherViewModel sourse, Teachers destination, TimetableEntities db)
        {
            destination.Name = sourse.Name;
            destination.Sex = (int)sourse.Sex;

            if (sourse.TeacherImageFile != null)
            {
                var image = db.TeacherImages.FirstOrDefault(x => x.Id == sourse.Id);
                if (image != null)
                    db.TeacherImages.Remove(image);

                var data = new byte[sourse.TeacherImageFile.ContentLength];
                sourse.TeacherImageFile.InputStream.Read(data, 0, sourse.TeacherImageFile.ContentLength);

                destination.TeacherImages = new TeacherImages()
                {
                    Guid = Guid.NewGuid(),
                    DateChanged = DateTime.Now,
                    Data = data,
                    ContentType = sourse.TeacherImageFile.ContentType,
                    FileName = sourse.TeacherImageFile.FileName
                };
            }
        }
        [HttpGet]
        public ActionResult GetImage(int id)
        {
            var db = new TimetableEntities();
            var image = db.TeacherImages.FirstOrDefault(x => x.Id == id);
            if (image == null)
            {
                FileStream fs = System.IO.File.OpenRead(Server.MapPath(@"~/Content/Images/not-foto.png"));
                byte[] fileData = new byte[fs.Length];
                fs.Read(fileData, 0, (int)fs.Length);
                fs.Close();

                return File(new MemoryStream(fileData), "image/jpeg");
            }

            return File(new MemoryStream(image.Data), image.ContentType);
        }

        private List<TeacherViewModel> MappingTeachers(List<Teachers> teachers)
        {
            var result = teachers.Select(x => new TeacherViewModel()
            {
                Id = x.Id,
                Name = x.Name,
                Sex = (Sex)x.Sex,
                TeacherImage = x.TeacherImages
            }).ToList();

            return result;
        }
    }
}