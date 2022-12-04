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
using TimeTracker.WindowsApp;

namespace TimeTracker.PagesApp
{
    /// <summary>
    /// Interaction logic for CategoriesPage.xaml
    /// </summary>
    public partial class CategoriesPage : Page
    {
        private List<Categories> _categories;
        public CategoriesPage()
        {
            InitializeComponent();
            _categories = App.Connection.Categories.Where(x => x.UserId == App.CurrentUser.IdUser).ToList();
            LbCategories.ItemsSource = _categories;
        }

        private void EventAddCategory(object sender, RoutedEventArgs e)
        {
            var addCategoryWindow = new AddCategoryWindow();
            if(addCategoryWindow.ShowDialog() == true)
            {
                try
                {
                    _categories = App.Connection.Categories.Where(x => x.UserId == App.CurrentUser.IdUser).ToList();
                    LbCategories.ItemsSource = null;
                    LbCategories.ItemsSource = _categories;
                }
                catch
                {
                    MessageBox.Show("Не удалось загрузить данные!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error)
                }
            }
        }
    }
}
