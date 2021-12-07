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
    public class LessonsController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            var db = new TimetableEntities();
            var lessons = MappingLessons(db.Lessons.ToList());

            return View(lessons);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var lesson = new LessonViewModel();
            return View(lesson);
        }

        [HttpPost]
        public ActionResult Create(LessonViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var db = new TimetableEntities();
            var lesson = new Lessons();
            MappingLesson(model, lesson, db);

           

            db.Lessons.Add(lesson);
            db.SaveChanges();

            return RedirectPermanent("/Lessons/Index");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var db = new TimetableEntities();
            var lesson = db.Lessons.FirstOrDefault(x => x.Id == id);
            if (lesson == null)
                return RedirectPermanent("/Lessons/Index");

            db.Lessons.Remove(lesson);
            db.SaveChanges();

            return RedirectPermanent("/Lessons/Index");
        }


        [HttpGet]
        public ActionResult Edit(int id)
        {
            var db = new TimetableEntities();
            var lesson = MappingLessons(db.Lessons.Where(x=>x.Id == id).ToList()).FirstOrDefault(x => x.Id == id);
            if (lesson == null)
                return RedirectPermanent("/Lessons/Index");

            return View(lesson);
        }

        [HttpPost]
        public ActionResult Edit(LessonViewModel model)
        {
            var db = new TimetableEntities();
            var lesson = db.Lessons.FirstOrDefault(x => x.Id == model.Id);
            if (lesson == null)
                ModelState.AddModelError("Id", "Пара не найдена");

            if (!ModelState.IsValid)
                return View(model);

            MappingLesson(model, lesson, db);

            db.Entry(lesson).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectPermanent("/Lessons/Index");
        }

        private void MappingLesson(LessonViewModel sourse, Lessons destination, TimetableEntities db)
        {
            destination.number = sourse.Number;
            destination.Name = sourse.Name;          
            destination.TeacherId = sourse.TeacherId;
            destination.Teachers = sourse.Teacher;


            if (destination.Groups != null)
                destination.Groups.Clear();

            if (sourse.GroupIds != null && sourse.GroupIds.Any())
                destination.Groups = db.Groups.Where(s => sourse.GroupIds.Contains(s.Id)).ToList();           
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

        private List<LessonViewModel> MappingLessons(List<Lessons> lessons)
        {
            var result = lessons.Select(x => new LessonViewModel()
            {
                Id = x.Id,
                Number = x.number,
                TeacherId = x.TeacherId,
                Teacher = x.Teachers,
                Name = x.Name,
                Groups = x.Groups

            }).ToList();

            return result;
        }

       
    }
}