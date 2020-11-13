﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Hypnosis.Web.Models;
using Hypnosis.Web.Data;
using Hypnosis.Web.Controllers;
using System.Data.Entity.Validation;
using System.Net;

namespace Hypnosis.Web.Data.DbOperations
{
    public class DbPerson: OperationBase
    {
        public static string tbl = "Persons";
        public IQueryable<Person> GetPersons()
        {
            IQueryable<Person> source = null;

            DateTime today = DateTime.Today;
            source = (from e in dbContext.Persons
                      select e);

            return source;
        }

        public Person GetPersonById(int ID)
        {


            var person = dbContext.Persons.SingleOrDefault(p => p.ID == ID);
            return person;
        }

        public PersonDetailsModel GetPersonDetailsById(int ID)
        {
            var person = dbContext.Persons.SingleOrDefault(p => p.ID == ID);
            var model = GetPersonDetailsModel(person);
            return model;
        }
    

        public PersonDetailsModel GetPersonDetailsModel(Person person)
        {

            PersonDetailsModel details = new PersonDetailsModel
            {
                Person_Senior = person.Person_Senior,
                ID = person.ID,
                TZ = person.TZ,
                BirthDate = person.BirthDate,
                FirstName = person.FirstName,
                LastName = person.LastName,
                DisplayName = person.DisplayName,
                InMailingList = person.InMailingList,
                Degree = person.Degree,

                Mobile = person.Mobile,
                Phones = person.Phones,
                Email = person.Email,
                City = person.City,
                ZipCode = person.ZipCode,
                Address = person.Address,
                Address_Comments = person.Address_Comments,
                Person_Comments = person.Comments,
                Psyhology_LicenseNumber = person.Psyhology_LicenseNumber,
                Psyhology_Specialization = person.Psyhology_Specialization,
                Medicine_LicenseNumber = person.Medicine_LicenseNumber,
                Medicine_Specialization = person.Medicine_Specialization,
                Stomatology_LicenseNumber = person.Stomatology_LicenseNumber,
                Stomatology_Specialization = person.Stomatology_Specialization,
                Ministry_CaseNumber = person.Ministry_CaseNumber,
                ImageName = person.ImageName,

            };

           

            return details;
        }


        public PersonEditModel GetPersonEditModel(Person person)
        {
            
            PersonDetailsModel details = new PersonDetailsModel
            {
                Person_Senior=person.Person_Senior,
                ID=person.ID,
                TZ=person.TZ,
                BirthDate=person.BirthDate,
                FirstName=person.FirstName,
                LastName=person.LastName,
                DisplayName=person.DisplayName,
                InMailingList=person.InMailingList,
                Degree=person.Degree,

                Mobile=person.Mobile,
                Phones=person.Phones,
                Email=person.Email,
                City=person.City,
                ZipCode=person.ZipCode,
                Address=person.Address,
                Address_Comments=person.Address_Comments,
                Person_Comments=person.Comments,

                Psyhology_LicenseNumber=person.Psyhology_LicenseNumber,
                Psyhology_Specialization=person.Psyhology_Specialization,
                Medicine_LicenseNumber=person.Medicine_LicenseNumber,
                Medicine_Specialization=person.Medicine_Specialization,
                Stomatology_LicenseNumber=person.Stomatology_LicenseNumber,
                Stomatology_Specialization=person.Stomatology_Specialization,
                Ministry_CaseNumber=person.Ministry_CaseNumber,
                ImageName=person.ImageName,

            };
           
            PersonEditModel editModel = new PersonEditModel();
            editModel.details = details;

            return editModel;
        }


