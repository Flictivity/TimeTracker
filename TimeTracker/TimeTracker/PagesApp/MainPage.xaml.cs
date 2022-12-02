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

namespace TimeTracker.PagesApp
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
        }
        private void EventNavigateTimeTrackPage(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new TimeTrackPage());
        }

        private void EventNavigateDayInfoPage(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new DayInfoPage());
        }

        private void EventNavigateTotalInfoPage(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new ReportPage());
        }

        private void EventNavigateCategoriesPage(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new CategoriesPage());
        }

        private void EventExit(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AuthorizationPage());
        }
    }
}
