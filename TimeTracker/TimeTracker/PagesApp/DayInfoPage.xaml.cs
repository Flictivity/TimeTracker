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

            FillRecords();
        }

        private void EventIncrementDay(object sender, RoutedEventArgs e)
        {
            currentDate = currentDate.AddDays(1);
            TblDate.Text = currentDate.ToString("dd/MM/yyyy");

            FillRecords();
        }

        private void EventDecrementDay(object sender, RoutedEventArgs e)
        {
            currentDate = currentDate.AddDays(-1);
            TblDate.Text = currentDate.ToString("dd/MM/yyyy");

            FillRecords();
        }

        private void EventAddActivity(object sender, RoutedEventArgs e)
        {
            var addActivityWindow = new AddActivityWindow();
            if(addActivityWindow.ShowDialog() == true)
            {
                FillRecords();
            }
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((DateTime)value).ToString("hh:mm:ss");
        }

        private void FillRecords()
        {
            _records = App.Connection.Records.Where(x => x.Date == currentDate && x.Categories.UserId == App.CurrentUser.IdUser)
                .OrderBy(z => z.TimeStart).ToList();
            LvDayInfo.ItemsSource = null;
            LvDayInfo.ItemsSource = _records;
        }
    }
}
