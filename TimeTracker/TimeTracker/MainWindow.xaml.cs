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
using TimeTracker.AdoApp;
using TimeTracker.PagesApp;

namespace TimeTracker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Frame.Navigate(new AuthorizationPage());
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if(App.StopWatch.IsRunning)
            {
                var newSession = new Sessions
                {
                    Categories = App.Category,
                    Time = App.StopWatch.ElapsedTimeSpan,
                    TimeStart = App.StartTime
                };
                if (!App.Connection.Sessions.Any(x => x.Categories.UserId == App.CurrentUser.IdUser))
                {
                    App.Connection.Sessions.Add(newSession);
                    App.CurrentSession = newSession;
                    App.Connection.SaveChanges();
                }
                else
                {
                    App.CurrentSession.Categories = App.Category;
                    App.CurrentSession.Time = App.StopWatch.ElapsedTimeSpan;
                    App.CurrentSession.TimeStart = App.StartTime;
                    App.Connection.SaveChanges();
                }
            }
            else
            {
                try
                {
                    App.CurrentSession.Time = new TimeSpan(0);
                    App.CurrentSession.TimeStart = new TimeSpan(0);
                    App.Connection.SaveChanges();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
