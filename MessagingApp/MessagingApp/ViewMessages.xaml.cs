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
using System.Windows.Shapes;

namespace MessagingApp
{
    /// <summary>
    /// Interaction logic for ViewMessages.xaml
    /// </summary>
    public partial class ViewMessages : Window
    {
        public ViewMessages()
        {
            InitializeComponent();
        }

        private void TypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            IDComboBox.Items.Clear();

            ComboBoxItem typeItem = (ComboBoxItem)TypeComboBox.SelectedItem;
            string selection = typeItem.Content.ToString();

            if (selection == "Tweet")
            {
                
                bool tweetsExist = false;
                foreach (var i in MainWindow.messagesJSON.messages)
                {
                    if (i.ID.StartsWith("T"))
                    {
                        IDComboBox.Items.Add(i.ID);

                        tweetsExist = true;
                    }
                }
                if (tweetsExist)
                {
                    IDComboBox.IsEnabled = true;
                }
                else
                {
                    IDComboBox.IsEnabled = false;
                }
                
            }
            else if (selection == "SMS") {
                
                bool smsExist = false;
                foreach (var i in MainWindow.messagesJSON.messages)
                {
                    if (i.ID.StartsWith("S"))
                    {
                        IDComboBox.Items.Add(i.ID);

                        smsExist = true;
                    }
                }

                if (smsExist)
                {
                    IDComboBox.IsEnabled = true;
                }
                else
                {
                    IDComboBox.IsEnabled = false;
                }
            }
            else if (selection == "E-Mail")
            {
                
                bool emailsExist = false;
                foreach (var i in MainWindow.messagesJSON.messages)
                {
                    if (i.ID.StartsWith("E"))
                    {
                        IDComboBox.Items.Add(i.ID);

                        emailsExist = true;
                    }
                }

                if (emailsExist)
                {
                    IDComboBox.IsEnabled = true;
                }
                else
                {
                    IDComboBox.IsEnabled = false;
                }
            }
            else
            {
                IDComboBox.IsEnabled = false;
            }
        }

        private void IDComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //ComboBoxItem typeItem = (ComboBoxItem)IDComboBox.SelectedItem;
            //string selection = typeItem.Content.ToString();
            if (IDComboBox.Items.Count != 0)
            {
                string selection = IDComboBox.SelectedItem.ToString();

                foreach (var message in MainWindow.messagesJSON.messages)
                {
                    if (message.ID == selection)
                    {
                        MsgTextBox.Clear();
                        MsgTextBox.AppendText("Sender: " + message.sender + "\n");
                        if (message.subject != "")
                        {
                            MsgTextBox.AppendText("Subject: " + message.subject + "\n");
                        }
                        MsgTextBox.AppendText("\nMessage: " + message.message + "\n");
                    }
                }
            }
            
        }
    }
}
