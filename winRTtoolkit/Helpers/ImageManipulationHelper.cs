using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Graphics.Imaging;
using Windows.Storage.Streams;
using Windows.UI;
using Windows.UI.Xaml.Media.Imaging;

namespace winRTtoolkit.Helpers
{
    public static class ImageManipulationHelper
    {
        public static Task<BitmapImage> ReplaceColor(byte[] imagePixels, Color from, Color to, bool ignoreTransparency = true)
        {
            Delegate delegateMethod = new Func<byte[], Color, Color, bool, byte[]>(ApplyTransform);
            return ConvertImage(imagePixels, delegateMethod, new object[] { from, to, ignoreTransparency });
        }

        public static async Task<byte[]> GetImageByteArray(string url)
        {
            try
            {
                if (url == null)
                {
                    return null;
                }

                HttpClient client = new HttpClient();
                var buffer = await client.GetByteArrayAsync(new Uri(url));
                return buffer;
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception);
            }

            return null;
        }

        private static byte[] ApplyTransform(byte[] imagePixels, Color from, Color to, bool ignoreTransparency = true)
        {
            for (int i = 0; i < imagePixels.Length; i += 4)
            {
                byte[] pixel = imagePixels.SubArray(i, 4);

                var newPixel = TransformPixel(pixel, @from, to, ignoreTransparency);

                imagePixels[i] = newPixel[0];
                imagePixels[i + 1] = newPixel[1];
                imagePixels[i + 2] = newPixel[2];
                imagePixels[i + 3] = newPixel[3];
            }
            return imagePixels;
        }

        private static byte[] TransformPixel(byte[] pixel, Color @from, Color to, bool ignoreTransparency)
        {
            if (pixel[0] == @from.B && pixel[1] == @from.G && pixel[2] == @from.R &&
                (ignoreTransparency || pixel[3] == @from.A))
            {
                pixel[0] = to.B;
                pixel[1] = to.G;
                pixel[2] = to.R;
                if (!ignoreTransparency)
                {
                    pixel[3] = to.A;
                }
            }
            return pixel;
        }

        private static T[] SubArray<T>(this T[] data, int index, int length)
        {
            T[] result = new T[length];
            Array.Copy(data, index, result, 0, length);
            return result;
        }

        private static async Task<BitmapImage> ConvertImage(byte[] imageSource, Delegate convertingfunc, object[] delegateParams)
        {
            if (imageSource == null)
            {
                return null;
            }

            MemoryStream memoryStream = new MemoryStream(imageSource);

            using (IRandomAccessStream randomAccessStream = memoryStream.AsRandomAccessStream())
            {
                BitmapDecoder decoder = await BitmapDecoder.CreateAsync(randomAccessStream);
                PixelDataProvider provider =
                    await
                        decoder.GetPixelDataAsync(decoder.BitmapPixelFormat, decoder.BitmapAlphaMode,
                            new BitmapTransform(), ExifOrientationMode.RespectExifOrientation,
                            ColorManagementMode.ColorManageToSRgb);
                byte[] pixels = provider.DetachPixelData();

                var parameters = InsertInArrayFirstPosition(delegateParams, pixels);
                pixels = convertingfunc.DynamicInvoke(parameters) as byte[];

                return await GetProcessedBitmapImage(decoder, pixels);
            }
        }

        private static object[] InsertInArrayFirstPosition(object[] oldArray, object pixels)
        {
            var newArray = new object[oldArray.Length + 1];
            oldArray.CopyTo(newArray, 1);
            newArray[0] = pixels;
            return newArray;
        }

        private static async Task<BitmapImage> GetProcessedBitmapImage(BitmapDecoder decoder, byte[] pixels)
        {
            using (InMemoryRandomAccessStream memoryRandomAccessStream = new InMemoryRandomAccessStream())
            {
                var imageBytes = await EncodeImageBytes(memoryRandomAccessStream, decoder, pixels);
                using (InMemoryRandomAccessStream stream = new InMemoryRandomAccessStream())
                {
                    return await ImageBytesToBitmapImage(stream, imageBytes);
                }
            }
        }

        private static async Task<BitmapImage> ImageBytesToBitmapImage(InMemoryRandomAccessStream stream,
            byte[] imageBytes)
        {
            BitmapImage image = new BitmapImage();
            await stream.WriteAsync(imageBytes.AsBuffer());
            stream.Seek(0);
            image.SetSource(stream);
            return image;
        }

        private static async Task<byte[]> EncodeImageBytes(InMemoryRandomAccessStream memoryRandomAccessStream,
            BitmapDecoder decoder, byte[] pixels)
        {
            BitmapEncoder encoder =
                await BitmapEncoder.CreateAsync(BitmapEncoder.PngEncoderId, memoryRandomAccessStream);
            encoder.SetPixelData(decoder.BitmapPixelFormat, decoder.BitmapAlphaMode, decoder.PixelWidth,
                decoder.PixelHeight, 96, 96, pixels);

            await encoder.FlushAsync();

            var imageBytes = new byte[memoryRandomAccessStream.Size];

            await
                memoryRandomAccessStream.ReadAsync(imageBytes.AsBuffer(), (uint)memoryRandomAccessStream.Size,
                    InputStreamOptions.None);

            return imageBytes;
        }
    }
}