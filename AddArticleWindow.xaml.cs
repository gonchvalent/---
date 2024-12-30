using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace AuthorApp
{
    public partial class AddArticleWindow : Page
    {
        public AddArticleWindow()
        {
            InitializeComponent();
        }

        private void RemovePlaceholderText(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null && textBox.Text == textBox.Tag.ToString())
            {
                textBox.Text = "";
                textBox.Foreground = Brushes.Black;
            }
        }

        private void AddPlaceholderText(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null && string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.Text = textBox.Tag.ToString();
                textBox.Foreground = Brushes.Gray;
            }
        }

        private void SaveArticle(object sender, RoutedEventArgs e)
        {
            if (ValidateInputs())
            {
                Car newCar = new Car
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

                DataManager.SaveCar(newCar);
                NavigationService.GoBack();
            }
            else
            {
                MessageBox.Show("Будь ласка, заповніть всі поля перед збереженням статті.", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

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

        private void GoBack(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}