using System;
using System.IO;
using System.Linq;
using System.Threading;

namespace SDX.FunctionsDemo.ImageProcessing
{
    /// <summary>
    /// Unter MacOS gibt es mit SkiaShapr eine Fehlermeldung:
    ///     SkiaSharp: Unable to load shared library 'libSkiaSharp' or one of its dependencies. 
    ///     
    /// Ursache vermutlich: https://github.com/mono/SkiaSharp/issues/775
    /// Scheint behoben, aber noch nicht als nuget package verfügbar.
    /// 
    /// Der Workaround liefert vorberechnete Bilder aus den Manifest Ressourcen.
    /// </summary>
    public static class ImageUtilsFake
    {
        public static byte[] ProcessImage(byte[] data, string imageType, string fileName)
        {
            var sleep = 3;
            Thread.Sleep(sleep * 1000);

            var names = typeof(ImageUtilsFake).Assembly.GetManifestResourceNames();
            var prefix = typeof(ImageUtilsFake).Namespace + ".FakeImages.";
            var name = prefix + Path.GetFileNameWithoutExtension(fileName) + "-" + imageType + ".png";
            name = names.Where(n => StringComparer.OrdinalIgnoreCase.Equals(n, name)).FirstOrDefault();
            if (name == null)
            {
                name = prefix + "ajdotnet-" + imageType + ".png";
                name = names.Where(n => StringComparer.OrdinalIgnoreCase.Equals(n, name)).FirstOrDefault();
            }

            using (var strm = typeof(ImageUtilsFake).Assembly.GetManifestResourceStream(name))
            {
                var ms = new MemoryStream();
                strm.CopyTo(ms);
                return ms.ToArray();
            }
        }
    }
}
