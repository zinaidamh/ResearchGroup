using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
namespace Hypnosis.Web.Models
{
    public class InstituteEventsListRowViewModel
    {
        [ScaffoldColumn(false)]
        public int ID { get; set; }

       [Display(Name = "שם המכון")]
        public string Name { get; set; }

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

        [Display(Name = "סוג ארוע אחרון")]
        public string Type_Name { get; set; }

        [Display(Name = "טלפון ראשי ")]
        public string MainPhone { get; set; }

        [Display(Name = "טלפונים נוספים")]
        public string Phones { get; set; }


        [Display(Name = "דוא\"ל")]
        public string Email { get; set; }

        [Display(Name = "ברשימת תפוצה ? ")]
        public string InMailingListString { get; set; }

      

        [Display(Name = "הערות מכון")]
        public string Institute_Comments { get; set; }

     

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

       

        [Display(Name = "תת סוג ארוע אחרון")]
        public int? SubType_ID { get; set; }

       

       
        

      
        [Display(Name = "רק ברשימת תפוצה")]
        public bool? InMailingList { get; set; }

       
    }


    public class InstituteEventsListRowExportModel
    {
        [ScaffoldColumn(false)]
        public int ID { get; set; }


    
        [Display(Name = "שם המכון")]
        public string Name { get; set; }

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
        public string MainPhone { get; set; }

        [Display(Name = "טלפונים נוספים")]
        public string Phones { get; set; }


        [Display(Name = "דוא\"ל")]
        public string Email { get; set; }

        [Display(Name = "ברשימת תפוצה ? ")]
        public string InMailingListString { get; set; }

     
        [Display(Name = "הערות אדם")]
        public string Institute_Comments { get; set; }

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


    public class InstituteDetailsModel
    {
        [ScaffoldColumn(false)]
        public int ID { get; set; }

        [Display(Name = "שם המכון")]
        [Required(ErrorMessage = "שם המכון חובה")]
        public string Name { get; set; }

        [Display(Name = "טלפון ראשי ")]
        public string MainPhone { get; set; }

        [Display(Name = "טלפונים נוספים")]
        public string Phones { get; set; }


        [Display(Name = "דוא\"ל")]
        [RegularExpression(@"^[\w-]+(\.[\w-]+)*@([a-z0-9-]+(\.[a-z0-9-]+)*?\.[a-z]{2,6}|(\d{1,3}\.){3}\d{1,3})(:\d{4})?$", ErrorMessage = "דוא\"ל לא נכון")]
        public string Email { get; set; }

        [Display(Name = "ברשימת תפוצה ? ")]
        public bool InMailingList { get; set; }



        [Display(Name = "הערות כלליות")]
        public string Institute_Comments { get; set; }



        [Display(Name = "רחוב ומספר")]
        public string Address { get; set; }

        [Display(Name = "ישוב")]
        public string City { get; set; }

        [Display(Name = "מיקןד")]
        public string ZipCode { get; set; }

        [Display(Name = "הערות כתובת")]
        public string Address_Comments { get; set; }
   

    }

    public class InstituteCreateModel
    {
        public InstituteDetailsModel details { get; set; } //to use the same template for edit/create
        public EventsFilterViewModel_ForList filter { get; set; }
    }


    public class InstituteEditModel
    {
        public EventsFilterViewModel_ForList filter { get; set; }

        public InstituteDetailsModel details  { get; set; }
        public EventsFilterViewModel_ForCard eventsFilter { get; set; }
        public InstituteEventsListRowViewModel eventsList { get; set; }
    }


  

}