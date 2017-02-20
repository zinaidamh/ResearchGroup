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
    public class InstitutesController : ControllerBase
    {
        //
        // GET: /Institutes/
        private DbInstitute InstituteOperations;
        public InstitutesController()
        {
            dbOperation = new DbInstitute();
            InstituteOperations = (DbInstitute)dbOperation;
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
            var source = InstituteOperations.GetRows(Type_ID,SubType_ID,InMailingListOnly);
            System.Linq.Expressions.Expression<Func<InstituteEventsListRowViewModel, bool>> prefilter = null;
            return new Models.DataTables.DataTablesActionResult<InstituteEventsListRowViewModel>(source, input, prefilter);
        }

        [HttpPost]
        public ActionResult EventsDataByCard(Models.DataTables.jqDataTableInput input, int Card_ID, int? Type_ID, int? SubType_ID, int? Category_ID, DateTime? fromDate, DateTime? toDate, bool EssenseOnly, bool ExpiredOnly, bool OpenOnly, bool FileOnly, bool SiteOnly)
        {
            DbEvents EventsOperations = new DbEvents();
            var source = EventsOperations.GetEventsByFilter(Card_ID, Type_ID, SubType_ID, Category_ID, fromDate, toDate, EssenseOnly, ExpiredOnly, OpenOnly, FileOnly, SiteOnly);
            System.Linq.Expressions.Expression<Func<EventsFullListRowViewModel, bool>> prefilter = null;
            return new Models.DataTables.DataTablesActionResult<EventsFullListRowViewModel>(source, input, prefilter);

        }

       
      
       
        public ActionResult Export(int? Type_ID, int? SubType_ID, bool? InMailingListOnly)
        {
            var source = InstituteOperations.GetExportRows(Type_ID, SubType_ID, InMailingListOnly);
            return this.Excel("מכונים", "Institutes", source);
        }


        [HttpPost]
        public JsonResult Delete(int Id)
        {
            var data = InstituteOperations.Delete(Id);
            if (data != "המחיקה בוצעה בהצלחה")
            {
                ModelState.AddModelError("", data);
            }
            return Json(data, JsonRequestBehavior.AllowGet);


        }


        public ActionResult Edit(int Id, int? type_ID, int? subType_ID, bool inMailingListOnly)
        {
            Institute card = InstituteOperations.GetInstituteById(Id);
            if (card == null)
            {
                return RedirectToAction("Index", new { Type_ID = type_ID, SubType_ID = subType_ID, InMailingListOnly = inMailingListOnly });
            }
            InstituteEditModel model = InstituteOperations.GetInstituteEditModel(card);
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

            return View(model);
        }
        

        [HttpPost]
        public ActionResult Edit(InstituteEditModel model)
        {
            try
            {

                InstituteOperations.CreateUpdate(model.details, true);
                return RedirectToAction("Index", new { Type_ID = model.filter.Type_ID, SubType_ID = model.filter.SubType_ID, InMailingListOnly = model.filter.InMailingListOnly });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(model);
            }
        }
        [HttpPost]
        public JsonResult ChangeEventsData()
        {
            DbEvents EventsOperations = new DbEvents();
            var relativePath = FilesHelper.RelativePath;
            var path = Server.MapPath(relativePath);
            bool operationResult = EventsOperations.ChangeData(this.Request, path);


            var data = new JsonObject { result = operationResult };
            return Json(data, JsonRequestBehavior.AllowGet);


        }

        public ActionResult Create(int? type_ID, int? subType_ID, bool inMailingListOnly)
        {
            var model = new InstituteCreateModel();

            model.filter = new  EventsFilterViewModel_ForList
            {
                Type_ID = type_ID,
                SubType_ID = subType_ID,
                InMailingListOnly = inMailingListOnly
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(InstituteCreateModel model)
        {
            try
            {

                InstituteOperations.CreateUpdate(model.details, false);
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
