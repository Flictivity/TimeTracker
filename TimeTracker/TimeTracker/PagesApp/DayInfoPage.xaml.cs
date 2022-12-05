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
            DpCurrentDate.SelectedDate = currentDate;

            FillRecords();
        }

        private void EventIncrementDay(object sender, RoutedEventArgs e)
        {
            currentDate = currentDate.AddDays(1);
            TblDate.Text = currentDate.ToString("dd/MM/yyyy");
            DpCurrentDate.SelectedDate = currentDate;

            FillRecords();
        }

        private void EventDecrementDay(object sender, RoutedEventArgs e)
        {
            currentDate = currentDate.AddDays(-1);
            TblDate.Text = currentDate.ToString("dd/MM/yyyy");
            DpCurrentDate.SelectedDate = currentDate;

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

        private void FillRecords()
        {
            try
            {
                _records = App.Connection.Records.Where(x => x.Date == currentDate && x.Categories.UserId == App.CurrentUser.IdUser)
                .OrderBy(z => z.TimeStart).ToList();
                LvDayInfo.ItemsSource = null;
                LvDayInfo.ItemsSource = _records;
            }
            catch
            {
                MessageBox.Show("Не удалось загрузить данные!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DpCurrentDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            currentDate = DpCurrentDate.SelectedDate.Value;
            TblDate.Text = currentDate.ToString("dd/MM/yyyy");

            FillRecords();
        }

        private void LvDayInfo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(e.AddedItems.Count == 0)
            {
                return;
            }
            var window = new DeleteRecordWindow(LvDayInfo.SelectedItem as Records);
            if(window.ShowDialog() == true)
            {
                MessageBox.Show("Успешно.");
                LvDayInfo.ItemsSource = null;
                FillRecords();
            }
        }
    }
}
