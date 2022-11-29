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
    /// Interaction logic for AuthorizationPage.xaml
    /// </summary>
    public partial class AuthorizationPage : Page
    {
        public AuthorizationPage()
        {
            InitializeComponent();
        }

        private void EventAuthorization(object sender, RoutedEventArgs e)
        {
            if(TbLogin.Text == "" || PbPassword.Password == "")
            {
                MessageBox.Show("Некорректно введены данные!");
                return;
            }
            var user = App.Connection.Logins.FirstOrDefault(x=> x.Login == TbLogin.Text && x.Password == PbPassword.Password);
            if(user != null)
            {
                App.CurrentUser = user.Users;
                MessageBox.Show("Авторизация прошла успешно.");
                NavigationService.Navigate(new MainPage());
            }
        }

        private void EventNavigateToRegistrationPage(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RegistrationPage());
        }
    }
}
