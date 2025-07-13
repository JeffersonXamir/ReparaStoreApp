using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReparaStoreApp.Common
{
    public class DocumentResponse
    {
        public bool Success { get; set; }
        public string Error { get; set; } = string.Empty;
        public int DocumentId { get; set; }
    }
}
