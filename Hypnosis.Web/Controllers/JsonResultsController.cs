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
    public class JsonResultsController : ControllerBase
    {
        //
        // GET: /JsonResults/

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult EventTypesInitJson(int? value)
        {
            dbOperation = new DbEventTypes();

            var data = ((DbEventTypes)dbOperation).EventTypesInit(value);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetEventTypesJson(string q, int page, int page_limit)
        {
            dbOperation = new DbEventTypes();
            var data =  ((DbEventTypes)dbOperation).getEventTypes(q, 0, page, page_limit);
            return Json(data, JsonRequestBehavior.AllowGet);
        }


        public JsonResult GetEventTypesJson_ByCategory(string q, int? category_id, int page, int page_limit)
        {
            dbOperation = new DbEventTypes();
            var data = ((DbEventTypes)dbOperation).getEventTypes(q, category_id, page, page_limit);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult EventTypeCategoriesInitJson(int? value)
        {
            dbOperation = new DbEventTypes();

            var data = ((DbEventTypes)dbOperation).EventTypeCategoriesInit(value);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetEventTypeCategoriesJson(string q, int page, int page_limit)
        {
            dbOperation = new DbEventTypes();
            var data = ((DbEventTypes)dbOperation).getEventTypeCategories(q, page, page_limit);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

       

   public JsonResult EventSubTypesInitJson(int? value)
        {
            dbOperation = new DbEventSubTypes();

            var data = ((DbEventSubTypes)dbOperation).EventSubTypesInit(value);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetEventSubTypesJson(string q, int page, int page_limit)
        {
            dbOperation = new DbEventSubTypes();
            var data =  ((DbEventSubTypes)dbOperation).getEventSubTypes(q, null, 0, page, page_limit);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

//person

        public JsonResult EventSubTypesInitJson_Person(int? value)
        {
            dbOperation = new DbEventSubTypes();

            var data = ((DbEventSubTypes)dbOperation).EventSubTypesInit(value);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetEventSubTypesJson_Person(string q, int? typeId, int page, int page_limit)
        {
            dbOperation = new DbEventSubTypes();
            var data = ((DbEventSubTypes)dbOperation).getEventSubTypes(q, typeId, (int)EventCategory.PersonEvent, page, page_limit);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult EventTypesInitJson_Person(int? value)
        {
            dbOperation = new DbEventSubTypes();

            var data = ((DbEventSubTypes)dbOperation).EventSubTypesInit(value);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetEventTypesJson_Person(string q, int page, int page_limit)
        {
            dbOperation = new DbEventTypes();
            var data = ((DbEventTypes)dbOperation).getEventTypes(q,  (int)EventCategory.PersonEvent, page, page_limit);
            return Json(data, JsonRequestBehavior.AllowGet);
        }


//institute
        public JsonResult EventSubTypesInitJson_Institute(int? value)
        {
            dbOperation = new DbEventSubTypes();

            var data = ((DbEventSubTypes)dbOperation).EventSubTypesInit(value);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetEventSubTypesJson_Institute(string q, int? typeId, int page, int page_limit)
        {
            dbOperation = new DbEventSubTypes();
            var data = ((DbEventSubTypes)dbOperation).getEventSubTypes(q, typeId, (int)EventCategory.InstituteEvent, page, page_limit);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult EventTypesInitJson_Institute(int? value)
        {
            dbOperation = new DbEventSubTypes();

            var data = ((DbEventSubTypes)dbOperation).EventSubTypesInit(value);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetEventTypesJson_Institute(string q, int page, int page_limit)
        {
            dbOperation = new DbEventTypes();
            var data = ((DbEventTypes)dbOperation).getEventTypes(q, (int)EventCategory.InstituteEvent, page, page_limit);
            return Json(data, JsonRequestBehavior.AllowGet);
        }


    }
}
