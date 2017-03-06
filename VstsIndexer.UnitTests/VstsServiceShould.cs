using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace VstsIndexer.UnitTests
{
    [TestClass]
    public class VstsServiceShould
    {
        [TestMethod]
        public void ReturnWorkItemsWhenGetWithExistingWorkItemsIds()
        {
            //Given
            string workItemsIds = "2";
            VstsService vstsService = new VstsService();

            //When
            WorkItems workitems = vstsService.GetWorkItemsWithLinksAndAttachments(workItemsIds);

            //Then
            Assert.AreEqual(1, workitems.count);
        }

        [TestMethod]
        public void ReturnWorkItemsWithLinksAndAttachmentsWhenGetWithExistingWorkItemsIds()
        {
            //Given
            string workItemsIds = "2";
            VstsService vstsService = new VstsService();
            
            //When
            var actualWorkitems = vstsService.GetWorkItemsWithLinksAndAttachments(workItemsIds);

            //Then
            Assert.AreEqual(1, actualWorkitems.count);
            Assert.AreEqual(1, actualWorkitems.value[0].relations.Count);
            Assert.AreEqual("Sample for REST API VSTS.docx", actualWorkitems.value[0].relations[0].attributes.name);
        }
        
        [TestMethod]
        public void ReturnWorkItemIdsWhenSearchedWorkItemsByQuery()
        {   
            //Given
            VstsService vstsService = new VstsService();
            string query = @"{""query"": ""Select [System.WorkItemType],[System.Title],[System.State],[Microsoft.VSTS.Scheduling.Effort],[System.IterationPath] FROM WorkItemLinks WHERE Source.[System.WorkItemType] IN GROUP 'Microsoft.RequirementCategory' AND Target.[System.WorkItemType] IN GROUP 'Microsoft.RequirementCategory' AND Target.[System.State] IN ('New','Approved','Committed') AND [System.Links.LinkType] = 'System.LinkTypes.Hierarchy-Forward' ORDER BY [Microsoft.VSTS.Common.BacklogPriority] ASC,[System.Id] ASC MODE (Recursive, ReturnMatchingChildren)""}";
            
            //When
            var workitems = vstsService.GetWorkItemsByQuery(query);

            //Then
            Assert.AreEqual(2, workitems.workItemRelations.Count);
        }

        [TestMethod]
        public void ReturnAllTheWorkItemsWithLinksAndAttachmentsWhenGetWithoutIdsRange()
        {
            //Given
            VstsService vstsService = new VstsService();
            
            //When
            var actualWorkitems = vstsService.GetWorkItemsWithLinksAndAttachments();

            //Then
            Assert.AreEqual(2, actualWorkitems.count);
            Assert.AreEqual(2, actualWorkitems.value[0].id);
            Assert.AreEqual(1, actualWorkitems.value[0].relations.Count);
            Assert.AreEqual("Sample for REST API VSTS.docx", actualWorkitems.value[0].relations[0].attributes.name);
        }
    }
}
