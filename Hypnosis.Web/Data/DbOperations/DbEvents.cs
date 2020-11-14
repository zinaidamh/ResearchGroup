using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Hypnosis.Web.Models;
using Hypnosis.Web.Data;
using Hypnosis.Web.Controllers;
using System.Data.Entity.Validation;

namespace Hypnosis.Web.Data.DbOperations
{
    public class DbEvents : OperationBase
    {
        public static string tbl = "Events";
        public IQueryable<Event> GetEvents()
        {
            IQueryable<Event> source = null;


            source = (from e in dbContext.Events
                      select e);

            return source;
        }

        public Event GetEventById(int ID)
        {


            var _event = dbContext.Events.SingleOrDefault(e => e.ID == ID);
            return _event;
        }



        public List<EventsExportModel> GetExportRows(int? Card_ID, int? Type_ID, int? SubType_ID, int? Category_ID, DateTime? fromDate, DateTime? toDate, bool EssenseOnly, bool ExpiredOnly, bool OpenOnly, bool FileOnly, bool SiteOnly)

        {
            IQueryable<EventsFullListRowViewModel> source = GetEventsByFilter(Card_ID, Type_ID, SubType_ID, Category_ID, fromDate, toDate, EssenseOnly, ExpiredOnly, OpenOnly, FileOnly, SiteOnly);
            var data = source.ToList();
             var q= from e in data
                    select new EventsExportModel
                    {

                        ID = e.ID,
                        TZ = e.TZ,
                        Person_Name = e.Person_Name,
                        Institute_Name = e.Institute_Name,
                       

                        SubType_Name = e.SubType_Name,
                        Type_Name = e.Type_Name,
                        Category_Name = e.Category_Name,
                        Description = e.Description!=null? e.Description: "",
                        

                        FirstDate = e.FirstDate!=null ?  e.FirstDate.Value.ToShortDateString() : "",

                        Agent_Name = e.Agent_Name,

                        ExpirationDate = e.ExpirationDate != null ? e.ExpirationDate.Value.ToShortDateString() : "",
                        AlertDate = e.AlertDate != null ? e.AlertDate.Value.ToShortDateString() : "",
                        alertDoneString = e.AlertDone == true ? "כן" : "לא",
                        FileName = e.FileName==null ? "": e.FileName,
                        SiteHref = e.SiteHref==null? "" : e.SiteHref,
                        CreatedAt = e.CreatedAt.ToShortDateString()


                    };

             return q.ToList();

        }


        public IQueryable<EventsFullListRowViewModel> GetRows()
        {
            IQueryable<Event> data = GetEvents();
            var relativePath = System.Configuration.ConfigurationManager.AppSettings["relativePath"];
            var q = from e in data
                    let person = dbContext.Persons.FirstOrDefault(f => e.Person_ID == f.ID)
                    let inst = dbContext.Institutes.FirstOrDefault(f => e.Institute_ID == f.ID)
                  


                    select new EventsFullListRowViewModel
                    {

                        ID = e.ID,
                        TZ = person == null ? "" : person.TZ,
                        Person_Name = person == null ? "" : person.DisplayName,
                        Institute_Name = inst == null ? "" : inst.Name,
                        Person_ID = e.Person_ID,
                        Institute_ID = e.Institute_ID,
                        SubType_ID=e.SubType_ID,
                        Type_ID=e.EventSubType.Type_ID,
                        Agent_ID=e.Agent_ID,

                        SubType_Name = e.EventSubType.SubType_Name,
                        Type_Name = e.EventSubType.EventType.Type_Name,
                        Category_Name = e.EventSubType.EventType.EventTypeCategory.Category_Name,
                        Category_ID=e.EventSubType.EventType.Type_Category,
                        Description = e.Description==null? "" : e.Description,
                        EssenseOrder=(int)(e.EventSubType.EssenseOrder==null? 0: e.EventSubType.EssenseOrder),

                        FirstDate = e.FirstDate,

                        Agent_Name = (e.Agent == null) ? "" : e.Agent.Agent_Name,

                        ExpirationDate = e.ExpirationDate,
                        AlertDate = e.AlertDate,
                        alertDoneString = e.AlertDone == true ? "כן" : "לא",
                        AlertDone = e.AlertDone,
                        FileName = e.FileName==null ? "": e.FileName,
                        SiteHref = e.SiteHref==null? "" : e.SiteHref,
                        CreatedAt = e.CreatedAt


                    };
            return q;
        }



