using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using SDX.FunctionsDemo.ImageProcessing;

namespace SDX.FunctionsDemo.Web.Services
{
    public class InMemoryImageFileService : IImageFileService
    {
        static Dictionary<string, Dictionary<string, byte[]>> _images = new Dictionary<string, Dictionary<string, byte[]>>();

        Task<string> IImageFileService.UploadImageAsync(string fileName, string contentType, byte[] data)
        {
            var images = new Dictionary<string, byte[]>
            {
                [ImageUtils.ImageTypeOriginal] = data,
            };

            foreach (var imageType in ImageUtils.ImageTypes)
            {
                Debug.WriteLine("Processing " + imageType + " ...");
                images[imageType] = ImageUtils.ProcessImage(data, imageType, fileName);
            }

            var id = Guid.NewGuid().ToString();
            _images[id] = images;
            return Task.FromResult(id);
        }

        Task<byte[]> IImageFileService.GetImageAsync(string id, string imageType)
        {
            if (!_images.TryGetValue(id, out var images))
                return Task.FromResult((byte[])null);

            if (!images.TryGetValue(imageType, out var data))
                return Task.FromResult((byte[])null);

            return Task.FromResult(data);
        }
    }
}
