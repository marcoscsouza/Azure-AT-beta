using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace webServicesDBInfra.Interfaces
{
    public interface IBlobService
    {
        Task<string> UploadAsync(Stream stream);
        Task DeleteAsync(string blobName);

    }
}
