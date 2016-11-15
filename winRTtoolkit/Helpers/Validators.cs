using System.Text.RegularExpressions;

namespace winRTtoolkit.Helpers
{
    public class Validators
    {
        public static bool IsValidEmail(string email)
        {
            var strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                 @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                 @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            var regex = new Regex(strRegex);
            return regex.IsMatch(email);
        }
    }
}
