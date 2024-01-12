using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;
using Calendar.NET;
using System.Data.SqlClient;
using Lizaso_Laundry_Hub.Class_Data;

namespace Lizaso_Laundry_Hub.Dashboard_Widget
{
    public partial class Calendar_Widget_Form : KryptonForm
    {
        private Get_Data_Class getData;

        public Calendar_Widget_Form()
        {
            InitializeComponent();
            getData = new Get_Data_Class();
        }

        public void DisplayGridAndCalendar()
        {
            getData.Get_CalendarBookingsInProgressAndReserved(grid_bookings_view);
        }

        private void Calendar_Widget_Form_Load(object sender, EventArgs e)
        {
            DisplayGridAndCalendar();
            UpdateDataAndCalendar();
        }

        private void UpdateDataAndCalendar()
        {
            var inProgressBookings = getData.Get_CalendarInProgressBookings();

            if (inProgressBookings != null)
            {
                UpdateCalendar(inProgressBookings);
            }
        }

        private void UpdateCalendar(List<Calendar_InProgress_Class> inProgressBookings)
        {
            // Assuming 'calendar1' is your custom calendar control
            calendar1.AllowEditingEvents = false;

            foreach (var booking in inProgressBookings)
            {
                var inProgressEvent = new CustomEvent
                {
                    //Date = booking.StartTime ,
                    Date = booking.StartTime ,
                    RecurringFrequency = RecurringFrequencies.None,
                    EventText = $"{booking.UnitName} is {booking.Status}"
                };

                calendar1.AddEvent(inProgressEvent);
            }
        }
    }
}
