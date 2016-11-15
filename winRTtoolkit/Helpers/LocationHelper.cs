using System;

namespace winRTtoolkit.Helpers
{
    public enum DistanceType { Miles, Kilometers };
    public static class LocationHelper
    {
        private const int KM = 6371;

        private const int MILES = 3960;

        public static double Distance(double pos1Lat, double pos1Long, double pos2Lat, double pos2Long, DistanceType type)
        {
            double r = (type == DistanceType.Miles) ? MILES : KM;
            var dLat = ToRadian(pos2Lat - pos1Lat);
            var dLon = ToRadian(pos2Long - pos1Long);
            var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                Math.Cos(ToRadian(pos1Lat)) * Math.Cos(ToRadian(pos2Lat)) *
                Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
            var c = 2 * Math.Asin(Math.Min(1, Math.Sqrt(a)));
            var d = r * c;
            return d;
        }

        private static double ToRadian(double val)
        {
            return (Math.PI / 180) * val;
        }
    }
}
