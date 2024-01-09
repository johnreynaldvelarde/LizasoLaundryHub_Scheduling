using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lizaso_Laundry_Hub.Notify_Module
{
    public class NotificationLog
    {
        public int LogID { get; set; }
        public DateTime LogDate { get; set; }
        public string UserName { get; set; }
        public string ActivityType { get; set; }
        public string Description { get; set; }
    }
}
