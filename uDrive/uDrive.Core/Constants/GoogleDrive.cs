namespace uDrive.Core.Constants
{
    public class GoogleDriveConstants
    {
        public partial class Fields
        {
            public const string Kind = "kind";
            public const string NextPageToken = "nextPageToken";
            public const string Files = "files";
            public const string IncompleteSearch = "incompleteSearch";
            public const string Id = "id";
            public const string Name = "name";
            public const string MimeType = "mimeType";
            public const string Description = "description";
            public const string WebViewLink = "webViewLink";
            public const string WebContentLink = "webContentLink";
            public const string ThumbnailLink = "thumbnailLink";
            public const string CreatedTime = "createdTime";
            public const string ModifiedTime = "modifiedTime";
            public const string OriginalFileName = "originalFileName";
            public const string FileExtension = "fileExtension";
            public const string Size = "size";
            public const string ImageWidth = "imageMediaMetadata.width";
            public const string ImageHeight = "imageMediaMetadata.height";

            public static string GetAllFileFields()
            {
                return string.Join(",", JoinStrings());
            }

            private static string[] JoinStrings()
            {
                return new string[] {
                    Id,
                    Name,
                    MimeType,
                    Description,
                    WebViewLink,
                    WebContentLink,
                    ThumbnailLink,
                    CreatedTime,
                    ModifiedTime,
                    OriginalFileName,
                    FileExtension,
                    Size,
                    ImageWidth,
                    ImageHeight
                };
            }
        }        
    }
}
