using System;
using System.Collections.Generic;
using System.IO;

namespace FileShareLibrary
{
    public class FileTransferService : IFileTransferService
    {
        public string UploadFolder { get; set; } = @"C:\DirectoryWCFFile\FileTransfer";

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

        public string[] ListFiles()
        {
            string[] files = Directory.GetFiles(UploadFolder);
            List<string> filesName = new List<string>();
            foreach (var pathFile in files)
                filesName.Add(new FileInfo(pathFile).Name);
            return filesName.ToArray();
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
