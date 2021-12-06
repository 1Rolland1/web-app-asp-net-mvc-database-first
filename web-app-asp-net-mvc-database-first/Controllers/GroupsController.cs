using System.Linq;
using System.Web.Mvc;
using System.Data.Entity;
using web_app_asp_net_mvc_database_first.Models.Entities;
using web_app_asp_net_mvc_database_first.Models;
using System.Collections.Generic;

namespace web_app_asp_net_mvc_database_first.Controllers
{
    public class GroupsController: Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            var db = new TimetableEntities();
            var groups = MappingGroups(db.Groups.ToList());

            return View(groups);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var group = new GroupViewModel();

            return View(group);
        }

        [HttpPost]
        public ActionResult Create(GroupViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            var db = new TimetableEntities();
            var group = new Groups();
            MappingGroup(model, group);

            db.Groups.Add(group);
            db.SaveChanges();


            return RedirectPermanent("/Groups/Index");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var db = new TimetableEntities();
            var group = db.Groups.FirstOrDefault(x => x.Id == id);
            if (group == null)
                return RedirectPermanent("/Groups/Index");

            db.Groups.Remove(group);
            db.SaveChanges();

            return RedirectPermanent("/Groups/Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var db = new TimetableEntities();
            var group = MappingGroups(db.Groups.Where(x=>x.Id==id).ToList()).FirstOrDefault(x => x.Id == id);
            if (group == null)
                return RedirectPermanent("/Groups/Index");

            return View(group);
        }

        [HttpPost]
        public ActionResult Edit(GroupViewModel model)
        {

            var db = new TimetableEntities();
            var group = db.Groups.FirstOrDefault(x => x.Id == model.Id);
            if (group == null)
            {
                ModelState.AddModelError("Id", "Группа не найдена");
            }
            if (!ModelState.IsValid)
                return View(model);

            MappingGroup(model, group);


            db.Entry(group).State = EntityState.Modified;
            db.SaveChanges();


            return RedirectPermanent("/Groups/Index");
        }

        private void MappingGroup(GroupViewModel sourse, Groups destination)
        {
            destination.GroupName = sourse.GroupName;
            destination.NumberOfStudents = sourse.NumberOfStudents;
            
        }


        private List<GroupViewModel> MappingGroups(List<Groups> groups)
        {
            var result = groups.Select(x => new GroupViewModel()
            {
                Id = x.Id,
                GroupName = x.GroupName,
                NumberOfStudents = x.NumberOfStudents
            }).ToList();

            return result;
        }
    }
}