using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hypnosis.Web.Models;

namespace Hypnosis.Web.Data.DbOperations
{
    public class DbEventTypes: OperationBase
    {
        public static string tbl = "Event Types";


        public  bool CreateUpdate(int? Id, string Name, int Category, bool isUpdate)
        {
            string operation =  tbl+ " Create Update ";

            Logger.Log.Debug(operation + "- Begin");
            if (string.IsNullOrEmpty(Name))
            {
                return false;
            }
            var c = new EventType();
            if (isUpdate && Id.HasValue)
            {
                c = dbContext.EventTypes.SingleOrDefault(f => f.ID == Id.Value);
                if (c == null) return false;
            }
            c.Type_Name = Name;
            c.Type_Category = Category;
            try
            {
                if (!isUpdate)
                {
                    dbContext.EventTypes.Add(c);
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

        public  IQueryable<EventType> GetData()
        {
            IQueryable<EventType> source = null;

            source = from c in dbContext.EventTypes.Include("EventTypeCategory")
                     select c;

            return source;
        }


        public  IQueryable<EventTypesRowViewModel> GetRows(IQueryable<EventType> data)
        {
            return from c in data
       
                   select new EventTypesRowViewModel
                   {
                       ID = c.ID,
                       Type_Name = c.Type_Name,
                       Type_Category = c.Type_Category,
                       Type_Category_Name=c.EventTypeCategory.Category_Name
                   };
        }




        public  bool Delete(int Id)
        {
            string operation = tbl + " Delete ";
            Logger.Log.Debug(operation + " - Begin");
            var c = dbContext.EventTypes.SingleOrDefault(f => f.ID == Id);
            if (c == null) return false;
            try
            {
                dbContext.EventTypes.Remove(c);
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

        public  IEnumerable<s2item> EventTypesInit(int? value)
        {
            var q = from a in dbContext.EventTypes
                    where value == null || a.ID == value.Value
                    select new
                    {
                        id = a.ID,
                        text = a.Type_Name
                    };
            return q.ToList().Select(f => new s2item
            {
                id = f.id,
                text = f.text.ToString()
            });
        }

        public s2result getEventTypes(string q, int page, int page_limit)
        {
            var dbq = from a in dbContext.EventTypes
                      select new
                      {
                          id = a.ID,
                          text = a.Type_Name
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



    }
}