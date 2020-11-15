using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Hypnosis.Web.Models;
using Hypnosis.Web.Data;
using Hypnosis.Web.Data.DbOperations;
using Hypnosis.Web.MyHelpers;

namespace Hypnosis.Web.Controllers
{
    public class ProjectsController : ControllerBase
    {
        //
        // GET: /Projects/
        private DbProject ProjectOperations;
        public ProjectsController()
        {
            dbOperation = new DbProject();
            ProjectOperations = (DbProject)dbOperation;
        }


        public ActionResult Index(int? Type_ID, int? SubType_ID, bool? InMailingListOnly)
        {
            var model = new EventsFilterViewModel_ForList();

            if (Type_ID.HasValue)
            {
                model.Type_ID = Type_ID.Value;
            }
            if (SubType_ID.HasValue)
            {
                model.SubType_ID = SubType_ID.Value;
            }
            if (InMailingListOnly.HasValue)
            {
                model.InMailingListOnly = InMailingListOnly.Value;
            }
            return View(model);
        }


        [HttpPost]
        public ActionResult IndexData(Models.DataTables.jqDataTableInput input, int? Type_ID, int? SubType_ID, bool InMailingListOnly)
        {
            var source = ProjectOperations.GetRows(Type_ID, SubType_ID, InMailingListOnly);
            System.Linq.Expressions.Expression<Func<ProjectEventsListRowViewModel, bool>> prefilter = null;
            return new Models.DataTables.DataTablesActionResult<ProjectEventsListRowViewModel>(source, input, prefilter);
        }





        public ActionResult Export(int? Type_ID, int? SubType_ID, bool? InMailingListOnly)
        {
            var source = ProjectOperations.GetExportRows(Type_ID, SubType_ID, InMailingListOnly);
            return this.Excel("מכונים", "Projects", source);
        }


        [HttpPost]
        public JsonResult Delete(int Id)
        {
            var msg = ProjectOperations.Delete(Id);
            if (msg != "המחיקה בוצעה בהצלחה")
            {
                ModelState.AddModelError("", msg);
                var data = new JsonObject { result = false };
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var data = new JsonObject { result = true };
                return Json(data, JsonRequestBehavior.AllowGet);
            }


        }


        public ActionResult Edit(int Id, int? type_ID, int? subType_ID, bool inMailingListOnly)
        {
            Project card = ProjectOperations.GetProjectById(Id);
            if (card == null)
            {
                return RedirectToAction("Index", new { Type_ID = type_ID, SubType_ID = subType_ID, InMailingListOnly = inMailingListOnly });
            }
            ProjectEditModel model = ProjectOperations.GetProjectEditModel(card);
            model.filter = new EventsFilterViewModel_ForList
            {
                Type_ID = type_ID,
                SubType_ID = subType_ID,
                InMailingListOnly = inMailingListOnly
            };
            model.eventsFilter = new EventsFilterViewModel_ForCard()
            {
                Category_ID = 1,
                Card_ID = Id
            };
            model.projectFilter = new ProjectFilterViewModel_ForCard()
            {
                ID = card.ID
            };
            
            return View(model);
        }


        [HttpPost]
        public ActionResult Edit(ProjectEditModel model)
        {
            var files = Request.Files;

            try
            {

                if (files.Count > 0)
                {
                    var file0 = files[0];
                    var relativePath = FilesHelper.RelativePath;
                    var path = Server.MapPath(relativePath);
                    ProjectOperations.CreateUpdate(model.details, files[0], path, true);

                }
                else
                {

                    ProjectOperations.CreateUpdate(model.details, null, "", true);
                }



                //ProjectOperations.CreateUpdate(model.details, true);
                return RedirectToAction("Index", new { Type_ID = model.filter.Type_ID, SubType_ID = model.filter.SubType_ID, InMailingListOnly = model.filter.InMailingListOnly });
           
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(model);
            }
        }


        public ActionResult Create(int? type_ID, int? subType_ID, bool inMailingListOnly)
        {
            var model = new ProjectCreateModel();

            model.filter = new EventsFilterViewModel_ForList
            {
                Type_ID = type_ID,
                SubType_ID = subType_ID,
                InMailingListOnly = inMailingListOnly
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(ProjectCreateModel model)
        {
            try
            {

                ProjectOperations.CreateUpdate(model.details,null,"", false);
                return RedirectToAction("Index", new { Type_ID = model.filter.Type_ID, SubType_ID = model.filter.SubType_ID, InMailingListOnly = model.filter.InMailingListOnly });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(model);
            }
        }



    }
}
