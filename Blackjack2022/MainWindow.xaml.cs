using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Blackjack2022
{
    public partial class MainWindow : Window
    {
        Blackjack game = new Blackjack();
        public MainWindow()
        {
            InitializeComponent();
            game.InitShoe();
        }
        
        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            game.Start();
            RefreshScreen();
            StartButton.IsEnabled = false;
            HitButton.IsEnabled = true;
            StandButton.IsEnabled = true;
            RestartButton.Visibility = Visibility.Hidden;
        }

        private void RestartButton_Click(object sender, RoutedEventArgs e)
        {
            game.Clear();
            StartButton.IsEnabled = true;
            HitButton.IsEnabled = true;
            StandButton.IsEnabled = true;
            PlayerPanel.Children.Clear();
            DealerPanel.Children.Clear();
            
        }
        private void HitButton_Click(object sender, RoutedEventArgs e)
        {
            int result = game.Hit();
            RefreshScreen();
            if(result < 0)
            {
                MessageBox.Show("You Lost :(");
                RestartButton.Visibility = Visibility.Visible;
                HitButton.IsEnabled = false;
            }
        }

        private void StandButton_Click(object sender, RoutedEventArgs e)
        {
            int dealerResult = game.Stand();
            RefreshScreen();
            if (dealerResult < 0)
            {
                MessageBox.Show("You Win! :)");
                RestartButton.Visibility = Visibility.Visible;
                HitButton.IsEnabled = false;
                StandButton.IsEnabled = false;

            }
            else
                MessageBox.Show("You Lost :(");
                RestartButton.Visibility = Visibility.Visible;
                HitButton.IsEnabled = false;
                StandButton.IsEnabled = false;
        }

        public void RefreshScreen()
        {
            PlayerPanel.Children.Clear();
            DealerPanel.Children.Clear();
            foreach (Card c in game.playerDeck)
            {
                ShowCard(c, PlayerPanel);
            }
            foreach (Card c in game.dealerDeck)
            {
                ShowCard(c, DealerPanel);
            }
        }

        public void ShowCard(Card c, Panel panel)
        {
            string path = "/png/";
            string filename = c.GetFileName();

            Uri uri = new Uri(path + filename, UriKind.Relative);
            BitmapImage bitmapImage = new BitmapImage(uri);
            Image image = new Image();
            image.Source = bitmapImage;

            panel.Children.Add(image);
        }
        public void ClearCards(List<Card> deck, Panel panel)
        {
            deck.Clear();
            PlayerPanel.Children.Clear();
            DealerPanel.Children.Clear();  
            
        }
    }
}
