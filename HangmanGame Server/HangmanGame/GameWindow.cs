using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Data.OleDb;
using System.Net;
using System.Net.Sockets;

namespace HangmanGame
{
    public partial class GameWindow : Form
    {
        //Connection code

        private const int MaxConnections = 2; // 2 players maximum to be connected to each other

        struct Connection_Struct // Define a structure to hold details about a single connection
        {
            public Socket Client_Socket;
            public bool bInUse;
        };

        Socket ListenSocket;
        Connection_Struct[] Connection_Array = new Connection_Struct[MaxConnections]; // Define an array to hold a number of connections

        System.Net.IPEndPoint LocalIPEndPoint;
        static int ConnectedClients;
        private static System.Windows.Forms.Timer CommunicationActivity_Timer;
        int TimeClock = 20; // Game timer

        public GameWindow()
        {
            InitializeComponent();
            Initialise_ConnectionArray();
            CommunicationActivity_Timer = new System.Windows.Forms.Timer(); // Check for communication activity on Non-Blocking sockets every 200ms
            CommunicationActivity_Timer.Tick += new EventHandler(PeriodicCommunicationActivityCheck); // Set event handler method for timer
            CommunicationActivity_Timer.Interval = 100;  // Timer interval is 0.1 seconds
            CommunicationActivity_Timer.Enabled = false;
            string LocalIPAddress = GetLocalIPAddress_AsString(); // Get local IP address as a default value
            txtIP_Address.Text = LocalIPAddress; // Place local IP address in IP address field

            txtReceivePort.Text = "8000"; // Default port number
            ConnectedClients = 0;
                  
            btnAccept.Enabled = false; // Disables accept connection request button once clicked

            try
            {   // Create the Listen socket for TCP
                ListenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                ListenSocket.Blocking = false;
            }
            catch (SocketException se)
            {   // Display error message if exception occurs
                MessageBox.Show(se.Message);
            }
            // Bind to the selected port and start listening / receiving
            try
            {
                // Get the Port number from text box

                String Port = txtReceivePort.Text;

                int iPort = System.Convert.ToInt16(Port, 10);

                // Create an Endpoint that will cause the listening activity to apply to all the local node's interfaces
                LocalIPEndPoint = new System.Net.IPEndPoint(IPAddress.Any, iPort);
                // Bind to the local IP Address and selected port
                ListenSocket.Bind(LocalIPEndPoint);
                
                // Prevent any further changes to the port number
                txtReceivePort.ReadOnly = true;
            }

            catch // Silently handle any other exception
            {
            }
            try
            {
              
                ListenSocket.Listen(2); // Listen for connections, with a backlog / queue maximum of 2   
                btnAccept.Enabled = true;
            }
            catch (SocketException se)
            {
                // Display error message if exception occurs
                MessageBox.Show(se.Message);
            }
        }

        private void Initialise_ConnectionArray()
        {
            int iIndex;
            for (iIndex = 0; iIndex < MaxConnections; iIndex++)
            {
                Connection_Array[iIndex].bInUse = false;
            }
        }

        public string GetLocalIPAddress_AsString()
        {
            string Host = Dns.GetHostName();
            string LocalIPaddress = "127.0.0.1";  // Default is local loopback address
            IPHostEntry IPHost = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress IP in IPHost.AddressList)
            {
                if (IP.AddressFamily == AddressFamily.InterNetwork) // Match only the IPv4 address
                {
                    LocalIPaddress = IP.ToString();
                    break;
                }
            }
            return LocalIPaddress;
        }

        private void Close_And_Quit()
        {   // Close the sockets and exit the application
            try
            {
                ListenSocket.Close();
            }
            catch
            {
            }
            try
            {
                int iIndex;
                for (iIndex = 0; iIndex < MaxConnections; iIndex++)
                {
                    Connection_Array[iIndex].Client_Socket.Shutdown(SocketShutdown.Both);
                    Connection_Array[iIndex].Client_Socket.Close();
                }
            }
            catch
            {
            }
            try
            {
                Close();
            }
            catch
            {
            }
        }

