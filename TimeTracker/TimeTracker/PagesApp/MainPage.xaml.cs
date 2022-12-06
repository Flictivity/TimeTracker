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
using System.Windows.Threading;
using TimeTracker.WindowsApp;

namespace TimeTracker.PagesApp
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        private string currentTime;
        public MainPage()
        {
            InitializeComponent();

            App.Dt.Tick += new EventHandler(dt_Tick);
            App.Dt.Interval = new TimeSpan(0, 0, 0, 0);

            App.CurrentSession = App.Connection.Sessions.FirstOrDefault(x => x.Categories.UserId == App.CurrentUser.IdUser);
            if(App.CurrentSession != null)
            {
                if(App.CurrentSession.Time != new TimeSpan(0))
                {
                    try
                    {
                        App.Category = App.CurrentSession.Categories;
                        App.StartTime = App.CurrentSession.TimeStart;
                        App.StopWatch = new StopWatchWithOffset(App.CurrentSession.Time);
                        App.StopWatch.Start();
                        App.Dt.Start();
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message);
                    }
                }
            }
        }

        private void EventNavigateTimeTrackPage(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new TimeTrackPage());
        }

        private void EventNavigateDayInfoPage(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new DayInfoPage());
        }

        private void EventNavigateTotalInfoPage(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new ReportPage());
        }

        private void EventNavigateCategoriesPage(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new CategoriesPage());
        }

        private void EventExit(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AuthorizationPage());
        }

        private void dt_Tick(object sender, EventArgs e)
        {
            var maxTs = new TimeSpan(23, 59, 59);
            if (App.StopWatch.IsRunning)
            {
                TimeSpan ts = App.StopWatch.ElapsedTimeSpan;
                if(ts >= maxTs)
                {
                    App.StopWatch.Stop();

                    var saveWindow = new SaveRecordWindow(App.Category);

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
                    ts.Hours, ts.Minutes, ts.Seconds);
                TblTimer.Text = currentTime;
                TblCategory.Text = App.Category.Name;
                PbProgressValue.Value = ts.TotalSeconds;

            }
            else
            {
                TblTimer.Text = "";
                TblCategory.Text = "";
                PbProgressValue.Value = 0;
            }
        }
    }
}
