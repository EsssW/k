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

namespace Repina_WPF.Контроллеры
{
    /// <summary>
    /// Логика взаимодействия для UI_Item.xaml
    /// </summary>
    public partial class UI_Item : UserControl
    {
        // при создании этого контроллера в аругменты необходимо передать название,размер,цену товара
        public UI_Item(string name,string size, string price)
        {
            InitializeComponent();
            name_label.Content = name;
            price_label.Content = price;
            size_label.Content = size;
        }

        private void UserControl_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MainWindow mainWindow = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            mainWindow.Add_Item_in_check(name_label.Content.ToString(), size_label.Content.ToString(), price_label.Content.ToString());
        }
    }
}
