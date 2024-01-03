using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lizaso_Laundry_Hub
{
    public class DB_Connection
    {
        public string MyConnection()
        {
            string con = @"Data Source=LENOVO-PC\SQLEXPRESS;Initial Catalog=DB_Laundry;Integrated Security=True";
            return con;
        }
    }
}
