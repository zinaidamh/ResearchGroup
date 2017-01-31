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
    public class DbInstitute : OperationBase
    {
        public static string tbl = "Institutes";

        public IQueryable<Institute> GetInstitutes()
        {
            IQueryable<Institute> source = null;

            DateTime today = DateTime.Today;
            source = (from e in dbContext.Institutes
                      select e);

            return source;
        }



        public Institute GetInstituteById(int ID)
        {


            var institute = dbContext.Institutes.SingleOrDefault(p => p.ID == ID);
            return institute;
        }


        public InstituteEditModel GetInstituteEditModel(Institute institute)
        {
            InstituteDetailsModel details = new InstituteDetailsModel
            {
              
                ID = institute.ID,
               
             
                Name = institute.Name,
                InMailingList = institute.InMailingList,
             

                MainPhone = institute.MainPhone,
                Phones = institute.Phones,
                Email = institute.Email,
                City = institute.City,
                ZipCode = institute.ZipCode,
                Address = institute.Address,
                Address_Comments = institute.Address_Comments,
                Institute_Comments = institute.Comments,

              

            };

            InstituteEditModel editModel = new InstituteEditModel();
            editModel.details = details;

            return editModel;
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
            var instituteEvents = from p in data
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
                instituteEvents = instituteEvents.Where(f => f.Type_ID == Type_ID);
            }
            if (SubType_ID.HasValue)
            {
                instituteEvents = instituteEvents.Where(f => f.SubType_ID == SubType_ID);
            }
            if (InMailingListOnly == true)
            {
                instituteEvents = instituteEvents.Where(f => f.InMailingList == InMailingListOnly);
            }
            var source = instituteEvents.ToList();

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

        public void CreateUpdate(InstituteDetailsModel model, bool isUpdate)
        {
            string operation = tbl + " Create Update ";
            Logger.Log.Debug(operation + " Begin ");
            var institute = new Institute();

            if (isUpdate)
            {
                institute = dbContext.Institutes.SingleOrDefault(f => f.ID == model.ID);
                if (institute == null)
                {
                    throw new Exception("האדם לא קיים");
                }

            }

            //mandatory
          

            if (string.IsNullOrEmpty(model.Name))
            {
                throw new Exception("שם חובה");
            }


            


          
            institute.Name = model.Name;
            institute.Address = model.Address;
            institute.Address_Comments = model.Address_Comments;
         
            institute.City = model.City;
        
            institute.Email = model.Email;
            institute.InMailingList = model.InMailingList;
            institute.MainPhone = model.MainPhone;
            institute.Comments = model.Institute_Comments;
            institute.Phones = model.Phones;
            institute.ZipCode = model.ZipCode;




            try
            {
                if (!isUpdate)
                {
                    dbContext.Institutes.Add(institute);
                }
                dbContext.SaveChanges();

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
            }
            Logger.Log.Debug(operation + " End ");
        }

        public string Delete(int Id)
        {

            string msg = "";
            var institute = dbContext.Institutes.SingleOrDefault(f => f.ID == Id);
            if (institute == null)
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
                dbContext.Institutes.Remove(institute);
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