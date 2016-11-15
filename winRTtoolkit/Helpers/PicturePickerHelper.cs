using System;
using System.IO;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.FileProperties;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using winRTtoolkit.Model;

namespace winRTtoolkit.Helpers
{
    public class PicturePickerHelper : IPicturePickerService
    {
        private readonly IAndContinueToAsyncService _continueService;

        public PicturePickerHelper(IAndContinueToAsyncService continueService)
        {
            _continueService = continueService;
        }

        public async Task<PictureData> GetPicture()
        {
            FileOpenPicker openPicker = new FileOpenPicker
            {
                SuggestedStartLocation = PickerLocationId.PicturesLibrary,
                ViewMode = PickerViewMode.Thumbnail
            };
            openPicker.FileTypeFilter.Clear();
            openPicker.FileTypeFilter.Add(".png");
            openPicker.FileTypeFilter.Add(".jpg");
            openPicker.FileTypeFilter.Add(".bmp");

            StorageFile storageFile = null;


#if WINDOWS_PHONE_APP

            openPicker.PickSingleFileAndContinue();

            FileOpenPickerContinuationEventArgs args = (FileOpenPickerContinuationEventArgs) await _continueService.GetTaskToNextActivatedEvent();

            if (args != null)
            {
                if (args.Files.Count == 0) return null;

                storageFile = args.Files[0];
            }

#elif WINDOWS_APP

            storageFile = await openPicker.PickSingleFileAsync();
#endif

            if (storageFile != null)
            {
                var thumbnail =
                   await
                       storageFile.GetThumbnailAsync(ThumbnailMode.PicturesView, 100, ThumbnailOptions.ResizeThumbnail);

                using (var stream = thumbnail.AsStream())
                {
                    byte[] bytes = new byte[Convert.ToUInt32(thumbnail.Size)];
                    stream.Position = 0;
                    await stream.ReadAsync(bytes, 0, bytes.Length);
                    var randomAccessStream = new InMemoryRandomAccessStream();
                    var writer = new DataWriter(randomAccessStream.GetOutputStreamAt(0));
                    writer.WriteBytes(bytes);
                    await writer.StoreAsync();

                    return new PictureData() { PictureBytes = bytes, Location = storageFile.Path };
                }
            }

            return null;
        }
    }
}