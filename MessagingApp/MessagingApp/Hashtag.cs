using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessagingApp
{

    public class Hashtag
    {
        public string name { get; set; }
        public int times_encountered { get; set; }

        public Hashtag(string _hashtag)
        {
            name = _hashtag;
            times_encountered = 1;
        }
    }

    public class RootHashtagObject
    {
        public List<Hashtag> hashtags { get; set; }
    }
}
