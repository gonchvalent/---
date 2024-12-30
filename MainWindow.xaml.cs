using System.Windows;

namespace AuthorApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Navigate(new AuthorMainMenu());
        }
    }
}