        public IQueryable<EventsFullListRowViewModel> GetEventsByFilter(int? Card_ID, int? Type_ID, int? SubType_ID, int? Category_ID, DateTime? fromDate, DateTime? toDate, bool EssenseOnly, bool ExpiredOnly, bool OpenOnly, bool FileOnly, bool SiteOnly)
        {
            int? Institute_ID=null; int? Person_ID=null;
            if (Category_ID == (int)EventCategory.InstituteEvent)
                Institute_ID = Card_ID;
            else
                Person_ID = Card_ID;



            
            var events = this.GetRows();

            if (Person_ID.HasValue)
            {
                events=events.Where(f => f.Person_ID == Person_ID);
            }

            if (Institute_ID.HasValue)
            {
                events=events.Where(f => f.Institute_ID == Institute_ID);
            }

            if (Type_ID.HasValue)
            {
                events = events.Where(f => f.Type_ID == Type_ID);
            }
            if (SubType_ID.HasValue)
            {
                events = events.Where(f => f.SubType_ID == SubType_ID);
            }
            if (Category_ID.HasValue)
            {
                events = events.Where(f => f.Category_ID == Category_ID);
            }
            var date = DateTime.Now.Date;
            if (ExpiredOnly)
            {
                events = events.Where(f => f.ExpirationDate <= date);
            }
            //also for Essense after explain
            if (FileOnly)
            {
                events = events.Where(f => f.FileName != null && f.FileName!="");
            }
            if (SiteOnly)
            {
                events = events.Where(f => f.SiteHref != null && f.SiteHref!="");
            }
            if (fromDate.HasValue)
            {
                events = events.Where(f => f.FirstDate >= fromDate);
            }
            if (toDate.HasValue)
            {
                events = events.Where(f => f.FirstDate <= toDate);
            }
            if (OpenOnly)
            {
                events = events.Where(f => f.AlertDone == false);
            
            }
            if (EssenseOnly)
            {
                events = events.Where(f => f.EssenseOrder > 0);
            }
            return events;

        }

//get agents
        public IEnumerable<s2item> AgentsInit(int? value)
        {
            var q = from a in dbContext.Agents
                    where value == null || a.ID == value.Value
                    select new
                    {
                        id = a.ID,
                        text = a.Agent_Name
                    };
            return q.ToList().Select(f => new s2item
            {
                id = f.id,
                text = f.text.ToString()
            });
        }

        public s2result getAgents(string q, int page, int page_limit)
        {
            var dbq = from a in dbContext.Agents


                      select new
                      {
                          id = a.ID,
                          text = a.Agent_Name
                      };
            if (!string.IsNullOrEmpty(q))
            {
                dbq = dbq.Where(f => f.text.Contains(q));
            }

            var aa = dbq.OrderBy(f => f.text).ToList().Select(f => new s2item { id = f.id, text = f.text });

            return new s2result
            {
                results = aa,
                more = aa.Count() > page_limit
            };
        }


