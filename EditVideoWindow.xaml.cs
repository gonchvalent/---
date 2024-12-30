using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace AuthorApp
{
    public partial class EditVideoWindow : Page
    {
        private Video _selectedVideo;

        public EditVideoWindow()
        {
            InitializeComponent();
            LoadVideoComboBox();
        }

        private void LoadVideoComboBox()
        {
            var videos = DataManager.GetAllVideos();
            VideoComboBox.ItemsSource = videos;
        }

        private void VideoComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (VideoComboBox.SelectedItem != null)
            {
                _selectedVideo = VideoComboBox.SelectedItem as Video;
                LoadVideoData(_selectedVideo);
            }
        }

        private void LoadVideoData(Video video)
        {
            if (video != null)
            {
                TitleBox.Text = video.Title;
                UrlBox.Text = video.Url;
                DescriptionBox.Text = video.Description;

                SetTextBoxColor(Brushes.Black);
            }
        }

        private void SetTextBoxColor(Brush color)
        {
            TitleBox.Foreground = color;
            UrlBox.Foreground = color;
            DescriptionBox.Foreground = color;
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
            if (ValidateInputs() && _selectedVideo != null)
            {
                try
                {
                    _selectedVideo.Title = TitleBox.Text;
                    _selectedVideo.Url = UrlBox.Text;
                    _selectedVideo.Description = DescriptionBox.Text;

                    DataManager.UpdateVideo(_selectedVideo);

                    MessageBox.Show("Відео успішно оновлено.", "Успіх", MessageBoxButton.OK, MessageBoxImage.Information);
                    NavigationService.GoBack();
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show($"Сталася помилка при оновленні відео: {ex.Message}", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Будь ласка, виберіть відео та заповніть всі поля перед збереженням.", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool ValidateInputs()
        {
            return !string.IsNullOrWhiteSpace(TitleBox.Text) && TitleBox.Text != TitleBox.Tag.ToString() &&
                   !string.IsNullOrWhiteSpace(UrlBox.Text) && UrlBox.Text != UrlBox.Tag.ToString() &&
                   !string.IsNullOrWhiteSpace(DescriptionBox.Text) && DescriptionBox.Text != DescriptionBox.Tag.ToString();
        }

        private void GoBack(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}