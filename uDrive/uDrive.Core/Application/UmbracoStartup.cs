using System;
using uDrive.Core.Constants;
using Umbraco.Core;
using Umbraco.Core.Models;

namespace uDrive.Core.Application
{
    public class UmbracoStartup : ApplicationEventHandler
    {
        protected override void ApplicationStarted(UmbracoApplicationBase umbraco, ApplicationContext context)
        {
            Section section = context.Services.SectionService.GetByAlias(PackageConstants.SectionAlias);
            if (section != null)
            {
                return;
            }
            
            context.Services.SectionService.MakeNew(PackageConstants.SectionName, PackageConstants.SectionAlias,
                PackageConstants.SectionIcon, sortOrder: 8);
        }
    }
}
