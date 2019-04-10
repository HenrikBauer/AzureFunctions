using System;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace SDX.FunctionsDemo.Web.Controllers
{
    static class Helpers
    {
        public static void SetMessage(this ViewDataDictionary viewDataDictionary, string messageType, string message)
        {
            viewDataDictionary["messageType"] = messageType;
            viewDataDictionary["message"] = message;
        }

        public static void SetImages(this ViewDataDictionary viewDataDictionary, string fileName, Tuple<string, string>[] images)
        {
            viewDataDictionary["fileName"] = fileName;
            viewDataDictionary["images"] = images;
        }

        public static byte[] CopyToArray(this IFormFile file)
        {
            using (var strm = file.OpenReadStream())
            {
                var ms = new MemoryStream();
                strm.CopyTo(ms);
                return ms.ToArray();
            }
        }

        public static string ToBase64String(this Stream image)
        {
            var ms = image as MemoryStream;
            if (ms == null)
            {
                ms = new MemoryStream();
                image.CopyTo(ms);
            }

            return Convert.ToBase64String(ms.ToArray());
        }

        public static string ToBase64String(this byte[] image)
        {
            return Convert.ToBase64String(image);
        }
    }
}
