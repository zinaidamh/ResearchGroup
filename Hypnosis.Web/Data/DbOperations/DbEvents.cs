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


        //public EventEditModel GetEventEditModel(Event person)
        //{
        //    PersonDetailsModel details = new PersonDetailsModel
        //    {
        //        Person_Senior = person.Person_Senior,
        //        ID = person.ID,
        //        TZ = person.TZ,
        //        BirthDate = person.BirthDate,
        //        FirstName = person.FirstName,
        //        LastName = person.LastName,
        //        DisplayName = person.DisplayName,
        //        InMailingList = person.InMailingList,
        //        Degree = person.Degree,

        //        Mobile = person.Mobile,
        //        Phones = person.Phones,
        //        Email = person.Email,
        //        City = person.City,
        //        ZipCode = person.ZipCode,
        //        Address = person.Address,
        //        Address_Comments = person.Address_Comments,
        //        Person_Comments = person.Comments,

        //        Psyhology_LicenseNumber = person.Psyhology_LicenseNumber,
        //        Psyhology_Specialization = person.Psyhology_Specialization,
        //        Medicine_LicenseNumber = person.Medicine_LicenseNumber,
        //        Medicine_Specialization = person.Medicine_Specialization,
        //        Stomatology_LicenseNumber = person.Stomatology_LicenseNumber,
        //        Stomatology_Specialization = person.Stomatology_Specialization,
        //        Ministry_CaseNumber = person.Ministry_CaseNumber

        //    };

        //    PersonEditModel editModel = new PersonEditModel();
        //    editModel.details = details;

        //    return editModel;
        //}


        public IQueryable<EventsFullListRowViewModel> GetRows()
        {
            IQueryable<Event> data = GetEvents();
            var q = from e in data
                    let person = dbContext.Persons.FirstOrDefault(f => e.Person_ID == f.ID)
                    let inst = dbContext.Institutes.FirstOrDefault(f => e.Institute_ID == f.ID)



                    select new EventsFullListRowViewModel
                    {

                        ID = e.ID,
                        TZ = person == null ? "" : person.TZ,
                        Person_Name = person == null ? "" : person.DisplayName,
                        Institute_Name = inst == null ? "" : inst.Name,
                        Person_ID=e.Person_ID,
                        Institute_ID=e.Institute_ID,


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
                        FileName = e.FileName,
                        SiteHref = e.SiteHref,
                        CreatedAt = e.CreatedAt


                    };
            return q;
        }

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



    //    public void CreateUpdate(PersonDetailsModel model, bool isUpdate)
    //    {
    //        string operation = tbl + " Create Update ";
    //        Logger.Log.Debug(operation + " Begin ");
    //        var person = new Person();

    //        if (isUpdate)
    //        {
    //            person = dbContext.Persons.SingleOrDefault(f => f.ID == model.ID);
    //            if (person == null)
    //            {
    //                throw new Exception("האדם לא קיים");
    //            }

    //        }

    //        //mandatory
    //        if (string.IsNullOrEmpty(model.TZ))
    //        {
    //            throw new Exception("שדה ת.ז. הוא חובה");
    //        }

    //        if (string.IsNullOrEmpty(model.FirstName))
    //        {
    //            throw new Exception("שם פרטי הוא חובה");
    //        }


    //        if (string.IsNullOrEmpty(model.LastName))
    //        {
    //            throw new Exception("שם משפחה הוא חובה");
    //        }


    //        person.FirstName = model.FirstName;
    //        person.LastName = model.LastName;
    //        person.TZ = model.TZ;
    //        person.Address = model.Address;
    //        person.Address_Comments = model.Address_Comments;
    //        person.BirthDate = model.BirthDate;
    //        person.City = model.City;
    //        person.Degree = model.Degree;
    //        person.DisplayName = model.DisplayName;
    //        person.Email = model.Email;
    //        person.InMailingList = model.InMailingList.HasValue ? (bool)model.InMailingList : false;
    //        person.Medicine_LicenseNumber = model.Medicine_LicenseNumber;
    //        person.Medicine_Specialization = model.Medicine_Specialization;
    //        person.Ministry_CaseNumber = model.Ministry_CaseNumber;
    //        person.Mobile = model.Mobile;
    //        person.Comments = model.Person_Comments;
    //        person.Phones = model.Phones;
    //        person.Psyhology_LicenseNumber = model.Psyhology_LicenseNumber;
    //        person.Psyhology_Specialization = model.Psyhology_Specialization;
    //        person.Person_Senior = model.Person_Senior;
    //        person.Stomatology_LicenseNumber = model.Stomatology_LicenseNumber;
    //        person.Stomatology_Specialization = model.Stomatology_Specialization;
    //        person.ZipCode = model.ZipCode;




    //        try
    //        {
    //            if (!isUpdate)
    //            {
    //                dbContext.Persons.Add(person);
    //            }
    //            dbContext.SaveChanges();

    //        }
    //        catch (DbEntityValidationException e)
    //        {
    //            string msgs = "";
    //            foreach (var eve in e.EntityValidationErrors)
    //            {
    //                Logger.Log.ErrorFormat("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
    //                    eve.Entry.Entity.GetType().Name, eve.Entry.State);
    //                foreach (var ve in eve.ValidationErrors)
    //                {
    //                    Logger.Log.ErrorFormat("- Property: \"{0}\", Error: \"{1}\"",
    //                        ve.PropertyName, ve.ErrorMessage);
    //                    msgs += ": " + ve.ErrorMessage;
    //                }
    //            }
    //            throw new Exception("הייתה בעיה בשמירת הנתונים" + msgs);
    //        }
    //        catch (Exception ex)
    //        {
    //            string msg = "";
    //            if (ex.InnerException != null)
    //            {
    //                var inner = ex.InnerException;
    //                while (inner != null)
    //                {
    //                    if (inner.InnerException != null)
    //                    {
    //                        inner = inner.InnerException;
    //                    }
    //                    else
    //                    {
    //                        break;
    //                    }
    //                }
    //                msg = inner.Message;
    //            }
    //            else
    //            {
    //                msg = ex.Message;
    //            }
    //            throw new Exception("הייתה בעיה בשמירת הנתונים: " + msg);
    //        }
    //        Logger.Log.Debug(operation + " End ");
    //    }


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

    //}
//}