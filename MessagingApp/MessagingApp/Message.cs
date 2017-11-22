using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }

    public class RootObject
    {
        public List<Message> messages { get; set; }
    }
}