        public IQueryable<PersonEventsListRowViewModel> GetRows(int? Type_ID, int? SubType_ID, bool? InMailingListOnly)
        {
          
            IQueryable<Person> data = GetPersons();
            var eventsList = dbContext.Events.Include("EventSubTypes").Include("EventTypes");
            var list= from p in data
                      let events = eventsList.OrderByDescending(f => f.ID).FirstOrDefault(f => f.Institute_ID == p.ID)
                      let events_Essense = eventsList.Where(f => f.EventSubType.EssenseOrder > 0).
                                              OrderByDescending(f => f.ID).FirstOrDefault(f => f.Institute_ID == p.ID)
                
    
                   
                   select new PersonEventsListRowViewModel
                   {
                              
                       ID=p.ID,
                       TZ=p.TZ,
                       LastName=p.LastName,
                       FirstName=p.FirstName,

                       SubType_ID = events.SubType_ID,
                       Type_ID = dbContext.EventSubTypes.Where(s => s.ID == events.SubType_ID).FirstOrDefault().Type_ID,

                       Type_ID_Essense = events_Essense.EventSubType.Type_ID,
                       SubType_ID_Essense = events_Essense.SubType_ID,

                       Description = events.Description,
                       SubType_Name = dbContext.EventSubTypes.Where(s => s.ID == events.SubType_ID).FirstOrDefault().SubType_Name,
                       Type_Name = dbContext.EventSubTypes.Where(s => s.ID == events.SubType_ID).FirstOrDefault().EventType.Type_Name,
                       FirstDate = events.FirstDate,

                       Description_Essense = events_Essense.Description,
                       SubType_Name_Essense = events_Essense.EventSubType.SubType_Name,
                       Type_Name_Essense = events_Essense.EventSubType.EventType.Type_Name,

                       //dbContext.EventSubTypes.Where(s => s.ID == events_Essense.SubType_ID).FirstOrDefault().SubType_Name,
                       FirstDate_Essense = events.FirstDate,



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
                       Address_Comments=p.Address_Comments,

                       ZipCode=p.ZipCode
                    
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

            IQueryable<PersonEventsListRowViewModel> result = list;
            return result;
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
                       LastName = p.LastName,
                       FirstName = p.FirstName,

                       SubType_ID = events.SubType_ID,

                       Description = events.Description,
                       SubType_Name = dbContext.EventSubTypes.Where(s => s.ID == events.SubType_ID).FirstOrDefault().SubType_Name,
                       Type_ID = dbContext.EventSubTypes.Where(s => s.ID == events.SubType_ID).FirstOrDefault().Type_ID,
                       Type_Name = dbContext.EventSubTypes.Where(s => s.ID == events.SubType_ID).FirstOrDefault().EventType.Type_Name,

                       FirstDate = events.FirstDate,

                       Description_Essense = events.Description,
                       SubType_Name_Essense = dbContext.EventSubTypes.Where(s => s.ID == events.SubType_ID).FirstOrDefault().SubType_Name,
                       FirstDate_Essense = events.FirstDate,


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
                       LastName = p.LastName,
                       FirstName = p.FirstName,

                     

                       Description =p.Description,
                       SubType_Name = p.SubType_Name,
                       Type_Name = p.Type_Name,

                       FirstDate = p.FirstDate.HasValue ? p.FirstDate.Value.ToShortDateString() : "",

                       Description_Essense = p.Description,
                       SubType_Name_Essense =p.SubType_Name,
                       FirstDate_Essense = p.FirstDate_Essense.HasValue ? p.FirstDate_Essense.Value.ToShortDateString() : "",



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



        public void CreateUpdate(PersonDetailsModel model, HttpPostedFileBase fileUploaded, string path, bool isUpdate)
        {
            string operation = tbl + " Create Update ";
            Logger.Log.Debug(operation + " Begin ");
            var person=new Person();
          
            if (isUpdate)
            {
                 person = dbContext.Persons.SingleOrDefault(f => f.ID == model.ID);
                if (person == null)
                {
                    throw new Exception("האדם לא קיים");
                }
               
            }
            
            //mandatory
            if (string.IsNullOrEmpty(model.DisplayName))
            {
                throw new Exception("שם לתצוגה חובה");
            }

            if (string.IsNullOrEmpty(model.FirstName))
            {
                throw new Exception("שם פרטי הוא חובה");
            }


            if (string.IsNullOrEmpty(model.LastName))
            {
                throw new Exception("שם משפחה הוא חובה");
            }


            person.FirstName=model.FirstName;
            person.LastName=model.LastName;
            person.TZ = model.TZ == null ? "-------" : model.TZ;
            person.Address = model.Address;
            person.Address_Comments = HttpUtility.HtmlDecode(model.Address_Comments);
            person.BirthDate = model.BirthDate;
            person.City = model.City;
            person.Degree = model.Degree;
            person.DisplayName = model.DisplayName;
            person.Email = model.Email;
            person.InMailingList = model.InMailingList;
            person.Medicine_LicenseNumber = model.Medicine_LicenseNumber;
            person.Medicine_Specialization = model.Medicine_Specialization;
            person.Ministry_CaseNumber = model.Ministry_CaseNumber;
            person.Mobile = model.Mobile;
            person.Comments = HttpUtility.HtmlDecode(model.Person_Comments);
            person.Phones = model.Phones;
            person.Psyhology_LicenseNumber = model.Psyhology_LicenseNumber;
            person.Psyhology_Specialization = model.Psyhology_Specialization;
            person.Person_Senior = model.Person_Senior;
            person.Stomatology_LicenseNumber = model.Stomatology_LicenseNumber;
            person.Stomatology_Specialization = model.Stomatology_Specialization;
            person.ZipCode = model.ZipCode;
            //person.ImageOriginalName = "bbb";
            //person.ImageName = "xxx";



            try
            {
                if (!isUpdate)
                {
                    dbContext.Persons.Add(person);
                }
                if (fileUploaded != null && path != "")
                {
                    Guid uniqfilename = Guid.NewGuid();
                    string oldFileName = fileUploaded.FileName;
                    string extension = System.IO.Path.GetExtension(oldFileName);
                    string newFileName = uniqfilename.ToString() + extension;
                    string fullFileName = System.IO.Path.Combine(path, newFileName);
                    fileUploaded.SaveAs(fullFileName);
                    //string newFileName = Hypnosis.Web.MyHelpers.FilesHelper.RelativePath+uniqfilename;
                    person.ImageOriginalName = newFileName;
                    person.ImageName = newFileName;
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
            var person = dbContext.Persons.SingleOrDefault(f => f.ID == Id);
            if (person == null)
            {
             
                msg = "האדם לא קיים";
                return msg;
              
            }
            if (dbContext.Events.Any(f => f.Person.ID == Id))
            {
            
                msg = " יש  ארועים-  לא ניתן למחוק את האדם";
                return msg;
             
            }
            try
            {
                dbContext.Persons.Remove(person);
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