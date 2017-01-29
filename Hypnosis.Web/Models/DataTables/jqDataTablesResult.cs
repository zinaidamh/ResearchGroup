using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hypnosis.Web.Models.DataTables
{
    public class jqDataTablesResult<T>
    {
        /// <summary>
        /// array 	aaData 	The data in item 2D array. Note that you can change the name of this parameter with sAjaxDataProp.
        /// </summary>
        public T[] aaData { get; set; }
        /// <summary>
        /// string 	sEcho 	An unaltered copy of sEcho sent from the client side. This parameter will change with each draw (it is basically item draw count) - so it is important that this is implemented. Note that it strongly recommended for security reasons that you 'cast' this parameter to an integer in order to prevent Cross Site Scripting (XSS) attacks.
        /// </summary>
        public string sEcho { get; set; }
        /// <summary>
        /// int 	iTotalRecords 	Total records, before filtering (i.e. the total number of records in the database)
        /// </summary>
        public int iTotalRecords { get; set; }
        /// <summary>
        /// int 	iTotalDisplayRecords 	Total records, after filtering (i.e. the total number of records after filtering has been applied - not just the number of records being returned in this result set)
        /// </summary>
        public int iTotalDisplayRecords { get; set; }

    }
}