using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using SampleRpg.Engine.Eventing;
using SampleRpg.Engine.Models;
using SampleRpg.Engine.ViewModels;

namespace SampleRpg.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Construction

        public MainWindow ()
        {
            InitializeComponent();

            InitializeBindings();

            //TODO: Do this via bindings
            _session.MessageRaised += OnMessageRaised;

            DataContext = _session;
        }
        #endregion

        //TODO: Make this a command
        //TODO: Generalize this
        private void OnMoveNorth ( object sender, RoutedEventArgs e ) => _session.MoveNorth();
        private void OnMoveSouth ( object sender, RoutedEventArgs e ) => _session.MoveSouth();
        private void OnMoveWest ( object sender, RoutedEventArgs e ) => _session.MoveWest();
        private void OnMoveEast ( object sender, RoutedEventArgs e ) => _session.MoveEast();

        private void OnTrade ( object sender, RoutedEventArgs e )
        {
            if (_session.CurrentTrader == null)
                return;

            var child = new TradeWindow() {
                Owner = this,
                DataContext = _session
            };

            child.ShowDialog();
        }

        private void OnMessageRaised ( object sender, GameMessageEventArgs e )
        {
            GameMessages.Document.Blocks.Add(new Paragraph(new Run(e.Message)));
            GameMessages.ScrollToEnd();
        }
        
        private void OnAttack ( object sender, RoutedEventArgs e ) => _session.Attack();

        private void OnCraft ( object sender, RoutedEventArgs e )
        {
            var recipe = ((FrameworkElement)sender).DataContext as Recipe;
            _session.CraftItem(recipe);
        }

        private void OnSlot1 ( object sender, RoutedEventArgs e ) => _session.UseSlot1();

        private void Window_KeyDown ( object sender, KeyEventArgs e )
        {
            if (_keyBindings.TryGetValue(e.Key, out var action))
                action();
        }

        #region Private Members

        private void FocusTab ( string name )
        {
            foreach (var tab in PlayerDataTabControl.Items.OfType<TabItem>())
            {
                if (String.Compare(tab.Name, name, true) == 0)
                {
                    tab.IsSelected = true;
                    return;                         
                };
            };
        }

        private void InitializeBindings ()
        {
            _keyBindings.Add(Key.W, () => _session.MoveNorth());
            _keyBindings.Add(Key.A, () => _session.MoveWest());
            _keyBindings.Add(Key.S, () => _session.MoveSouth());
            _keyBindings.Add(Key.D, () => _session.MoveEast());

            _keyBindings.Add(Key.Z, () => _session.Attack());
            _keyBindings.Add(Key.D1, () => _session.UseSlot1());

            _keyBindings.Add(Key.I, () => FocusTab("InventoryTab"));
            _keyBindings.Add(Key.J, () => FocusTab("QuestsTab"));
            _keyBindings.Add(Key.R, () => FocusTab("RecipesTab"));
            _keyBindings.Add(Key.T, () => OnTrade(this, new RoutedEventArgs()));
        }

        private readonly GameSession _session = new GameSession();

        private readonly Dictionary<Key, Action> _keyBindings = new Dictionary<Key, Action>();
           
        #endregion        
    }
}
