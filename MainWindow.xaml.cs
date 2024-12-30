using NAudio.Wave;
using System;
using System.IO;
using System.Windows;

namespace CarApp
{
    public partial class MainWindow : Window
    {
        private IWavePlayer wavePlayer;
        private AudioFileReader audioFile;

        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Navigate(new WelcomePage());
        }

        private void PlayMusic(object sender, RoutedEventArgs e)
        {
            try
            {
                // Перевірка, чи існує файл перед спробою програвання
                string filePath = "Fast & Furious 1 8 Top 15 Best Music fast and furious film 720p.mp3";
                if (!File.Exists(filePath))
                {
                    MessageBox.Show("Audio file not found. Please check the file path.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (wavePlayer == null)
                {
                    wavePlayer = new WaveOutEvent();
                    audioFile = new AudioFileReader(filePath);
                    wavePlayer.Init(audioFile);
                }
                wavePlayer.Play();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error playing audio: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void StopMusic(object sender, RoutedEventArgs e)
        {
            try
            {
                if (wavePlayer != null)
                {
                    wavePlayer.Stop();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error stopping audio: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        protected override void OnClosed(EventArgs e)
        {
            DisposeAudioResources();
            base.OnClosed(e);
        }

        private void DisposeAudioResources()
        {
            // Звільнення ресурсів аудіо
            if (wavePlayer != null)
            {
                wavePlayer.Dispose();
                wavePlayer = null;
            }
            if (audioFile != null)
            {
                audioFile.Dispose();
                audioFile = null;
            }
        }
    }
}