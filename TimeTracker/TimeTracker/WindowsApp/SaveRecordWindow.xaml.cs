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

namespace TimeTracker.WindowsApp
{
    /// <summary>
    /// Interaction logic for SaveRecordWindow.xaml
    /// </summary>
    public partial class SaveRecordWindow : Window
    {
        Stopwatch _sw;
        private Categories _category;
        public SaveRecordWindow(Stopwatch sw, Categories category)
        {
            InitializeComponent();
            _category = category;
            _sw = sw;
        }

        private void EventCansel(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            this.Close();
        }

        private void EventSaveRecord(object sender, RoutedEventArgs e)
        {
            _sw.Stop();
            var totalTime = _sw.Elapsed;
            Records newRecord = new Records
            {
                Categories = _category,
                TimeEnd = DateTime.Now.TimeOfDay,
                TimeStart = DateTime.Now.TimeOfDay - totalTime,
                Time = totalTime,
                Date = DateTime.Today,
                Info = TbInfo.Text
            };
            try
            {
                App.Connection.Records.Add(newRecord);
                App.Connection.SaveChanges();
            }
            catch
            {
                MessageBox.Show("Не удалось сохранить запись!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            DialogResult = true;
            this.Close();
        }
    }
}
