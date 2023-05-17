using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ChatServerSide
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow :Window
    {
        public MainWindow( )
        {
            InitializeComponent( );
        }

        private void Border_MouseDown( object sender , MouseButtonEventArgs e )
        {
            if( e.ChangedButton == MouseButton.Left )
            {
                DragMove( );
            }
        }

        private void ShutdownApp( object sender , RoutedEventArgs e )
        {
            Application.Current.Shutdown( );
        }
        private void StartServer( object sender , RoutedEventArgs e )
        {

        }
        private void CloseServer(object sender , RoutedEventArgs e )
        {

        }
    }
}
