using System.Windows;
using System.Windows.Controls;

namespace AuthorApp
{
    public partial class EditVideosSection : Page
    {
        public EditVideosSection()
        {
            InitializeComponent();
        }

        private void GoToAddVideo(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddVideoWindow());
        }

        private void GoToEditVideo(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new EditVideoWindow());
        }

        private void GoToDeleteVideo(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new DeleteVideoWindow());
        }

        private void GoToMainMenu(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}