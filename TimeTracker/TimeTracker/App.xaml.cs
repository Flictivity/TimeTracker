using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using TimeTracker.AdoApp;

namespace TimeTracker
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static TimeTrackerEntities Connection = new TimeTrackerEntities();
        public static Users CurrentUser = new Users();
    }
}
