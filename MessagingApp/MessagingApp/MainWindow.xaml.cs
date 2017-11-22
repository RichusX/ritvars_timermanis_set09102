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

        public static RootObject json = JsonConvert.DeserializeObject<RootObject>(File.ReadAllText(@"messages.json"));

        public string messagesJSON;

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
                    
                    Message tweet = new Message(ID.newID(ID.getRecent(0)), header, subject, body);
                    json.messages.Add(tweet);
                    writeJsonToFile();
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
                    Message sms = new Message(ID.newID(ID.getRecent(1)), header, subject, body);
                    json.messages.Add(sms);
                    writeJsonToFile();
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
                        }
                        else // Normal email
                        {
                            Message email = new Message(ID.newID(ID.getRecent(2)), header, subject, body);
                            json.messages.Add(email);
                            writeJsonToFile();
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
            }
        }

        private void ViewMsgBtn_Click(object sender, RoutedEventArgs e)
        {
            ViewMessages viewMessages = new ViewMessages();

            viewMessages.Show();
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            //RootObject json = JsonConvert.DeserializeObject<RootObject>(File.ReadAllText(@"messages.json"));
            //foreach (var i in json.messages)
            //{
            //    Debug.Print("ID: {0}\nSender: {1}\nSubject: {2}\nMessage: {3}\n", i.ID, i.sender, i.subject, i.message);
            //}
        }
        private void writeJsonToFile()
        {
            File.WriteAllText(@"messages.json", JsonConvert.SerializeObject(json));
        }
    }
}
