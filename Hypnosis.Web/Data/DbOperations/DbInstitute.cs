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


        public IQueryable<InstituteEventsListRowViewModel> GetRows(int? Type_ID, int? SubType_ID, bool? InMailingListOnly)
        {
                                                     
            
            var data = GetInstitutes();
            var eventsList = dbContext.Events.Include("EventSubTypes").Include("EventTypes");
            var list= from p in data
                   let events = eventsList.OrderByDescending(f => f.ID).FirstOrDefault(f => f.Institute_ID == p.ID)
                   let events_Essense = eventsList.Where(f=>f.EventSubType.EssenseOrder>0).
                                           OrderByDescending(f => f.ID).FirstOrDefault(f => f.Institute_ID == p.ID)
                


                   select new InstituteEventsListRowViewModel
                   {

                       ID = p.ID,
                     
                       Name = p.Name,

                       SubType_ID = events.SubType_ID,
                       Type_ID = dbContext.EventSubTypes.Where(s => s.ID == events.SubType_ID).FirstOrDefault().Type_ID,

                       Type_ID_Essense = events_Essense.EventSubType.Type_ID,
                       SubType_ID_Essense=events_Essense.SubType_ID,

                       Description = events.Description,
                       SubType_Name = dbContext.EventSubTypes.Where(s => s.ID == events.SubType_ID).FirstOrDefault().SubType_Name,
                       Type_Name = dbContext.EventSubTypes.Where(s => s.ID == events.SubType_ID).FirstOrDefault().EventType.Type_Name,
                       FirstDate = events.FirstDate,

                       Description_Essense = events_Essense.Description,
                       SubType_Name_Essense =events_Essense.EventSubType.SubType_Name,
                       Type_Name_Essense=events_Essense.EventSubType.EventType.Type_Name,
                     
                       //dbContext.EventSubTypes.Where(s => s.ID == events_Essense.SubType_ID).FirstOrDefault().SubType_Name,
                       FirstDate_Essense = events.FirstDate,


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
                list = list.Where(f => f.Type_ID == Type_ID && f.Type_ID_Essense == Type_ID);
            }
            if (SubType_ID.HasValue)
            {
                list = list.Where(f => f.SubType_ID == SubType_ID && f.SubType_ID_Essense == SubType_ID);
            }
            if (InMailingListOnly == true)
            {
                list = list.Where(f => f.InMailingList == InMailingListOnly);
            }

            IQueryable < InstituteEventsListRowViewModel> result= list.OrderBy(f => f.Name);
            return result;
        }



        public List<InstituteEventsListRowExportModel> GetExportRows(int? Type_ID, int? SubType_ID, bool? InMailingListOnly)
        {

            var instituteEvents = GetRows(Type_ID,SubType_ID,InMailingListOnly);


           
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

                             Description_Essense = p.Description,
                             SubType_Name_Essense = p.SubType_Name,
                             FirstDate_Essense = p.FirstDate_Essense.HasValue ? p.FirstDate_Essense.Value.ToShortDateString() : "",



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
            institute.Address_Comments = HttpUtility.HtmlDecode(model.Address_Comments);
         
            institute.City = model.City;
        
            institute.Email = model.Email;
            institute.InMailingList = model.InMailingList;
            institute.MainPhone = model.MainPhone;
            institute.Comments = HttpUtility.HtmlDecode(model.Institute_Comments);
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

        //json
        public IEnumerable<s2item> InstitutesInit(int? value)
        {
            var q = from a in dbContext.Institutes
                    where value == null || a.ID == value.Value
                    select new
                    {
                        id = a.ID,
                        text = a.Name
                    };
            return q.ToList().Select(f => new s2item
            {
                id = f.id,
                text = f.text.ToString()
            });
        }

        public s2result getInstitutes(string q, int page, int page_limit)
        {
            var dbq = from a in dbContext.Institutes


                      select new
                      {
                          id = a.ID,
                          text = a.Name
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




    }
}