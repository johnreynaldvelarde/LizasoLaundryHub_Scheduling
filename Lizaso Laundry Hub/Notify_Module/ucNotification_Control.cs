using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lizaso_Laundry_Hub.Notify_Module
{
    public partial class ucNotification_Control : UserControl
    {
        public NotificationLog Log { get; private set; }

        public ucNotification_Control(NotificationLog log)
        {
            InitializeComponent();
            Log = log;
            ShowNotification();
        }

        public void ShowNotification()
        {
            int LogID = Log.LogID;
            
            txt_Description.Text = Log.Description;

            // Calculate the time difference
            TimeSpan timeDifference = DateTime.Now - Log.LogDate;

            // Assign the result of the conditional expression to a variable
            string elapsedTime = FormatElapsedTime(timeDifference);

            // Update the Label_Time text using the variable
            Label_Time.Text = elapsedTime;
        }

        private string FormatElapsedTime(TimeSpan timeDifference)
        {
            if (timeDifference.TotalMinutes < 1)
            {
                return "Just now";
            }
            else if (timeDifference.TotalMinutes < 60)
            {
                int minutes = (int)timeDifference.TotalMinutes;
                return $"{minutes} minute{(minutes != 1 ? "s" : "")} ago";
            }
            else if (timeDifference.TotalHours < 24)
            {
                int hours = (int)timeDifference.TotalHours;
                return $"{hours} hour{(hours != 1 ? "s" : "")} ago";
            }
            else
            {
                return Log.LogDate.ToString();
            }
        }

        private int CalculateTextHeight(string text, Font font, int width)
        {
            using (Graphics g = this.CreateGraphics())
            {
                SizeF size = g.MeasureString(text, font, width);
                return (int)size.Height;
            }
        }
    }
}
