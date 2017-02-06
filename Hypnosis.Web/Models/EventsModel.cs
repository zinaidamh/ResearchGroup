using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hypnosis.Web.Models
{
    public class EventsFilterViewModel
    {

        [Display(Name = "קטגוריה")]
        public int? Category_ID { get; set; }
        [Display(Name = "סוג ארוע")]
        public int? Type_ID { get; set; }
        [Display(Name = "תת סוג ארוע")]
        public int? SubType_ID { get; set; }

        [Display(Name = "תמצית בלבד")]
        public bool EssenceOnly { get; set; }

        [Display(Name = "מתאריך")]
        public DateTime? fromDate { get; set; }

        [Display(Name = "עד תאריך")]
        public DateTime? toDate { get; set; }

        [Display(Name = "רק פתוחות")]
        public bool OpenOnly { get; set; }

        [Display(Name = "רק פג תוקף הסתיים")]
        public bool ExpiredOnly { get; set; }

        [Display(Name = "רק קישורים")]
        public bool SiteOnly { get; set; }
           
        [Display(Name = "רק קבצים")]
        public bool FileOnly{ get; set; }

      
    }


    public class EventsFullListRowViewModel
    {

       


        [ScaffoldColumn(false)]
        public int ID { get; set; }


        [Display(Name = "תאריך תחילה")]
        public DateTime? FirstDate { get; set; }

        [Display(Name = "קטגוריה")]
        public string Category_Name { get; set; }
        [Display(Name = "סוג ארוע")]
        public string Type_Name { get; set; }
        [Display(Name = "תת סוג ארוע")]
        public string SubType_Name { get; set; }

        [Display(Name = "ת.ז")]
        public string TZ { get; set; }


        [Display(Name = "שם האדם")]
        public string  Person_Name { get; set; }

        [Display(Name = "שם המכון")]
        public string Institute_Name { get; set; }


        [Display(Name = "שם האדם")]
        public int? Person_ID { get; set; }

        [Display(Name = "שם המכון")]
        public int? Institute_ID { get; set; }

        [Display(Name = "תאור")]
        public string Description { get; set; }

        [Display(Name = "גורם / אמצאי הפניה")]
        public string  Agent_Name { get; set; }   

        [Display(Name = "גורם / אמצאי הפניה")]
        public int? Agent_ID { get; set; }   

        [Display(Name = "תאריך תוקף")]
        public DateTime? ExpirationDate { get; set; }

        [Display(Name = "תאריך התראה")]
        public DateTime? AlertDate { get; set; }

        [Display(Name = "התראה טופלה")]
        public string  alertDoneString { get; set; }

        [Display(Name = "התראה טופלה")]
        public bool AlertDone { get; set; }

        [Display(Name = "קובץ")]
        public string FileName { get; set; }

        [Display(Name = "קישור")]
        public string SiteHref { get; set; }

        [Display(Name = "תאריך הזנה")]
        public DateTime CreatedAt { get; set; }

        //filter

        [Display(Name = "קטגוריה")]
        public int? Category_ID { get; set; }
        [Display(Name = "סוג ארוע")]
        public int? Type_ID { get; set; }
        [Display(Name = "תת סוג ארוע")]
        public int? SubType_ID { get; set; }

       
    }

}