using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Text.RegularExpressions; // Regex
using System.IO;
using Newtonsoft.Json;
using System.Diagnostics;

namespace MessagingApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string header;
        public string body;
        public string subject;

        public static RootMessageObject messagesJSON;// = JsonConvert.DeserializeObject<RootMessageObject>(File.ReadAllText(@"messages.json"));
        public static RootHashtagObject hashtagsJSON;// = JsonConvert.DeserializeObject<RootHashtagObject>(File.ReadAllText(@"hashtags.json"));
        public static RootSirObject sirJSON;

        public static Dictionary<string, string> textspeakDict = File.ReadLines("data/textspeak.csv").Select(line => line.Split(',')).ToDictionary(line => line[0], line => line[1]);

        //public string messagesJSON;

        string headerTwitterPattern = @"^@(\w+)";
        string headerSmsPattern = @"^(((\+44\s?\d{4}|\(?0\d{4}\)?)\s?\d{3}\s?\d{3})|((\+44\s?\d{3}|\(?0\d{3}\)?)\s?\d{3}\s?\d{4})|((\+44\s?\d{2}|\(?0\d{2}\)?)\s?\d{4}\s?\d{4}))(\s?\#(\d{4}|\d{3}))?$";
        string headerEmailPattern = @"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*" + "@" + @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$";

        public MainWindow()
        {
            InitializeComponent();
        }

        private void SendBtn_Click(object sender, RoutedEventArgs e)
        {
            header = MsgHeaderBox.Text;
            subject = MsgSubjectBox.Text;
            body = MsgBodyBox.Text;

            Match tweetResult = Regex.Match(header, headerTwitterPattern);
            Match smsResult = Regex.Match(header, headerSmsPattern);
            Match emailResult = Regex.Match(header, headerEmailPattern);

            MessageBox.Show("Recent Tweet ID: "+ ID.getRecent(0)+"\nRecent SMS ID: "+ ID.getRecent(1)+"\nRecent E-Mail ID: "+ ID.getRecent(2));

            if (tweetResult.Success)
            {

                if (body.Length > 0 && body.Length <= 140)
                {
                    // length is between 1 and 140 ( all gucci! )
                    
                    Message tweet = new Message(ID.newID(ID.getRecent(0)), header, subject, Message.textspeakConvert(body));
                    tweet.updateHashtagsFromMessage();
                    messagesJSON.messages.Add(tweet);
                    writeMessagesToFile();
                    writeHashtagsToFile();
                }
                else
                {
                    if (body.Length == 0)
                    {
                        MessageBox.Show("Error. Message body cannot be empty!");
                    }
                    else
                    {
                        MessageBox.Show("Error. Message body for tweets cannot be longer than 140 characters!");
                    }

                }
            }
            else if (smsResult.Success)
            {
                
                if (body.Length > 0 && body.Length <= 140)
                {
                    // length is between 1 and 140 ( all gucci! )
                    Message sms = new Message(ID.newID(ID.getRecent(1)), header, subject, Message.textspeakConvert(body));
                    messagesJSON.messages.Add(sms);
                    writeMessagesToFile();
                }
                else
                {
                    if (body.Length == 0){
                        MessageBox.Show("Error. Message body cannot be empty!");
                    }
                    else
                    {
                        MessageBox.Show("Error. Message body for SMS cannot be longer than 140 characters!");
                    }
                    
                }
            }
            else if (emailResult.Success)
            {
                if (body.Length > 0 && body.Length <= 1028)
                {
                    if (subject.Length > 0 && subject.Length <= 20)
                    {
                        if (subject.StartsWith("SIR")) // Check if SIR email
                        {
                            MessageBox.Show("That's an SIR email!");
                            Message email = new Message(ID.newID(ID.getRecent(2)), header, subject, Message.quarantineURL(body));
                            SIR sir = new SIR(subject, ); // Left off here
                            messagesJSON.messages.Add(email);
                            writeMessagesToFile();
                        }
                        else // Normal email
                        {
                            Message email = new Message(ID.newID(ID.getRecent(2)), header, subject, Message.quarantineURL(body));
                            messagesJSON.messages.Add(email);
                            writeMessagesToFile();
                        }

                    }
                    else
                    {
                        MessageBox.Show("Error. Invalid subject. Try again!");
                    }
                }
                
            }
            else
            {
                MessageBox.Show("Incorrect sender. Try again!");
            }


        }

        private void ClearBtn_Click(object sender, RoutedEventArgs e)
        {
            MsgHeaderBox.Clear();
            MsgSubjectBox.Clear();
            MsgBodyBox.Clear();
        }

        private void MsgHeaderBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            Match emailResult = Regex.Match(MsgHeaderBox.Text, headerEmailPattern);
            if (emailResult.Success)
            {
                MsgSubjectBox.Visibility = System.Windows.Visibility.Visible;
                MsgSubjectLabel.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                MsgSubjectBox.Visibility = System.Windows.Visibility.Hidden;
                MsgSubjectLabel.Visibility = System.Windows.Visibility.Hidden;
                MsgSubjectBox.Clear();
            }
        }

        private void ViewMsgBtn_Click(object sender, RoutedEventArgs e)
        {
            ViewMessages viewMessages = new ViewMessages();

            viewMessages.Show();
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            Directory.CreateDirectory("data");

            if (File.Exists(@"data/messages.json"))
            {
                messagesJSON = JsonConvert.DeserializeObject<RootMessageObject>(File.ReadAllText(@"data/messages.json"));
            }
            else
            {
                string content = "{\"messages\": []}";
                File.WriteAllText(@"data/messages.json", content);
                messagesJSON = JsonConvert.DeserializeObject<RootMessageObject>(File.ReadAllText(@"data/messages.json"));
            }

            if (File.Exists(@"data/hashtags.json"))
            {
                hashtagsJSON = JsonConvert.DeserializeObject<RootHashtagObject>(File.ReadAllText(@"data/hashtags.json"));
            }
            else
            {
                string content = "{\"hashtags\": []}";
                File.WriteAllText(@"data/hashtags.json", content);
                hashtagsJSON = JsonConvert.DeserializeObject<RootHashtagObject>(File.ReadAllText(@"data/hashtags.json"));
            }

            if (File.Exists(@"data/SIR.json"))
            {
                sirJSON = JsonConvert.DeserializeObject<RootSirObject>(File.ReadAllText(@"data/SIR.json"));
            }
            else
            {
                string content = "{\"SIR\": []}";
                File.WriteAllText(@"data/SIR.json", content);
                sirJSON = JsonConvert.DeserializeObject<RootSirObject>(File.ReadAllText(@"data/SIR.json"));
            }



        }
        private void writeMessagesToFile()
        {
            File.WriteAllText(@"data/messages.json", JsonConvert.SerializeObject(messagesJSON, Formatting.Indented));
        }
        private void writeHashtagsToFile()
        {
            File.WriteAllText(@"data/hashtags.json", JsonConvert.SerializeObject(hashtagsJSON, Formatting.Indented));
        }

        private void writeSirToFile()
        {
            File.WriteAllText(@"data/SIR.json", JsonConvert.SerializeObject(sirJSON, Formatting.Indented));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ViewHashtags viewHashtags = new ViewHashtags();
            viewHashtags.Show();
        }
    }
}
