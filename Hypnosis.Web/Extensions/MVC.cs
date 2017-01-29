using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace System.Web.Mvc.Html
{
    public static class MVCExtensions
    {
        public static MvcHtmlString TitleFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
        {
            var metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);
            var expressionText = ExpressionHelper.GetExpressionText(expression);
            var text = metadata.DisplayName ?? metadata.PropertyName;
            return new MvcHtmlString(text);
        }

        private static string labeled_display_fs = "<span><span>{0}:&ensp;</span><strong>{1}</strong></span>&emsp;";
        public static MvcHtmlString LabeledDisplayForFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
        {
            var label = html.TitleFor(expression);

            var editor = html.DisplayFor(expression);

            return new MvcHtmlString(string.Format(labeled_display_fs, label.ToString(), editor.ToString()));
        }
        private static readonly string form_group = "<div class=\"form-group\">{0}{1}{2}</div>";
        public static MvcHtmlString LabeledEditorForFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
        {
            var label = html.LabelFor(expression);

            var editor = html.EditorFor(expression, new { htmlAttributes = new { @class = "form-control" }, });

            var validationMessage = html.ValidationMessageFor(expression);

            return new MvcHtmlString(string.Format(form_group, label, editor, validationMessage));
        }

        public static MvcHtmlString LabeledEditorForFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, object additionalViewData)
        {
            var label = html.LabelFor(expression);

            var adddviewdata = Merge(additionalViewData, new { htmlAttributes = new { @class = "form-control" }, });

            var editor = html.EditorFor(expression, adddviewdata);

            var validationMessage = html.ValidationMessageFor(expression);

            return new MvcHtmlString(string.Format(form_group, label, editor, validationMessage));
        }

        static MergedType<T1, T2> Merge<T1, T2>(T1 t1, T2 t2)
        {
            return new MergedType<T1, T2>(t1, t2);
        }
        class MergedType<T1, T2> : System.Dynamic.DynamicObject
        {
            T1 t1;
            T2 t2;
            Dictionary<string, object> members = new Dictionary<string, object>(StringComparer.InvariantCultureIgnoreCase);


            public MergedType(T1 t1, T2 t2)
            {
                this.t1 = t1;
                this.t2 = t2;
                foreach (System.Reflection.PropertyInfo fi in typeof(T1).GetProperties())
                {
                    members[fi.Name] = fi.GetValue(t1, null);
                }
                foreach (System.Reflection.PropertyInfo fi in typeof(T2).GetProperties())
                {
                    members[fi.Name] = fi.GetValue(t2, null);
                }
            }

            public override bool TryGetMember(System.Dynamic.GetMemberBinder binder, out object result)
            {
                string name = binder.Name.ToLower();
                return members.TryGetValue(name, out result);
            }
        }


        /// <summary>
        /// Get the display name of member
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static string DisplayName<TSource, TResult>(Expression<Func<TSource, TResult>> expression)
        {
            var m = ((System.Linq.Expressions.MemberExpression)expression.Body).Member;
            return m.Name;

        }

        /// <summary>
        /// get the property name
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static string PropertyName<TSource, TResult>(Expression<Func<TSource, TResult>> expression)
        {
            var m = ((System.Linq.Expressions.MemberExpression)expression.Body).Member;
            return m.Name;

        }

        public static MvcHtmlString FieldIdFor<TModel, TValue>(this HtmlHelper<TModel> html,
           Expression<Func<TModel, TValue>> expression)
        {
            string htmlFieldName = ExpressionHelper.GetExpressionText(expression);
            string inputFieldId = html.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldId(htmlFieldName);
            return MvcHtmlString.Create(inputFieldId);
        }
        public static MvcHtmlString FieldNameFor<TModel, TValue>(this HtmlHelper<TModel> html,
           Expression<Func<TModel, TValue>> expression)
        {
            string htmlFieldName = ExpressionHelper.GetExpressionText(expression);
            string inputFieldId = html.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(htmlFieldName);
            return MvcHtmlString.Create(inputFieldId);
        }
    }
}