        private void PeriodicCommunicationActivityCheck(Object myObject, EventArgs myEventArgs)
        {   // On timed event that periodicly examines if a connection request is pending or if a message has been received on a connected socket     
            // Check for pending connection requests first
            int iIndex;
            iIndex = GetAvailable_ConnectionArray(); // Find an available array entry for next connection request
            if (-1 != iIndex)
            {   // Only continue with Accept if there is an array entry available to hold the details
                try
                {
                    Connection_Array[iIndex].Client_Socket = ListenSocket.Accept();  // If pending accept a connection and assign a new socket to it 
                    // Will 'catch' if NO connection was pending, so statements below only occur when a connection WAS pending
                    Connection_Array[iIndex].bInUse = true;
                    Connection_Array[iIndex].Client_Socket.Blocking = false;  // Make the new socket operate in non-blocking mode
                    ConnectedClients++;
                    SendUpdateMessageToClients();
                }
                catch (SocketException se) // Handle socket-related exception
                {   // If an exception occurs, display an error message
                    if (10053 == se.ErrorCode || 10054 == se.ErrorCode) // Remote end closed the connection
                    {
                        CloseConnection(iIndex);
                    }
                    else if (10035 != se.ErrorCode)
                    {   // Ignore error messages relating to normal behaviour of non-blocking sockets
                        MessageBox.Show(se.Message);
                    }
                }
                catch // Silently handle any other exception
                {
                }
            }

            // Check for received messages on each connected socket
            for (iIndex = 0; iIndex < MaxConnections; iIndex++)
            {
                if (true == Connection_Array[iIndex].bInUse)
                {
                    try
                    {
                        EndPoint localEndPoint = (EndPoint)LocalIPEndPoint;
                        byte[] ReceiveBuffer = new byte[1024];
                        int ReceiveByteCount;
                        ReceiveByteCount = Connection_Array[iIndex].Client_Socket.ReceiveFrom(ReceiveBuffer, ref localEndPoint);

                        string ReceivedMessage;
                        if (0 < ReceiveByteCount)
                        {   // Copy the number of bytes received, from the message buffer to the text control
                            ReceivedMessage = Encoding.ASCII.GetString(ReceiveBuffer, 0, ReceiveByteCount);
                            if ("QuitConnection" == ReceivedMessage)
                            {
                                CloseConnection(iIndex);
                            }
                            else
                            {
                                // Declaring string length
                                int string_length;
                                string rmchances;

                                // Split and receieve message which is the player name of the server side and the category name
                                if (ReceivedMessage.Substring(0, 1) == "1") // Gets character substring from string
                                {
                                    string_length = ReceivedMessage.Count();

                                    lblPlayerName2.Text = ReceivedMessage.Substring(1, string_length - 1);
                                }

                                // Split and receive the second message for the receiver which is the server sides score counter
                                if (ReceivedMessage.Substring(0, 1) == "2")
                                {
                                    string_length = ReceivedMessage.Count();
                                     
                                    ScoreCounter2.Text = ReceivedMessage.Substring(1, string_length - 1);
                                    txtLetter.Enabled = true;
                                    btnPass.Enabled = true;
                                    GameTimer.Enabled = true;
                                }

                                // Split and receive the third message for the receiver which is the server sides hanging process
                                if (ReceivedMessage.Substring(0, 1) == "3")
                                {
                                    string_length = ReceivedMessage.Count();

                                    rmchances  = ReceivedMessage.Substring(1, string_length - 1);
                                    remaining_chances2 = Convert.ToInt16(rmchances);
                                    picHangman2.Invalidate();
                                }

                                // Split and receive the fourth message for the receiver which is the defeat message to the client if the server successfully gets the word first
                                if (ReceivedMessage.Substring(0, 1) == "4")
                                {
                                    DialogResult win = MessageBox.Show("You Lose");
                                    GameTimer.Enabled = false;
                                    clear();                                
                                    select_word();
                                }

                                //Split and receive the 20 second game timer to the client 
                                if (ReceivedMessage.Substring(0, 1)  == "5")
                                {
                                    TimeClock = 20;
                                    GameTimer.Enabled = true;
                                    txtLetter.Enabled = true;
                                    btnYourTurn.Enabled = true;
                                    btnPass.Enabled = true;
                                }

                                //Replay
                                if (ReceivedMessage.Substring(0, 1) == "6")
                                {

                                    string_length = ReceivedMessage.Count();
                                    string Pname = ReceivedMessage.Substring(1, string_length - 1);


                                    DialogResult replay = MessageBox.Show(Pname + " want to play again", "", MessageBoxButtons.YesNo);

                                    // If the player clicks yes then set the visibility of all controls to true, reset the game and select a new random word
                                    if (replay == DialogResult.Yes)
                                    {
                                        picHangman.Visible = true;
                                        txtLetter.Visible = true;
                                        lblLettersUsed.Visible = true;
                                        lblWord.Visible = true;
                                        lblCategory.Visible = true;
                                        btnEnter.Visible = true;
                                        label1.Visible = true;
                                        txtLetter.Visible = true;
                                        btnEnter.Visible = true;

                                        // Resets game and select new random word from the text file
                                        clear();
                                        select_word();
                                        lblClock.Text = "00"; // Resets timer label
                                        TimeClock = 20; // Resets timer
                                        int iiIndex;

                                        for (iiIndex = 0; iiIndex < MaxConnections; iiIndex++)
                                        {
                                            if (true == Connection_Array[iiIndex].bInUse)
                                            {
                                                // Declaring the players name for the server side as a string
                                                string PlayerName;

                                                PlayerName = 1 + lblPlayerName.Text + "-" + lblCategory.Text + "-" + word;

                                                // Encodes characters of the players name from the server side into a sequence of bytes and send the message to the client side to view their opponent
                                                byte[] SendMessage = System.Text.Encoding.ASCII.GetBytes(PlayerName);
                                                Connection_Array[iIndex].Client_Socket.Send(SendMessage, SocketFlags.None);  
                                            }
                                        }
                                    }

                                    else if (replay == DialogResult.No)
                                    {

                                        int iiIndex;

                                        for (iiIndex = 0; iiIndex < MaxConnections; iiIndex++)
                                        {
                                            if (true == Connection_Array[iiIndex].bInUse)
                                            {
                                                string NoMore;

                                                NoMore = "7NoMore";

                                                byte[] SendMessage = System.Text.Encoding.ASCII.GetBytes(NoMore);
                                                Connection_Array[iIndex].Client_Socket.Send(SendMessage, SocketFlags.None);
                                            }
                                        }

                                        GameWindow GameWindow = new GameWindow();
                                        GameWindow.Show();
                                        this.Hide();
                                    }
                                }
                            }
                        }
                    }
                    catch (SocketException se) // Handle socket-related exception
                    {   // If an exception occurs, display an error message
                        if (10053 == se.ErrorCode || 10054 == se.ErrorCode) // Remote end closed the connection
                        {
                            CloseConnection(iIndex);
                        }
                        else if (10035 != se.ErrorCode)
                        {   // Ignore error messages relating to normal behaviour of non-blocking sockets
                            MessageBox.Show(se.Message);
                        }
                    }
                    catch // Silently handle any other exception
                    {
                    }
                }
            }
        }

