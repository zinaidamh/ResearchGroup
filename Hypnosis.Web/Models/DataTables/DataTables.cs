using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace Hypnosis.Web.Models.DataTables
{
    public enum SordDirection
    {
        asc,
        desc
    }

    public class DataTablesActionResult<TSource> : System.Web.Mvc.JsonResult
    {
        IQueryable<TSource> queryableData;
        jqDataTableInput param;
        Expression<Func<TSource, bool>> preFilter;


        public DataTablesActionResult(IQueryable<TSource> source, jqDataTableInput param)
        {
            this.queryableData = source;
            this.param = param;
        }
        public DataTablesActionResult(IQueryable<TSource> source, jqDataTableInput param, Expression<Func<TSource, bool>> preFilter)
            : this(source, param)
        {

            this.preFilter = preFilter;
        }

        private static IQueryable<T> ApplySearch<T>(IQueryable<T> source, string sSearch, Expression<Func<T, bool>> preFilter)
        {

            var stringProps = typeof(T).GetProperties().Where(f => f.PropertyType == typeof(string));

            var sourceParameter = System.Linq.Expressions.Expression.Parameter(typeof(T));

            var containsMethod = typeof(string).GetMethod("Contains");

            var constExpr = Expression.Constant(sSearch, typeof(string));

            if (preFilter != null)
            {
                source = source.Where(preFilter);
            }

            if (sSearch == null)
            {
                return source;
            }

            Expression sSearchPredicateBody = null;
            foreach (var strprop in stringProps)
            {
                //f=>f.Property
                var propExpr = Expression.PropertyOrField(sourceParameter, strprop.Name);
                var a = Expression.Call(propExpr, containsMethod, constExpr);
                if (sSearchPredicateBody == null) { sSearchPredicateBody = a; }
                else
                {
                    sSearchPredicateBody = Expression.OrElse(sSearchPredicateBody, a);
                }

            }




            MethodCallExpression whereCallExpression = Expression.Call(
               typeof(Queryable),
               "Where",
               new Type[] { source.ElementType },
               source.Expression,
               Expression.Lambda<Func<T, bool>>(sSearchPredicateBody, new ParameterExpression[] { sourceParameter }));

            var newQuery = source.Provider.CreateQuery<T>(whereCallExpression);
            return newQuery;


        }
        private static IOrderedQueryable<T> OrderBy<T>(IQueryable<T> q, string SortField, bool asc)
        {
            var method = asc ? "OrderBy" : "OrderByDescending";
            var param = Expression.Parameter(typeof(T), "p");
            var prop = Expression.Property(param, SortField);
            var exp = Expression.Lambda(prop, param);

            Type[] types = new Type[] { q.ElementType, exp.Body.Type };
            var mce = Expression.Call(typeof(Queryable), method, types, q.Expression, exp);
            return (IOrderedQueryable<T>)q.Provider.CreateQuery<T>(mce);
        }
        private static IOrderedQueryable<T> ThenBy<T>(IQueryable<T> q, string SortField, bool asc)
        {
            var method = asc ? "ThenBy" : "ThenByDescending";
            var param = Expression.Parameter(typeof(T), "p");
            var prop = Expression.Property(param, SortField);
            var exp = Expression.Lambda(prop, param);

            Type[] types = new Type[] { q.ElementType, exp.Body.Type };
            var mce = Expression.Call(typeof(Queryable), method, types, q.Expression, exp);
            return (IOrderedQueryable<T>)q.Provider.CreateQuery<T>(mce);
        }

        public override void ExecuteResult(System.Web.Mvc.ControllerContext context)
        {
            //apply ssearch filtering
            var filtered = ApplySearch<TSource>(this.queryableData, this.param.sSearch, this.preFilter);

            //order the results
            var ordered = OrderBy(filtered, this.param.mDataProp_[this.param.iSortCol_[0]], this.param.sSortDir_[0] == "asc");
            for (var i = 1; i < this.param.iSortingCols; i++)
            {
                ordered = ThenBy(ordered, this.param.mDataProp_[this.param.iSortCol_[i]], this.param.sSortDir_[i] == "asc");
            }

            var data = ordered.ToList();

            //result object
            var result = new jqDataTablesResult<TSource>()
            {
                sEcho = this.param.sEcho,
                iTotalRecords = this.queryableData.Count(),
                iTotalDisplayRecords = filtered.Count(),
                aaData = data.Skip(this.param.iDisplayStart).Take(this.param.iDisplayLength).ToArray()
            };


            var serializedString = Newtonsoft.Json.JsonConvert.SerializeObject(result, Newtonsoft.Json.Formatting.None, new Newtonsoft.Json.JsonSerializerSettings
            {
                ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore,
            });
            context.HttpContext.Response.Write(serializedString);
        }
    }
    public static class DataTablesActionResultHelper
    {
        public static DataTablesActionResult<T> Create<T>(IQueryable<T> source, jqDataTableInput param)
        {
            return new DataTablesActionResult<T>(source, param);
        }
    }
}