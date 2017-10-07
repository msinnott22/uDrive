using System.Net.Http.Formatting;
using uDrive.Core.Constants;
using Umbraco.Web.Models.Trees;
using Umbraco.Web.Mvc;
using Umbraco.Web.Trees;

namespace uDrive.Core.Controllers
{
    [umbraco.businesslogic.Tree(PackageConstants.SectionAlias, "test", "Testing")]
    [PluginController(PackageConstants.SectionName)]
    public class UDriveTreeController : TreeController
    {
        protected override TreeNodeCollection GetTreeNodes(string id, FormDataCollection queryStrings)
        {
            TreeNodeCollection nodes = new TreeNodeCollection();

            if (id == "-1")
            {
                nodes.Add(CreateTreeNode("Test", id, queryStrings, "Item-Name", "icon-umb-media"));
            }

            return nodes;
        }

        protected override MenuItemCollection GetMenuForNode(string id, FormDataCollection queryStrings)
        {
            return new MenuItemCollection();
        }
    }
}
