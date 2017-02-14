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
                        Description = e.Description,


                        FirstDate = e.FirstDate,

                        Agent_Name = (e.Agent == null) ? "" : e.Agent.Agent_Name,

                        ExpirationDate = e.ExpirationDate,
                        AlertDate = e.AlertDate,
                        alertDoneString = e.AlertDone == true ? "כן" : "לא",
                        AlertDone = e.AlertDone,
                        FileName = e.FileName==null ? "": e.FileName,
                        SiteHref = e.SiteHref,
                        CreatedAt = e.CreatedAt


                    };
            return q;
        }



        public IQueryable<EventsFullListRowViewModel> GetEventsByFilter(int? Person_ID, int? Institute_ID, int? Type_ID, int? SubType_ID, int? Category_ID, DateTime? fromDate, DateTime? toDate, bool EssenseOnly, bool ExpiredOnly, bool OpenOnly, bool FileOnly, bool SiteOnly)
        {
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
            if (ExpiredOnly)
            {
                events = events.Where(f => f.ExpirationDate <= DateTime.Now.Date);
            }
            //also for Essense after explain
            if (FileOnly)
            {
                events = events.Where(f => f.FileName != null);
            }
            if (SiteOnly)
            {
                events = events.Where(f => f.SiteHref != null);
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




        //create-update
   public bool CreateUpdate(int? Id, int? Person_ID, int? SubType_ID, int? Type_ID, int? Agent_ID, int? Institute_ID, string FileName, DateTime? FirstDate, DateTime? ExpirationDate, DateTime? AlertDate, bool AlertDone, string SiteHref, bool isUpdate)
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
            _event.SubType_ID=(int) SubType_ID;
            _event.FileName = FileName;
            _event.FirstDate = FirstDate;
            _event.AlertDate = AlertDate;
            _event.ExpirationDate = ExpirationDate;
            _event.AlertDone = AlertDone;
            _event.SiteHref = SiteHref;
           
            var RelativePath=System.Configuration.ConfigurationManager.AppSettings["relativePath"];





            try
            {
                if (!isUpdate)
                {
                    _event.CreatedAt = DateTime.Now;
                    _event.Person_ID = Person_ID;
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
                return false;
            }
            Logger.Log.Debug(operation + " End ");
       }


    //    public string Delete(int Id)
    //    {

    //        string msg = "";
    //        var person = dbContext.Persons.SingleOrDefault(f => f.ID == Id);
    //        if (person == null)
    //        {

    //            msg = "האדם לא קיים";
    //            return msg;

    //        }
    //        if (dbContext.Events.Any(f => f.Person.ID == Id))
    //        {

    //            msg = " יש  ארועים-  לא ניתן למחוק את האדם";
    //            return msg;

    //        }
    //        try
    //        {
    //            dbContext.Persons.Remove(person);
    //            dbContext.SaveChanges();
    //        }
    //        catch (System.Data.Entity.Infrastructure.DbUpdateException ex)
    //        {

    //            msg = "הייתה בעיה במחיקת האדם: " + ex.Message;
    //            return msg;

    //        }
    //        msg = "המחיקה בוצעה בהצלחה";
    //        return msg;


    //    }


    }
}

    //    public List<PersonEventsListRowExportModel> GetExportRows(int? Type_ID, int? SubType_ID, bool? InMailingListOnly)
    //    {
    //        IQueryable<Person> data = GetPersons();
    //        var personEvents = from p in data
    //                           let events = dbContext.Events.OrderByDescending(f => f.ID).FirstOrDefault(f => f.Person_ID == e.ID) //temporal


    //                           select new PersonEventsListRowViewModel
    //                           {

    //                               ID = e.ID,
    //                               TZ = e.TZ,
    //                               LastName = e.LastName,
    //                               FirstName = e.FirstName,

    //                               SubType_ID = events.SubType_ID,

    //                               Description = events.Description,
    //                               SubType_Name = dbContext.EventSubTypes.Where(s => s.ID == events.SubType_ID).FirstOrDefault().SubType_Name,
    //                               Type_ID = dbContext.EventSubTypes.Where(s => s.ID == events.SubType_ID).FirstOrDefault().Type_ID,
    //                               Type_Name = dbContext.EventSubTypes.Where(s => s.ID == events.SubType_ID).FirstOrDefault().EventType.Type_Name,

    //                               FirstDate = events.FirstDate,

    //                               Description_Short = events.Description,
    //                               SubType_Name_Short = dbContext.EventSubTypes.Where(s => s.ID == events.SubType_ID).FirstOrDefault().SubType_Name,
    //                               FirstDate_Short = events.FirstDate,


    //                               Mobile = e.Mobile,
    //                               Phones = e.Phones,
    //                               Email = e.Email,
    //                               City = e.City,

    //                               InMailingList = e.InMailingList,
    //                               InMailingListString = e.InMailingList ? "כן" : "לא",

    //                               Psyhology_LicenseNumber = e.Psyhology_LicenseNumber,
    //                               Medicine_LicenseNumber = e.Medicine_LicenseNumber,
    //                               Stomatology_LicenseNumber = e.Stomatology_LicenseNumber,

    //                               Person_Comments = e.Comments,

    //                               BirthDate = e.BirthDate,
    //                               Address = e.Address,
    //                               Address_Comments = e.Address_Comments



    //                           };


    //        if (Type_ID.HasValue)
    //        {
    //            personEvents = personEvents.Where(f => f.Type_ID == Type_ID);
    //        }
    //        if (SubType_ID.HasValue)
    //        {
    //            personEvents = personEvents.Where(f => f.SubType_ID == SubType_ID);
    //        }
    //        if (InMailingListOnly == true)
    //        {
    //            personEvents = personEvents.Where(f => f.InMailingList == InMailingListOnly);
    //        }
    //        var source = personEvents.ToList();

    //        var export = from p in source
    //                     select new PersonEventsListRowExportModel

    //                     {

    //                         ID = e.ID,
    //                         TZ = e.TZ,
    //                         LastName = e.LastName,
    //                         FirstName = e.FirstName,



    //                         Description = e.Description,
    //                         SubType_Name = e.SubType_Name,
    //                         Type_Name = e.Type_Name,

    //                         FirstDate = e.FirstDate.HasValue ? e.FirstDate.Value.ToShortDateString() : "",

    //                         Description_Short = e.Description,
    //                         SubType_Name_Short = e.SubType_Name,
    //                         FirstDate_Short = e.FirstDate_Short.HasValue ? e.FirstDate_Short.Value.ToShortDateString() : "",



    //                         Mobile = e.Mobile,
    //                         Phones = e.Phones,
    //                         Email = e.Email,
    //                         City = e.City,


    //                         InMailingListString = e.InMailingListString,

    //                         Psyhology_LicenseNumber = e.Psyhology_LicenseNumber,
    //                         Medicine_LicenseNumber = e.Medicine_LicenseNumber,
    //                         Stomatology_LicenseNumber = e.Stomatology_LicenseNumber,

    //                         Person_Comments = HttpUtility.HtmlDecode(e.Person_Comments),

    //                         BirthDate = e.BirthDate.HasValue ? e.BirthDate.Value.ToShortDateString() : "",

    //                         Address = e.Address,
    //                         Address_Comments = e.Address_Comments



    //                     };

    //        return export.ToList();

    //    }



  
    //}
//}