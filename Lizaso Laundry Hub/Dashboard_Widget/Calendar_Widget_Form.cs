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

namespace Lizaso_Laundry_Hub.Dashboard_Widget
{
    public partial class Calendar_Widget_Form : KryptonForm
    {
        public Calendar_Widget_Form()
        {
            InitializeComponent();
            CalendarView();
        }

        public void CalendarView()
        {
            calendar1.AllowEditingEvents = false;
            
            var groundhogEvent = new HolidayEvent
            {
                Date = new DateTime(2024, 1, 1),
                EventText = "Groundhog Day",
                RecurringFrequency = RecurringFrequencies.Yearly
            };

            calendar1.AddEvent(groundhogEvent);

            var exerciseEvent = new CustomEvent
            {
                Date = DateTime.Now,
                RecurringFrequency = RecurringFrequencies.None,
                EventText = "Time for Exercise!"
            };

            calendar1.AddEvent(exerciseEvent);

            var exerciseEvent2 = new CustomEvent
            {
                Date = DateTime.Now,
                RecurringFrequency = RecurringFrequencies.None,
                EventText = "dadad!"
            };

            calendar1.AddEvent(exerciseEvent2);

        }
    }
}
