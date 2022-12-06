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
        string currentTime = string.Empty;
        public TimeTrackPage()
        {
            InitializeComponent();
            App.Dt.Tick += new EventHandler(dt_Tick);
            App.Dt.Interval = new TimeSpan(0, 0, 0, 0);

            LbCategories.ItemsSource = App.Connection.Categories.Where(x => x.UserId == App.CurrentUser.IdUser).ToList();

            if(App.StopWatch.IsRunning)
            {
                LbCategories.SelectedItem = App.Category;
            }
        }
        void dt_Tick(object sender, EventArgs e)
        {
            var maxTs = new TimeSpan(23, 59, 59);

            if (App.StopWatch.IsRunning)
            {
                TimeSpan ts = App.StopWatch.ElapsedTimeSpan;
                if(ts >= maxTs)
                {
                    App.StopWatch.Stop();
                    var category = LbCategories.SelectedItem as Categories;
                    var saveWindow = new SaveRecordWindow(category);

                    if (saveWindow.ShowDialog() == true)
                    {
                        App.StopWatch.Reset();
                        TblTimer.Text = "00:00:00";
                    }
                    else
                    {
                        App.StopWatch.Reset();
                        TblTimer.Text = "00:00:00";
                    }
                }
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
            App.StartTime = DateTime.Now.TimeOfDay;
            App.Category = LbCategories.SelectedItem as Categories;
            App.StopWatch = new StopWatchWithOffset(new TimeSpan(0));
            App.StopWatch.Start();
            App.Dt.Start();
        }

        private void EventStopWatch(object sender, RoutedEventArgs e)
        {
            if (App.StopWatch.IsRunning)
            {
                var messageWindow = new TimerStopMessageWindow();
                if(messageWindow.ShowDialog() == true)
                {
                    var category = LbCategories.SelectedItem as Categories;
                    var saveWindow = new SaveRecordWindow(category);

                    if (saveWindow.ShowDialog() == true)
                    {
                        App.StopWatch.Reset();
                        TblTimer.Text = "00:00:00";
                    }
                }
                else
                {
                    App.StopWatch.Reset();
                    TblTimer.Text = "00:00:00";
                }
            }
            else
            {
                App.StopWatch.Reset();
                TblTimer.Text = "00:00:00";
            }
        }

        private void EventResetWatch(object sender, RoutedEventArgs e)
        {
            App.StopWatch.Reset();
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

            var saveWindow = new SaveRecordWindow(category);
            if(saveWindow.ShowDialog() == true)
            {
                App.StopWatch.Reset();
                TblTimer.Text = "00:00:00";
            }
        }
    }
}
