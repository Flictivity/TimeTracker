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

namespace TimeTracker.WindowsApp
{
    /// <summary>
    /// Interaction logic for TimerStopMessageWindow.xaml
    /// </summary>
    public partial class TimerStopMessageWindow : Window
    {
        public TimerStopMessageWindow()
        {
            InitializeComponent();
        }

        private void EventStop(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            this.Close();
        }

        private void EventSaveStop(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            this.Close();
        }
    }
}
