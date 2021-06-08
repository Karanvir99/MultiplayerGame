using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;

namespace HangmanGame
{
    public partial class ChatRoom : Form
    {
        Socket m_ReceiveSocket;
        IPEndPoint m_localIPEndPoint;

        public ChatRoom()
        {
            InitializeComponent();
            string szLocalIPAddress = GetLocalIPAddress_AsString(); // Get local IP address as a default value
            IP_Address_textBox.Text = szLocalIPAddress;             // Place local IP address in IP address field
            ReceivePort_textBox.Text = "8000";  // Default port number
            SendPort_textBox.Text = "8000";
            //Receive_button.Enabled = false;     // Receive button is not enabled until the Bind has completed
            try
            {   // Create the Receive socket, for UDP use
                m_ReceiveSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                m_ReceiveSocket.Blocking = false;
            }
            catch (SocketException se)
            {   // If an exception occurs, display an error message
                MessageBox.Show(se.Message);
            }

            // Bind to the selected port and start listening / receiving
            try
            {
                // Prevent any further changes to the port number
                ReceivePort_textBox.ReadOnly = true;
                // Get the Port number from the appropriate text box
                String szPort = ReceivePort_textBox.Text;
                int iPort = System.Convert.ToInt16(szPort, 10);
                // Create an Endpoint that will cause the listening activity to apply to all the local node's interfaces
                m_localIPEndPoint = new System.Net.IPEndPoint(IPAddress.Any, iPort);
                // Bind to the local IP Address and selected port
                m_ReceiveSocket.Bind(m_localIPEndPoint);
             
            }
            catch (SocketException se)
            {
                // If an exception occurs, display an error message
                MessageBox.Show(se.Message);
            }
        }

        public string GetLocalIPAddress_AsString()
        {
            string szHost = Dns.GetHostName();
            string szLocalIPaddress = "127.0.0.1";  // Default is local loopback address
            IPHostEntry IPHost = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress IP in IPHost.AddressList)
            {
                if (IP.AddressFamily == AddressFamily.InterNetwork) // Match only the IPv4 address
                {
                    szLocalIPaddress = IP.ToString();
                    break;
                }
            }
            return szLocalIPaddress;
        }

        private void Send_button_Click(object sender, EventArgs e)
        {
            try
            {
                // Get the IP address from the appropriate text box
                String szIPAddress = IP_Address_textBox.Text;
                System.Net.IPAddress DestinationIPAddress = System.Net.IPAddress.Parse(szIPAddress);

                // Get the Port number from the appropriate text box
                String szPort = SendPort_textBox.Text;
                int iPort = System.Convert.ToInt16(szPort, 10);

                // Combine Address and Port to create an Endpoint
                System.Net.IPEndPoint remoteEndPoint = new System.Net.IPEndPoint(DestinationIPAddress, iPort);

                //m_SendSocket.Connect(remoteEndPoint);
                String szData = MainMenu.Playername + ":  "+ txtSendingMessage.Text ;
                if (szData.Equals(""))
                {
                    szData = "Default message";
                }
                byte[] byData = System.Text.Encoding.ASCII.GetBytes(szData);
                m_ReceiveSocket.SendTo(byData, remoteEndPoint);

            }
            catch (SocketException se)
            {
                // If an exception occurs, display an error message
                MessageBox.Show(se.Message);
            }
        }

        private void Receive_button_Click(object sender, EventArgs e)
        {          
        }

        private void Message_textBox_VisibleChanged(object sender, EventArgs e)
        {                    
        }

        private void Message_textBox_TextChanged(object sender, EventArgs e)
        {                     
        }

        private void Auto_Received_timer_Tick(object sender, EventArgs e)
        {
            try
            {
                EndPoint localEndPoint = (EndPoint)m_localIPEndPoint;
                byte[] ReceiveBuffer = new byte[1024];
                int iReceiveByteCount;
                iReceiveByteCount = m_ReceiveSocket.ReceiveFrom(ReceiveBuffer, ref localEndPoint);

                if (0 < iReceiveByteCount)
                {   // Copy the number of bytes received, from the message buffer to the text control
                    Message_textBox.Text = Message_textBox.Text + "\r\n" + Encoding.ASCII.GetString(ReceiveBuffer, 0, iReceiveByteCount);

                    //  Message_textBox.AppendText(Message_textBox.Text);
                    Message_textBox.SelectionStart = Message_textBox.TextLength;
                    Message_textBox.ScrollToCaret();                  
                }
            }
            catch // Catch any errors
            {   // Display a diagnostic message
                //  Message_textBox.Text = "*** No message received ***";
            }
        }
    }
}
