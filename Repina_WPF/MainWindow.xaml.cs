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
using System.IO;
using Repina_WPF.Контроллеры;

namespace Repina_WPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // глобальная перменная для проверки актуальности промокода
        bool promo_is_actual = false;
        // глобальная перменная определения размера скидки
        int promo_discount=0;

        public MainWindow()
        {
            InitializeComponent();
        }
        // событие загрузки окна( срабатывает при запуске приложения)
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            result_tb.Text = "Название  | Объем | Цена(Руб)";
            // очистка файла с чеком 
            var sw = new StreamWriter("..\\..\\Данные\\Чек.txt");
            sw.WriteLine("Названи   | ОБЪЕМ | ЦЕНА ");
            sw.Close();

            string[] split; // массив для ременных записей строк из файлов
            // цикл проходит по всем строкам  файла по указанному пути
            foreach(var cofe in File.ReadAllLines("..\\..\\Данные\\Кофе.txt"))
            {
                split = cofe.Split('|'); // приведение записи каждой строки в номальный вид и запись во временный массив
                // на главный экран элементу StackPanel добавляються дочерние элменты типа UI_Item 
                // в аругменты передеються данные из файла 
                Coffee_sp.Children.Add(new UI_Item(split[0], split[1], split[2]));
            }
            foreach (var dessert in File.ReadAllLines("..\\..\\Данные\\Десерты.txt"))
            {
                split = dessert.Split('|'); 
                Desserts_sp.Children.Add(new UI_Item(split[0], split[1], split[2]));
            }
        }

        // метод вызываеться из Контроллера UI_Item при нажатии на любой товар на главной панели 
        public void Add_Item_in_check(string name, string size, string price)
        {
            // меняем стоимость заказа
            Sum_label.Content = Convert.ToDouble(Sum_label.Content) + Convert.ToDouble(price);

            // вывод на экран дабавленного продукта 
            result_tb.Text+=$"\n {name} | {size}  | {price} руб";
            // открытие поток для записи в файл нового выбранного продукта
            var sw = new StreamWriter("..\\..\\Данные\\Чек.txt", true);
            // запись в файл
            sw.WriteLine($"{name} | {size} | {price} Руб");
            // закрытие потока
            sw.Close();
        }

        // событие срабатывет при наведени на поле для ввода промокода
        private void PROMOCODE_TB_MouseEnter(object sender, MouseEventArgs e)
        {
            PROMOCODE_TB.Text = "";
        }

        // СОБЫТИЕ НАЖАТИЯ НА КНОПКУ ОПЛАТЫ 
        private void PAY_BTN_Click(object sender, RoutedEventArgs e)
        {
            // открытие поток для записи в файл нового выбранного продукта
            var sw = new StreamWriter("..\\..\\Данные\\Чек.txt", true);
            string[] split;

            // проходим по всем прмокодам из файла и проверяем сходимость с введнным
            // если введенный прмокод активный то делаем скидку соответствующую нашему прмокоду
            foreach (var promo in File.ReadAllLines("..\\..\\Данные\\Промокоды.txt"))
            {
                split = promo.Split('|');
                // если в файле есть такой прмокод
                if(PROMOCODE_TB.Text==split[0])
                {
                    promo_is_actual = true; // указываем что скидка есть
                    promo_discount = Convert.ToInt32(split[1]); // указываем размер скидки 
                }
            }
            // если промокод актинвный 
            if (promo_is_actual==true)
            {
                // меняем итог всего заказа
                Sum_label.Content = Convert.ToDouble(Sum_label.Content) * (1- (promo_discount / 100));
                MessageBox.Show($"Введенный прмокод активен! Сумма закза снижена на {promo_discount}%");
                // дозапись в файл данных
                sw.WriteLine($"ИТОГ {Sum_label.Content.ToString()}\n Был использован прмокод на {promo_discount}%");
                sw.Close();
            }
            else
            {
                MessageBox.Show($"Введенный прмокод не активен");
                sw.WriteLine($"ИТОГ {Sum_label.Content.ToString()}");
                sw.Close();
            }
            // вывод чека
            MessageBox.Show(File.ReadAllText("..\\..\\Данные\\Чек.txt"));
            
            
        }

        
    }
}
