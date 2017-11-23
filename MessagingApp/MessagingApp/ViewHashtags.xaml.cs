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
    /// Interaction logic for ViewHashtags.xaml
    /// </summary>
    public partial class ViewHashtags : Window
    {
        public ViewHashtags()
        {
            InitializeComponent();
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            foreach (var i in MainWindow.hashtagsJSON.hashtags)
            {
                HashtagBox.AppendText(i.name + ": " + i.times_encountered.ToString()+ "\n");
            }
        }
    }
}
