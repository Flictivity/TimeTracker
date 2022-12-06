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
using TimeTracker.DTOApp;

namespace TimeTracker.PagesApp
{
    /// <summary>
    /// Interaction logic for ReportPage.xaml
    /// </summary>
    public partial class ReportPage : Page
    {
        private DateTime currentDate;
        private List<ReportDto> _reports;
        public ReportPage()
        {
            InitializeComponent();
            currentDate = DateTime.Today;
            TblDate.Text = currentDate.ToString("dd/MM/yyyy");
            DpCurrentDate.SelectedDate = currentDate;

            FillReports();
        }
        private void EventIncrementDay(object sender, RoutedEventArgs e)
        {
            currentDate = currentDate.AddDays(1);
            TblDate.Text = currentDate.ToString("dd/MM/yyyy");
            DpCurrentDate.SelectedDate = currentDate;

            FillReports();
        }

        private void EventDecrementDay(object sender, RoutedEventArgs e)
        {
            currentDate = currentDate.AddDays(-1);
            TblDate.Text = currentDate.ToString("dd/MM/yyyy");
            DpCurrentDate.SelectedDate = currentDate;

            FillReports();
        }

        private void FindReport()
        {
            try
            {
                _reports = App.Connection.Records.Where(x => x.Date == currentDate && x.Categories.UserId == App.CurrentUser.IdUser)
                .GroupBy(z => z.Categories).ToList()
                .Select(g => new ReportDto
                {
                    CategoryName = g.Key.Name,
                    Time = new TimeSpan(g.Sum(a => a.Time.Ticks)),
                    CategoryId = g.Key.IdCategory
                })
                .OrderBy(d => d.Time).ToList();
            }
            catch
            {
                MessageBox.Show("Не удалось загрузить данные!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void FillReports()
        {
            FindReport();
            LvDayInfo.ItemsSource = null;
            LvDayInfo.ItemsSource = _reports;
        }

        private void DpCurrentDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            currentDate = DpCurrentDate.SelectedDate.Value;
            TblDate.Text = currentDate.ToString("dd/MM/yyyy");

            FillReports();
        }
    }
}
