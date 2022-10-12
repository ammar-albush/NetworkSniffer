using System.Text;

namespace NetworkSniffer
{
    public class DNSPacket
    {

        public byte[] Data { get; set; }

        public string Type { get; set; }
        public DNSPacket(byte[] data,string type)
        {
            Data = data;
            Type = type;

        }


        public string ID => Convert.ToString(Data[0], 16).ToUpper().PadLeft(2, '0') + Convert.ToString(Data[1], 16).ToUpper().PadLeft(2, '0');

        public enum QR_Type { request, response }

        public QR_Type QR => ((((Data[2] & 128) >> 7).ToString()) == "1") ? QR_Type.request : QR_Type.response;
        public enum OP_Code_Type { Standard_Query, Inverse_Query, Server_Status_Request }

        public OP_Code_Type OP_Code
        {
            get
            {

                string value = (((Data[2] & 120) >> 3).ToString());

                if (value == "0")
                    return OP_Code_Type.Standard_Query;
                else if (value == "1")
                    return OP_Code_Type.Inverse_Query;
                else return OP_Code_Type.Server_Status_Request;
            }

        }

        public enum Authoritative_Answer_Type { authoritative, non_authoritative }

        public Authoritative_Answer_Type Authoritative_Answer
        {
            get
            {

                string value = ((Data[2] & 4) >> 2).ToString();
                if (value == "0")
                    return Authoritative_Answer_Type.non_authoritative;
                else
                    return Authoritative_Answer_Type.authoritative;


            }
        }

        public enum Truncation_Type { exceeds_the_allowed_length_of_512_bytes, non_exceeds }

        public Truncation_Type Truncation
        {
            get
            {

                string value = ((Data[2] & 2) >> 1).ToString();
                if (value == "0")
                    return Truncation_Type.non_exceeds;
                else
                    return Truncation_Type.exceeds_the_allowed_length_of_512_bytes;


            }
        }

        public string Recursion_Desired => (Data[2] & 1).ToString();

        public string Recursion_Available => ((Data[3] & 128) >> 7).ToString();

        public string Zero => ((Data[3] & 112) >> 4).ToString();

        public enum Response_Code_Type { No_Error, Format_Error, Server_Failure, Name_Error, Not_Emplemented, Refused }


        public Response_Code_Type Response_Code
        {
            get
            {

                string result = (Data[3] & 15).ToString();

                if (result == "0")
                {

                    return Response_Code_Type.No_Error;

                }
                else if (result == "1")
                {

                    return Response_Code_Type.Format_Error;

                }
                else if (result == "2")

                    return Response_Code_Type.Server_Failure;

                else if (result == "3")
                    return Response_Code_Type.Name_Error;

                else if (result == "4")
                    return Response_Code_Type.Not_Emplemented;


                else return Response_Code_Type.Refused;


            }
        }


        public int Number_of_Questions
        {

            get {

                string result = ((Data[4] << 8) + Data[5]).ToString();

                return int.Parse(result); 
            
            }

        }

        public int Number_of_Answer
        {

            get
            {

                string result = ((Data[6] << 8) + Data[7]).ToString();

                return int.Parse(result);

            }

        }

        public int Number_of_Authority
        {

            get
            {

                string result = ((Data[8] << 8) + Data[9]).ToString();

                return int.Parse(result);

            }

        }

        public int Number_of_Additional
        {

            get
            {

                string result = ((Data[10] << 8) + Data[11]).ToString();

                return int.Parse(result);

            }

        }

