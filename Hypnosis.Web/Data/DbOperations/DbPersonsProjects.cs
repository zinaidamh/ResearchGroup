using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Hypnosis.Web.Models;
using Hypnosis.Web.Data;
using Hypnosis.Web.Controllers;
using System.Data.Entity.Validation;
using System.Net;
using OpenXmlPowerTools;

namespace Hypnosis.Web.Data.DbOperations
{
    public class DbPersonsProjects : OperationBase
    {
        public static string tbl = "Persons";
        public IQueryable<PersonProjectModel> GetRows()
        {
            IQueryable<PersonProjectModel> source = null;

            DateTime today = DateTime.Today;
            source = (from e in dbContext.PersonsProjects
                      select new PersonProjectModel()
                      {
                          ID=e.ID,
                          PersonOrder=e.PersonOrder==null ? 0: (int)e.PersonOrder,
                          Project_ID=e.Project_ID,
                          Person_ID=e.Person_ID
                      }
                );

            return source;
        }

        public IQueryable<PersonProjectViewModel> GetViewRows()
        {
            IQueryable<PersonProjectViewModel> source = null;

            DateTime today = DateTime.Today;
            source = (from e in dbContext.PersonsProjects
                      select new PersonProjectViewModel()
                      {
                          ID = e.ID,
                          PersonOrder = e.PersonOrder == null ? 0 : (int)e.PersonOrder,
                          Project_ID = e.Project_ID,
                          Person_ID = e.Person_ID,
                          ProjectName=e.Project.Name,
                          PersonName=e.Person.FirstName + " "+ e.Person.LastName
                      }
                );

            return source;
        }

        public string SaveOrder(int[] ids)
        {
            var list = dbContext.PersonsProjects.Where(p => p.Project_ID == 1);
            int num = 1;
            foreach (int id in ids)
            {
                // entities.HolidayLocations.Find(id);
                var row = list.Where(i => i.ID == id).SingleOrDefault();
                row.PersonOrder = num;
                num++;
            }
            dbContext.SaveChanges();
            return "";
        }

        public string CreateUpdate(int? Id, int? Person_ID, int? Project_ID, int? PersonOrder, bool isUpdate)
        {
            string operation = tbl + " Create Update ";

            Logger.Log.Debug(operation + "- Begin");
            if (Person_ID == null)
            {
                return "חוקר לא מוגדר";
            }
            if (Project_ID == null)
            {
                return "מחקר לא מוגדר";
            }
            var c = new PersonsProject();
            if (isUpdate && Id.HasValue)
            {
                c = dbContext.PersonsProjects.SingleOrDefault(f => f.ID == Id.Value);
                if (c == null) return "לא נימצא נתונים";
            }

            c.Project_ID = (int)Project_ID;
            c.Person_ID = (int)Person_ID;
            var pr = dbContext.PersonsProjects.Where(p => p.Project_ID == (int)Project_ID).Max(p => p.PersonOrder);
            int newNum = 1;
            if (pr!=null)
            {
                newNum = (int)pr + 1;
            }
            c.PersonOrder = newNum;
            try
            {
                if (!isUpdate)
                {
                    dbContext.PersonsProjects.Add(c);
                }
                dbContext.SaveChanges();
                Logger.Log.Debug(operation + "- End");
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
                
                string err = "Error on " + operation;
                Logger.Log.ErrorFormat(err, "msg = {0}", msg);
                return msg;
            }
            return "";
        }

        public IQueryable<PersonProjectViewModel> GetDataByProjectID(int? Project_ID)
        {




            var data = this.GetViewRows();

            if (Project_ID.HasValue)
            {
                data = data.Where(f => f.Project_ID == Project_ID).OrderBy(f=>f.PersonOrder);
            }


            return data;

        }

        public IQueryable<PersonProjectViewModel> GetDataByPersonID(int? Person_ID)
        {




            var data = this.GetViewRows();

            if (Person_ID.HasValue)
            {
                data = data.Where(f => f.Person_ID == Person_ID);
            }


            return data;

        }

        public bool Delete(int Id)
        {
            string operation = tbl + " Delete ";
            Logger.Log.Debug(operation + " - Begin");
            var c = dbContext.PersonsProjects.SingleOrDefault(f => f.ID == Id);
            if (c == null) return false;
            try
            {
                dbContext.PersonsProjects.Remove(c);
                dbContext.SaveChanges();
                Logger.Log.Debug(operation + " End ");
                return true;
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
                Logger.Log.ErrorFormat("Error on " + operation, " msg = {0}", msg);
                return false;
            }

        }
    }

}