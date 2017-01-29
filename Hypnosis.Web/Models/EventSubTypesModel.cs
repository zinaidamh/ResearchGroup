using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hypnosis.Web.Models
{
    public class EventSubTypesRowViewModel
    {
        [ScaffoldColumn(false)]
        public int ID { get; set; }
      
        [Display(Name = "שם תת סוג אירוע")]
        public string SubType_Name { get; set; }

        [Display(Name = " סוג אירוע")]
        public int Type_ID { get; set; }

        [Display(Name = " סוג אירוע")]
        public string Type_Name { get; set; }
    }


    public class EventSubTypesViewModel
    {
        [ScaffoldColumn(false)]
        [Display(Name = "שם  תת  סוג אירוע")]
        public int? ID { get; set; }

           
    }

}