        public List<DNS_INFO> DNS_INFO
        {

            get {

                List<DNS_INFO> result = new List<DNS_INFO>();

                byte[] dnsdata = Data;

                if (Data.Length > 12)
                {
                    int offset = 12;
                    int labelLen;
                    for (int i = 0; i < this.Number_of_Questions; i++)
                    {
                        string name = GetLabelName(Data, offset, out labelLen);
                        offset += labelLen;
                        offset++;
                        string Type = ((dnsdata[offset++] << 8) + dnsdata[offset]).ToString();
                        offset++;
                        string Class = ((dnsdata[offset++] << 8) + dnsdata[offset]).ToString();
                        offset++;
                        result.Add(new DNS_INFO("Question",name,labelLen, Type,Class));

                    }
                    for (int i = 0; i <Number_of_Answer; i++)
                    {
                        string name = GetLabelName(dnsdata, offset, out labelLen);
                        offset += labelLen;
                        offset++;
                        string Type = ((dnsdata[offset++] << 8) + dnsdata[offset]).ToString();
                        offset++;
                        string Class = ((dnsdata[offset++] << 8) + dnsdata[offset]).ToString();
                        offset++;
                        string TTL = ((dnsdata[offset++] << 24) + (dnsdata[offset++] << 16) + (dnsdata[offset++] << 8) + dnsdata[offset]).ToString();
                        offset++;
                        string Length = ((dnsdata[offset++] << 8) + dnsdata[offset]).ToString();
                        offset++;

                        string data = data_analysis(dnsdata, offset, int.Parse(Length), type_analysis(Type));
                        result.Add(new DNS_INFO("Answer", name,labelLen, data, Type, Class));
                        offset += int.Parse(Length);
                    }
                    for (int i = 0; i < Number_of_Authority; i++)
                    {

                        string name = GetLabelName(dnsdata, offset, out labelLen);
                        offset += labelLen;

                        offset++;
                        string Type = ((dnsdata[offset++] << 8) + dnsdata[offset]).ToString();

                        offset++;
                        string Class = ((dnsdata[offset++] << 8) + dnsdata[offset]).ToString();

                        offset++;
                        string TTL = ((dnsdata[offset++] << 24) + (dnsdata[offset++] << 16) + (dnsdata[offset++] << 8) + dnsdata[offset]).ToString();

                        offset++;
                        string Length = ((dnsdata[offset++] << 8) + dnsdata[offset]).ToString();
                        offset++;
                        string data = data_analysis(dnsdata, offset, int.Parse(Length), type_analysis(Type));
                        result.Add(new DNS_INFO("Authority", name, labelLen, data, Type, Class));

                        offset += int.Parse(Length);
                    }
                
                    for (int i = 0; i < Number_of_Additional; i++)
                    {

                        string name = GetLabelName(dnsdata, offset, out labelLen);
                        offset += labelLen;

                        offset++;
                        string Type = ((dnsdata[offset++] << 8) + dnsdata[offset]).ToString();

                        offset++;
                        string Class = ((dnsdata[offset++] << 8) + dnsdata[offset]).ToString();

                        offset++;
                        string TTL = ((dnsdata[offset++] << 24) + (dnsdata[offset++] << 16) + (dnsdata[offset++] << 8) + dnsdata[offset]).ToString();

                        offset++;
                        string Length = ((dnsdata[offset++] << 8) + dnsdata[offset]).ToString();
                        offset++;
                        string data = data_analysis(dnsdata, offset, int.Parse(Length), type_analysis(Type));

                        result.Add(new DNS_INFO("Additional", name,labelLen , data, Type, Class));

                        offset += int.Parse(Length);
                    }
           
                }

                return result;


            }
        }

