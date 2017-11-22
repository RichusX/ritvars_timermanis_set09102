using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace MessagingApp
{
    public class ID
    {
        public string recentTweet = getRecent(0);
        public string recentText = getRecent(1);
        public string recentEmail = getRecent(2);

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


            id = int.Parse(idToIncrement.Substring(1, 9));
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
        public static string getRecent(int _type) // 0 - Tweet; 1 - SMS; 2 - Email
        {
            int id = 0;
            string id_type = "";

            int largestID = 0;

            foreach (var i in MainWindow.json.messages)
            {
                id = int.Parse(i.ID.Substring(1, 9));
                id_type = i.ID.Substring(0, 1);

                switch (_type)
                {
                    case (0):
                        if (id_type == "T")
                        {
                            if (id > largestID)
                            {
                                largestID = id;
                            }
                        }
                        break;
                    case (1):
                        if (id_type == "S")
                        {
                            if (id > largestID)
                            {
                                largestID = id;
                            }
                        }
                        break;
                    case (2):
                        if (id_type == "E")
                        {
                            if (id > largestID)
                            {
                                largestID = id;
                            }
                        }
                        break;
                    default:
                        break;
                }
            }

            switch (_type)
            {
                case (0):
                    id_type = "T";
                    break;
                case (1):
                    id_type = "S";
                    break;
                case (2):
                    id_type = "E";
                    break;
            }

            string toReturn = id_type + formatID(largestID);
            return toReturn;
        }

        public static string formatID(int _id)
        {
            string toReturn = _id.ToString();
            return toReturn.PadLeft(9, '0');
        }

        public static string newID(string _currentID)
        {
            string toReturn = "";

            int id = int.Parse(_currentID.Substring(1, 9));
            string id_type = _currentID.Substring(0, 1);

            id++;

            toReturn = id_type + formatID(id);

            return toReturn;
        }
    }
}
