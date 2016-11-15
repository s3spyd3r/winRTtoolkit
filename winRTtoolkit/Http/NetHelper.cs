using System.Net.NetworkInformation;
using Windows.Networking.Connectivity;

namespace winRTtoolkit.Http
{
    public class NetHelper
    {
        public static bool CheckNetworkConnection()
        {
            bool isConnected = NetworkInterface.GetIsNetworkAvailable();
            if (!isConnected)
            {
                return false;
            }
            else
            {
                ConnectionProfile internetConnectionProfile = NetworkInformation.GetInternetConnectionProfile();
                NetworkConnectivityLevel connection = internetConnectionProfile.GetNetworkConnectivityLevel();
                if (connection == NetworkConnectivityLevel.None || connection == NetworkConnectivityLevel.LocalAccess)
                {
                    return false;
                }
            }
            return true;
        }
    }
}