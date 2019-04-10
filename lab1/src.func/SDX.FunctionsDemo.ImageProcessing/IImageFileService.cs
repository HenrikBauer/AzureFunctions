using System;
using System.Threading.Tasks;

namespace SDX.FunctionsDemo.ImageProcessing
{
    public interface IImageFileService
    {
        Task<string> UploadImageAsync(string fileName, string contentType, byte[] data);
        Task<byte[]> GetImageAsync(string id, string imageType);
    }
}
