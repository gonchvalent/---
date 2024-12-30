using System.Windows;
using System.Windows.Controls;

namespace AuthorApp
{
    public partial class AuthorMainMenu : Page
    {
        public AuthorMainMenu()
        {
            InitializeComponent();
        }

        private void GoToEditCarsSection(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new EditCarsSection());
        }

        private void GoToEditVideosSection(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new EditVideosSection());
        }

        private void ExitApplication(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}