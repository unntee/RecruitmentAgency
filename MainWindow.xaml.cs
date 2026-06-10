using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RecruitmentAgency
{
    public partial class MainWindow : Window
    {
        //Коллекции вакансий и соискателей для таблиц
        public ObservableCollection<Vacancy> Vacancies { get; set; } = new ObservableCollection<Vacancy>();
        private int _nextId = 1;

        public ObservableCollection<Applicant> Applicants { get; set; } = new ObservableCollection<Applicant>();
        private int _nextApplicantId = 1;

        public MainWindow()
        {
            InitializeComponent();
            VacanciesDG.ItemsSource = Vacancies; //Привязываем колекции к таблицам
            ApplicantsDG.ItemsSource = Applicants;
        }

        //Логика кнопки добавления вакансии
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            AddWindow addWin = new AddWindow(); //Создаем новое окно добавления
            if (addWin.ShowDialog() == true)
            {
                addWin.CreatedVacancy.ID = _nextId++; //Обновляем индекс ивакансии
                Vacancies.Add(addWin.CreatedVacancy); //Добавляем вакансию в коллекцию
            }
        }

        //Логика кнопки редактирования вакансии с доп проверками
        private void ChangeButton_Click(object sender, RoutedEventArgs e)
        {
            if (VacanciesDG.SelectedItem is Vacancy selected)
            {
                ChangeWindow changeWin = new ChangeWindow(selected);
                if (changeWin.ShowDialog() == true)
                {
                    VacanciesDG.Items.Refresh();
                }
            }
            else
            {
                MessageBox.Show("Выберите вакансию для редактирования!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        //Логика кнопки удаления вакансии с доп подтверждением
        private void DelButton_Click(object sender, RoutedEventArgs e)
        {
            if (VacanciesDG.SelectedItem is Vacancy selected)
            {
                var result = MessageBox.Show($"Удалить вакансию {selected.Title}?", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    Vacancies.Remove(selected);
                }
            }
            else
            {
                MessageBox.Show("Выберите вакансию для удаления!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        //Обновление списка вакансий
        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            VacanciesDG.Items.Refresh();
        }

        //Анологичные кнопки добавления, редактирования, удаления и обновления списка соискателей
        private void AddAppButton_Click(object sender, RoutedEventArgs e)
        {
            AddApplicantWindow addAppWin = new AddApplicantWindow();
            if (addAppWin.ShowDialog() == true)
            {
                addAppWin.CreatedApplicant.ID = _nextApplicantId++;
                Applicants.Add(addAppWin.CreatedApplicant);
            }
        }

        private void ChangeAppButton_Click(object sender, RoutedEventArgs e) //Логика кнопки редактирования соискателя с доп проверками
        {
            if (ApplicantsDG.SelectedItem is Applicant selected)
            {
                ChangeApplicantWindow changeAppWin = new ChangeApplicantWindow(selected);
                if (changeAppWin.ShowDialog() == true)
                {
                    ApplicantsDG.Items.Refresh();
                }
            }
            else
            {
                MessageBox.Show("Выберите соискателя для редактирования!", "Внимание");
            }
        }

        private void DelAppButton_Click(object sender, RoutedEventArgs e) //Логика кнопки удаления соискателя с доп подтверждением
        {
            if (ApplicantsDG.SelectedItem is Applicant selected)
            {
                var result = MessageBox.Show($"Удалить соискателя \"{selected.FullName}\"?", "Удаление", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    Applicants.Remove(selected);
                }
            }
            else
            {
                MessageBox.Show("Выберите соискателя для удаления!", "Внимание");
            }
        }

        private void RefreshAppButton_Click(object sender, RoutedEventArgs e) //Обновление списка соискателей
        {
            ApplicantsDG.Items.Refresh();
        }

        private void VacanciesDG_MouseDoubleClick(object sender, MouseButtonEventArgs e) //Функция для открытия меню редактирования по двойному клику для таблицы вакансий
        {
            if (ItemsControl.ContainerFromElement(VacanciesDG, e.OriginalSource as DependencyObject) is DataGridRow)
            {
                ChangeButton_Click(sender, e);
            }
        }

        private void ApplicantsDG_MouseDoubleClick(object sender, MouseButtonEventArgs e) //Функция для открытия меню редактирования по двойному клику для таблицы соискателей
        {
            if (ItemsControl.ContainerFromElement(ApplicantsDG, e.OriginalSource as DependencyObject) is DataGridRow)
            {
                ChangeAppButton_Click(sender, e);
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e) //Закрытие окна с подтверждением
        {
            MessageBoxResult result = MessageBox.Show("Вы действительно хотите закрыть приложение?", "Закрытие", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.No)
            {
                e.Cancel = true;
            }
        }
    }
}