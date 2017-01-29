using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hypnosis.Web.Data.DbOperations
{

    public class s2item
    {
        public int id { get; set; }
        public string text { get; set; }
        public IEnumerable<s2item> children { get; set; }

        public IEnumerable<string> allchildren { get; set; }
    }

    public class s2result
    {
        public s2result()
        {
            this.results = new s2item[0];
        }
        public IEnumerable<s2item> results { get; set; }
        public bool more { get; set; }

        public string term { get; set; }
    }
    public class OperationBase
    {
        public  HypnosisEntities dbContext;

        public OperationBase ()
        {
            dbContext = new HypnosisEntities();
        }
        public void Dispose(bool disposing)
        {
            dbContext.Dispose();
           
        }

    }
}