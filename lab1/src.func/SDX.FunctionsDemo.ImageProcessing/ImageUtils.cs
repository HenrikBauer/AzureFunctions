using System;
using System.IO;
using System.Linq;
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

        /// <summary>
        /// Unter MacOS und Ubuntu 18.04 gibt es mit SkiaSharp eine Fehlermeldung:
        ///     SkiaSharp: Unable to load shared library 'libSkiaSharp' or one of its dependencies. 
        ///     
        /// Ursache vermutlich: https://github.com/mono/SkiaSharp/issues/775
        /// Scheint behoben, aber noch nicht als nuget package verfügbar.
        /// 
        /// Der Workaround liefert vorberechnete Bilder aus den Manifest Ressourcen.
        /// </summary>
        public static byte[] ProcessImage(byte[] data, string imageType, string fileName)
        {
            var sleep = 3;
            Thread.Sleep(sleep * 1000);

            var os = Environment.GetEnvironmentVariable("OS");
            if (os == "Windows_NT")
                return ProcessImageReal(data, imageType);

            return ProcessImageFake(data, imageType, fileName);
        }

        static byte[] ProcessImageReal(byte[] data, string imageType)
        {
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

        static byte[] ProcessImageFake(byte[] data, string imageType, string fileName)
        {
            var names = typeof(ImageUtils).Assembly.GetManifestResourceNames();
            var prefix = typeof(ImageUtils).Namespace + ".FakeImages.";
            var name = prefix + Path.GetFileNameWithoutExtension(fileName) + "-" + imageType + ".png";
            name = names.Where(n => StringComparer.OrdinalIgnoreCase.Equals(n, name)).FirstOrDefault();
            if (name == null)
            {
                name = prefix + "ajdotnet-" + imageType + ".png";
                name = names.Where(n => StringComparer.OrdinalIgnoreCase.Equals(n, name)).FirstOrDefault();
            }

            using (var strm = typeof(ImageUtils).Assembly.GetManifestResourceStream(name))
            {
                var ms = new MemoryStream();
                strm.CopyTo(ms);
                return ms.ToArray();
            }
        }
    }
}
