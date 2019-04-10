using System.Threading;

namespace SDX.FunctionsDemo.ImageProcessing
{
    public static class ImageUtils
    {
        public const string ContentTypePng = "image/png";

        public const string ImageTypeOriginal = "original";

        public static readonly string[] ImageTypes = new string[]
        {
            "100",
            "200",
            "400",
            "500",
            "200 round",
            "200 gray",
            "200 recolor"
        };

        public static byte[] ProcessImage(byte[] data, string imageType)
        {
            var sleep = 3;
            Thread.Sleep(sleep * 1000);

            switch (imageType)
            {
                case "100": return ImageProcessor.ResizePng(data, 100);
                case "200": return ImageProcessor.ResizePng(data, 200);
                case "400": return ImageProcessor.ResizePng(data, 400);
                case "500": return ImageProcessor.ResizePng(data, 500);
                case "200 round": return ImageProcessor.CreateRoundImage(data, 200);
                case "200 gray": return ImageProcessor.GrayScale(data, 200);
                case "200 recolor": return ImageProcessor.Recolor(data, 200);
                default: return ImageProcessor.ResizePng(data, 50);
            }
        }
    }
}
