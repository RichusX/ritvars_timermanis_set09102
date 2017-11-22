using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessagingApp
{
    public class ID
    {
        public string recentTweet;
        public string recentText;
        public string recentEmail;

        public void increment(int _type) // 0 - Tweet; 1 - SMS; 2 - Email
        {
            string idToIncrement = "";
            int id;
            string id_type;

            switch (_type)
            {
                case (0):
                    idToIncrement = recentTweet;
                    break;
                case (1):
                    idToIncrement = recentText;
                    break;
                case (2):
                    idToIncrement = recentEmail;
                    break;
                default:
                    break;
            }


            id = int.Parse(idToIncrement.Substring(1, 10));
            id_type = idToIncrement.Substring(0, 1);

            switch (_type)
            {
                case (0):
                    recentTweet = id_type + id;
                    break;
                case (1):
                    recentText = id_type + id;
                    break;
                case (2):
                    recentEmail = id_type + id;
                    break;
                default:
                    break;
            }
        }
    }
}