        public bool ChangeData(HttpRequestBase Request, string path)
        {
            int? Id = null; bool isUpdate = false; int? SubType_ID = null; int? Type_ID = null; int? Agent_ID = null; int? Institute_ID = null;
            DateTime? FirstDate = null, AlertDate = null, ExpirationDate = null; string SiteHref = ""; bool AlertDone = false;
            int? Person_ID = null;
            int? Category_ID = null; int? Card_ID = null;
            string Description="";
            try
            {

                if (Request["Id"] != null)
                {
                    Id = Int32.Parse(Request["Id"]);
                }
                if (Request["Category_ID"] != null)
                {
                    Category_ID = Int32.Parse(Request["Category_ID"]);

                }
                if (Request["Card_ID"] != null)
                {
                    Card_ID = Int32.Parse(Request["Card_ID"]);

                }
                if (Request["SubType_ID"] != null && Request["SubType_ID"]!="" && Request["SubType_ID"]!="null")
                {
                    SubType_ID = Int32.Parse(Request["SubType_ID"]);
                }
                if (Request["Type_ID"] != null && Request["Type_ID"] != "" && Request["Type_ID"] != "null")
                {
                    Type_ID = Int32.Parse(Request["Type_ID"]);
                }
                if (Request["Agent_ID"] != null && Request["Agent_ID"]!="" && Request["Agent_ID"]!="null" )
                {
                    Agent_ID = Int32.Parse(Request["Agent_ID"]);
                }
                if (Request["Institute_ID"] != null && Request["Institute_ID"] !="" && Request["Institute_ID"]!="null")
                {
                    Institute_ID = Int32.Parse(Request["Institute_ID"]);
                }
                if (Request["isUpdate"] != null)
                {
                    isUpdate = bool.Parse(Request["isUpdate"]);
                }
                if (Request["FirstDate"] != null && Request["FirstDate"] != "" && Request["FirstDate"] != "null")
                {
                    FirstDate = DateTime.Parse(Request["FirstDate"] );
                }
                if (Request["AlertDate"] != null && Request["AlertDate"] != "" && Request["AlertDate"] != "null")
                {
                    AlertDate = DateTime.Parse(Request["AlertDate"]);
                }
                if (Request["ExpirationDate"] != null && Request["ExpirationDate"]!="" && Request["ExpirationDate"] != "")
                {
                    ExpirationDate = DateTime.Parse(Request["ExpirationDate"]);
                }
                if (Request["SiteHref"] != null && Request["SiteHref"]!="" &&  Request["SiteHref"]!="null" )
                {
                    SiteHref = Request["SiteHref"].ToString();
                }
                if (Request["AlertDone"] != null && Request["AlertDone"]!="")
                {
                    AlertDone = bool.Parse(Request["AlertDone"]);
                }
                if (Request["Person_ID"] != null && Request["Person_ID"] != "" && Request["Person_ID"] != "null")
                {
                    Person_ID = Int32.Parse(Request["Person_ID"]);
                }
                if (Request["Description"] != null && Request["Description"] != "" && Request["Description"] != "null")
                {
                    Description = Request["Description"];
                }
            }
            catch
            {
                {
                    return false;
                }
            }

            if (SubType_ID == null || Card_ID==null || Category_ID==null || Id==null)
            {
          
                return false;
            }

            //set by card
            if (Institute_ID == null && Category_ID ==(int) EventCategory.InstituteEvent) 
                Institute_ID = Card_ID;
            if (Person_ID == null && Category_ID == (int) EventCategory.PersonEvent)
                Person_ID = Card_ID;


            var files = Request.Files;
            string FileName = "";


            if (files.Count > 0)
            {
                var file0 = files[0];
              
                try
                {
                    file0.SaveAs(System.IO.Path.Combine(path, file0.FileName));
                    FileName = file0.FileName;

                }
                catch
                {
                    return false;
                }
            }

            if (CreateUpdate(Id, Person_ID, SubType_ID, Type_ID, Agent_ID, Institute_ID, FileName, FirstDate, ExpirationDate, AlertDate, AlertDone, SiteHref, Description, isUpdate))
            {
                return true;
            }
            else
            {
                return false;
            }
        
        
        }
        //create-update
   public bool CreateUpdate(int? Id, int? Person_ID, int? SubType_ID, int? Type_ID, int? Agent_ID, int? Institute_ID, string FileName, DateTime? FirstDate, DateTime? ExpirationDate, DateTime? AlertDate, bool AlertDone, string SiteHref, string Description, bool isUpdate)
    {
        string operation = tbl + " Create Update ";
           Logger.Log.Debug(operation + " Begin ");
           var _event = new Event();

           if (isUpdate)
            {
                _event = dbContext.Events.SingleOrDefault(f => f.ID == Id);
               if (_event == null)
                {
                    throw new Exception("הארוע לא קיים");
               }

            }

    //        //mandatory
            if (SubType_ID==null)
            {
                throw new Exception("שדה סוג ארוע הוא חובה");
            }

            if (SubType_ID == null)
            {
                throw new Exception("שדה תת סוג ארוע הוא חובה");
            }


            _event.Agent_ID = Agent_ID;
            _event.Institute_ID=Institute_ID;
            _event.Person_ID = Person_ID;
            _event.SubType_ID=(int) SubType_ID;
            _event.FileName = FileName;
            _event.FirstDate = FirstDate;
            _event.AlertDate = AlertDate;
            _event.ExpirationDate = ExpirationDate;
            _event.AlertDone = AlertDone;
            _event.SiteHref = SiteHref;
            _event.Description = Description;
           
         //   var RelativePath=System.Configuration.ConfigurationManager.AppSettings["relativePath"];





            try
            {
                if (!isUpdate)
                {
                    _event.CreatedAt = DateTime.Now;
                    
                    dbContext.Events.Add(_event);
                }
                dbContext.SaveChanges();
                return true;

            }
            catch (DbEntityValidationException e)
            {
                string msgs = "";
                foreach (var eve in e.EntityValidationErrors)
                {
                    Logger.Log.ErrorFormat("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Logger.Log.ErrorFormat("- Property: \"{0}\", Error: \"{1}\"",
                           ve.PropertyName, ve.ErrorMessage);
                       msgs += ": " + ve.ErrorMessage;
                    }
               }
                throw new Exception("הייתה בעיה בשמירת הנתונים" + msgs);
            }
            catch (Exception ex)
            {
                string msg = "";
                if (ex.InnerException != null)
                {
                    var inner = ex.InnerException;
                    while (inner != null)
                    {
                        if (inner.InnerException != null)
                        {
                            inner = inner.InnerException;
                        }
                        else
                        {
                            break;
                        }
                    }
                    msg = inner.Message;
                }
                else
                {
                    msg = ex.Message;
                }
                throw new Exception("הייתה בעיה בשמירת הנתונים: " + msg);
               // return false;
            }
            //Logger.Log.Debug(operation + " End ");
       }


