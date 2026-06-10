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
    public partial class AddApplicantWindow : Window
    {
        public Applicant CreatedApplicant { get; private set; }

        public AddApplicantWindow()
        {
            InitializeComponent();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e) //Кнопка сохранить с проверками
        {
            TextBox[] textBoxes = { NameTB, AgeTB, EduTB, ExpTB, ContactsTB,}; //Сброс цветов рамок к стандартным
            foreach (var box in textBoxes)
            {
                box.ClearValue(BorderBrushProperty);
            }

            bool hasErr = false; //переменная для нахождения ошибок

            //Проверки на пустой ввод с перекрашиванием рамок в красный цвет
            if (string.IsNullOrWhiteSpace(NameTB.Text))
            {
                NameTB.BorderBrush = Brushes.Red;
                hasErr = true;
            }

            if (string.IsNullOrWhiteSpace(AgeTB.Text))
            {
                AgeTB.BorderBrush = Brushes.Red;
                hasErr = true;
            }

            if (string.IsNullOrWhiteSpace(EduTB.Text))
            {
                EduTB.BorderBrush = Brushes.Red;
                hasErr = true;
            }

            if (string.IsNullOrWhiteSpace(ExpTB.Text))
            {
                ExpTB.BorderBrush = Brushes.Red;
                hasErr = true;
            }

            if (string.IsNullOrWhiteSpace(ContactsTB.Text))
            {
                ContactsTB.BorderBrush = Brushes.Red;
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
            bool isAgeOk = int.TryParse(AgeTB.Text, out int age);
            bool isExpOk = int.TryParse(ExpTB.Text, out int experience);

            if (!isAgeOk || !isExpOk)
            {
                AgeTB.BorderBrush = Brushes.Red;
                ExpTB.BorderBrush = Brushes.Red;
                SaveButton.IsEnabled = false;
                MessageBox.Show("Возраст или Опыт указаны неверно!\nПроверьте:\n'Возраст' - число\n'Образование' - число", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            CreatedApplicant = new Applicant //Создаем новый объект соискателя
            {
                FullName = NameTB.Text,
                Age = age,
                Education = EduTB.Text,
                ExperienceYears = experience,
                Contacts = ContactsTB.Text
            };

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
