namespace uDrive.Core.Models.Google.User
{
    public class User
    {
        public string Kind { get; set; }
        public string DisplayName { get; set; }
        public string PhotoLink { get; set; }
        public bool Me { get; set; }
        public string PermissionId { get; set; }
        public string EmailAddress { get; set; }
    }
}