         string GetLabelName(byte[] data, int offset, out int labelLen)
        {
            bool alreadyJump = false;
            int seek = offset;
            int len = data[seek];
            labelLen = 0;
            StringBuilder result = new StringBuilder(63);
            while (len > 0 && seek < data.Length)
            {
                if (len > 191 && len < 255)
                {
                    if (alreadyJump)
                    {
                        labelLen = seek - offset;
                        return result.ToString();
                    }
                    int tempLen;
                    result.Append(GetLabelName(data, data[++seek] + (len - 192) * 256, out tempLen));
                    alreadyJump = true;
                    labelLen = seek - offset;
                    return result.ToString();
                }
                else if (len < 64)
                {
                    for (; len > 0; len--)
                    {
                        result.Append((char)data[++seek]);
                    }
                    len = data[++seek];
                    if (len > 0) result.Append(".");
                }
            }
            labelLen = seek - offset;
            return result.ToString();
        }
        /// <summary>
        /// Type
        /// </summary>
         string type_analysis(string type)
        {
            switch (type)
            {
                case "1":
                    return "A";
                case "2":
                    return "NS";
                case "3":
                    return "MD";
                case "4":
                    return "MF";
                case "5":
                    return "CNAME";
                case "6":
                    return "SOA";
                case "7":
                    return "MB";
                case "8":
                    return "MG";
                case "9":
                    return "MR";
                case "10":
                    return "NULL";
                case "11":
                    return "WKS";
                case "12":
                    return "PTR";
                case "13":
                    return "HINFO";
                case "14":
                    return "MINFO";
                case "15":
                    return "MX";
                case "16":
                    return "TXT";
                case "28":
                    return "AAAA";
                case "32":
                    return "NB";
                case "41":
                    return "OPT";
                case "100":
                    return "UINFO";
                case "101":
                    return "UID";
                case "102":
                    return "GID";
                case "255":
                    return "ANY";
                default:
                    return "UNKOWN";
            }
        }
        /// <summary>
        /// Class
        /// </summary>
         string class_analysis(string class_int)
        {
            switch (class_int)
            {
                case "1":
                    return "IN";
                case "2":
                    return "CSNET";
                case "3":
                    return "CHAOS";
                case "4":
                    return "HESIOD";
                case "255":
                    return "ANY";
                default:
                    return "UNKOWN";
            }
        }

         string data_analysis(byte[] data, int offset, int dataLength, string type)
        {
            int labelLen;
            string NameServer = "";
            string Mail = "";
            switch (type)
            {
                case "A":
                    string address = "";
                    for (int i = 0; i < 4; i++)
                    {
                        address += data[offset++].ToString() + ".";
                    }
                    address = address.TrimEnd('.');
                    return address;
                case "CNAME":
                    string name = "";
                    name += GetLabelName(data, offset, out labelLen);
                    return name;
                case "MX":
                    int Preference;
                    Preference = data[offset++] << 8 + data[offset++];
                    Mail = GetLabelName(data, offset, out labelLen);
                    return "Preference = " + Preference + " | Mail = " + Mail;
                case "NS":
                    NameServer += GetLabelName(data, offset, out labelLen);
                    return NameServer;
                case "SOA":
                    int endOffset = offset + dataLength;
                    NameServer = GetLabelName(data, offset, out labelLen);
                    offset += labelLen;
                    Mail = GetLabelName(data, ++offset, out labelLen);
                    offset += labelLen;
                    offset++;
                    int Serial = data[offset++] << 24 + data[offset++] << 16 + data[offset++] << 8 + data[offset++];
                    int Refresh = data[offset++] << 24 + data[offset++] << 16 + data[offset++] << 8 + data[offset++];
                    int Retry = data[offset++] << 24 + data[offset++] << 16 + data[offset++] << 8 + data[offset++];
                    int Expire = data[offset++] << 24 + data[offset++] << 16 + data[offset++] << 8 + data[offset++];
                    int TTL = data[offset++] << 24 + data[offset++] << 16 + data[offset++] << 8 + data[offset++];
                    return "nameServer = " + NameServer + " | mail = " + Mail + " | serial = " + Serial.ToString() + " | refresh = " + Refresh.ToString() + " | ...";
                case "TXT":

                    int len = dataLength;
                    StringBuilder build = new StringBuilder(len);
                    for (; len > 0; len--)
                    {
                        build.Append((char)data[offset++]);
                    }
                    return build.ToString();
                default:
                    return "UNKOWN";
            }
        }



    }
}

