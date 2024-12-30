using System.Windows;
using System.Windows.Controls;
using static System.Net.Mime.MediaTypeNames;
using System.Windows.Media.Imaging;

namespace AuthorApp
{
    public partial class DeleteArticleWindow : Page
    {
        public DeleteArticleWindow()
        {
            InitializeComponent();
            LoadBrandComboBox();
        }

        // Завантаження брендів у ComboBox
        private void LoadBrandComboBox()
        {
            var brands = DataManager.GetAllBrands(); // Метод для отримання всіх брендів
            BrandComboBox.ItemsSource = brands;
        }

        // Завантаження моделей на основі вибраного бренду
        private void BrandComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (BrandComboBox.SelectedItem != null)
            {
                var selectedBrand = BrandComboBox.SelectedItem.ToString();
                LoadModelComboBox(selectedBrand);
            }
        }

        // Завантаження моделей у ComboBox
        private void LoadModelComboBox(string brand)
        {
            var models = DataManager.GetModelsByBrand(brand); // Метод для отримання моделей за брендом
            ModelComboBox.ItemsSource = models;
        }

        // Метод для видалення статті
        private void DeleteArticle(object sender, RoutedEventArgs e)
        {
            if (ModelComboBox.SelectedItem != null)
            {
                var selectedBrand = BrandComboBox.SelectedItem.ToString();
                var selectedModel = ModelComboBox.SelectedItem.ToString();

                var result = MessageBox.Show($"Ви впевнені, що хочете видалити статтю для моделі {selectedModel} бренду {selectedBrand}?",
                                             "Підтвердження видалення",
                                             MessageBoxButton.YesNo,
                                             MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    DataManager.DeleteArticle(selectedBrand, selectedModel); // Метод для видалення статті

                    MessageBox.Show("Стаття успішно видалена.", "Інформація", MessageBoxButton.OK, MessageBoxImage.Information);

                    // Оновлення списку моделей після видалення статті
                    LoadModelComboBox(selectedBrand);
                }
            }
            else
            {
                MessageBox.Show("Будь ласка, виберіть модель для видалення.", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void ModelComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Отримати вибрану модель з ComboBox
            string selectedModel = ModelComboBox.SelectedItem as string;
        }
        // Метод для повернення на попередню сторінку
        private void GoBack(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}