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
            var model = new InstituteEventsViewModel();

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
            var personEvents = InstituteOperations.GetRows();


            if (Type_ID.HasValue)
            {
                personEvents = personEvents.Where(f => f.Type_ID == Type_ID);
            }
            if (SubType_ID.HasValue)
            {
                personEvents = personEvents.Where(f => f.SubType_ID == SubType_ID);
            }
            if (InMailingListOnly == true)
            {
                personEvents = personEvents.Where(f => f.InMailingList == InMailingListOnly);
            }

            var source = personEvents;
            System.Linq.Expressions.Expression<Func<InstituteEventsListRowViewModel, bool>> prefilter = null;
            return new Models.DataTables.DataTablesActionResult<InstituteEventsListRowViewModel>(source, input, prefilter);
        }

        [HttpPost]
        public ActionResult EventsDataByCard(Models.DataTables.jqDataTableInput input, int Card_ID, int? Type_ID, int? SubType_ID, int? Category_ID, DateTime? fromDate, DateTime? toDate, bool EssenseOnly, bool ExpiredOnly, bool OpenOnly, bool FileOnly, bool SiteOnly)
        {
            DbEvents EventsOperations = new DbEvents();
            var source = EventsOperations.GetEventsByFilter(Card_ID, null, Type_ID, SubType_ID, Category_ID, fromDate, toDate, EssenseOnly, ExpiredOnly, OpenOnly, FileOnly, SiteOnly);
            System.Linq.Expressions.Expression<Func<EventsFullListRowViewModel, bool>> prefilter = null;
            return new Models.DataTables.DataTablesActionResult<EventsFullListRowViewModel>(source, input, prefilter);

        }

       
        //public ActionResult IndexData(Models.DataTables.jqDataTableInput input, int? Type_ID, int? SubType_ID, bool InMailingListOnly)
        //{
        //    var personEvents = InstituteOperations.GetRows();


        //    if (Type_ID.HasValue)
        //    {
        //        personEvents = personEvents.Where(f => f.Type_ID == Type_ID);
        //    }
        //    if (SubType_ID.HasValue)
        //    {
        //        personEvents = personEvents.Where(f => f.SubType_ID == SubType_ID);
        //    }
        //    if (InMailingListOnly == true)
        //    {
        //        personEvents = personEvents.Where(f => f.InMailingList == InMailingListOnly);
        //    }

        //    var source = personEvents;
        //    System.Linq.Expressions.Expression<Func<PersonEventsListRowViewModel, bool>> prefilter = null;
        //    return new Models.DataTables.DataTablesActionResult<PersonEventsListRowViewModel>(source, input, prefilter);
        //}

       
        public ActionResult Export(int? Type_ID, int? SubType_ID, bool? InMailingListOnly)
        {
            var source = InstituteOperations.GetExportRows(Type_ID, SubType_ID, InMailingListOnly);
            return this.Excel("אנשים", "Institutes", source);
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
            Institute person = InstituteOperations.GetInstituteById(Id);
            if (person == null)
            {
                return RedirectToAction("Index", new { Type_ID = type_ID, SubType_ID = subType_ID, InMailingListOnly = inMailingListOnly });
            }
            InstituteEditModel model = InstituteOperations.GetInstituteEditModel(person);
            model.filter = new InstituteEventsViewModel
            {
                Type_ID = type_ID,
                SubType_ID = subType_ID,
                InMailingListOnly = inMailingListOnly
            };
            model.eventsFilter = new EventsFilterViewModel()
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


        public ActionResult Create(int? type_ID, int? subType_ID, bool inMailingListOnly)
        {
            var model = new InstituteCreateModel();

            model.filter = new InstituteEventsViewModel
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
