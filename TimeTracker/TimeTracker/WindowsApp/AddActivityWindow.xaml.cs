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
using System.Windows.Shapes;
using TimeTracker.AdoApp;
using TimeTracker.DTOApp;

namespace TimeTracker.WindowsApp
{
    /// <summary>
    /// Interaction logic for AddActivityWindow.xaml
    /// </summary>
    public partial class AddActivityWindow : Window
    {
        public AddActivityWindow()
        {
            InitializeComponent();
            LbCategories.ItemsSource = App.Connection.Categories.Where(x => x.UserId == App.CurrentUser.IdUser).ToList();
            DpDateBegin.SelectedDate = DateTime.Now;
        }

        private void EventCansel(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            this.Close();
        }

        private void EventSaveActivity(object sender, RoutedEventArgs e)
        {
            TimeSpan timeBegin;
            TimeSpan timeEnd;

            if (TbtimeBegin.Text == "" || !TimeSpan.TryParse(TbtimeBegin.Text, out timeBegin))
            {
                MessageBox.Show("Неправильное время начала!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if(TbTimeEnd.Text == "" || !TimeSpan.TryParse(TbTimeEnd.Text, out timeEnd))
            {
                MessageBox.Show("Неправильное вермя окончания!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if(timeEnd < timeBegin)
            {
                MessageBox.Show("Время начала не может быть позже времени окончания", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if(DpDateBegin.SelectedDate == null)
            {
                MessageBox.Show("Не выбрана дата!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if(LbCategories.SelectedItem == null)
            {
                MessageBox.Show("Не выбрана категория!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var newRecord = new Records
            {
                Categories = LbCategories.SelectedItem as Categories,
                TimeStart = timeBegin,
                TimeEnd = timeEnd,
                Time = timeEnd - timeBegin,
                Date = DpDateBegin.SelectedDate.Value,
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

                if ((newRecord.Time + reports.
                    FirstOrDefault(x => x.CategoryId == newRecord.Categories.IdCategory).Time)
                    > new TimeSpan(23, 59, 59))
                {
                    MessageBox.Show("Невозможно проводить активность больше 24 часов в сутки!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                App.Connection.Records.Add(newRecord);
                App.Connection.SaveChanges();
            }
            catch
            {
                MessageBox.Show("Не удалось сохранить запись!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            DialogResult = true;
            MessageBox.Show("Успешно.", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();
        }

        private void DpDateBegin_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if(DpDateBegin.SelectedDate != null)
            {
                TblDateEnd.Text = Convert.ToDateTime(DpDateBegin.Text).ToString("dd/MM/yyyy"); ;
            }
        }
    }
}
