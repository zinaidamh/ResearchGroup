using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hypnosis.Web.Models;

namespace Hypnosis.Web.Data.DbOperations
{
    public class DbEventSubTypes: OperationBase
    {
        public static string tbl = "Event Sub Types";


        public  bool CreateUpdate(int? Id, string Name, int TypeId, bool isUpdate)
        {
            string operation =  tbl+ " Create Update ";

            Logger.Log.Debug(operation + "- Begin");
            if (string.IsNullOrEmpty(Name))
            {
                return false;
            }
            var c = new EventSubType();
            if (isUpdate && Id.HasValue)
            {
                c = dbContext.EventSubTypes.SingleOrDefault(f => f.ID == Id.Value);
                if (c == null) return false;
            }
            c.SubType_Name = Name;
            c.Type_ID = TypeId;
            try
            {
                if (!isUpdate)
                {
                    dbContext.EventSubTypes.Add(c);
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
                string err="Error on " + operation;
                Logger.Log.ErrorFormat(err, "msg = {0}", msg);
                return false;
            }
            return true;
        }

        public  IQueryable<EventSubType> GetData()
        {
            IQueryable<EventSubType> source = null;

            source = from c in dbContext.EventSubTypes.Include("EventTypes")
                     select c;

            return source;
        }


        public  IQueryable<EventSubTypesRowViewModel> GetRows(IQueryable<EventSubType> data)
        {
            return from c in data
       
                   select new EventSubTypesRowViewModel
                   {
                       ID = c.ID,
                       SubType_Name = c.SubType_Name,
                       Type_ID = c.Type_ID,
                       Type_Name=c.EventType.Type_Name
                   };
        }




        public  bool Delete(int Id)
        {
            string operation = tbl + " Delete ";
            Logger.Log.Debug(operation + " - Begin");
            var c = dbContext.EventSubTypes.SingleOrDefault(f => f.ID == Id);
            if (c == null) return false;
            try
            {
                dbContext.EventSubTypes.Remove(c);
                dbContext.SaveChanges();
                Logger.Log.Debug(operation+ " End ");
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

        public  IEnumerable<s2item> EventSubTypesInit(int? value)
        {
            var q = from a in dbContext.EventSubTypes
                    where value == null || a.ID == value.Value
                    select new
                    {
                        id = a.ID,
                        text = a.SubType_Name
                    };
            return q.ToList().Select(f => new s2item
            {
                id = f.id,
                text = f.text.ToString()
            });
        }

        public s2result getEventSubTypes(string q, int? typeId, int category, int page, int page_limit)
        {
            var dbq = from a in dbContext.EventSubTypes
                      select new
                      {
                          id = a.ID,
                          text = a.SubType_Name,
                          category=a.EventType.Type_Category,
                          typeID=a.Type_ID

                      };
            if (!string.IsNullOrEmpty(q))
            {
                dbq = dbq.Where(f => f.text.Contains(q));
            }
            if (typeId!=null)
            {
                dbq = dbq.Where(f => f.typeID == typeId);
            }
            if (category!=0)
            {
                dbq = dbq.Where(f => f.category == category); //person or institute events only
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