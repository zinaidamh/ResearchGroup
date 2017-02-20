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
    
    public class EventSubTypesController : ControllerBase
    {

        private DbEventSubTypes EventSubTypeOperations;
        public EventSubTypesController()
        {
            dbOperation = new DbEventSubTypes();
            EventSubTypeOperations = (DbEventSubTypes)dbOperation;
        }

        public ActionResult Index(int? filterId)
        {
            var model = new EventSubTypesViewModel();
            if (filterId.HasValue)
            {
                model.ID = filterId.Value;
            }
            return View(model);
        }

        public ActionResult IndexData(Models.DataTables.jqDataTableInput input, int? filterId)
        {
            var data = EventSubTypeOperations.GetData();

            if (filterId.HasValue)
            {
                data = data.Where(f => f.ID == filterId.Value);
            }

            var source = EventSubTypeOperations.GetRows(data);
            System.Linq.Expressions.Expression<Func<EventSubTypesRowViewModel, bool>> prefilter = null;
            return new Models.DataTables.DataTablesActionResult<EventSubTypesRowViewModel>(source, input, prefilter);
        }


        public JsonResult ChangeData(int? Id, string SubType_Name, int? Type_ID, int? EssenseOrder, bool isUpdate)
        {
            if (string.IsNullOrEmpty(SubType_Name))
            {
                var data = new JsonObject { result = false };
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            if (Type_ID==null)
            {

                var data = new JsonObject { result = false };
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            if (EventSubTypeOperations.CreateUpdate(Id, SubType_Name, Type_ID, EssenseOrder, isUpdate))
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
            if (EventSubTypeOperations.Delete(Id))
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




