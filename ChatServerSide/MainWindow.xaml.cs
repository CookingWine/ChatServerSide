using ChatServerSide.Code;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
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
        ///<summary>ip地址</summary>
        private IPAddress m_LocalAddress;

        private TcpListener m_TcpListener = null;
        private TcpClient m_TcpClinet = null;
        ///<summary>数据流</summary>
        private NetworkStream m_NetworkStream = null;
        ///<summary>读</summary>
        private BinaryReader m_BinaryReader = null;
        ///<summary>写</summary>
        private BinaryWriter m_BinaryWriter = null;

        ///<summary>端口</summary>
        private readonly int m_Port;
        ///<summary>连接列表</summary>
        private Dictionary<long , ClientData> m_ClientUserList = new Dictionary<long , ClientData>( );
        public int UserCount
        {
            get { return m_ClientUserList.Count; }
        }
        public MainWindow( )
        {
            InitializeComponent( );
            m_LocalAddress = new IPAddress( Dns.GetHostByName( Dns.GetHostName( ) ).AddressList[0].Address );
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

        private bool m_OpenServer = false;

        private void StartServer( object sender , RoutedEventArgs e )
        {
            if( m_OpenServer )
            {
                return;
            }
            m_OpenServer = true;
            m_TcpListener = new TcpListener( m_LocalAddress , m_Port );
            m_TcpListener.Start( );
            while( true )
            {
                try
                {
                    m_TcpClinet = m_TcpListener.AcceptTcpClient( );
                    m_NetworkStream = m_TcpClinet.GetStream( );
                    m_BinaryReader = new BinaryReader( m_NetworkStream );
                    m_BinaryWriter = new BinaryWriter( m_NetworkStream );

                }
                catch
                {

                    return;
                }
            }
        }
        private void CloseServer( object sender , RoutedEventArgs e )
        {
            CloseServer( );
        }

        /// <summary>
        /// 关闭所有连接
        /// </summary>
        public void CloseAllClient( )
        {
            foreach( var item in m_ClientUserList )
            {
                item.Value.Stop( );
            }
            m_ClientUserList.Clear( );
        }

        /// <summary>
        /// 关闭服务器
        /// </summary>
        private void CloseServer( )
        {
            CloseAllClient( );
            m_BinaryReader?.Close( );
            m_BinaryWriter?.Close( );
            m_TcpClinet?.Close( );
            m_TcpListener?.Stop( );
            m_OpenServer = false;
        }
    }
}
