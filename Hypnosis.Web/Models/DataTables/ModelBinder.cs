using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hypnosis.Web.Models.DataTables
{
    public class ModelBinder : System.Web.Mvc.DefaultModelBinder
    {
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {

            jqDataTableInput _input = (jqDataTableInput)base.BindModel(controllerContext, bindingContext);
            ValueProviderResult vResult;

            // create array for sort values based on column count

            _input.iSortCol_ = new int[(int)_input.iSortingCols];
            _input.sSortDir_ = new string[(int)_input.iSortingCols];
            for (var i = 0; i < _input.iSortingCols; i++)
            {
                _input.iSortCol_[i] = GetValue<int>(bindingContext, "iSortCol_" + i.ToString()).Value;

                bindingContext.ValueProvider.TryGetValue("sSortDir_" + i.ToString(), out vResult);
                if (vResult != null)
                {
                    _input.sSortDir_[i] = (String)vResult.ConvertTo(typeof(String));
                }
            }

            _input.sSearch_ = new string[(int)_input.iColumns];
            _input.bSearchable_ = new bool?[(int)_input.iColumns];
            _input.bSortable_ = new bool?[(int)_input.iColumns];

            _input.mDataProp_ = new string[(int)_input.iColumns];

            //_input.bEscapeRegex_ = new bool?[(int)_input.iColumns];

            // get results based on column number in name
            // zero based array list

            for (var i = 0; i < _input.iColumns; i++)
            {
                bindingContext.ValueProvider.TryGetValue("sSearch_" + i.ToString(), out vResult);
                if (vResult != null)
                {
                    _input.sSearch_[i] = (String)vResult.ConvertTo(typeof(String));
                }


                bindingContext.ValueProvider.TryGetValue("mDataProp_" + i.ToString(), out vResult);
                if (vResult != null)
                {
                    _input.mDataProp_[i] = (String)vResult.ConvertTo(typeof(String));
                }

                _input.bSearchable_[i] = GetValue<bool>(bindingContext, "bSearchable_" + i.ToString());
                _input.bSortable_[i] = GetValue<bool>(bindingContext, "bSortable_" + i.ToString());

            }



            return _input;
        }

        // get processing value
        private Nullable<T> GetValue<T>(ModelBindingContext bindingContext, string key) where T : struct
        {
            ValueProviderResult valueResult;
            bindingContext.ValueProvider.TryGetValue(key, out valueResult);
            if (valueResult == null)
                return null;
            else
                return (Nullable<T>)valueResult.ConvertTo(typeof(T));
        }
    }
    public static class asdf
    {
        public static bool TryGetValue(this IValueProvider valueProvider, string key, out ValueProviderResult result)
        {
            try
            {

                result = valueProvider.GetValue(key);
                return true;
            }
            catch
            {
                result = null;
                return false;
            }
        }
    }
}