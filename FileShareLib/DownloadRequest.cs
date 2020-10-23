using System.ServiceModel;

namespace FileShareLib
{
    [MessageContract]
    public class DownloadRequest
    {
        [MessageBodyMember]
        public string FileName;
    }
}
