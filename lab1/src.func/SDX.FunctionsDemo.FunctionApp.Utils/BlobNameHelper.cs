using System;
using SDX.FunctionsDemo.ImageProcessing;

namespace SDX.FunctionsDemo.FunctionApp.Utils
{
    /// <summary>Diese Klasse hilft, die Namenskonvention für die abgelegten Bilder einzuhalten.</summary>
    public static class BlobNameHelper
    {
        public static string CreateBlobName(string id)
        {
            return CreateBlobName(id, ImageUtils.ImageTypeOriginal);
        }

        public static string CreateBlobName(string id, string imageType)
        {
            return $"{id}-{imageType}.png";
        }
    }
}
