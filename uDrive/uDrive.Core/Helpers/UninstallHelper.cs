using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uDrive.Core.Constants;
using Umbraco.Web;

namespace uDrive.Core.Helpers
{
    public class UninstallHelper
    {
        public void RemoveTranslations()
        {
            TranslationHelper.RemoveTranslations();
        }

        public void RemoveSection()
        {
            var services = UmbracoContext.Current.Application.Services;
            var uDriveSection = services.SectionService.GetByAlias(PackageConstants.SectionAlias);
            if (uDriveSection != null)
            {
                services.SectionService.DeleteSection(uDriveSection);
            }
        }
    }
}
