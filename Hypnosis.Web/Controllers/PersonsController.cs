﻿using System;
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
    public class PersonsController : ControllerBase
    {
        //
        // GET: /Persons/
        private DbPerson PersonOperations;
        public PersonsController()
        {
            dbOperation = new DbPerson();
            PersonOperations = (DbPerson)dbOperation;
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
            var source = PersonOperations.GetRows(Type_ID, SubType_ID, InMailingListOnly);
            System.Linq.Expressions.Expression<Func<PersonEventsListRowViewModel, bool>> prefilter = null;
            return new Models.DataTables.DataTablesActionResult<PersonEventsListRowViewModel>(source, input, prefilter);
        }





        public ActionResult Export(int? Type_ID, int? SubType_ID, bool? InMailingListOnly)
        {
            var source = PersonOperations.GetExportRows(Type_ID, SubType_ID, InMailingListOnly);
            return this.Excel("אנשים", "Persons", source);
        }


        [HttpPost]
        public JsonResult Delete(int Id)
        {
            var msg = PersonOperations.Delete(Id);
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
            Person card = PersonOperations.GetPersonById(Id);
            if (card == null)
            {
                return RedirectToAction("Index", new { Type_ID = type_ID, SubType_ID = subType_ID, InMailingListOnly = inMailingListOnly });
            }
            PersonEditModel model = PersonOperations.GetPersonEditModel(card);
            model.filter = new EventsFilterViewModel_ForList
            {
                Type_ID = type_ID,
                SubType_ID = subType_ID,
                InMailingListOnly = inMailingListOnly
            };
            model.eventsFilter = new EventsFilterViewModel_ForCard()
            {
                Category_ID = 2,
                Card_ID = Id
            };

            return View(model);
        }


        [HttpPost]
        public ActionResult Edit(PersonEditModel model)
        {
            try
            {

                PersonOperations.CreateUpdate(model.details, true);
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
            var model = new PersonCreateModel();

            model.filter = new EventsFilterViewModel_ForList
            {
                Type_ID = type_ID,
                SubType_ID = subType_ID,
                InMailingListOnly = inMailingListOnly
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(PersonCreateModel model)
        {
            try
            {

                PersonOperations.CreateUpdate(model.details, false);
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
