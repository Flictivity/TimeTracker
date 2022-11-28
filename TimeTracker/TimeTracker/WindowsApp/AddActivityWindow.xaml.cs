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
        }

        private void EventCansel(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            this.Close();
        }

        private void EventSaveActivity(object sender, RoutedEventArgs e)
        {
            DateTime timeBegin;
            DateTime timeEnd;

            if (TbtimeBegin.Text == "" || !DateTime.TryParse(TbtimeBegin.Text, out timeBegin))
            {
                MessageBox.Show("Неправильное время начала!");
                return;
            }
            if(TbTimeEnd.Text == "" || !DateTime.TryParse(TbTimeEnd.Text, out timeEnd))
            {
                MessageBox.Show("Неправильное вермя окончания!");
                return;
            }

            if(timeEnd < timeBegin)
            {
                MessageBox.Show("Время начала не может быть позже времени окончания");
                return;
            }

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
