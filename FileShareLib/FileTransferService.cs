using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace FileShareLib
{
    public class FileTransferService : IFileTransferService
    {
        public string UploadFolder { get; set; }

        public RemoteFileInfo DownloadFile(DownloadRequest request)
        {
            RemoteFileInfo result = new RemoteFileInfo();
            try
            {
                string filePath = Path.Combine(UploadFolder, request.FileName);
                FileInfo fileInfo = new FileInfo(filePath);

                if (!fileInfo.Exists)
                    throw new FileNotFoundException("File not found", request.FileName);

                FileStream stream = new FileStream(filePath,
                          FileMode.Open, FileAccess.Read);

                result.FileName = request.FileName;
                result.Length = fileInfo.Length;
                result.FileByteStream = stream;
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return result;
        }

        public void UploadFile(RemoteFileInfo request)
        {
            string filePath = Path.Combine(UploadFolder, Path.GetFileName(request.FileName));
            using (Stream sourceStream = request.FileByteStream)
            {
                using (FileStream targetStream = new FileStream(filePath, FileMode.Create,
                                                      FileAccess.Write, FileShare.None))
                {
                    sourceStream.CopyTo(targetStream);
                }
            }

        }
    }
}
