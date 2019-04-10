using System;
using System.IO;
using System.Linq;
using SkiaSharp;

namespace SDX.FunctionsDemo.ImageProcessing
{
    internal static class ImageProcessor
    {
      public  static byte[] ResizePng(byte[] data, int size)
        {
            using (var bitmap = SKBitmap.Decode(data))
            {
                var widthHeight = Math.Min(bitmap.Width, bitmap.Height);
                var scale = (float)size / widthHeight;
                var newInfo = new SKImageInfo((int)(bitmap.Width * scale), (int)(bitmap.Height * scale));

                using (var scaled = bitmap.Resize(newInfo, SKFilterQuality.High))
                    return ToPngData(scaled);
            }
        }

        public static byte[] GrayScale(byte[] data, int size)
        {
            using (var bitmap = SKBitmap.Decode(data))
            {
                var widthHeight = Math.Min(bitmap.Width, bitmap.Height);
                var scale = (float)size / widthHeight;
                var newInfo = new SKImageInfo((int)(bitmap.Width * scale), (int)(bitmap.Height * scale));
                newInfo = newInfo.WithColorType(SKColorType.Gray8);

                using (var scaled = bitmap.Resize(newInfo, SKFilterQuality.High))
                    return ToPngData(scaled);
            }
        }

        public static byte[] CreateRoundImage(byte[] data, int size)
        {
            using (var bitmap = SKBitmap.Decode(data))
            using (var resultBitmap = new SKBitmap(size, size))
            using (var canvas = new SKCanvas(resultBitmap))
            using (var path = new SKPath())
            {
                var widthHeight = Math.Min(bitmap.Width, bitmap.Height);
                var scale = (float)size / widthHeight;

                canvas.Clear();
                path.AddCircle(size / 2, size / 2, size / 2);
                canvas.ClipPath(path);
                canvas.Scale(scale, scale);
                canvas.DrawBitmap(bitmap, (widthHeight - bitmap.Width) / 2, (widthHeight - bitmap.Height) / 2);

                return ToPngData(resultBitmap);
            }
        }

        public static byte[] Recolor(byte[] data, int size)
        {
            var bitmap = SKBitmap.Decode(data);
            var widthHeight = Math.Min(bitmap.Width, bitmap.Height);
            var scale = (float)size / widthHeight;

            using (var resultBitmap = new SKBitmap(size, size))
            using (var canvas = new SKCanvas(resultBitmap))
            using (var paint = new SKPaint())
            {
                var colorTable = Enumerable.Range(0, 256).Select(i => (byte)(0xC0 & i)).ToArray();
                paint.ColorFilter = SKColorFilter.CreateTable(null, null, colorTable, colorTable);
                canvas.Scale(scale, scale);
                canvas.DrawBitmap(bitmap, (widthHeight - bitmap.Width) / 2, (widthHeight - bitmap.Height) / 2, paint);

                return ToPngData(resultBitmap);
            }
        }

        private static byte[] ToPngData(SKBitmap bitmap)
        {
            var pixels = bitmap.PeekPixels();
            using (var resultData = pixels.Encode(SKPngEncoderOptions.Default))
            {
                var ms = new MemoryStream();
                resultData.AsStream().CopyTo(ms);
                return ms.ToArray();
            }
        }
    }
}
