using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace winRTtoolkit.IO
{
    public class BaseJsonStorage
    {
        public static Task<string> Serialize<T>(T type)
        {
            var result = string.Empty;

            if (type != null)
                result = JsonConvert.SerializeObject(type);

            return Task.FromResult(result);
        }

        public static Task<T> Deserialize<T>(string data)
        {
            try
            {
                T result = default(T);

                if (!string.IsNullOrEmpty(data))
                    result = JsonConvert.DeserializeObject<T>(data);

                return Task.FromResult(result);
            }
            catch (Exception)
            {
                return Task.FromResult(default(T));
            }
        }
    }
}
