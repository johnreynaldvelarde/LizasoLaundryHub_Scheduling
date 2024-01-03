using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lizaso_Laundry_Hub
{
    public class SelectUnitFormBridge
    {
        public ucUnit_Control UcUnit { get; private set; }

        public SelectUnitFormBridge(ucUnit_Control ucUnit)
        {
            UcUnit = ucUnit;
            
        }
    }
}
