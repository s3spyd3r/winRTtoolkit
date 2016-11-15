using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Windows.Storage;

namespace winRTtoolkit.IO
{
    public class Storage
    {
        public static async Task<T> GetContentFileDeserializedAsync<T>(string uri)
        {
            try
            {
                string fileContent;
                StorageFile file = await StorageFile.GetFileFromApplicationUriAsync(new Uri(uri)).AsTask().ConfigureAwait(false);
                using (StreamReader sRead = new StreamReader(await file.OpenStreamForReadAsync()))
                    fileContent = await sRead.ReadToEndAsync();

                if (!string.IsNullOrEmpty(fileContent))
                    return JsonConvert.DeserializeObject<T>(fileContent);
                else
                    return default(T);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return default(T);
            }
        }

        public static async Task<T> GetContentFileDeserializedAsync<T>(StorageFolder folder, string fileName)
        {
            try
            {
                StorageFile file = await folder.GetFileAsync(fileName);
                string fileContent = await FileIO.ReadTextAsync(file);

                if (!string.IsNullOrEmpty(fileContent))
                    return JsonConvert.DeserializeObject<T>(fileContent);
                else
                    return default(T);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return default(T);
            }
        }

        public static async Task<bool> SetContentFileSerializedAsync<T>(StorageFolder folder, string fileName, T content)
        {
            try
            {
                string fileContent = JsonConvert.SerializeObject(content);
                StorageFile File =
                    await folder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);
                await FileIO.WriteTextAsync(File, fileContent);
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return false;
            }
        }

        public static Task<T> GetFromLocalSettings<T>(string key)
        {
            try
            {
                ApplicationDataContainer _localSettings = ApplicationData.Current.LocalSettings;

                if (_localSettings.Values.ContainsKey(key))
                    return Task.FromResult((T)_localSettings.Values[key]);
                else
                    return Task.FromResult(default(T));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return Task.FromResult(default(T));
            }
        }

        public static void SetIntoLocalSettings<T>(string key, T value)
        {
            try
            {
                ApplicationDataContainer _localSettings = ApplicationData.Current.LocalSettings;

                if (_localSettings.Values.ContainsKey(key))
                    _localSettings.Values[key] = value;
                else
                    _localSettings.Values.Add(key, value);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
    }
}
