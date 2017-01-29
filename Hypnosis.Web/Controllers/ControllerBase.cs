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
    [AuthorizeAttribute]
    public class ControllerBase : System.Web.Mvc.Controller
    {
        protected OperationBase dbOperation;

        
        protected override void Dispose(bool disposing)
        {
            if (dbOperation != null)
            {
                dbOperation.Dispose(disposing);
            }
            base.Dispose(disposing);
        }

       
    }
}