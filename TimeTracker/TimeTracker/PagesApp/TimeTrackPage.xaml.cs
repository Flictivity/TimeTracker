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
using TimeTracker.AdoApp;
using TimeTracker.WindowsApp;

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
        public TimeTrackPage()
        {
            InitializeComponent();
            dt.Tick += new EventHandler(dt_Tick);
            dt.Interval = new TimeSpan(0, 0, 0, 0);

            LbCategories.ItemsSource = App.Connection.Categories.Where(x => x.UserId == App.CurrentUser.IdUser).ToList();
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
            if(LbCategories.SelectedItem == null)
            {
                MessageBox.Show("Не выбрана категория!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            sw.Start();
            dt.Start();
        }

        private void EventStopWatch(object sender, RoutedEventArgs e)
        {
            if (sw.IsRunning)
            {
                var messageWindow = new TimerStopMessageWindow();
                if(messageWindow.ShowDialog() == true)
                {
                    var category = LbCategories.SelectedItem as Categories;
                    var saveWindow = new SaveRecordWindow(sw, category);

                    if (saveWindow.ShowDialog() == true)
                    {
                        sw.Reset();
                        TblTimer.Text = "00:00:00";
                    }
                }
                else
                {
                    sw.Reset();
                    TblTimer.Text = "00:00:00";
                }
            }
        }

        private void EventResetWatch(object sender, RoutedEventArgs e)
        {
            sw.Reset();
            TblTimer.Text = "00:00:00";
        }

        private void EventSaveRecord(object sender, RoutedEventArgs e)
        {
            var category = LbCategories.SelectedItem as Categories;
            if(category == null)
            {
                MessageBox.Show("Не выбрана категория!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var saveWindow = new SaveRecordWindow(sw, category);
            if(saveWindow.ShowDialog() == true)
            {
                sw.Reset();
                TblTimer.Text = "00:00:00";
            }
        }
    }
}
