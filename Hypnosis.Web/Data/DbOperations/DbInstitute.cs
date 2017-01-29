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
    public class DbInstitute : OperationBase
    {

        public IQueryable<Institute> GetInstitutes()
        {
            IQueryable<Institute> source = null;

            DateTime today = DateTime.Today;
            source = (from e in dbContext.Institutes
                      select e);

            return source;
        }

        public IQueryable<InstituteEventsListRowViewModel> GetRows()
        {
            IQueryable<Institute> data = GetInstitutes();
            return from p in data
                   let events = dbContext.Events.OrderByDescending(f => f.ID).FirstOrDefault(f => f.Institute_ID == p.ID) //temporal


                   select new InstituteEventsListRowViewModel
                   {

                       ID = p.ID,
                     
                       Name = p.Name,

                       SubType_ID = events.SubType_ID,

                       Description = events.Description,
                       SubType_Name = dbContext.EventSubTypes.Where(s => s.ID == events.SubType_ID).FirstOrDefault().SubType_Name,
                       Type_ID = dbContext.EventSubTypes.Where(s => s.ID == events.SubType_ID).FirstOrDefault().Type_ID,
                       Type_Name = dbContext.EventSubTypes.Where(s => s.ID == events.SubType_ID).FirstOrDefault().EventType.Type_Name,

                       FirstDate = events.FirstDate,

                       Description_Short = events.Description,
                       SubType_Name_Short = dbContext.EventSubTypes.Where(s => s.ID == events.SubType_ID).FirstOrDefault().SubType_Name,
                       FirstDate_Short = events.FirstDate,


                       MainPhone = p.MainPhone,
                       Phones = p.Phones,
                       Email = p.Email,
                       City = p.City,

                       InMailingList = p.InMailingList,
                       InMailingListString = p.InMailingList ? "כן" : "לא",

                      

                       Institute_Comments = p.Comments,

                      
                       Address = p.Address,
                       Address_Comments = p.Address_Comments



                   };
        }



        public List<InstituteEventsListRowExportModel> GetExportRows(int? Type_ID, int? SubType_ID, bool? InMailingListOnly)
        {
            IQueryable<Institute> data = GetInstitutes();
            var personEvents = from p in data
                               let events = dbContext.Events.OrderByDescending(f => f.ID).FirstOrDefault(f => f.Institute_ID == p.ID) //temporal


                               select new InstituteEventsListRowViewModel
                               {

                                   ID = p.ID,
                                  
                                   Name = p.Name,

                                   SubType_ID = events.SubType_ID,

                                   Description = events.Description,
                                   SubType_Name = dbContext.EventSubTypes.Where(s => s.ID == events.SubType_ID).FirstOrDefault().SubType_Name,
                                   Type_ID = dbContext.EventSubTypes.Where(s => s.ID == events.SubType_ID).FirstOrDefault().Type_ID,
                                   Type_Name = dbContext.EventSubTypes.Where(s => s.ID == events.SubType_ID).FirstOrDefault().EventType.Type_Name,

                                   FirstDate = events.FirstDate,

                                   Description_Short = events.Description,
                                   SubType_Name_Short = dbContext.EventSubTypes.Where(s => s.ID == events.SubType_ID).FirstOrDefault().SubType_Name,
                                   FirstDate_Short = events.FirstDate,


                                   MainPhone = p.MainPhone,
                                   Phones = p.Phones,
                                   Email = p.Email,
                                   City = p.City,

                                   InMailingList = p.InMailingList,
                                   InMailingListString = p.InMailingList ? "כן" : "לא",

                                   

                                   Institute_Comments = p.Comments,

                                
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

            var export = from p in source
                         select new InstituteEventsListRowExportModel

                         {

                             ID = p.ID,
                           
                             Name = p.Name,



                             Description = p.Description,
                             SubType_Name = p.SubType_Name,
                             Type_Name = p.Type_Name,

                             FirstDate = p.FirstDate.HasValue ? p.FirstDate.Value.ToShortDateString() : "",

                             Description_Short = p.Description,
                             SubType_Name_Short = p.SubType_Name,
                             FirstDate_Short = p.FirstDate_Short.HasValue ? p.FirstDate_Short.Value.ToShortDateString() : "",



                             MainPhone = p.MainPhone,
                             Phones = p.Phones,
                             Email = p.Email,
                             City = p.City,


                             InMailingListString = p.InMailingListString,

                          

                             Institute_Comments = HttpUtility.HtmlDecode(p.Institute_Comments),

                         

                             Address = p.Address,
                             Address_Comments = p.Address_Comments



                         };

            return export.ToList();

        }


        public string Delete(int Id)
        {

            string msg = "";
            var person = dbContext.Institutes.SingleOrDefault(f => f.ID == Id);
            if (person == null)
            {

                msg = "המכון לא קיים";
                return msg;

            }
            if (dbContext.Events.Any(f => f.Institute_ID == Id))
            {

                msg = " יש  ארועים-  לא ניתן למחוק את המכון";
                return msg;

            }
            try
            {
                dbContext.Institutes.Remove(person);
                dbContext.SaveChanges();
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException ex)
            {

                msg = "הייתה בעיה במחיקת המכון: " + ex.Message;
                return msg;

            }
            msg = "המחיקה בוצעה בהצלחה";
            return msg;


        }

    }
}