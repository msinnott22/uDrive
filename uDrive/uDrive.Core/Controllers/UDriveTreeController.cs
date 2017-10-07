using System.Collections.Generic;
using System.Net.Http.Formatting;
using uDrive.Core.Constants;
using uDrive.Core.Models;
using Umbraco.Web.Models.Trees;
using Umbraco.Web.Mvc;
using Umbraco.Web.Trees;

namespace uDrive.Core.Controllers
{
    [Tree(PackageConstants.SectionAlias, "udriveTree", "uDrive")]
    [PluginController(PackageConstants.SectionName)]
    public class UDriveTreeController : TreeController
    {
        protected override TreeNodeCollection GetTreeNodes(string id, FormDataCollection queryStrings)
        {
            TreeNodeCollection nodes = new TreeNodeCollection();

            if (id == "-1")
            {
                var mainRoute = "/udrive/udriveTree";
                var treeNodes = new List<SectionTreeNode>
                {
                    new SectionTreeNode()
                    {
                        Id = "settings",
                        Title = "Settings",
                        Icon = "icon-settings",
                        Route = string.Format("{0}/edit/{1}", mainRoute, "settings")
                    }
                };

                foreach (var treeNode in treeNodes)
                {
                    var nodeToAdd = CreateTreeNode(treeNode.Id, null, queryStrings, treeNode.Title, treeNode.Icon,
                        false, treeNode.Route);

                    nodes.Add(nodeToAdd);
                }

                return nodes;
            }

            return nodes;
        }

        protected override MenuItemCollection GetMenuForNode(string id, FormDataCollection queryStrings)
        {
            return new MenuItemCollection();
        }
    }
}
