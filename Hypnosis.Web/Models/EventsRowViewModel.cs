using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hypnosis.Web.Models
{
    public class EventsRowViewModel
    {
        [Display(Name = "תאריך ארוע")]
        public DateTime? FirstDate { get; set; }

        [Display(Name = " תת סוג ארוע ")]
        public string SubType_Name { get; set; }

        [Display(Name = "  סוג ארוע ")]
        public string Type_Name { get; set; }

        [Display(Name = " תת סוג ארוע ")]
        public int SubType_ID { get; set; }

        [Display(Name = "  סוג ארוע ")]
        public int Type_ID { get; set; }

        [Display(Name = " תאור ארוע ")]
        public string Description { get; set; }

        [Display(Name = " גורם / אמצאי הפנייה ")]
        public string Agent_Name { get; set; }

        [Display(Name = "תאריך תוקף")]
        public DateTime? ExpirationDate { get; set; }

        [Display(Name = "תאריך הקפצת התראה")]
        public DateTime? AlertDate { get; set; }

        [Display(Name = "התראה טופלה")]
        public bool AlertDone { get; set; }

        [Display(Name = "התראה טופלה")]
        public bool AlertDoneString { get; set; }

        [Display(Name = "מכון")]
        public bool InstituteName { get; set; }

        [Display(Name = "קופץ")]
        public bool FileName { get; set; }


        [Display(Name = "קופץ")]
        public bool FileHRef { get; set; }

        [Display(Name = "קישור")]
        public bool SiteName { get; set; }

        [Display(Name = "תאריך הזנה")]
        public DateTime? CreatedAt { get; set; }


    }


}