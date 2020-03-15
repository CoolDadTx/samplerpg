using System;
using System.Windows;
using System.Windows.Documents;
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

            //TODO: Do this via bindings
            _session = new GameSession();
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

        private void OnMessageRaised ( object sender, GameMessageEventArgs e )
        {
            GameMessages.Document.Blocks.Add(new Paragraph(new Run(e.Message)));
            GameMessages.ScrollToEnd();
        }

        #region Private Members

        private GameSession _session;
        #endregion
    }
}
