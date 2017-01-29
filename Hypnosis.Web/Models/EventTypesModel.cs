using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hypnosis.Web.Models
{
    public class EventTypesRowViewModel
    {
        [ScaffoldColumn(false)]
        public int ID { get; set; }
      
        [Display(Name = "שם סוג אירוע")]
        public string Type_Name { get; set; }

        [Display(Name = "קטגוריית סוג אירוע")]
        public int Type_Category { get; set; }

        [Display(Name = "קטגוריית סוג אירוע")]
        public string Type_Category_Name { get; set; }
    }


    public class EventTypesViewModel
    {
        [ScaffoldColumn(false)]
        [Display(Name = "שם סוג אירוע")]
        public int? ID { get; set; }

           
    }

}