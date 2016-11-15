using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace winRTtoolkit.PhoneTasks
{
    public class Launcher
    {
        /// <summary>
        /// Phone Task Launcher opening urls or specific services
        /// </summary>
        /// <param name="site/service url"></param>
        public async static Task LaunchUriAsync(string uri)
        {
            try
            {
                await Windows.System.Launcher.LaunchUriAsync(new Uri(uri, UriKind.Absolute));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
    }
}
