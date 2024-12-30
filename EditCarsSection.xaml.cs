using System.Windows;
using System.Windows.Controls;

namespace AuthorApp
{
    public partial class EditCarsSection : Page
    {
        public EditCarsSection()
        {
            InitializeComponent();
        }

        // Метод для обробки натискання кнопки "Додати новий автомобіль"
        private void AddNewCar(object sender, RoutedEventArgs e)
        {
            // Перехід на сторінку додавання нового автомобіля
            NavigationService.Navigate(new AddArticleWindow());
        }

        // Метод для обробки натискання кнопки "Редагувати існуючий автомобіль"
        private void EditExistingCar(object sender, RoutedEventArgs e)
        {
            // Перехід на сторінку редагування існуючого автомобіля
            NavigationService.Navigate(new EditArticleWindow());
        }

        // Метод для обробки натискання кнопки "Видалити автомобіль"
        private void RemoveCar(object sender, RoutedEventArgs e)
        {
            // Перехід на сторінку видалення автомобіля
            NavigationService.Navigate(new DeleteArticleWindow());
        }

        // Метод для обробки натискання кнопки "Назад"
        private void GoBack(object sender, RoutedEventArgs e)
        {
            // Повернення на головне меню або попередню сторінку
            NavigationService.GoBack();
        }
    }
}