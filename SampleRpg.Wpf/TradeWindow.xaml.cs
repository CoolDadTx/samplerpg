using System;
using System.Collections.Generic;
using System.Windows;

using SampleRpg.Engine.Models;
using SampleRpg.Engine.ViewModels;

namespace SampleRpg.Wpf
{
    /// <summary>
    /// Interaction logic for TradeWindow.xaml
    /// </summary>
    public partial class TradeWindow : Window
    {
        public TradeWindow ()
        {
            InitializeComponent();
        }        

        private void OnBuy ( object sender, RoutedEventArgs e)
        {
            var item = ((FrameworkElement)sender).DataContext as GameItem;
            if (item != null)
            {
                Session.CurrentPlayer.Gold -= item.Price;
                Session.CurrentTrader.RemoveFromInventory(item);
                Session.CurrentPlayer.AddToInventory(item);
            };
        }

        private void OnSell ( object sender, RoutedEventArgs e )
        {
            var item = ((FrameworkElement)sender).DataContext as GameItem;
            if (item != null)
            {
                Session.CurrentPlayer.Gold += item.Price;
                Session.CurrentPlayer.RemoveFromInventory(item.ItemTypeId);
                Session.CurrentTrader.AddToInventory(item);                
            };
        }

        private void OnClose ( object sender, RoutedEventArgs e )
        {
            Close();
        }

        private GameSession Session => DataContext as GameSession;
    }
}
