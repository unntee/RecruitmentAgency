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

namespace RecruitmentAgency
{
    public partial class ChangeWindow : Window
    {
        private Vacancy _vacancyToEdit;

        public ChangeWindow(Vacancy vacancy)
        {
            InitializeComponent();
            _vacancyToEdit = vacancy;

            VacTB.Text = _vacancyToEdit.Title; //Заполняем поля текущими данными
            ComTB.Text = _vacancyToEdit.Company;
            MinZpTB.Text = _vacancyToEdit.SalaryFrom.ToString();
            MaxZpTB.Text = _vacancyToEdit.SalaryTo.ToString();
            TrebTB.Text = _vacancyToEdit.Requirements;
            StatTB.Text = _vacancyToEdit.Status;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e) //Кнопка сохранения с проверками
        {
            TextBox[] textBoxes = { VacTB, ComTB, MinZpTB, MaxZpTB, TrebTB, StatTB }; //Сброс цветов рамок к стандартным
            foreach (var box in textBoxes)
            {
                box.ClearValue(BorderBrushProperty);
            }

            bool hasErr = false; //переменная для нахождения ошибок

            //Проверки на пустой ввод с перекрашиванием рамок в красный цвет
            if (string.IsNullOrWhiteSpace(VacTB.Text))
            {
                VacTB.BorderBrush = Brushes.Red;
                hasErr = true;
            }

            if (string.IsNullOrWhiteSpace(ComTB.Text))
            {
                ComTB.BorderBrush = Brushes.Red;
                hasErr = true;
            }

            if (string.IsNullOrWhiteSpace(MinZpTB.Text))
            {
                MinZpTB.BorderBrush = Brushes.Red;
                hasErr = true;
            }

            if (string.IsNullOrWhiteSpace(MaxZpTB.Text))
            {
                MaxZpTB.BorderBrush = Brushes.Red;
                hasErr = true;
            }

            if (string.IsNullOrWhiteSpace(TrebTB.Text))
            {
                TrebTB.BorderBrush = Brushes.Red;
                hasErr = true;
            }

            if (string.IsNullOrWhiteSpace(StatTB.Text))
            {
                StatTB.BorderBrush = Brushes.Red;
                hasErr = true;
            }

            //Если нашли ошибку блокируем кнопку Сохранить и выводим сообщение
            if (hasErr)
            {
                SaveButton.IsEnabled = false;
                MessageBox.Show("Пожалуйста заполните все поля!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            //Доп проверки числовых значений
            bool isMinOk = decimal.TryParse(MinZpTB.Text, out decimal minZp);
            bool isMaxOk = decimal.TryParse(MaxZpTB.Text, out decimal maxZp);

            if (!isMinOk || !isMaxOk || minZp < 27093 || maxZp < minZp || minZp > 1000000 || maxZp > 1000000)
            {
                MinZpTB.BorderBrush = Brushes.Red;
                MaxZpTB.BorderBrush = Brushes.Red;
                SaveButton.IsEnabled = false;
                MessageBox.Show("Зарплата указана неверно!\nПроверьте:\n'Зарплата от' не меньше МРОТа\n'Зарплата до' не меньше 'Зарплата от'.\n'Зарплата от/до не больше 1000000", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            _vacancyToEdit.Title = VacTB.Text; //Перезаписываем данные
            _vacancyToEdit.Company = ComTB.Text;
            _vacancyToEdit.SalaryFrom = minZp;
            _vacancyToEdit.SalaryTo = maxZp;
            _vacancyToEdit.Requirements = TrebTB.Text;
            _vacancyToEdit.Status = StatTB.Text;

            this.DialogResult = true;
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e) // Делаем кнопу снова доступной при постановке курсора в одно из полей
        {
            SaveButton.IsEnabled = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e) //Кнопка отмены с доп подтверждением
        {
            MessageBoxResult result = MessageBox.Show("Вы уверены что хотите отменить действие?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                this.DialogResult = false;
            }
        }
    }
}
