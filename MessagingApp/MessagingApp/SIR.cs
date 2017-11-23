using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessagingApp
{

    public class SIR
    {
        public string id { get; set; }
        public string sports_centre_code { get; set; }
        public string incident_nature { get; set; }

        public SIR(string _id, string _sports_centre_code, string _incident_nature)
        {
            id = _id;
            sports_centre_code = _sports_centre_code;
            incident_nature = _incident_nature;
        }
    }

    public class RootSirObject
    {
        public List<SIR> SIR { get; set; }
    }
}
