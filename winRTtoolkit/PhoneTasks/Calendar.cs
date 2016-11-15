using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Windows.ApplicationModel.Appointments;
using Windows.Foundation;
using Windows.UI.Popups;

namespace winRTtoolkit.PhoneTasks
{
    public class Calendar
    {
        public static async Task<string> CreateAppointments(DateTime start, string subject, bool allday, string location,
            string details)
        {
            try
            {
                var appointment = new Appointment
                {
                    StartTime = start,
                    Subject = subject,
                    AllDay = allday,
                    Location = location,
                    Details = details
                };

                string appointmentId =
                    await AppointmentManager.ShowAddAppointmentAsync(appointment, new Rect(), Placement.Default);
                return appointmentId;
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception);
                return null;
            }
        }
    }
}