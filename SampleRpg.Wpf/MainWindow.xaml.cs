using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using SampleRpg.Engine.Eventing;
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

        private void OnSlot1 ( object sender, RoutedEventArgs e ) => _session.UseSlot1();

        private void Window_KeyDown ( object sender, KeyEventArgs e )
        {
            if (_keyBindings.TryGetValue(e.Key, out var action))
                action();
        }

        #region Private Members

        private void InitializeBindings ()
        {
            _keyBindings.Add(Key.W, () => _session.MoveNorth());
            _keyBindings.Add(Key.A, () => _session.MoveWest());
            _keyBindings.Add(Key.S, () => _session.MoveSouth());
            _keyBindings.Add(Key.D, () => _session.MoveEast());

            _keyBindings.Add(Key.Z, () => _session.Attack());
            _keyBindings.Add(Key.D1, () => _session.UseSlot1());
        }

        private readonly GameSession _session = new GameSession();

        private readonly Dictionary<Key, Action> _keyBindings = new Dictionary<Key, Action>();
           
        #endregion        
    }
}
