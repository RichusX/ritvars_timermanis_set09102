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

        string headerTwitterPattern = @"@(\w+)";
        string bodyTwitterPattern = ""; // TODO
        public MainWindow()
        {
            InitializeComponent();
        }

        private void SendBtn_Click(object sender, RoutedEventArgs e)
        {
            header = MsgHeaderBox.Text;
            body = MsgBodyBox.Text;

            Match result = Regex.Match(header, headerTwitterPattern);
            if (result.Success)
            {
                MessageBox.Show("Woohoo, that's a twitter tag!");
            }


        }

        private void ClearBtn_Click(object sender, RoutedEventArgs e)
        {
            MsgHeaderBox.Text = ""; // Clear header text box
            MsgBodyBox.Text = ""; // Clear body text box
        }
    }
}
