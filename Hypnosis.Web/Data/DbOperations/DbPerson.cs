using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Hypnosis.Web.Models;
using Hypnosis.Web.Data;
using Hypnosis.Web.Controllers;

namespace Hypnosis.Web.Data.DbOperations
{
    public class DbPerson: OperationBase
    {

        public IQueryable<Person> GetPersons()
        {
            IQueryable<Person> source = null;

            DateTime today = DateTime.Today;
            source = (from e in dbContext.Persons
                      select e);

            return source;
        }

        public IQueryable<Person> GetPersons(int ID)
        {
            var persons = GetPersons();
            var source = from p in persons
                         where p.ID == ID
                         select p;

            return source;
        }

        public IQueryable<PersonEventsListRowViewModel> GetRows()
        {
            IQueryable<Person> data = GetPersons();
            return from p in data
                   let events = dbContext.Events.OrderByDescending(f=>f.ID).FirstOrDefault(f => f.Person_ID ==p.ID) //temporal
                  
                   
                   select new PersonEventsListRowViewModel
                   {
                              
                       ID=p.ID,
                       TZ=p.TZ,
                       FamilyName=p.FamilyName,
                       FirstName=p.FirstName,

                       SubType_ID=events.SubType_ID,
                      
                       Description=events.Description,
                       SubType_Name=dbContext.EventSubTypes.Where(s=>s.ID==events.SubType_ID).FirstOrDefault().SubType_Name,
                       Type_ID = dbContext.EventSubTypes.Where(s => s.ID == events.SubType_ID).FirstOrDefault().Type_ID,
                       Type_Name = dbContext.EventSubTypes.Where(s => s.ID == events.SubType_ID).FirstOrDefault().EventType.Type_Name,
                       
                       FirstDate=events.FirstDate,

                       Description_Short = events.Description,
                       SubType_Name_Short = dbContext.EventSubTypes.Where(s => s.ID == events.SubType_ID).FirstOrDefault().SubType_Name,
                       FirstDate_Short = events.FirstDate,


                       Mobile = p.Mobile,
                       Phones = p.Phones,
                       Email = p.Email,
                       City = p.City,
                      
                       InMailingList=p.InMailingList,
                       InMailingListString=p.InMailingList ? "כן": "לא",

                       Psyhology_LicenseNumber=p.Psyhology_LicenseNumber,
                       Medicine_LicenseNumber=p.Medicine_LicenseNumber,
                       Stomatology_LicenseNumber=p.Stomatology_LicenseNumber,

                       Person_Comments=p.Comments,

                       BirthDate = p.BirthDate,
                       Address=p.Address,
                       Address_Comments=p.Address_Comments


                    
                   };
        }



        public List<PersonEventsListRowExportModel> GetExportRows(int? Type_ID, int? SubType_ID, bool? InMailingListOnly)
        {
            IQueryable<Person> data = GetPersons();
           var personEvents= from p in data
                   let events = dbContext.Events.OrderByDescending(f => f.ID).FirstOrDefault(f => f.Person_ID == p.ID) //temporal


                   select new PersonEventsListRowViewModel
                   {

                       ID = p.ID,
                       TZ = p.TZ,
                       FamilyName = p.FamilyName,
                       FirstName = p.FirstName,

                       SubType_ID = events.SubType_ID,

                       Description = events.Description,
                       SubType_Name = dbContext.EventSubTypes.Where(s => s.ID == events.SubType_ID).FirstOrDefault().SubType_Name,
                       Type_ID = dbContext.EventSubTypes.Where(s => s.ID == events.SubType_ID).FirstOrDefault().Type_ID,
                       Type_Name = dbContext.EventSubTypes.Where(s => s.ID == events.SubType_ID).FirstOrDefault().EventType.Type_Name,

                       FirstDate = events.FirstDate,

                       Description_Short = events.Description,
                       SubType_Name_Short = dbContext.EventSubTypes.Where(s => s.ID == events.SubType_ID).FirstOrDefault().SubType_Name,
                       FirstDate_Short = events.FirstDate,


                       Mobile = p.Mobile,
                       Phones = p.Phones,
                       Email = p.Email,
                       City = p.City,

                       InMailingList = p.InMailingList,
                       InMailingListString = p.InMailingList ? "כן" : "לא",

                       Psyhology_LicenseNumber = p.Psyhology_LicenseNumber,
                       Medicine_LicenseNumber = p.Medicine_LicenseNumber,
                       Stomatology_LicenseNumber = p.Stomatology_LicenseNumber,

                       Person_Comments = p.Comments,

                       BirthDate = p.BirthDate,
                       Address = p.Address,
                       Address_Comments = p.Address_Comments



                   };


           if (Type_ID.HasValue)
           {
               personEvents = personEvents.Where(f => f.Type_ID == Type_ID);
           }
           if (SubType_ID.HasValue)
           {
               personEvents = personEvents.Where(f => f.SubType_ID == SubType_ID);
           }
           if (InMailingListOnly == true)
           {
               personEvents = personEvents.Where(f => f.InMailingList == InMailingListOnly);
           }
           var source = personEvents.ToList();

           var export=from p in source
                      select new PersonEventsListRowExportModel
                      
                   {

                       ID = p.ID,
                       TZ = p.TZ,
                       FamilyName = p.FamilyName,
                       FirstName = p.FirstName,

                     

                       Description =p.Description,
                       SubType_Name = p.SubType_Name,
                       Type_Name = p.Type_Name,

                       FirstDate = p.FirstDate.HasValue ? p.FirstDate.Value.ToShortDateString() : "",

                       Description_Short = p.Description,
                       SubType_Name_Short =p.SubType_Name,
                       FirstDate_Short = p.FirstDate_Short.HasValue ? p.FirstDate_Short.Value.ToShortDateString() : "",



                       Mobile = p.Mobile,
                       Phones = p.Phones,
                       Email = p.Email,
                       City = p.City,

                      
                       InMailingListString = p.InMailingListString,

                       Psyhology_LicenseNumber = p.Psyhology_LicenseNumber,
                       Medicine_LicenseNumber = p.Medicine_LicenseNumber,
                       Stomatology_LicenseNumber = p.Stomatology_LicenseNumber,

                       Person_Comments = HttpUtility.HtmlDecode(p.Person_Comments),

                       BirthDate = p.BirthDate.HasValue ? p.BirthDate.Value.ToShortDateString() : "",

                       Address = p.Address,
                       Address_Comments = p.Address_Comments



                   };

           return export.ToList();

        }


        public string Delete(int Id)
        {

            string msg = "";
            var person = dbContext.Persons.SingleOrDefault(f => f.ID == Id);
            if (person == null)
            {
             
                msg = "האיש לא קיים";
                return msg;
              
            }
            if (dbContext.Events.Any(f => f.Person.ID == Id))
            {
            
                msg = " יש  ארועים-  לא ניתן למחוק את האיש";
                return msg;
             
            }
            try
            {
                dbContext.Persons.Remove(person);
                dbContext.SaveChanges();
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException ex)
            {
              
                msg = "הייתה בעיה במחיקת האיש: " + ex.Message;
                return msg;
               
            }
            msg = "המחיקה בוצעה בהצלחה";
            return msg;

         
        }

    }
}