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
            LbCategories.ItemsSource = App.Connection.Categories.ToList();
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
                MessageBox.Show("Неправильное время начала!");
                return;
            }
            if(TbTimeEnd.Text == "" || !TimeSpan.TryParse(TbTimeEnd.Text, out timeEnd))
            {
                MessageBox.Show("Неправильное вермя окончания!");
                return;
            }

            if(timeEnd < timeBegin)
            {
                MessageBox.Show("Время начала не может быть позже времени окончания");
                return;
            }

            if(DpDateBegin.SelectedDate == null)
            {
                MessageBox.Show("Не выбрана дата!");
                return;
            }

            if(LbCategories.SelectedItem == null)
            {
                MessageBox.Show("Не выбрана категория!");
                return;
            }

            var newRecord = new Records
            {
                Categories = LbCategories.SelectedItem as Categories,
                Users = App.CurrentUser,
                TimeStart = timeBegin,
                TimeEnd = timeEnd,
                Time = timeEnd - timeBegin,
                Date = DpDateBegin.SelectedDate.Value,
                Info = TbInfo.Text
            };

            App.Connection.Records.Add(newRecord);
            App.Connection.SaveChanges();

            DialogResult = true;
            MessageBox.Show("s");
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
