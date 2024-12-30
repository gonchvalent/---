using System.Windows;
using System.Windows.Controls;

namespace AuthorApp
{
    public partial class DeleteVideoWindow : Page
    {
        public DeleteVideoWindow()
        {
            InitializeComponent();
            LoadVideoComboBox();
        }

        private void LoadVideoComboBox()
        {
            var videos = DataManager.GetAllVideos();
            VideoComboBox.ItemsSource = videos;
        }

        private void DeleteVideo(object sender, RoutedEventArgs e)
        {
            if (VideoComboBox.SelectedItem != null)
            {
                var selectedVideo = VideoComboBox.SelectedItem as Video;

                MessageBoxResult result = MessageBox.Show($"Ви впевнені, що хочете видалити відео '{selectedVideo.Title}'?", "Підтвердження", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        DataManager.DeleteVideo(selectedVideo);
                        MessageBox.Show("Відео успішно видалено.", "Успіх", MessageBoxButton.OK, MessageBoxImage.Information);
                        NavigationService.GoBack();
                    }
                    catch (System.Exception ex)
                    {
                        MessageBox.Show($"Сталася помилка при видаленні відео: {ex.Message}", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Будь ласка, виберіть відео перед видаленням.", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void GoBack(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void VideoComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (VideoComboBox.SelectedItem != null)
            {
                var selectedVideo = VideoComboBox.SelectedItem as Video;

                if (selectedVideo != null)
                {
                    MessageBox.Show($"Ви вибрали відео: {selectedVideo.Title}", "Інформація", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }
    }
}