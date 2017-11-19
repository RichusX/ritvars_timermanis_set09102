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

namespace MessagingApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string header;
        public string body;

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
            body = MsgBodyBox.Text;

            Match tweetResult = Regex.Match(header, headerTwitterPattern);
            Match smsResult = Regex.Match(header, headerSmsPattern);
            Match emailResult = Regex.Match(header, headerEmailPattern);

            if (tweetResult.Success)
            {

                if (body.Length > 0 && body.Length <= 140)
                {
                    // length is between 1 and 140 ( all gucci! )
                    MessageBox.Show("Woohoo, that's a valid tweet!");
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
                    MessageBox.Show("Woohoo, that's a valid SMS!");
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
                    if (MsgSubjectBox.Text.Length > 0 && MsgSubjectBox.Text.Length <= 20)
                    {
                        MessageBox.Show("Woohoo, that's a valid email!");
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
    }
}
