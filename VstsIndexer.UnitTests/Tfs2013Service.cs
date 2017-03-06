using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.Framework.Client;
using Microsoft.TeamFoundation.Framework.Common;
using Microsoft.TeamFoundation.WorkItemTracking.Client;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VstsIndexer.UnitTests
{
    public class Tfs2013Service
    {
        public void GetWorkItems()
        {
            // Connect to the work item store
            TfsTeamProjectCollection tpc = new TfsTeamProjectCollection(
                  new Uri("https://sboinot.visualstudio.com"));
                   //new Uri("https://sboinot.visualstudio.com/tfs/DefaultCollection"));

            WorkItemStore workItemStore = (WorkItemStore)tpc.GetService(typeof(WorkItemStore));

            // Run a query.
            WorkItemCollection queryResults = workItemStore.Query(
               "Select * " +
               "From WorkItems " +
               "Where [System.WorkItemType] = 'User Story' ");// +
            //"Order By [System.State] Asc, [Changed Date] Desc");
            using (System.IO.StreamWriter file =
            new System.IO.StreamWriter(string.Format(@"C:\Temp\allAttachments_{0}.csv", Guid.NewGuid()),true, Encoding.UTF8))
            {
                file.WriteLine("item.Title;item.Description; item.IterationPath; item.CreatedBy; item.CreatedDate; item.ChangedDate; item.ChangedBy; item.State; attachment.Name; attachment.AttachedTime; attachment.Uri.AbsoluteUri");
                foreach (WorkItem item in queryResults)
                {
                    int i = item.AttachedFileCount;
                    if (i >= 1)
                    {
                        foreach (Attachment attachment in item.Attachments)
                        {
                            string name = attachment.Name;
                            string attachmentInformation = string.Format("{0};{1};{2};{3};{4};{5};{6};{7};{8};{9};{10}", item.Title, "desc", item.IterationPath, item.CreatedBy, item.CreatedDate, item.ChangedDate, item.ChangedBy, item.State, attachment.Name, attachment.AttachedTime, attachment.Uri.AbsoluteUri);
                            file.WriteLine(attachmentInformation);
                        }
                    }
                }
            }
            //Run a saved query.
            //QueryHierarchy queryRoot = workItemStore.Projects[1].QueryHierarchy;
            //QueryFolder folder = (QueryFolder)queryRoot["Requêtes partagées"];
            //QueryDefinition query = (QueryDefinition)folder["Backlog"];
            //queryResults = workItemStore.Query(query.QueryText);
        }

        public void GetTeamProjects()
        {
            // Connect to Team Foundation Server
            //     Server is the name of the server that is running the application tier for Team Foundation.
            //     Port is the port that Team Foundation uses. The default port is 8080.
            //     VDir is the virtual path to the Team Foundation application. The default path is tfs.
            Uri tfsUri = new Uri("https://tfs.axacolor.igo6.com/tfs");

            TfsConfigurationServer configurationServer =
                TfsConfigurationServerFactory.GetConfigurationServer(tfsUri);

            // Get the catalog of team project collections
            ReadOnlyCollection<CatalogNode> collectionNodes = configurationServer.CatalogNode.QueryChildren(
                new[] { CatalogResourceTypes.ProjectCollection },
                false, CatalogQueryOptions.None);

            // List the team project collections
            foreach (CatalogNode collectionNode in collectionNodes)
            {
                // Use the InstanceId property to get the team project collection
                Guid collectionId = new Guid(collectionNode.Resource.Properties["InstanceId"]);
                TfsTeamProjectCollection teamProjectCollection = configurationServer.GetTeamProjectCollection(collectionId);

                // Print the name of the team project collection
                Console.WriteLine("Collection: " + teamProjectCollection.Name);

                // Get a catalog of team projects for the collection
                ReadOnlyCollection<CatalogNode> projectNodes = collectionNode.QueryChildren(
                    new[] { CatalogResourceTypes.TeamProject },
                    false, CatalogQueryOptions.None);

                // List the team projects in the collection
                foreach (CatalogNode projectNode in projectNodes)
                {
                    Console.WriteLine(" Team Project: " + projectNode.Resource.DisplayName);
                                        
                }
            }
        }
    }
}
