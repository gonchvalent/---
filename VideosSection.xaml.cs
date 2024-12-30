﻿using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace CarApp
{
    public partial class VideosSection : Page
    {
        private List<Video> _videos; // Список відеозаписів

        public VideosSection(List<Video> videos)
        {
            InitializeComponent();
            _videos = videos;
            LoadVideos();
        }

        private void LoadVideos()
        {
            if (VideosListBox != null)
            {
                VideosListBox.ItemsSource = _videos;
            }
        }

        private void DisplayVideos(IEnumerable<Video> videos)
        {
            if (VideosListBox != null)
            {
                VideosListBox.ItemsSource = videos.ToList();
            }
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            var searchText = SearchBox.Text.ToLower();
            if (string.IsNullOrWhiteSpace(searchText) || searchText == "введіть назву відео")
            {
                DisplayVideos(_videos); // Показати всі відеозаписи, якщо текст пошуку порожній
            }
            else
            {
                var filteredVideos = _videos.Where(v => v.Title.ToLower().Contains(searchText)).ToList();
                DisplayVideos(filteredVideos);
            }
        }

        private void GoToMainMenu(object sender, RoutedEventArgs e)
        {
            if (NavigationService != null)
            {
                NavigationService.Navigate(new MainMenu());
            }
        }

        private void GoToVideoDetails(object sender, SelectionChangedEventArgs e)
        {
            if (VideosListBox.SelectedItem is Video selectedVideo)
            {
                if (NavigationService != null)
                {
                    NavigationService.Navigate(new VideoDetailsPage(selectedVideo));
                }
            }
        }

        private void SearchBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox textBox && textBox.Text == "Введіть назву відео")
            {
                textBox.Text = "";
                textBox.Foreground = Brushes.Black;
            }
        }

        private void SearchBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox textBox && string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.Text = "Введіть назву відео";
                textBox.Foreground = Brushes.Gray;
            }
        }
    }
}