using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace AuthorApp
{
    public partial class AddVideoWindow : Page
    {
        public AddVideoWindow()
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

        private void SaveVideo(object sender, RoutedEventArgs e)
        {
            if (ValidateInputs())
            {
                try
                {
                    Video newVideo = new Video
                    {
                        Title = TitleBox.Text,
                        Url = UrlBox.Text,
                        Description = DescriptionBox.Text
                    };

                    DataManager.SaveVideo(newVideo);

                    MessageBox.Show("Відео успішно збережено.", "Успіх", MessageBoxButton.OK, MessageBoxImage.Information);
                    ClearInputs();
                    NavigationService.GoBack();
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show($"Сталася помилка при збереженні відео: {ex.Message}", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Будь ласка, заповніть всі поля перед збереженням відео.", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool ValidateInputs()
        {
            return !string.IsNullOrWhiteSpace(TitleBox.Text) && TitleBox.Text != TitleBox.Tag.ToString() &&
                   !string.IsNullOrWhiteSpace(UrlBox.Text) && UrlBox.Text != UrlBox.Tag.ToString() &&
                   !string.IsNullOrWhiteSpace(DescriptionBox.Text) && DescriptionBox.Text != DescriptionBox.Tag.ToString();
        }

        private void ClearInputs()
        {
            TitleBox.Text = TitleBox.Tag.ToString();
            TitleBox.Foreground = Brushes.Gray;

            UrlBox.Text = UrlBox.Tag.ToString();
            UrlBox.Foreground = Brushes.Gray;

            DescriptionBox.Text = DescriptionBox.Tag.ToString();
            DescriptionBox.Foreground = Brushes.Gray;
        }

        private void GoBack(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}