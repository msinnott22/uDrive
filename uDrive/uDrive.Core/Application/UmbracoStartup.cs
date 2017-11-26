using System;
using System.Linq;
using uDrive.Core.Constants;
using uDrive.Core.Helpers;
using umbraco.BusinessLogic;
using umbraco.cms.businesslogic.packager;
using Umbraco.Core;
using Umbraco.Web.Trees;

namespace uDrive.Core.Application
{
    public class UmbracoStartup : ApplicationEventHandler
    {
        protected override void ApplicationStarted(UmbracoApplicationBase umbraco, ApplicationContext context)
        {
            //Add config setting for installation notification

            var install = new InstallHelper();

            install.AddTranslations();

            install.AddSection(context);

            InstalledPackage.BeforeDelete += InstalledPackage_BeforeDelete;
        }

        void InstalledPackage_BeforeDelete(InstalledPackage sender, EventArgs e)
        {
            if (sender.Data.Name == PackageConstants.SectionName)
            {
                var uninstall = new UninstallHelper();

                uninstall.RemoveTranslations();
                uninstall.RemoveSection();
            }
        }

        void TreeControllerBase_TreeNodesRendering(TreeControllerBase sender, TreeNodesRenderingEventArgs e)
        {
            var currentUser = User.GetCurrent();

            if (sender.TreeAlias == PackageConstants.SectionAlias && !currentUser.IsAdmin())
            {
                var settingNode = e.Nodes.SingleOrDefault(x => x.Id.ToString() == "settings");
                if (settingNode != null)
                {
                    e.Nodes.Remove(settingNode);
                }
            }
        }
    }
}
