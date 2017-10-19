using System.Collections.Generic;

namespace uDrive.Core.Models.Google.User
{
    public class UserInfo
    {
        public string Kind { get; set; }
        public User User { get; set; }
        public StorageQuota StorageQuota { get; set; }
        public ImportFormats ImportFormats { get; set; }
        public ExportFormats ExportFormats { get; set; }
        public MaxImportSizes MaxImportSizes { get; set; }
        public long MaxUploadSize { get; set; }
        public bool AppInstalled { get; set; }
        public string[] FolderColorPalette { get; set; }
        public List<TeamDriveTheme> TeamDriveThemes { get; set; }
    }
}
