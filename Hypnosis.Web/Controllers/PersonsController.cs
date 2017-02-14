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
            var model = new PersonEventsViewModel();
          
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
        public ActionResult IndexDataByPerson(Models.DataTables.jqDataTableInput input, int Person_ID, int? Type_ID, int? SubType_ID, int? Category_ID, DateTime? fromDate, DateTime? toDate, bool EssenseOnly, bool ExpiredOnly, bool OpenOnly, bool FileOnly, bool SiteOnly)
        {
            DbEvents EventsOperations = new DbEvents();
            var source = EventsOperations.GetEventsByFilter(Person_ID, null, Type_ID, SubType_ID, Category_ID, fromDate, toDate, EssenseOnly, ExpiredOnly, OpenOnly, FileOnly, SiteOnly);
            System.Linq.Expressions.Expression<Func<EventsFullListRowViewModel, bool>> prefilter = null;
            return new Models.DataTables.DataTablesActionResult<EventsFullListRowViewModel>(source, input, prefilter);

        }

        [HttpPost]
        public ActionResult IndexData(Models.DataTables.jqDataTableInput input, int? Type_ID, int? SubType_ID, bool InMailingListOnly)
        {
            var personEvents = PersonOperations.GetRows();

          
            if (Type_ID.HasValue)
            {
                personEvents = personEvents.Where(f => f.Type_ID == Type_ID);
            }
            if (SubType_ID.HasValue)
            {
                personEvents = personEvents.Where(f => f.SubType_ID == SubType_ID);
            }
            if (InMailingListOnly==true)
            {
                personEvents = personEvents.Where(f => f.InMailingList == InMailingListOnly);
            }

            var source = personEvents;
            System.Linq.Expressions.Expression<Func<PersonEventsListRowViewModel, bool>> prefilter = null;
            return new Models.DataTables.DataTablesActionResult<PersonEventsListRowViewModel>(source, input, prefilter);
        }

        
        public JsonResult ChangeEventsData()
        {
            int? Id=null; bool isUpdate=false; int? SubType_ID=null; int? Type_ID=null; int? Agent_ID=null; int? Institute_ID=null;
            DateTime? FirstDate=null, AlertDate=null, ExpirationDate=null; string SiteHref=""; bool AlertDone=false;
            int? Person_ID = null;
            try
            {
              
                if (Request["Id"]!=null)
                {
                Id=Int32.Parse(Request["Id"]);
                }
                if (Request["SubType_ID"] != null)
                {
                    SubType_ID = Int32.Parse(Request["SubType_ID"]);
                }
                if (Request["Type_ID"] != null)
                {
                    Type_ID = Int32.Parse(Request["Type_ID"]);
                }
                if (Request["Agent_ID"]!=null)
                {
                Agent_ID = Int32.Parse(Request["Agent_ID"]);
                }
                if (Request["Institute_ID"] != null)
                {
                    Institute_ID = Int32.Parse(Request["Institute_ID"]);
                }
                if (Request["isUpdate"] != null)
                {
                    isUpdate = bool.Parse(Request["isUpdate"]);
                }
                if (Request["FirstDate"] != null)
                {
                    FirstDate = DateTime.Parse(Request["FirstDate"]);
                }
                if (Request["AlertDate"] != null)
                {
                    AlertDate = DateTime.Parse(Request["AlertDate"]);
                }
                if (Request["ExpirationDate"] != null)
                {
                    ExpirationDate = DateTime.Parse(Request["ExpirationDate"]);
                }
                if (Request["SiteHref"] != null)
                {
                    SiteHref = Request["SiteHref"].ToString();
                }
                if (Request["AlertDone"] != null)
                {
                    AlertDone = bool.Parse(Request["AlertDone"]);
                }
                if (Request["Person_ID"] != null)
                {
                    Person_ID = Int32.Parse(Request["Person_ID"]);
                }
            }
            catch
            {
                {
                var data = new JsonObject { result = false };
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            }
            DbEvents EventsOperations = new DbEvents();
             if (SubType_ID == null)
            {
                var data = new JsonObject { result = false };
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            if (Type_ID == null)
            {

                var data = new JsonObject { result = false };
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            var files = this.Request.Files;
            string FileName = "";
            
            
                if (files.Count > 0)
                {
                    var file0 = files[0];
                    var relativePath = FilesHelper.RelativePath;
                    var path = Server.MapPath(relativePath);
                    try
                    {
                        file0.SaveAs(System.IO.Path.Combine(path, file0.FileName));
                        FileName = file0.FileName;
                        
                    }
                    catch
                    {
                        var data = new JsonObject { result = false };
                        return Json(data, JsonRequestBehavior.AllowGet);
                    }
                }
                if (EventsOperations.CreateUpdate(Id, Person_ID, SubType_ID, Type_ID, Agent_ID, Institute_ID, FileName,FirstDate, ExpirationDate, AlertDate, AlertDone, SiteHref, isUpdate))
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

        // [HttpPost]
        //public JsonResult ChangeEventsData()
        //{
        //    var files = this.Request.Files;
        //     if (files.Count>0)
        //     {
        //         var file0 = files[0];
        //         var relativePath=System.Configuration.ConfigurationManager.AppSettings["relativePath"];
        //         var path = Server.MapPath(relativePath);
        //         file0.SaveAs(System.IO.Path.Combine(path, file0.FileName));
        //     }
           
        //    var data = new JsonObject { result = false };
        //    return Json(data, JsonRequestBehavior.AllowGet);

        //}


        public ActionResult Edit(int Id, int? type_ID, int? subType_ID, bool inMailingListOnly)
        {
            Person person = PersonOperations.GetPersonById(Id);
            if (person == null)
            {
                return RedirectToAction("Index", new { Type_ID = type_ID, SubType_ID = subType_ID, InMailingListOnly = inMailingListOnly });
            }
            PersonEditModel model = PersonOperations.GetPersonEditModel(person);
            model.filter = new PersonEventsViewModel
            {
                Type_ID = type_ID,
                SubType_ID = subType_ID,
                InMailingListOnly = inMailingListOnly
            };
            return View(model);
        }


        [HttpPost]
        public ActionResult Edit(PersonEditModel model)
        {
            try
            {
               
                PersonOperations.CreateUpdate(model.details,true);
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

            model.filter = new PersonEventsViewModel
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



        public ActionResult Export(int? Type_ID, int? SubType_ID, bool? InMailingListOnly)
        {
            var source = PersonOperations.GetExportRows(Type_ID,  SubType_ID,InMailingListOnly);
            return this.Excel("אנשים", "Persons", source);
        }


        [HttpPost]
        public JsonResult Delete(int Id)
        {
            var data=PersonOperations.Delete(Id);
            if (data != "המחיקה בוצעה בהצלחה")
            {
                ModelState.AddModelError("", data);
            }
            return Json(data, JsonRequestBehavior.AllowGet);
            
         
        }

    }
}
