using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace VstsIndexer.UnitTests
{    
    public class VstsService
    {
        private const string VSTS_URL = "https://sboinot.visualstudio.com/DefaultCollection/";
        private const string PERSONNAL_ACCESS_TOKEN = "m6kwlx4yau4irw4ganufsophwl3tutbgo36qxslkyjj4mq2fn23a";

        public VstsService()
        {

        }

        public WorkItems GetWorkItemsWithLinksAndAttachments(string ids)
        {
            string _personalAccessToken = PERSONNAL_ACCESS_TOKEN;
            string _credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes(string.Format("{0}:{1}", "", _personalAccessToken)));

            //use the httpclient
            using (var client = new HttpClient())
            {
                //set our headers
                client.BaseAddress = new Uri(VSTS_URL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", _credentials);

                //send the request and content
                HttpResponseMessage response = client.GetAsync("_apis/wit/workitems?ids=" + ids + "&$expand=all&api-version=2.2").Result;
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    return JsonConvert.DeserializeObject<WorkItems>(result);
                }
            }

            return new WorkItems();
        }
        
        public WorkitemQueryResult GetWorkItemsByQuery(string query)
        {
            string _credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes(string.Format("{0}:{1}", "", PERSONNAL_ACCESS_TOKEN)));
            var result = string.Empty;
            
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(VSTS_URL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", _credentials);

                var content = new StringContent(query, Encoding.UTF8, "application/json");
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                HttpResponseMessage response = client.PostAsync("_apis/wit/wiql?api-version=2.2", content).Result;

                if (response.IsSuccessStatusCode)
                {
                    result = response.Content.ReadAsStringAsync().Result;
                    return JsonConvert.DeserializeObject<WorkitemQueryResult>(result);
                }
            }

            return new WorkitemQueryResult();
        }

        public WorkItems GetWorkItemsWithLinksAndAttachments()
        {
            string query = @"{""query"": ""Select [System.WorkItemType],[System.Title],[System.State],[Microsoft.VSTS.Scheduling.Effort],[System.IterationPath] FROM WorkItemLinks WHERE Source.[System.WorkItemType] IN GROUP 'Microsoft.RequirementCategory' AND Target.[System.WorkItemType] IN GROUP 'Microsoft.RequirementCategory' AND Target.[System.State] IN ('New','Approved','Committed') AND [System.Links.LinkType] = 'System.LinkTypes.Hierarchy-Forward' ORDER BY [Microsoft.VSTS.Common.BacklogPriority] ASC,[System.Id] ASC MODE (Recursive, ReturnMatchingChildren)""}";

            var workitemQueryResults = GetWorkItemsByQuery(query);
           
            string ids = string.Empty;
            foreach (var item in workitemQueryResults.workItemRelations)
            {
                ids += string.Format("{0},", item.target.id);
            }
            ids = ids.TrimEnd(new char[] { ',' });

            var workitems = GetWorkItemsWithLinksAndAttachments(ids);
            return workitems;
        }
    }
}
