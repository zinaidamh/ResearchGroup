using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hypnosis.Web.Models.DataTables
{
    /// <summary>
    /// Object to use for call from datatables get
    /// </summary>
    [System.Web.Mvc.ModelBinder(typeof(ModelBinder))]
    public class jqDataTableInput
    {
        // https://datatables.net/usage/server-side

        /// <summary>
        /// int 	iDisplayStart 	Display start point in the current data set.
        /// </summary>
        public int iDisplayStart { get; set; }

        /// <summary>
        /// int 	iDisplayLength 	Number of records that the table can display in the current draw. It is expected that the number of records returned will be equal to this number, unless the server has fewer records to return.
        /// </summary>
        public int iDisplayLength { get; set; }

        /// <summary>
        /// int 	iColumns 	Number of columns being displayed (useful for getting individual column search info)
        /// </summary>
        public int iColumns { get; set; }

        /// <summary>
        /// string 	sSearch 	Global search field
        /// </summary>
        public string sSearch { get; set; }

        /// <summary>
        /// bool 	bRegex 	True if the global filter should be treated as item regular expression for advanced filtering, false if not.
        /// </summary>
        public bool bRegex { get; set; }

        /// <summary>
        /// bool 	bSearchable_(int) 	Indicator for if item column is flagged as searchable or not on the client-side
        /// </summary>
        public bool?[] bSearchable_ { get; set; }

        /// <summary>
        /// string 	sSearch_(int) 	Individual column filter
        /// </summary>
        public string[] sSearch_ { get; set; }

        /// <summary>
        /// bool 	bRegex_(int) 	True if the individual column filter should be treated as item regular expression for advanced filtering, false if not
        /// </summary>
        public bool[] bRegex_ { get; set; }

        /// <summary>
        /// bool 	bSortable_(int) 	Indicator for if item column is flagged as sortable or not on the client-side
        /// </summary>
        public bool?[] bSortable_ { get; set; }

        /// <summary>
        /// int 	iSortingCols 	Number of columns to sort on
        /// </summary>
        public int? iSortingCols { get; set; }

        /// <summary>
        /// int 	iSortCol_(int) 	Column being sorted on (you will need to decode this number for your database)
        /// </summary>
        public int[] iSortCol_ { get; set; }

        /// <summary>
        /// string 	sSortDir_(int) 	Direction to be sorted - "desc" or "asc".
        /// </summary>
        public string[] sSortDir_ { get; set; }

        /// <summary>
        /// string 	mDataProp_(int) 	The value specified by mDataProp for each column. This can be useful for ensuring that the processing of data is independent from the order of the columns.
        /// </summary>
        public string[] mDataProp_ { get; set; }

        /// <summary>
        /// string 	sEcho 	Information for DataTables to use for rendering.
        /// </summary>
        public string sEcho { get; set; }
    }
}