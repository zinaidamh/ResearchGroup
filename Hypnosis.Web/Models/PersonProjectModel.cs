using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
namespace Hypnosis.Web.Models
{

    public class PersonProjectModel
    {

        [ScaffoldColumn(false)]

        public int ID { get; set; }

        public int Person_ID { get; set; }

        public int Project_ID { get; set; }

        public int PersonOrder { get; set; }

     
    }

    public class PersonProjectViewModel
    {

        [ScaffoldColumn(false)]
        public int ID { get; set; }

        public int Person_ID { get; set; }

        public int Project_ID { get; set; }

        public int PersonOrder { get; set; }

        public string ProjectName { get; set; }

        public string PersonName { get; set; }

    }

}