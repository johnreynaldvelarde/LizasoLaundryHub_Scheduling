﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lizaso_Laundry_Hub
{
    public class In_Reserved_Class
    {
        public int BookingID { get; set; }
        public int CustomerID { get; set; }
        public int UnitID { get; set; }
        public string Customer_Name { get; set; }
        public string Unit_Name { get; set; }
        public string ServiceType { get; set; }
        public string ReservedStartTime { get; set; }
        public string ReservedEndTime { get; set; }
        public string Status { get; set; }
    }
}
