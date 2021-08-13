using System;
using System.Collections.Generic;
using System.Text;
using teamcare.common.ReferenceData;

namespace teamcare.business.Models
{
    public class FileUploadModel : BaseModel
    {
        public string BlobName { get; set; }
        public string ContentType { get; set; }
        public string FileName { get; set; }

        public byte[] DocumentBytes { get; set; }

        public long FileSizeInBytes { get; set; }

        public string DestinationFolder { get; set; }
        public bool IsTemporary { get; set; }
    }
}
