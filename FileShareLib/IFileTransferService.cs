using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace FileShareLib
{
    
    [ServiceContract]
    public interface IFileTransferService
    {
        string UploadFolder { get; set; }

        [OperationContract]
        void UploadFile(RemoteFileInfo request);

        [OperationContract]
        RemoteFileInfo DownloadFile(DownloadRequest request);
    }
}
