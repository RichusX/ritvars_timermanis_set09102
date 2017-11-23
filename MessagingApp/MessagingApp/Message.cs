using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Diagnostics;
using Newtonsoft.Json;
using System.IO;

namespace MessagingApp
{
    public class Message
    {
        public string ID { get; set; }
        public string sender { get; set; }
        public string subject { get; set; }
        public string message { get; set; }

        public Message(string _id, string _sender, string _subject, string _message)
        {
            ID = _id;
            sender = _sender;
            subject = _subject;
            message = _message;
        }

        public void updateHashtagsFromMessage()
        {
            List<string> hashtags = new List<string>();
            List<string> newHashtags = new List<string>();
            hashtags.Clear();


            MainWindow.hashtagsJSON = JsonConvert.DeserializeObject<RootHashtagObject>(File.ReadAllText(@"data/hashtags.json"));

            Regex hash = new Regex(@"(?:(?<=\s)|^)#(\w*[A-Za-z_]+\w*)");

            foreach (var hTag in hash.Matches(message))
            {
               if (!hashtags.Contains(hTag.ToString()))
                {
                    hashtags.Add(hTag.ToString());
                }
            }

            bool skip = false;
            foreach (string name in hashtags)
            {
                foreach (var jsontags in MainWindow.hashtagsJSON.hashtags)
                {
                    if (name == jsontags.name)
                    {
                        jsontags.times_encountered++;
                        skip = true;
                        break;
                    }
                }

                if (skip)
                {
                    skip = false;
                    continue;
                }
                else
                {
                    newHashtags.Add(name);
                }
            }


                if (hashtags.Count != 0)
            {
                foreach (string item in newHashtags)
                {
                    Debug.Write("in list: "+item+"\n");
                    Hashtag hashtag = new Hashtag(item);
                    MainWindow.hashtagsJSON.hashtags.Add(hashtag);
                }
            }
        }

        public static string textspeakConvert(string _message)
        {
            string result = _message;
            
            foreach (var i in MainWindow.textspeakDict)
            {
                string pattern = string.Format(@"\b{0}\b", Regex.Escape(i.Key.ToLower()));
                string replace = i.Value;

                result = Regex.Replace(result, pattern, replace, RegexOptions.IgnoreCase);
                
            }
            return result;
        }
    }

    public class RootMessageObject
    {
        public List<Message> messages { get; set; }
    }
}
