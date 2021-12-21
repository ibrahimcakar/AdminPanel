using System;
using System.Collections.Generic;
using System.Text;

namespace Cogito.Services.Models
{
    public class EmailProperties
    {
        public string subject { get; set; }
        public string body { get; set; }
        public string fromAddress { get; set; }
        public string fromName { get; set; }
        public string toAddress { get; set; }
        public string toName { get; set; }
        public string replyTo { get; set; } = null;
        public string replyToName { get; set; } = null;
        public IEnumerable<string> bcc { get; set; } = null;
        public IEnumerable<string> cc { get; set; } = null;
        public string attachmentFilePath { get; set; } = null; 
        public string attachmentFileName { get; set; } = null;
        public int attachedDownloadId { get; set; } = 0;
        public IDictionary<string, string> headers { get; set; } = null;
    }
}
