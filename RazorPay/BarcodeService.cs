using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZXing.Common;
using ZXing;
using NLog;

namespace RazorPay
{
    public class BarcodeService
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        public byte[] GenerateBarcode(string content)
        {

            try
            {
                var writer = new BarcodeWriter<Bitmap>
                {
                    Format = BarcodeFormat.CODE_128,
                    Options = new EncodingOptions
                    {
                        Height = 25,
                        Width = 120
                    },
                    Renderer = new BitmapRenderer() // Set the renderer instance here
                };

                using (var bitmap = writer.Write(content))
                {
                    using (var stream = new MemoryStream())
                    {
                        bitmap.Save(stream, ImageFormat.Png);
                        return stream.ToArray();
                    }
                }
            }
            catch(Exception ex) 
            {
                Logger.Info($"{ex.ToString()}");
                return null;
            }
        }
    }

    public class BitmapRenderer : ZXing.Rendering.IBarcodeRenderer<Bitmap>
    {
        public Bitmap Render(BitMatrix matrix, BarcodeFormat format, string content, EncodingOptions options)
        {
            var width = matrix.Width;
            var height = matrix.Height;
            var bitmap = new Bitmap(width, height);
            for (var y = 0; y < height; y++)
            {
                for (var x = 0; x < width; x++)
                {
                    var pixelColor = matrix[x, y] ? Color.Black : Color.White;
                    bitmap.SetPixel(x, y, pixelColor);
                }
            }
            return bitmap;
        }

        public Bitmap Render(BitMatrix matrix, BarcodeFormat format, string content)
        {
            return Render(matrix, format, content, null);
        }
    }
}
