using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;

namespace winRTtoolkit.PhoneTasks
{
    public class GPS
    {
        public async static Task<Geoposition> GetPositionAsync(int accuracy)
        {
            try
            {
                var geolocator = new Geolocator();
                if (geolocator.LocationStatus == PositionStatus.Disabled)
                    return null;

                geolocator.DesiredAccuracyInMeters = Convert.ToUInt32(accuracy);
                Geoposition position = await geolocator.GetGeopositionAsync();

                return position;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return null;
            }
        }
    }
}
