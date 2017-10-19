namespace uDrive.Core.Models.Google.User
{
    public class StorageQuota
    {
        public long Limit { get; set; }
        public long Usage { get; set; }
        public long UsageInDrive { get; set; }
        public long UsageInDriveTrash { get; set; }
    }
}