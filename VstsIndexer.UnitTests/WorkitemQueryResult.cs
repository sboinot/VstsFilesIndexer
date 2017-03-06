using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VstsIndexer.UnitTests
{
    public class WorkitemQueryResult
    {
        public string queryType { get; set; }
        public string queryResultType { get; set; }
        public DateTime asOf { get; set; }
        public List<Column> columns { get; set; }
        public List<Sortcolumn> sortColumns { get; set; }
        public List<Workitemrelation> workItemRelations { get; set; }
    }

    public class Column
    {
        public string referenceName { get; set; }
        public string name { get; set; }
        public string url { get; set; }
    }

    public class Sortcolumn
    {
        public Field field { get; set; }
        public bool descending { get; set; }
    }

    public class Field
    {
        public string referenceName { get; set; }
        public string name { get; set; }
        public string url { get; set; }
    }

    public class Workitemrelation
    {
        public Target target { get; set; }
    }

    public class Target
    {
        public int id { get; set; }
        public string url { get; set; }
    }

}
