using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace AuthorApp
{
    public partial class EditArticleWindow : Page
    {
        public EditArticleWindow()
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

        // Подія зміни вибраного бренду
        private void BrandComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (BrandComboBox.SelectedItem != null)
            {
                var selectedBrand = BrandComboBox.SelectedItem.ToString();
                LoadModelComboBox(selectedBrand);
            }
        }

        // Завантаження моделей у ComboBox на основі вибраного бренду
        private void LoadModelComboBox(string brand)
        {
            var models = DataManager.GetModelsByBrand(brand); // Метод для отримання моделей по бренду
            ModelComboBox.ItemsSource = models;
        }

        // Подія зміни вибраної моделі
        private void ModelComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ModelComboBox.SelectedItem != null)
            {
                var selectedBrand = BrandComboBox.SelectedItem.ToString();
                var selectedModel = ModelComboBox.SelectedItem.ToString();
                LoadArticleData(selectedBrand, selectedModel);
            }
        }

        // Завантаження даних статті на основі вибраного бренду і моделі
        private void LoadArticleData(string brand, string model)
        {
            // Отримання статті на основі вибраного бренду і моделі
            var article = DataManager.GetArticleByBrandAndModel(brand, model);

            // Перевірка, чи знайдена стаття
            if (article != null)
            {
                // Заповнення полів даними зі статті
                BrandBox.Text = article.Brand;
                ModelBox.Text = article.Model;
                GenerationBox.Text = article.Generation;
                ProductionYearsBox.Text = article.ProductionYears;
                BodyTypeBox.Text = article.BodyType;
                EngineBox.Text = article.Engine;
                TransmissionBox.Text = article.Transmission;
                WeightBox.Text = article.Weight;
                Image1Box.Text = article.Image1Url;
                Image2Box.Text = article.Image2Url;
                DescriptionBox.Text = article.Description;

                // Встановлення кольору тексту на чорний, щоб виділити, що це реальні дані
                SetTextBoxColor(Brushes.Black);
            }
            else
            {
                // Відображення повідомлення про помилку, якщо статтю не знайдено
                MessageBox.Show($"Статтю не знайдено для бренду: {brand}, моделі: {model}", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);

                // Очистка всіх текстових полів у разі, якщо стаття не знайдена
                ClearTextBoxes();
            }
        }

        // Метод для очищення всіх текстових полів
        private void ClearTextBoxes()
        {
            BrandBox.Text = string.Empty;
            ModelBox.Text = string.Empty;
            GenerationBox.Text = string.Empty;
            ProductionYearsBox.Text = string.Empty;
            BodyTypeBox.Text = string.Empty;
            EngineBox.Text = string.Empty;
            TransmissionBox.Text = string.Empty;
            WeightBox.Text = string.Empty;
            Image1Box.Text = string.Empty;
            Image2Box.Text = string.Empty;
            DescriptionBox.Text = string.Empty;

            // Встановлення кольору тексту на сірий (якщо використовується для підказок)
            SetTextBoxColor(Brushes.Gray);
        }

        // Метод для встановлення кольору тексту у всіх TextBox
        private void SetTextBoxColor(Brush color)
        {
            BrandBox.Foreground = color;
            ModelBox.Foreground = color;
            GenerationBox.Foreground = color;
            ProductionYearsBox.Foreground = color;
            BodyTypeBox.Foreground = color;
            EngineBox.Foreground = color;
            TransmissionBox.Foreground = color;
            WeightBox.Foreground = color;
            Image1Box.Foreground = color;
            Image2Box.Foreground = color;
            DescriptionBox.Foreground = color;
        }

        // Метод для видалення тексту підказки, коли фокус знаходиться на текстовому полі
        private void RemovePlaceholderText(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null && textBox.Text == textBox.Tag.ToString())
            {
                textBox.Text = "";
                textBox.Foreground = Brushes.Black;
            }
        }

        // Метод для додавання тексту підказки, коли фокус зникає з текстового поля
        private void AddPlaceholderText(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null && string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.Text = textBox.Tag.ToString();
                textBox.Foreground = Brushes.Gray;
            }
        }

        // Метод для збереження зміненої статті
        private void SaveArticle(object sender, RoutedEventArgs e)
        {
            if (ValidateInputs())
            {
                var updatedArticle = new Car
                {
                    Brand = BrandBox.Text,
                    Model = ModelBox.Text,
                    Generation = GenerationBox.Text,
                    ProductionYears = ProductionYearsBox.Text,
                    BodyType = BodyTypeBox.Text,
                    Engine = EngineBox.Text,
                    Transmission = TransmissionBox.Text,
                    Weight = WeightBox.Text,
                    Image1Url = Image1Box.Text,
                    Image2Url = Image2Box.Text,
                    Description = DescriptionBox.Text
                };

                DataManager.UpdateArticle(updatedArticle); // Метод для оновлення статті

                MessageBox.Show("Статтю успішно оновлено.", "Інформація", MessageBoxButton.OK, MessageBoxImage.Information);

                NavigationService.GoBack(); // Повернення до попередньої сторінки
            }
            else
            {
                MessageBox.Show("Будь ласка, заповніть всі поля перед збереженням змін.", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        // Метод для перевірки правильності введених даних
        private bool ValidateInputs()
        {
            return !string.IsNullOrWhiteSpace(BrandBox.Text) && BrandBox.Text != BrandBox.Tag.ToString() &&
                   !string.IsNullOrWhiteSpace(ModelBox.Text) && ModelBox.Text != ModelBox.Tag.ToString() &&
                   !string.IsNullOrWhiteSpace(GenerationBox.Text) && GenerationBox.Text != GenerationBox.Tag.ToString() &&
                   !string.IsNullOrWhiteSpace(ProductionYearsBox.Text) && ProductionYearsBox.Text != ProductionYearsBox.Tag.ToString() &&
                   !string.IsNullOrWhiteSpace(BodyTypeBox.Text) && BodyTypeBox.Text != BodyTypeBox.Tag.ToString() &&
                   !string.IsNullOrWhiteSpace(EngineBox.Text) && EngineBox.Text != EngineBox.Tag.ToString() &&
                   !string.IsNullOrWhiteSpace(TransmissionBox.Text) && TransmissionBox.Text != TransmissionBox.Tag.ToString() &&
                   !string.IsNullOrWhiteSpace(WeightBox.Text) && WeightBox.Text != WeightBox.Tag.ToString() &&
                   !string.IsNullOrWhiteSpace(Image1Box.Text) && Image1Box.Text != Image1Box.Tag.ToString() &&
                   !string.IsNullOrWhiteSpace(Image2Box.Text) && Image2Box.Text != Image2Box.Tag.ToString() &&
                   !string.IsNullOrWhiteSpace(DescriptionBox.Text) && DescriptionBox.Text != DescriptionBox.Tag.ToString();
        }

        // Метод для повернення на попередню сторінку
        private void GoBack(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}