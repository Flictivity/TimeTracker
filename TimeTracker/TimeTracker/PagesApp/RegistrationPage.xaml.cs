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

namespace TimeTracker.PagesApp
{
    /// <summary>
    /// Interaction logic for RegistrationPage.xaml
    /// </summary>
    public partial class RegistrationPage : Page
    {
        public RegistrationPage()
        {
            InitializeComponent();
        }

        private void EventNavigateToAuthorizationPage(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void EventRegistration(object sender, RoutedEventArgs e)
        {
            if(TbName.Text == "" || TbLogin.Text == "" || PbPassword.Password == "")
            {
                MessageBox.Show("Некорректно введены данные!");
                return;
            }
            Users newUser = new Users
            {
                Name = TbName.Text
            };
            Logins newLoginData = new Logins
            {
                Login = TbLogin.Text,
                Password = PbPassword.Password,
                Users = newUser
            };

            App.Connection.Logins.Add(newLoginData);
            App.Connection.SaveChanges();
            MessageBox.Show("Регистрация прошла успешно.");
            NavigationService.Navigate(new AuthorizationPage());
        }
    }
}
