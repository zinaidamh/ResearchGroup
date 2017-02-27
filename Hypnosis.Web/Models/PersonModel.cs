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
        public string LastName { get; set; }

        [Display(Name = "שם פרטי")]
        public string FirstName { get; set; }

        [Display(Name = "תאריך ארוע אחרון")]
        public DateTime? FirstDate { get; set; }

        [Display(Name = " תת סוג ארוע אחרון")]
        public string SubType_Name { get; set; }


        [Display(Name = " תאור ארוע אחרון")]
        public string Description { get; set; }



        [Display(Name = "תאריך ארוע אחרון בתמצית ")]
        public DateTime? FirstDate_Essense { get; set; }

        [Display(Name = "תת סוג ארוע אחרון בתמצית")]
        public string SubType_Name_Essense { get; set; }




        [Display(Name = "סוג ארוע אחרון בתמצית")]
        public string Type_Name_Essense { get; set; }


        [Display(Name = "תאור ארוע אחרון בתמצית")]
        public string Description_Essense { get; set; }

        [Display(Name = "סוג ארוע אחרון בתמצית")]
        public int? Type_ID_Essense { get; set; }

        [Display(Name = "תת סוג ארוע אחרון בתמצית")]
        public int? SubType_ID_Essense { get; set; }

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
        [Display(Name = "? ברשימת תפוצה")]

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
        public string LastName { get; set; }

        [Display(Name = "שם פרטי")]
        public string FirstName { get; set; }

        [Display(Name = "תאריך ארוע אחרון")]
        public string FirstDate { get; set; }

        [Display(Name = " תת סוג ארוע אחרון")]
        public string SubType_Name { get; set; }


        [Display(Name = " תאור ארוע אחרון")]
        public string Description { get; set; }


        [Display(Name = "תאריך ארוע אחרון בתמצית ")]
        public string FirstDate_Essense { get; set; }

        [Display(Name = "תת סוג ארוע אחרון בתמצית")]
        public string SubType_Name_Essense { get; set; }


        [Display(Name = "תאור ארוע אחרון בתמצית")]
        public string Description_Essense { get; set; }


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
      
        public string Type_Name { get; set; }
    }


   


    public class PersonCreateModel
    {
        public PersonDetailsModel details { get; set; } //to use the same template for edit/create
        public EventsFilterViewModel_ForList filter { get; set; }
    }


    public class PersonEditModel
    {
        public EventsFilterViewModel_ForList filter { get; set; }

        public PersonDetailsModel details { get; set; }
        public EventsFilterViewModel_ForCard eventsFilter { get; set; }
        public PersonEventsListRowViewModel eventsList { get; set; }
    }


    public class PersonDetailsModel
    {
        [ScaffoldColumn(false)]
        public int ID { get; set; }


        [Display(Name = "ת.ז")]
        public string TZ { get; set; }


        [Display(Name = "שם משפחה")]
        [Required(ErrorMessage = "שם משפחה חובה")]
        public string LastName { get; set; }

        [Display(Name = "שם פרטי")]
        [Required(ErrorMessage = "שם פרטי חובה")]
        public string FirstName { get; set; }

        [Display(Name = "שם לתצוגה")]
        [Required(ErrorMessage = "שם לתצוגה חובה")]
        public string DisplayName { get; set; }

        [Display(Name = "טלפון נייד ")]
        public string Mobile { get; set; }

        [Display(Name = "טלפונים נוספים")]
        public string Phones { get; set; }


        [Display(Name = "דוא\"ל")]
        [RegularExpression(@"^[\w-]+(\.[\w-]+)*@([a-z0-9-]+(\.[a-z0-9-]+)*?\.[a-z]{2,6}|(\d{1,3}\.){3}\d{1,3})(:\d{4})?$", ErrorMessage = "דוא\"ל לא נכון")]

        public string Email { get; set; }

        [Display(Name = "ברשימת תפוצה ? ")]
        public string InMailingListString { get; set; }

        [Display(Name = "מס רשיון")]
        public string Psyhology_LicenseNumber { get; set; }


        [Display(Name = "מס רשיון")]
        public string Medicine_LicenseNumber { get; set; }

        [Display(Name = "מס רשיון")]
        public string Stomatology_LicenseNumber { get; set; }


        [Display(Name = "מומחיות")]
        public string Psyhology_Specialization { get; set; }
        
        [Display(Name = "מומחיות")]
        public string Medicine_Specialization { get; set; }

        [Display(Name = "מומחיות")]
        public string Stomatology_Specialization { get; set; }

        [Display(Name = "הערות כלליות")]
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

        public bool InMailingList { get; set; }

        [Display(Name = "סוג ארוע")]
        public string Type_Name { get; set; }


        [Display(Name = "בכיר")]
        public bool Person_Senior { get; set; }


        [Display(Name = "תואר")]
        public string Degree { get; set; }

         [Display(Name = "מספר תיק במשרד הבריאות ")]
        public string Ministry_CaseNumber { get; set; }


    }
   

  

}

