using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Hypnosis.Web.Data;
using Hypnosis.Web.Models;
using Hypnosis.Web.Data.DbOperations;

namespace Hypnosis.Web.Controllers
{
    
    public class EventTypesController : ControllerBase
    {

        private DbEventTypes EventTypeOperations;
        public EventTypesController()
        {
            dbOperation = new DbEventTypes();
            EventTypeOperations = (DbEventTypes)dbOperation;
        }

        public ActionResult Index(int? filterId)
        {
            var model = new EventTypesViewModel();
            if (filterId.HasValue)
            {
                model.ID = filterId.Value;
            }
            return View(model);
        }

        public ActionResult IndexData(Models.DataTables.jqDataTableInput input, int? filterId)
        {
            var data = EventTypeOperations.GetData();

            if (filterId.HasValue)
            {
                data = data.Where(f => f.ID == filterId.Value);
            }

            var source = EventTypeOperations.GetRows(data);
            System.Linq.Expressions.Expression<Func<EventTypesRowViewModel, bool>> prefilter = null;
            return new Models.DataTables.DataTablesActionResult<EventTypesRowViewModel>(source, input, prefilter);
        }


        public JsonResult ChangeData(int? Id, string Type_Name, int? Type_Category, bool isUpdate)
        {
            if (string.IsNullOrEmpty(Type_Name))
            {
                var data = new JsonObject { result = false };
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            if (Type_Category==null)
            {

                var data = new JsonObject { result = false };
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            if (EventTypeOperations.CreateUpdate(Id, Type_Name, (int)Type_Category, isUpdate))
            {
                var data = new JsonObject { result = true };
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var data = new JsonObject { result = false };
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult Delete(int Id)
        {
            if (EventTypeOperations.Delete(Id))
            {
                var data = new JsonObject { result = true };
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var data = new JsonObject { result = false };
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }


    }
}




