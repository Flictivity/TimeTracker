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

        private void EventCloseWindow(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            this.Close();
        }

        private void EventSaveActivity(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            MessageBox.Show("SS");
        }
    }
}
