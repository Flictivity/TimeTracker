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
using System.Windows.Shapes;
using TimeTracker.AdoApp;
using TimeTracker.DTOApp;

namespace TimeTracker.WindowsApp
{
    /// <summary>
    /// Interaction logic for SaveRecordWindow.xaml
    /// </summary>
    public partial class SaveRecordWindow : Window
    {
        private Categories _category;
        public SaveRecordWindow(Categories category)
        {
            InitializeComponent();
            _category = category;
        }

        private void EventCansel(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            this.Close();
        }

        private void EventSaveRecord(object sender, RoutedEventArgs e)
        {
            App.StopWatch.Stop();

            var totalTime = App.StopWatch.ElapsedTimeSpan;
            Records newRecord = new Records
            {
                Categories = _category,
                TimeEnd = DateTime.Now.TimeOfDay,
                TimeStart = App.StartTime,
                Time = totalTime,
                Date = DateTime.Today,
                Info = TbInfo.Text
            };
            try
            {
                var reports = App.Connection.Records.Where(x => x.Date == DateTime.Today && x.Categories.UserId == App.CurrentUser.IdUser)
                .GroupBy(z => z.Categories).ToList()
                .Select(g => new ReportDto
                {
                    CategoryName = g.Key.Name,
                    CategoryId = g.Key.IdCategory,
                    Time = new TimeSpan(g.Sum(a => a.Time.Ticks))
                })
                .OrderBy(d => d.Time).ToList();

                var record = reports
                    .FirstOrDefault(x => x.CategoryId == newRecord.Categories.IdCategory);

                if(record != null)
                {
                    if ((newRecord.Time + record.Time)
                    > new TimeSpan(23, 59, 59))
                    {
                        MessageBox.Show("Невозможно проводить активность больше 24 часов в сутки!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                }

                App.Connection.Records.Add(newRecord);
                App.Connection.SaveChanges();
                DialogResult = true;
                this.Close();
            }
            catch
            {
                MessageBox.Show("Не удалось сохранить запись!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                DialogResult = false;
                this.Close();
            }
        }
    }
}
