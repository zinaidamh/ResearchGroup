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
    public class GalleryController : Controller
    {
        //
        // GET: /AdminHome/
       

        public ActionResult Persons()
        {

            DbPerson PersonOperations;
            OperationBase dbOperation = new DbPerson();
            PersonOperations = (DbPerson)dbOperation;
            var model = PersonOperations.GetPersons();
            return View(model);


        }

        public ActionResult PersonResume(int Id)
        {
            DbPerson PersonOperations;
            OperationBase dbOperation = new DbPerson();
            PersonOperations = (DbPerson)dbOperation;
            var model = PersonOperations.GetPersonDetailsById(Id);
            return View(model);

        }
    }
}