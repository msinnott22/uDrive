using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace uDrive.Core.Models.Google.Files
{
    public class AllFiles
    {
        public string Kind { get; set; }
        public string NextPageToken { get; set; }
        public List<File> Files { get; set; }
        public bool IncompleteSearch { get; set; }
    }
}
