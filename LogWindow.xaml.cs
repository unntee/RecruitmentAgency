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
using System.ComponentModel;

namespace RecruitmentAgency
{
    public partial class LogWindow : Window
    {
        public LogWindow()
        {
            InitializeComponent();
        }

        private void LogInButton_Click(object sender, RoutedEventArgs e) //Лоика кнопки входа с проверкой на правильность логина и пароля
        {
            LoginTB.ClearValue(BorderBrushProperty); //Сброс цветов рамок к стандартным
            PasswordTB.ClearValue(BorderBrushProperty);
            
            if (LoginTB.Text == "hr" && PasswordTB.Password == "admin")
            {
                new MainWindow().Show();
                this.Close();
            }
            else
            {
                LoginTB.BorderBrush = Brushes.Red;
                PasswordTB.BorderBrush = Brushes.Red;
                MessageBox.Show("Неверный логин или пароль!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Information);
                LoginTB.Clear();
                PasswordTB.Clear();
            }
        }

        private void LogOutButton_Click(object sender, RoutedEventArgs e) //Логика кнопки выхода с доп подтверждением
        {
            MessageBoxResult result = MessageBox.Show("Вы действительно хотите выйти?", "Выход", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown();
            }
        }
    }
}
