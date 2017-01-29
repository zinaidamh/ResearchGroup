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

        //public ActionResult Edit(int Id)
        //{
        //    //var employee = dbContext.TblEmployees.SingleOrDefault(f => f.E_Id == Id);
        //    //if (employee == null)
        //    //{
        //    //    return RedirectToAction("Index", new { statusId = statusId, managerId = managerId, jobId = jobId });
        //    //}
        //    //var model = GetDetailsModel(employee);
        //    //model.Filter = new EmployeesViewModel
        //    //{
        //    //    StatusId = statusId,
        //    //    ManagerId = managerId,
        //    //    FilterJobId = jobId
        //    //};
        //   // return View(model);
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

    }
}
