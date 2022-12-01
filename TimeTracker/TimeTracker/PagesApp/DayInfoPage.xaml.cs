using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using TimeTracker.AdoApp;
using TimeTracker.WindowsApp;

namespace TimeTracker.PagesApp
{
    /// <summary>
    /// Interaction logic for DayInfoPage.xaml
    /// </summary>
    public partial class DayInfoPage : Page
    {
        private DateTime currentDate;
        private List<Records> _records;
        public DayInfoPage()
        {
            InitializeComponent();
            currentDate = DateTime.Today;
            TblDate.Text = currentDate.ToString("dd/MM/yyyy");

            _records = App.Connection.Records.Where(x => x.Date == currentDate && x.UserId == App.CurrentUser.IdUser).ToList();
            LvDayInfo.ItemsSource = _records;
        }

        private void EventIncrementDay(object sender, RoutedEventArgs e)
        {
            currentDate = currentDate.AddDays(1);
            TblDate.Text = currentDate.ToString("dd/MM/yyyy");

            _records = App.Connection.Records.Where(x => x.Date == currentDate && x.UserId == App.CurrentUser.IdUser).ToList();
            LvDayInfo.ItemsSource = null;
            LvDayInfo.ItemsSource = _records;
        }

        private void EventDecrementDay(object sender, RoutedEventArgs e)
        {
            currentDate = currentDate.AddDays(-1);
            TblDate.Text = currentDate.ToString("dd/MM/yyyy");

            _records = App.Connection.Records.Where(x => x.Date == currentDate && x.UserId == App.CurrentUser.IdUser).ToList();
            LvDayInfo.ItemsSource = null;
            LvDayInfo.ItemsSource = _records;
        }

        private void EventAddActivity(object sender, RoutedEventArgs e)
        {
            var addActivityWindow = new AddActivityWindow();
            if(addActivityWindow.ShowDialog() == true)
            {
                _records = App.Connection.Records.Where(x => x.Date == currentDate && x.UserId == App.CurrentUser.IdUser).ToList();
                LvDayInfo.ItemsSource = null;
                LvDayInfo.ItemsSource = _records;
            }
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((DateTime)value).ToString("hh:mm:ss");
        }
    }
}
