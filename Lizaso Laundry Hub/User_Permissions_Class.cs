using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lizaso_Laundry_Hub
{
    public class User_Permissions_Class
    {
        public bool Dashboard { get; set; }
        public bool Available_Services { get; set; }
        public bool Schedule { get; set; }
        public bool Customer_Manage { get; set; }
        public bool Payments { get; set; }
        public bool User_Manage { get; set; }
        public bool Inventory { get; set; }
        public bool Settings { get; set; }
    }
}
