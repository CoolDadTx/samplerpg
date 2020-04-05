﻿using System;
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
            var item = ((FrameworkElement)sender).DataContext as InventoryItem;
            if (item != null)
            {
                Session.CurrentPlayer.RemoveGold(item.Item.Price);
                Session.CurrentTrader.RemoveFromInventory(item.Item.Id);
                Session.CurrentPlayer.AddToInventory(item.Item);
            };
        }

        private void OnSell ( object sender, RoutedEventArgs e )
        {
            var item = ((FrameworkElement)sender).DataContext as InventoryItem;
            if (item != null)
            {
                Session.CurrentPlayer.AddGold(item.Item.Price);
                Session.CurrentPlayer.RemoveFromInventory(item.Item.Id);
                Session.CurrentTrader.AddToInventory(item.Item);                
            };
        }

        private void OnClose ( object sender, RoutedEventArgs e )
        {
            Close();
        }

        private GameSession Session => DataContext as GameSession;
    }
}
