using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VstsIndexer.UnitTests
{
    public class WorkItems
    {
        public int count { get; set; }
        public List<WorkItemDetails> value { get; set; }
    }
    
    public class WorkItemDetails
    {
        public int id { get; set; }
        public int rev { get; set; }
        public List<Relation> relations { get; set; }
        public IDictionary<string, string> fields;
        public Links _links { get; set; }
        public string url { get; set; }
    }

    public class Fields
    {
        public int SystemId { get; set; }
        public int SystemAreaId { get; set; }
        public string SystemAreaPath { get; set; }
        public string SystemTeamProject { get; set; }
        public string SystemNodeName { get; set; }
        public string SystemAreaLevel1 { get; set; }
        public int SystemRev { get; set; }
        public DateTime SystemAuthorizedDate { get; set; }
        public DateTime SystemRevisedDate { get; set; }
        public int SystemIterationId { get; set; }
        public string SystemIterationPath { get; set; }
        public string SystemIterationLevel1 { get; set; }
        public string SystemWorkItemType { get; set; }
        public string SystemState { get; set; }
        public string SystemReason { get; set; }
        public DateTime SystemCreatedDate { get; set; }
        public string SystemCreatedBy { get; set; }
        public DateTime SystemChangedDate { get; set; }
        public string SystemChangedBy { get; set; }
        public string SystemAuthorizedAs { get; set; }
        public int SystemPersonId { get; set; }
        public int SystemWatermark { get; set; }
        public string SystemTitle { get; set; }
        public string SystemBoardColumn { get; set; }
        public float MicrosoftVSTSCommonBacklogPriority { get; set; }
        public int MicrosoftVSTSCommonPriority { get; set; }
        public bool WEF_17B7548A5BF64F36A41DF46BC38F393F_SystemExtensionMarker { get; set; }
        public string WEF_17B7548A5BF64F36A41DF46BC38F393F_KanbanColumn { get; set; }
        public string MicrosoftVSTSCommonValueArea { get; set; }
    }

    public class Links
    {
        public Self self { get; set; }
        public Workitemupdates workItemUpdates { get; set; }
        public Workitemrevisions workItemRevisions { get; set; }
        public Workitemhistory workItemHistory { get; set; }
        public Html html { get; set; }
        public Workitemtype workItemType { get; set; }
        public Fields1 fields { get; set; }
    }

    public class Self
    {
        public string href { get; set; }
    }

    public class Workitemupdates
    {
        public string href { get; set; }
    }

    public class Workitemrevisions
    {
        public string href { get; set; }
    }

    public class Workitemhistory
    {
        public string href { get; set; }
    }

    public class Html
    {
        public string href { get; set; }
    }

    public class Workitemtype
    {
        public string href { get; set; }
    }

    public class Fields1
    {
        public string href { get; set; }
    }

    public class Relation
    {
        public string rel { get; set; }
        public string url { get; set; }
        public Attributes attributes { get; set; }
    }

    public class Attributes
    {
        public DateTime authorizedDate { get; set; }
        public int id { get; set; }
        public DateTime resourceCreatedDate { get; set; }
        public DateTime resourceModifiedDate { get; set; }
        public DateTime revisedDate { get; set; }
        public int resourceSize { get; set; }
        public string name { get; set; }
    }


}
