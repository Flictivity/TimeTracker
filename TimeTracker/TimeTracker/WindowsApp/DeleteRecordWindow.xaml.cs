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
    /// Interaction logic for DeleteRecordWindow.xaml
    /// </summary>
    public partial class DeleteRecordWindow : Window
    {
        private Records _record;
        public DeleteRecordWindow(Records record)
        {
            InitializeComponent();
            _record = record;
        }

        private void EventDelete(object sender, RoutedEventArgs e)
        {
            try
            {
                DialogResult = true;
                var record = App.Connection.Records.FirstOrDefault(x => x.IdRecord == _record.IdRecord);
                App.Connection.Records.Remove(record);
                App.Connection.SaveChanges();
            }
            catch
            {
                MessageBox.Show("Ошибка удаления!");
            }
        }

        private void EventCancel(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            this.Close();
        }
    }
}
