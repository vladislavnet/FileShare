using System.ServiceModel;

namespace FileShareLibrary
{
    [MessageContract]
    public class DownloadRequest
    {
        [MessageBodyMember]
        public string FileName;
    }
}
