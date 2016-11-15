using System.Globalization;
using Windows.UI;

namespace winRTtoolkit.Helpers
{
    public class ColorConverter
    {
        public static Color ConvertFromString(string argb)
        {
            uint result;
            if (uint.TryParse(argb.TrimStart('#', '0'), NumberStyles.HexNumber, null, out result))
            {
                uint a = 0xFF;
                uint r = result >> 16;
                uint g = (result << 8) >> 16;
                uint b = (result << 16) >> 16;

                return Color.FromArgb((byte)a, (byte)r, (byte)g, (byte)b);
            }
            return Colors.Black;
        }
    }
}
