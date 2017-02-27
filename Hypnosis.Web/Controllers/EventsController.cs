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
            var model = new EventsFilterViewModel_ForCard();
            return View(model);
        }

       

        [HttpPost]
        public ActionResult IndexData(Models.DataTables.jqDataTableInput input, int? Type_ID, int? SubType_ID, int? Category_ID, DateTime? fromDate, DateTime? toDate, bool EssenseOnly, bool ExpiredOnly, bool OpenOnly, bool FileOnly, bool SiteOnly)
        {
            var source = EventsOperations.GetEventsByFilter(null,Type_ID, SubType_ID, Category_ID, fromDate, toDate, EssenseOnly, ExpiredOnly, OpenOnly, FileOnly, SiteOnly);
            System.Linq.Expressions.Expression<Func<EventsFullListRowViewModel, bool>> prefilter = null;
            return new Models.DataTables.DataTablesActionResult<EventsFullListRowViewModel>(source, input, prefilter);
        }

        [HttpPost]
        public ActionResult DataByCard(Models.DataTables.jqDataTableInput input, int? Card_ID, int? Type_ID, int? SubType_ID, int? Category_ID, DateTime? fromDate, DateTime? toDate, bool EssenseOnly, bool ExpiredOnly, bool OpenOnly, bool FileOnly, bool SiteOnly)
        {
            DbEvents EventsOperations = new DbEvents();
           
        
            
            var source = EventsOperations.GetEventsByFilter( Card_ID, Type_ID, SubType_ID, Category_ID, fromDate, toDate, EssenseOnly, ExpiredOnly, OpenOnly, FileOnly, SiteOnly);
            System.Linq.Expressions.Expression<Func<EventsFullListRowViewModel, bool>> prefilter = null;
            return new Models.DataTables.DataTablesActionResult<EventsFullListRowViewModel>(source, input, prefilter);

        }

        [HttpPost]
        public JsonResult ChangeData()
        {
            DbEvents EventsOperations = new DbEvents();
            var relativePath = FilesHelper.RelativePath;
            var path = Server.MapPath(relativePath);
            bool operationResult = EventsOperations.ChangeData(this.Request, path);


            var data = new JsonObject { result = operationResult };
            return Json(data, JsonRequestBehavior.AllowGet);


        }



        public ActionResult Export(int? Card_ID, int? Type_ID, int? SubType_ID, int? Category_ID, DateTime? fromDate, DateTime? toDate, bool EssenseOnly, bool ExpiredOnly, bool OpenOnly, bool FileOnly, bool SiteOnly)
        {
            var source = EventsOperations.GetExportRows(Card_ID, Type_ID, SubType_ID, Category_ID, fromDate, toDate, EssenseOnly, ExpiredOnly, OpenOnly, FileOnly, SiteOnly);
            return this.Excel("ארועים", "Events", source);
        }



        public JsonResult Delete(int Id)
        {
            var msg = EventsOperations.Delete(Id);
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

    }
}
