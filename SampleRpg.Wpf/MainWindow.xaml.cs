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
        private void OnAddXP ( object sender, RoutedEventArgs e )
        {
            _session.CurrentPlayer.ExperiencePoints += 10;
        }

        #region Private Members

        private GameSession _session;
        #endregion
    }
}
