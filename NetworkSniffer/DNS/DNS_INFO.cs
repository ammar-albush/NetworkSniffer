using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkSniffer
{
    public class DNS_INFO
    {

        public string RRs_Type { get; set; }

        public string DNS_NAME { get; set; }

        public int DNS_NAME_LENGTH { get; set; }
        public string DNS_DATA { get; set; }
        public string DNS_TYPE { get; set; }

        public string DNS_CLASS { get; set; }

        public DNS_INFO(string type, string d_name, int d_n_l, string d_type, string d_class)
        {

            RRs_Type = type;
            DNS_NAME = d_name;
            DNS_NAME_LENGTH = d_n_l;
            DNS_TYPE = d_type;
            DNS_CLASS = d_class;

        }
        public DNS_INFO(string type,string d_name,int d_n_l,string data,string d_type,string d_class)
        { 
        
            RRs_Type = type;
            DNS_NAME = d_name;
            DNS_NAME_LENGTH = d_n_l;
            DNS_TYPE = d_type;
            DNS_CLASS = d_class;
            DNS_DATA = data;
        
        }



    }
}
