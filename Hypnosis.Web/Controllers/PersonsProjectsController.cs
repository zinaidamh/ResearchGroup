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
    public class PersonsProjectsController : ControllerBase
    {
        //
        // GET: /PersonsProjects/

        private DbPersonsProjects ppOperations;
        public PersonsProjectsController()
        {
            dbOperation = new DbPersonsProjects();
            ppOperations = (DbPersonsProjects)dbOperation;
        }

        public ActionResult Index()
        {
            return View();
            
        }

        public JsonResult SaveOrder(string Id, int projectId)
        {
            int[] intArray = Id.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
            ppOperations.SaveOrder(intArray);
            var data = new JsonObject { result = true };
            return Json(data, JsonRequestBehavior.AllowGet);
        }


            public JsonResult ChangeData(int? Id,  int? Person_ID, int? Project_ID, int? PersonOrder, bool isUpdate)
        {
            if (Person_ID == null)
            {
                var data = new JsonObject { result = false };
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            if (Project_ID == null)
            {

                var data = new JsonObject { result = false };
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            string msg = ppOperations.CreateUpdate(Id, Person_ID, Project_ID, PersonOrder, isUpdate);
            if (msg=="")
            {
                var data = new JsonObject { result = true };
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var data = new JsonObject { result = false, error = msg };
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult DataByProjectCard(Models.DataTables.jqDataTableInput input, int? Project_ID)
        {
            var source = ppOperations.GetDataByProjectID(Project_ID);
            System.Linq.Expressions.Expression<Func<PersonProjectViewModel, bool>> prefilter = null;
            return new Models.DataTables.DataTablesActionResult<PersonProjectViewModel>(source, input, prefilter);

        }

        public ActionResult DataByPersonCard(Models.DataTables.jqDataTableInput input, int? Person_ID)
        {
            var source = ppOperations.GetDataByPersonID(Person_ID);
            System.Linq.Expressions.Expression<Func<PersonProjectViewModel, bool>> prefilter = null;
            return new Models.DataTables.DataTablesActionResult<PersonProjectViewModel>(source, input, prefilter);

        }

        public JsonResult Delete(int Id)
        {
            if (ppOperations.Delete(Id))
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
