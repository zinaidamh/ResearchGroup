//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Hypnosis.Web.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class PersonsProject
    {
        public int Project_ID { get; set; }
        public int Person_ID { get; set; }
        public Nullable<int> PersonOrder { get; set; }
        public int ID { get; set; }
    
        public virtual Person Person { get; set; }
        public virtual Project Project { get; set; }
    }
}
