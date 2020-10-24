using System.ServiceModel;

namespace FileShareLibrary
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
