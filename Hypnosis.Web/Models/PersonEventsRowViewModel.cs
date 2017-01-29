using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
namespace Hypnosis.Web.Models
{
    public class PersonEventsListRowViewModel
    {
        [ScaffoldColumn(false)]
        public int ID { get; set; }


        [Display(Name = "ת.ז")]
        public string TZ { get; set; }


        [Display(Name = "שם משפחה")]
        public string FamilyName { get; set; }

        [Display(Name = "שם פרטי")]
        public string FirstName { get; set; }

        [Display(Name = "תאריך ארוע אחרון")]
        public DateTime? FirstDate { get; set; }

        [Display(Name = " תת סוג ארוע אחרון")]
        public string SubType_Name { get; set; }


        [Display(Name = " תאור ארוע אחרון")]
        public string Description { get; set; }


        [Display(Name = "תאריך ארוע אחרון בתמצית ")]
        public DateTime? FirstDate_Short { get; set; }

        [Display(Name = "תת סוג ארוע אחרון בתמצית")]
        public string SubType_Name_Short { get; set; }


        [Display(Name = "תאור ארוע אחרון בתמצית")]
        public string Description_Short { get; set; }


        [Display(Name = "טלפון נייד ")]
        public string Mobile { get; set; }

        [Display(Name = "טלפונים נוספים")]
        public string Phones { get; set; }


        [Display(Name = "דוא\"ל")]
        public string Email { get; set; }

        [Display(Name = "ברשימת תפוצה ? ")]
        public string InMailingListString { get; set; }

        [Display(Name = "מס רשיון פסיכולוגיה")]
        public string Psyhology_LicenseNumber { get; set; }


        [Display(Name = "מס רשיון רפואה")]
        public string Medicine_LicenseNumber { get; set; }

        [Display(Name = "מספר רשיון רפואת שיניים")]
        public string Stomatology_LicenseNumber { get; set; }


        [Display(Name = "הערות אדם")]
        public string Person_Comments { get; set; }

        [Display(Name = "תאריך לידה")]
        public DateTime? BirthDate { get; set; }

        [Display(Name = "רחוב ומספר")]
        public string Address { get; set; }

        [Display(Name = "ישוב")]
        public string City { get; set; }

        [Display(Name = "מיקןד")]
        public string ZipCode { get; set; }

        [Display(Name = "הערות כתובת")]
        public string Address_Comments { get; set; }
        [Display(Name = "סוג ארוע")]
        public int? Type_ID { get; set; }
        [Display(Name = "תת סוג ארוע")]
       
          
     
        public int? SubType_ID { get; set; }
        [Display(Name = "רק ברשימת תפוצה")]

        public bool? InMailingList { get; set; }

        [Display(Name = "סוג ארוע")]
        public string Type_Name { get; set; }
    }


    public class PersonEventsListRowExportModel
    {
        [ScaffoldColumn(false)]
        public int ID { get; set; }


        [Display(Name = "ת.ז")]
        public string TZ { get; set; }


        [Display(Name = "שם משפחה")]
        public string FamilyName { get; set; }

        [Display(Name = "שם פרטי")]
        public string FirstName { get; set; }

        [Display(Name = "תאריך ארוע אחרון")]
        public string FirstDate { get; set; }

        [Display(Name = " תת סוג ארוע אחרון")]
        public string SubType_Name { get; set; }


        [Display(Name = " תאור ארוע אחרון")]
        public string Description { get; set; }


        [Display(Name = "תאריך ארוע אחרון בתמצית ")]
        public string FirstDate_Short { get; set; }

        [Display(Name = "תת סוג ארוע אחרון בתמצית")]
        public string SubType_Name_Short { get; set; }


        [Display(Name = "תאור ארוע אחרון בתמצית")]
        public string Description_Short { get; set; }


        [Display(Name = "טלפון נייד ")]
        public string Mobile { get; set; }

        [Display(Name = "טלפונים נוספים")]
        public string Phones { get; set; }


        [Display(Name = "דוא\"ל")]
        public string Email { get; set; }

        [Display(Name = "ברשימת תפוצה ? ")]
        public string InMailingListString { get; set; }

        [Display(Name = "מס רשיון פסיכולוגיה")]
        public string Psyhology_LicenseNumber { get; set; }


        [Display(Name = "מס רשיון רפואה")]
        public string Medicine_LicenseNumber { get; set; }

        [Display(Name = "מספר רשיון רפואת שיניים")]
        public string Stomatology_LicenseNumber { get; set; }


        [Display(Name = "הערות אדם")]
        public string Person_Comments { get; set; }

        [Display(Name = "תאריך לידה")]
        public string BirthDate { get; set; }

        [Display(Name = "רחוב ומספר")]
        public string Address { get; set; }

        [Display(Name = "ישוב")]
        public string City { get; set; }

        [Display(Name = "מיקןד")]
        public string ZipCode { get; set; }

        [Display(Name = "הערות כתובת")]
        public string Address_Comments { get; set; }
        [Display(Name = "סוג ארוע")]
        //public int? Type_ID { get; set; }
        //[Display(Name = "תת סוג ארוע")]



        //public int? SubType_ID { get; set; }
        //[Display(Name = "רק ברשימת תפוצה")]

        //public bool? InMailingList { get; set; }

        //[Display(Name = "סוג ארוע")]
        public string Type_Name { get; set; }
    }


    public class PersonEventsViewModel
    {
        [Display(Name = "סוג ארוע")]
        public int? Type_ID { get; set; }
        [Display(Name = "תת סוג ארוע")]
        public int? SubType_ID { get; set; }
        [Display(Name = "רק ברשימת תפוצה")]
        public bool InMailingListOnly { get; set; }
    }

}