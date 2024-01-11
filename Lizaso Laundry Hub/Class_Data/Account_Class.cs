using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lizaso_Laundry_Hub
{
    public class Account_Class
    {
        static int user_id;
        static string user_name;
        static byte is_SuperUSer;

        public int User_ID
        {
            get { return user_id; }
            set { user_id = value; }
        }

        public string User_Name
        {
            get { return user_name; }
            set { user_name = value; }
        }

        public byte Is_SuperUser
        {   
            get { return is_SuperUSer; }
            set { is_SuperUSer = value; }
        }

        //public int User_ID { get; set; }
        //public string User_Name { get; set; }
    }
}