   public IEnumerable<s2item> EventTypeCategoriesInit(int? value)
   {
       var q = from a in dbContext.EventTypeCategories
               where value == null || a.ID == value.Value
               select new
               {
                   id = a.ID,
                   text = a.Category_Name
               };
       return q.ToList().Select(f => new s2item
       {
           id = f.id,
           text = f.text.ToString()
       });
   }

   public s2result getEventTypeCategories(string q, int page, int page_limit)
   {
       var dbq = from a in dbContext.EventTypeCategories


                 select new
                 {
                     id = a.ID,
                     text = a.Category_Name
                 };
       if (!string.IsNullOrEmpty(q))
       {
           dbq = dbq.Where(f => f.text.Contains(q));
       }

       var aa = dbq.OrderBy(f => f.text).ToList().Select(f => new s2item { id = f.id, text = f.text });

       return new s2result
       {
           results = aa,
           more = aa.Count() > page_limit
       };
   }

   public string Delete(int Id)
   {

       string msg = "";
       var _event = dbContext.Events.SingleOrDefault(f => f.ID == Id);
       if (_event == null)
       {

           msg = "הארוע לא קיים";
           return msg;

       }
       
       try
       {
           dbContext.Events.Remove(_event);
           dbContext.SaveChanges();
       }
       catch (System.Data.Entity.Infrastructure.DbUpdateException ex)
       {

           msg = "הייתה בעיה במחיקת האדם: " + ex.Message;
           return msg;

       }
       msg = "המחיקה בוצעה בהצלחה";
       return msg;


   }


    }
}

    