        private int GetAvailable_ConnectionArray()
        {
            int iIndex;
            for (iIndex = 0; iIndex < MaxConnections; iIndex++)
            {
                if (false == Connection_Array[iIndex].bInUse)
                {
                    return iIndex; // Return the index value of the first not-in-use entry found
                }
            }
            return -1; // Signal that there were no available entries
        }

        private void SendUpdateMessageToClients()
        {   // Send message to each connected client informing of the total number of connected clients
            int iIndex;
            for (iIndex = 0; iIndex < MaxConnections; iIndex++)
            {
                if (true == Connection_Array[iIndex].bInUse)
                {
                    string Message;

                    if (1 == ConnectedClients)
                    {
                        Message = string.Format("There is now {0} client connected", ConnectedClients);
                        btnAccept.Text = "Player Found";
                        btnAccept.Enabled = false;
                    }
                    else
                    {
                        Message = string.Format("There are now {0} clients connected", ConnectedClients);
                    }
                    byte[] SendMessage = System.Text.Encoding.ASCII.GetBytes(Message);
                    Connection_Array[iIndex].Client_Socket.Send(SendMessage, SocketFlags.None);
                }
            }
        }

        private void CloseConnection(int iIndex)
        {
            try
            {
                Connection_Array[iIndex].bInUse = false;
                Connection_Array[iIndex].Client_Socket.Shutdown(SocketShutdown.Both);
                Connection_Array[iIndex].Client_Socket.Close();
                ConnectedClients--;
                SendUpdateMessageToClients();
            }
            catch // Silently handle any exceptions
            {
            }
        }

