using System;

namespace uDrive.Core.Models.Google.Files
{
    public class File
    {
        public string Kind { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string MimeType { get; set; }
        public string WebContentLink { get; set; }
        public string WebViewLink { get; set; }
        public string IconLink { get; set; }
        public bool HasThumbnail { get; set; }
        public string ThumbnailLink { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime ModifiedTime { get; set; }
        public bool Shared { get; set; }
        public string OriginalFilename { get; set; }
        public string FullFileExtension { get; set; }
        public string FileExtension { get; set; }
        public int Size { get; set; }
        public ImageMediaMetadata ImageMediaMetadata { get; set; }
    }
}
