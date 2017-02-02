using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Hypnosis.Web.Models;
using Hypnosis.Web.Data;
using Hypnosis.Web.Data.DbOperations;

namespace Hypnosis.Web.Controllers
{
    public class EventsController : ControllerBase
    {
        //
        // GET: /Persons/


        private DbEvents EventsOperations;
        public EventsController()
        {
            dbOperation = new DbEvents();
            EventsOperations = (DbEvents)dbOperation;



        }
        public ActionResult Index()
        {
            var model = new EventsFilterViewModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult IndexData(Models.DataTables.jqDataTableInput input, int? Type_ID, int? SubType_ID, int? Category_ID, DateTime? fromDate, DateTime? toDate, bool EssenseOnly, bool ExpiredOnly, bool OpenOnly, bool FileOnly, bool SiteOnly)
        {

              
            var events = EventsOperations.GetRows();


            if (Type_ID.HasValue)
            {
                events = events.Where(f => f.Type_ID == Type_ID);
            }
            if (SubType_ID.HasValue)
            {
                events = events.Where(f => f.SubType_ID == SubType_ID);
            }
            if (Category_ID.HasValue)
            {
                events = events.Where(f => f.Category_ID==Category_ID);
            }
            if (ExpiredOnly)
            {
                events = events.Where(f => f.ExpirationDate <= DateTime.Now.Date);
            }
            //also for Essense after explain
            if (FileOnly)
            {
                events = events.Where(f => f.FileName!=null);
            }
            if (SiteOnly)
            {
                events = events.Where(f => f.SiteHref!=null);
            }
            if (fromDate.HasValue)
            {
                events = events.Where(f => f.FirstDate >= fromDate);
            }
            if (toDate.HasValue)
            {
                events = events.Where(f => f.FirstDate <= toDate);
            }
            if (OpenOnly)
            {
                events = events.Where(f => f.AlertDone == false);
            }

            var source = events;
            System.Linq.Expressions.Expression<Func<EventsFullListRowViewModel, bool>> prefilter = null;
            return new Models.DataTables.DataTablesActionResult<EventsFullListRowViewModel>(source, input, prefilter);
        }

        //public ActionResult Edit(int Id, int? type_ID, int? subType_ID, bool inMailingListOnly)
        //{
        //    Person person = PersonOperations.GetPersonById(Id);
        //    if (person == null)
        //    {
        //        return RedirectToAction("Index", new { Type_ID = type_ID, SubType_ID = subType_ID, InMailingListOnly = inMailingListOnly });
        //    }
        //    PersonEditModel model = PersonOperations.GetPersonEditModel(person);
        //    model.filter = new PersonEventsViewModel
        //    {
        //        Type_ID = type_ID,
        //        SubType_ID = subType_ID,
        //        InMailingListOnly = inMailingListOnly
        //    };
        //    return View(model);
        //}


        //[HttpPost]
        //public ActionResult Edit(PersonEditModel model)
        //{
        //    try
        //    {

        //        PersonOperations.CreateUpdate(model.details, true);
        //        return RedirectToAction("Index", new { Type_ID = model.filter.Type_ID, SubType_ID = model.filter.SubType_ID, InMailingListOnly = model.filter.InMailingListOnly });
        //    }
        //    catch (Exception ex)
        //    {
        //        ModelState.AddModelError("", ex.Message);
        //        return View(model);
        //    }
        //}


        //public ActionResult Create(int? type_ID, int? subType_ID, bool inMailingListOnly)
        //{
        //    var model = new PersonCreateModel();

        //    model.filter = new PersonEventsViewModel
        //    {
        //        Type_ID = type_ID,
        //        SubType_ID = subType_ID,
        //        InMailingListOnly = inMailingListOnly
        //    };
        //    return View(model);
        //}

        //[HttpPost]
        //public ActionResult Create(PersonCreateModel model)
        //{
        //    try
        //    {

        //        PersonOperations.CreateUpdate(model.details, false);
        //        return RedirectToAction("Index", new { Type_ID = model.filter.Type_ID, SubType_ID = model.filter.SubType_ID, InMailingListOnly = model.filter.InMailingListOnly });
        //    }
        //    catch (Exception ex)
        //    {
        //        ModelState.AddModelError("", ex.Message);
        //        return View(model);
        //    }
        //}



        //public ActionResult Export(int? Type_ID, int? SubType_ID, bool? InMailingListOnly)
        //{
        //    var source = PersonOperations.GetExportRows(Type_ID, SubType_ID, InMailingListOnly);
        //    return this.Excel("אנשים", "Persons", source);
        //}


        //[HttpPost]
        //public JsonResult Delete(int Id)
        //{
        //    var data = PersonOperations.Delete(Id);
        //    if (data != "המחיקה בוצעה בהצלחה")
        //    {
        //        ModelState.AddModelError("", data);
        //    }
        //    return Json(data, JsonRequestBehavior.AllowGet);


        //}

    }
}
