using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFirstConsoleApp
{
    public  interface ICloudStorageProvide
    {
        void UploadFile(string path);

        void DownloadFile(string fileId);

        void DeleteFile(string fileId);
    }

    public class S3Storage : ICloudStorageProvide
    {
        public void UploadFile(string path)
        {
            Console.WriteLine($"Uploding : {path} to Amazon S3");
        }
        public void DownloadFile(string fileId)
        {
            Console.WriteLine($"Downloading : {fileId} from Amazon S3");
        }
        public void DeleteFile(string fileId)
        {
            Console.WriteLine($"Deleteing : {fileId} from Amazon S3");
        }
    }

    public class AzureStorage : ICloudStorageProvide
    {
        public void UploadFile(string path)
        {
            Console.WriteLine($"Uploding {path} to Azure Blob storage");
        }
        public void DownloadFile(string fileId)
        {
            Console.WriteLine($"Downloading {fileId} from Azure Blob storage");
        }
        public void DeleteFile(string fileId)
        {
            Console.WriteLine($"Deleteing {fileId} from Azure Blob storage");
        }
    }
}
