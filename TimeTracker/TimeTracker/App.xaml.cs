using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using TimeTracker.AdoApp;
using TimeTracker.PagesApp;

namespace TimeTracker
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static TimeTrackerEntities Connection = new TimeTrackerEntities();
        public static Users CurrentUser = new Users();
        public static StopWatchWithOffset StopWatch = new StopWatchWithOffset(new TimeSpan(0));
        public static DispatcherTimer Dt = new DispatcherTimer();
        public static Categories Category;
        public static Sessions CurrentSession;
        public static TimeSpan StartTime;
    }
}
