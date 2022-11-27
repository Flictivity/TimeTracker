using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Threading;

namespace TimeTracker.PagesApp
{
    /// <summary>
    /// Interaction logic for TimeTrackPage.xaml
    /// </summary>
    public partial class TimeTrackPage : Page
    {
        DispatcherTimer dt = new DispatcherTimer();
        Stopwatch sw = new Stopwatch();
        string currentTime = string.Empty;
        long totalTime;
        public TimeTrackPage()
        {
            InitializeComponent();
            dt.Tick += new EventHandler(dt_Tick);
            dt.Interval = new TimeSpan(0, 0, 0, 0);
        }
        void dt_Tick(object sender, EventArgs e)
        {
            if (sw.IsRunning)
            {
                TimeSpan ts = sw.Elapsed;
                currentTime = String.Format("{0:00}:{1:00}:{2:00}",
                    ts.Hours,ts.Minutes, ts.Seconds);
                TblTimer.Text = currentTime;
            }
        }

        private void EventStartWatch(object sender, RoutedEventArgs e)
        {
            sw.Start();
            dt.Start();
        }

        private void EventStopWatch(object sender, RoutedEventArgs e)
        {
            if (sw.IsRunning)
            {
                sw.Stop();
                totalTime = sw.ElapsedTicks;
            }
        }

        private void EventResetWatch(object sender, RoutedEventArgs e)
        {
            sw.Reset();
            TblTimer.Text = "00:00:00";
        }

        private void EventSaveRecord(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
