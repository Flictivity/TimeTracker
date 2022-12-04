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
    /// Interaction logic for AddCategoryWindow.xaml
    /// </summary>
    public partial class AddCategoryWindow : Window
    {
        public AddCategoryWindow()
        {
            InitializeComponent();
        }

        private void EventCansel(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            this.Close();
        }

        private void EventSaveRecord(object sender, RoutedEventArgs e)
        {
            if(TbInfo.Text == "")
            {
                MessageBox.Show("Не введено название категории!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            Categories newCategory = new Categories
            {
                Name = TbInfo.Text,
                Users = App.CurrentUser
            };
            try
            {
                App.Connection.Categories.Add(newCategory);
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
