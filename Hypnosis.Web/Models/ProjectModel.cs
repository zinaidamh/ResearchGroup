using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
namespace Hypnosis.Web.Models
{
    public class ProjectEventsListRowViewModel
    {
        [ScaffoldColumn(false)]
        public int ID { get; set; }

        [Display(Name = "שם המחקר")]
        public string Name { get; set; }

        [Display(Name = "תאור")]
        public string Project_Description { get; set; }

        [Display(Name = "מספר")]
        public string PersonOrder { get; set; }

        [Display(Name = "תמונה")]
        public string ImageName { get; set; }

        public string ImageOriginalName { get; set; }


    }

    public class ProjectEventsListRowExportModel
    {
        [ScaffoldColumn(false)]
        public int ID { get; set; }

        [Display(Name = "שם המחקר")]
        public string Name { get; set; }

        [Display(Name = "תאור")]
        public string Project_Description { get; set; }

        [Display(Name = "מספר")]
        public string PersonOrder { get; set; }


        [Display(Name = "תמונה")]
        public string ImageName { get; set; }

        public string ImageOriginalName { get; set; }


    }


    public class HolidayLocation
    {

       
        public int ID { get; set; }

       
        public string Location { get; set; }

        
        public int Preference { get; set; }

    }
    
    public class ProjectDetailsModel
    {
        [ScaffoldColumn(false)]
        public int ID { get; set; }

        [Display(Name = "שם המחקר")]
        public string Name { get; set; }

        [Display(Name = "תאור")]
        public string Project_Description { get; set; }

        [Display(Name = "מספר")]
        public string PersonOrder { get; set; }


        [Display(Name = "תמונה")]
        public string ImageName { get; set; }

        public string ImageOriginalName { get; set; }

        public List<HolidayLocation> HolidayLocation;
    }


    public class ProjectFilterViewModel_ForCard
    {
        public int ID { get; set; }
    }




    public class ProjectCreateModel
    {
        public ProjectDetailsModel details { get; set; } //to use the same template for edit/create
        public EventsFilterViewModel_ForList filter { get; set; }
    }


    public class ProjectEditModel
    {
        public EventsFilterViewModel_ForList filter { get; set; }

        public ProjectDetailsModel details { get; set; }
        public EventsFilterViewModel_ForCard eventsFilter { get; set; }
        public ProjectEventsListRowViewModel eventsList { get; set; }
        public ProjectFilterViewModel_ForCard projectFilter { get; set; }

    }




}