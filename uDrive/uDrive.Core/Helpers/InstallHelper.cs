using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uDrive.Core.Constants;
using Umbraco.Core;

namespace uDrive.Core.Helpers
{
    public class InstallHelper
    {
        public void AddTranslations()
        {
            TranslationHelper.AddTranslations();
        }

        public void AddSection(ApplicationContext applicationContext)
        {
            var sectionService = applicationContext.Services.SectionService;
            var uDriveSection = sectionService.GetSections().SingleOrDefault(x => x.Alias == PackageConstants.SectionAlias);
            if (uDriveSection == null)
            {
                sectionService.MakeNew(PackageConstants.SectionName, PackageConstants.SectionAlias, PackageConstants.SectionAlias);
            }
        }
    }
}