        // Game code
        string[] alphabet = { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
        HangingProcess cl_hanging_Process;
        string word;
        string[] selected_alphabet;

        //Declaring lives for server and client
        int lives = 7;
        int lives2 = 7;

        //Declaring the chances the players have each time an incorrect word is entered
        int remaining_chances;
        int remaining_chances2;

        //Declaring the width and height for hangman paint event
        int width, height;
        int width2, height2;

        private void GameWindow_Load(object sender, EventArgs e)
        {
            lblPlayerName.Text = MainMenu.Playername; //Indicates inputted playername from server side

            // Width and height of hangman drawing for server and client
            width = picHangman.Width;
            height = picHangman.Height;

            width2 = picHangman2.Width;
            height2 = picHangman2.Height;

            // Associate remaining chances to the lives for the hanging process
            remaining_chances = lives;
            remaining_chances2 = lives2;

            selected_alphabet = new string[0];
            cl_hanging_Process = new HangingProcess();

            // Enable visibility of the controls 
            picHangman.Visible = true;
            picHangman2.Visible = true;
            txtLetter.Visible = true;
            lblLettersUsed.Visible = true;
            lblWord.Visible = true;
            lblCategory.Visible = true;
            btnEnter.Visible = true;
            label1.Visible = true;

            // Get random word from txt file
            select_word();
        }

        private void select_word()
        {
            HangingProcess.New_Word words = cl_hanging_Process.get_word(); // Get word from data.txt with the use of HangingProcess class
            lblCategory.Text = "One" + words.category; // Selects random word from the txt file and assigns the word as a label
            word = words.word;
            foreach (char letter in word.ToCharArray())
            {
                lblWord.Text = lblWord.Text + "_ "; // Sets each character of the selected word to be an underscore in order to hide the word
            }
        }

        private void btnEnter_Click(object sender, EventArgs e)
        {
            // Variable declarations
            string data = txtLetter.Text;
            bool found = false;
            bool mentioned = false;
            TimeClock = 20;

            foreach (var item in selected_alphabet)
            {
                // If the letter entered in the textbox is identical to the letter in selected alphabet then give an indication that the letter has already been previously entered
                if (data.ToLower() == item.ToLower())
                {
                    mentioned = true;
                    MessageBox.Show("Letter already entered");
                    return;
                }
            }
            // If letter has not been previously mentioned then compare the entered letter to the alphabet and find the word
            if (!mentioned)
            {
                for (int i = 0; i < alphabet.Length; i++)
                {
                    if (data.ToLower() == alphabet[i])
                    {
                        found = true;
                        find_word(data);
                        break;
                    }
                }
                // If the entered character is not found then display message box
                if (!found)
                {
                    MessageBox.Show("Write one of the 26 letters");
                }
                update();
            }

            // Empty letter textbox and disable the control after use
            int index = lblWord.Text.IndexOf("_");
            if (index == -1)
            {
                next_section();
            }
            txtLetter.Enabled = false;
            txtLetter.Text = "";

            int iIndex;
            for (iIndex = 0; iIndex < MaxConnections; iIndex++)
            {
                if (true == Connection_Array[iIndex].bInUse)
                {
                    // Declaring score counter for the server side as a string
                    string Player1Score; 

                    Player1Score = 2 + ScoreCounter.Text;

                    // Encodes characters of the score counter into a sequence of bytes and send the message to the client side
                    byte[] SendMessage = System.Text.Encoding.ASCII.GetBytes(Player1Score);
                    Connection_Array[iIndex].Client_Socket.Send(SendMessage, SocketFlags.None);
                }
            }
            GameTimer.Enabled = false; 
            lblClock.Text = "00"; 
            txtLetter.Enabled = false; 
            btnYourTurn.Enabled = false; 
            btnPass.Enabled = false;
        }

        private void find_word(string letter)
        {
            bool is_it_correct = false;
            char[] pattern = word.ToCharArray();
            for (int a = 0; a < pattern.Length; a++)
            {
                if (pattern[a].ToString() != "")
                {
                    // If the letter is equal to one of the letters of the hidden word then remove underscore and insert the correct letter
                    if (letter == pattern[a].ToString().ToLower())
                    {
                        is_it_correct = true;
                        lblWord.Text = lblWord.Text.Remove(a * 2, 1);
                        lblWord.Text = lblWord.Text.Insert(a * 2, letter).ToUpper();
                        ScoreCounter.Text = Convert.ToString(Convert.ToInt32(ScoreCounter.Text) + 5); // Increment the score counter by 5 points
                    }
                }
            }
            // If the entered letter is not correct then decrease the remaining chances, update the hangman picturebox and decrement the score counter by 5 points
            if (!is_it_correct)
            {
                remaining_chances--;
                picHangman.Invalidate();
                ScoreCounter.Text = Convert.ToString(Convert.ToInt32(ScoreCounter.Text) - 5);

                int iIndex;
                for (iIndex = 0; iIndex < MaxConnections; iIndex++)
                {
                    if (true == Connection_Array[iIndex].bInUse)
                    {
                        // Declaring the servers sides remaining chances for letter entering as a string
                        string rmchances = remaining_chances.ToString();

                        // Encodes characters of the remaining chances into a sequence of bytes and send the message to the client side to update the pichangman picturebox
                        byte[] byData = System.Text.Encoding.ASCII.GetBytes(3 + rmchances);
                        Connection_Array[iIndex].Client_Socket.Send(byData, SocketFlags.None);
                        System.Threading.Thread.Sleep(500); // Set a delay from sending this message amongst other messages to prevent collision
                    }
                }
            }
            enter_letter(letter);
        }

        private void update()
        {
            // Display each letter that the player uses
            lblLettersUsed.Text = "";
            foreach(string item in selected_alphabet)
            {
                lblLettersUsed.Text = lblLettersUsed.Text + item + " ";
            }
        }

        private void picHangman_Paint(object sender, PaintEventArgs e)
        {
            // Using pen to draw lines to assemble a hangman picture each time an incorrect letter is entered from the server side
            Pen pen = new Pen(Color.GhostWhite, 12);
            pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;

            if (remaining_chances < lives)
            {
                e.Graphics.DrawLine(pen, width / 10, height / 15, width / 10, height / 15 * 14);
                e.Graphics.DrawLine(pen, width / 10, height / 15, width / 2, height / 15);
            }
            if (remaining_chances < lives - 1)//head and rope
            {
                e.Graphics.DrawLine(pen, width / 2, height / 15, width / 2, height / 15 * 3);
                e.Graphics.DrawEllipse(pen, width / 2-width/20, height / 5, width / 10, height / 10);
            }
            if (remaining_chances < lives - 2)//body
            {
                e.Graphics.DrawLine(pen, width / 2, height / 10 * 3, width / 2, height / 10 * 6);
            }
            if (remaining_chances < lives - 3)//right arm
            {
                e.Graphics.DrawLine(pen, width / 2, height / 10 * 3, width / 2 + width / 10, height / 10 * 3 + height / 5);
            }
            if (remaining_chances < lives - 4)//left arm
            {
                e.Graphics.DrawLine(pen, width / 2, height / 10 * 3, width / 2 - width / 10, height / 10 * 3 + height / 5);
            }
            if (remaining_chances < lives - 5)//right leg
            {
                e.Graphics.DrawLine(pen, width / 2, height / 10 * 6, width / 2 + width / 10, height / 10 * 6 + height / 10);
            }
            if (remaining_chances < lives - 6)//left leg
            {
                e.Graphics.DrawLine(pen, width / 2, height / 10 * 6, width / 2 - width / 10, height / 10 * 6 + height / 10);
                game_over();
            }
        }

        private void next_section()
        {
            // Display message to indicate that the player from the server side wins if they get the word
            DialogResult win = MessageBox.Show("You Win");

            int iIndex;
            for (iIndex = 0; iIndex < MaxConnections; iIndex++)
            {
                if (true == Connection_Array[iIndex].bInUse)
                {
                    // Declaring the message for the client side losing the game as a string
                    string Message; ;

                    Message = 4 + "Lose";

                    // Encodes characters of the losing message into a sequence of bytes and send the message to the client side if the player from the server side wins
                    byte[] SendMessage = System.Text.Encoding.ASCII.GetBytes(Message);
                    Connection_Array[iIndex].Client_Socket.Send(SendMessage, SocketFlags.None);
                }
            }
            GameTimer.Enabled = false;
            game_over();
            // Resets game and select new random word from the text file
            clear();
           // select_word();
        }

        private void game_over()
        {
            // Game over dialog box
            DialogResult replay = MessageBox.Show("Game Over. Correct Word: " + word + ". Replay?", "", MessageBoxButtons.YesNo);

            // If the player clicks yes then set the visibility of all controls to true, reset the game and select a new random word
            if (replay == DialogResult.Yes)
            {
                picHangman.Visible = true;
                txtLetter.Visible = true;
                lblLettersUsed.Visible = true;
                lblWord.Visible = true;
                lblCategory.Visible = true;
                btnEnter.Visible = true;
                label1.Visible = true;              
                txtLetter.Visible = true;
                btnEnter.Visible = true;

                // Resets game and select new random word from the text file
                clear();
                select_word();
                lblClock.Text = "00"; // Resets timer label
                TimeClock = 20; // Resets timer
                int iIndex;

                for (iIndex = 0; iIndex < MaxConnections; iIndex++)
                {
                    if (true == Connection_Array[iIndex].bInUse)
                    {
                        // Declaring the players name for the server side as a string
                        string PlayerName;

                        PlayerName = 1 + lblPlayerName.Text + "-" + lblCategory.Text + "-" + word;

                        // Encodes characters of the players name from the server side into a sequence of bytes and send the message to the client side to view their opponent
                        byte[] SendMessage = System.Text.Encoding.ASCII.GetBytes(PlayerName);
                        Connection_Array[iIndex].Client_Socket.Send(SendMessage, SocketFlags.None);
                    }
                }
                btnStart.Enabled = false;
            }
            // If the player clicks no then hide and reopen GameWindow to refresh it
            else if (replay == DialogResult.No)
            {
                GameWindow GameWindow = new GameWindow();
                GameWindow.Show();
                this.Hide();
            }           
        }

        private void clear()
        {
            // Reset selected alphabet for server side
            selected_alphabet = new string[0];

            // Reset remaining chances and hangman picturebox for server side
            remaining_chances = lives;
            picHangman.Invalidate();

            // Reset remaining chances and hangman picturebox for client side
            remaining_chances2 = lives2;
            picHangman2.Invalidate();

            // Reset text
            lblCategory.Text = "";
            lblWord.Text = "";
            lblLettersUsed.Text = "";
            txtLetter.Text = "";
            ScoreCounter.Text = "0";
            lblPlayerName2.Text = "";
            ScoreCounter2.Text = "";
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            CommunicationActivity_Timer.Start(); // Start the timer to perform periodic checking for connection requests   
            btnAccept.Text = "Searching Player";

            btnAccept.Enabled = false;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            int iIndex;

            for (iIndex = 0; iIndex < MaxConnections; iIndex++)
            {
                if (true == Connection_Array[iIndex].bInUse)
                {
                    // Declaring the players name for the server side as a string
                    string PlayerName;

                    PlayerName = 1 + lblPlayerName.Text + "-" + lblCategory.Text + "-" + word;

                    // Encodes characters of the players name from the server side into a sequence of bytes and send the message to the client side to view their opponent
                    byte[] SendMessage = System.Text.Encoding.ASCII.GetBytes(PlayerName);
                    Connection_Array[iIndex].Client_Socket.Send(SendMessage, SocketFlags.None);
                }
            }
            btnStart.Enabled = false;
        }

        private void btnPass_Click(object sender, EventArgs e)
        {
            int iIndex;

            GameTimer.Enabled = false; // Disables countedown timer if player clicks the pass button
            lblClock.Text = "00"; // Resets timer label
            TimeClock = 20; // Resets timer
            btnYourTurn.Enabled = false; // Disable your turn button for visual indication that it is not the players turn anymore
            txtLetter.Text = ""; // Empties the text box in which the player inputs their letter
            txtLetter.Enabled = false; // Disables letter entering text box
            btnPass.Enabled = false; // Disables pass button once clicked by the player

            for (iIndex = 0; iIndex < MaxConnections; iIndex++)
            {
                if (true == Connection_Array[iIndex].bInUse)
                {
                    // Declaring the pass message as a string
                    string Pass;

                    Pass = 5 + "Pass";

                    // Encodes characters of the pass message into a sequence of bytes and send the message to the client side
                    byte[] SendMessage = System.Text.Encoding.ASCII.GetBytes(Pass);
                    Connection_Array[iIndex].Client_Socket.Send(SendMessage, SocketFlags.None);
                }
            }
        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            int iIndex;
            btnYourTurn.Enabled = true;
            TimeClock = TimeClock - 1;

            lblClock.Text = TimeClock.ToString();

            if (TimeClock == 0)
            {
                lblClock.Text = "00"; // Reset timer label if countdown goes to 0
                btnYourTurn.Enabled = false; // Disable your turn button for visual indication that it is not the players turn anymore
                txtLetter.Enabled = false; // Disables letter entering text box
                btnPass.Enabled = false; // Disables pass button once clicked by the player
                for (iIndex = 0; iIndex < MaxConnections; iIndex++)
                {
                    if (true == Connection_Array[iIndex].bInUse)
                    {
                        // Declaring the pass message as a string
                        string Pass;

                        Pass = 5 + "Pass";

                        // Encodes characters of the pass message into a sequence of bytes and send the message to the client side
                        byte[] SendMessage = System.Text.Encoding.ASCII.GetBytes(Pass);
                        Connection_Array[iIndex].Client_Socket.Send(SendMessage, SocketFlags.None);
                    }
                }
                // Disables players countdown timer
                GameTimer.Enabled = false;
            }
        }

        private void btnChat_Click(object sender, EventArgs e)
        {
            new ChatRoom().Show();
        }

        private void picHangman2_Paint(object sender, PaintEventArgs e)
        {
            // Using pen to draw lines to assemble a hangman picture each time an incorrect letter is entered from the client side
            Pen pen = new Pen(Color.Blue, 12);
            pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;

            if (remaining_chances2 < lives2)
            {
                e.Graphics.DrawLine(pen, width / 10, height2 / 15, width / 10, height2 / 15 * 14);
                e.Graphics.DrawLine(pen, width / 10, height2 / 15, width / 2, height2 / 15);
            }
            if (remaining_chances2 < lives2 - 1)//head and rope
            {
                e.Graphics.DrawLine(pen, width / 2, height2 / 15, width / 2, height2 / 15 * 3);
                e.Graphics.DrawEllipse(pen, width / 2 - width / 20, height2 / 5, width / 10, height2 / 10);
            }
            if (remaining_chances2 < lives2 - 2)//body
            {
                e.Graphics.DrawLine(pen, width / 2, height2 / 10 * 3, width / 2, height2 / 10 * 6);
            }
            if (remaining_chances2 < lives2 - 3)//right arm
            {
                e.Graphics.DrawLine(pen, width / 2, height2 / 10 * 3, width / 2 + width / 10, height2 / 10 * 3 + height2 / 5);
            }
            if (remaining_chances2 < lives2 - 4)//left arm
            {
                e.Graphics.DrawLine(pen, width / 2, height2 / 10 * 3, width / 2 - width / 10, height2 / 10 * 3 + height2 / 5);
            }
            if (remaining_chances2 < lives2 - 5)//right leg
            {
                e.Graphics.DrawLine(pen, width / 2, height2 / 10 * 6, width / 2 + width / 10, height2 / 10 * 6 + height2 / 10);
            }
            if (remaining_chances2 < lives2 - 6)//left leg
            {
                e.Graphics.DrawLine(pen, width / 2, height2 / 10 * 6, width / 2 - width / 10, height2 / 10 * 6 + height2 / 10);
               // game_over();
            }
        }

        private void enter_letter(string letter)
        {
            string[] a = new string[selected_alphabet.Length + 1];
            for (int i = 0; i < selected_alphabet.Length; i++)
            {
                a[i] = selected_alphabet[i];
            }
            a[a.Length - 1] = letter;
            selected_alphabet = a;
        }
    }
}
