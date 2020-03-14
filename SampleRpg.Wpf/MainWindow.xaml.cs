using System;
using System.Windows;
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

            //TODO: Do this via bindings
            _session = new GameSession();

            DataContext = _session;
        }
        #endregion

        //TODO: Make this a command
        //TODO: Generalize this
        private void OnMoveNorth ( object sender, RoutedEventArgs e ) => _session.MoveNorth();
        private void OnMoveSouth ( object sender, RoutedEventArgs e ) => _session.MoveSouth();
        private void OnMoveWest ( object sender, RoutedEventArgs e ) => _session.MoveWest();
        private void OnMoveEast ( object sender, RoutedEventArgs e ) => _session.MoveEast();

        #region Private Members

        private GameSession _session;
        #endregion
    }
}
