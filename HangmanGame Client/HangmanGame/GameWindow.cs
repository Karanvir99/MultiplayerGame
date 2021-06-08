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
using System.Text.RegularExpressions;

namespace HangmanGame
{
    public partial class GameWindow : Form
    {
        //Connection code

        Socket ClientSocket;
        System.Net.IPEndPoint m_remoteEndPoint;
        private static System.Windows.Forms.Timer CommunicationActivity_Timer;
        int TimeClock=20;

        public GameWindow()
        {
            InitializeComponent();

            CommunicationActivity_Timer = new System.Windows.Forms.Timer(); // Check for communication activity on Non-Blocking sockets every 200ms
            CommunicationActivity_Timer.Tick += new EventHandler(PeriodicCommunicationActivityCheck); // Set event handler method for timer
            CommunicationActivity_Timer.Interval = 100;  // Timer interval is 0.1 seconds
            CommunicationActivity_Timer.Enabled = false;
      
            string LocalIPAddress = GetLocalIPAddress_AsString(); // Get local IP address as a default value
            txtIP_Address.Text = LocalIPAddress; // Place local IP address in IP address field
            txtSendPort.Text = "8000"; // Default port number
        }

        private string GetLocalIPAddress_AsString()
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

        // Game code
        string[] alphabet = { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
        HangingProcess cl_hanging_Process;
        string word;
        string[] selected_alphabet;

        // Declaring lives for server and client
        int lives = 7;
        int lives2 = 7;

        // Declaring the chances the players have each time an incorrect word is entered
        int remaining_chances;
        int remaining_chances2;

        // Declaring the width and height for hangman paint event
        int width, height;
        int width2, height1;

        // Declaring the game category as a string
        String GameWord;  

        private void GameWindow_Load(object sender, EventArgs e)
        {
            lblPlayerName.Text = MainMenu.Playername; //Indicates inputted playername from client side

            // Width and height of hangman drawing for server and client
            width = picHangman.Width;
            height = picHangman.Height;

            width2 = picHangman2.Width;
            height1 = picHangman2.Height;

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
            btnSelect.Visible = true;
            label1.Visible = true;

            // Disable visibility of the controls 
            lblWord.Visible = false;
            lblCategory.Visible = false;
            txtLetter.Visible = false;
            btnSelect.Visible = false;
        }

        private void select_word()
        {
            lblWord.Text = ""; // Blank label for the category name
            word = GameWord; // Category name which will be dependant on the server
            foreach (char letter in word.ToCharArray())
            {
                lblWord.Text = lblWord.Text + "_ "; // Sets each character of the selected word to be an underscore in order to hide the word
            }
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            // Variable declarations
            string data = txtLetter.Text;
            bool found = false;
            bool mentioned = false;

            
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

            // Declaring score counter for the client side as a string
            String ScoreData = 2 + ScoreCounter.Text;

            // Encodes characters of the score counter into a sequence of bytes and send the message to the server side
            byte[] byData = System.Text.Encoding.ASCII.GetBytes(ScoreData);
            ClientSocket.Send(byData, SocketFlags.None);

            GameTimer.Enabled = false;
            lblClock.Text = "00";
            txtLetter.Enabled = false;
            btnYourTurn.Enabled = false;
            TimeClock = 20;
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

                // Declaring the client sides remaining chances for letter entering as a string
                string rmchances = remaining_chances.ToString();

                // Encodes characters of the remaining chances into a sequence of bytes and send the message to the server side to update the pichangman picturebox
                byte[] byData = System.Text.Encoding.ASCII.GetBytes(3+rmchances);
                ClientSocket.Send(byData, SocketFlags.None);
                System.Threading.Thread.Sleep(100); // Set a delay from sending this message amongst other messages to prevent collision
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
            // Using pen to draw lines to assemble a hangman picture each time an incorrect letter is entered from the client side
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

            // Declaring the message for the server side losing the game as a string
            String Message = 4 + "Lose";

            // Encodes characters of the losing message into a sequence of bytes and send the message to the server side if the player from the client side wins
            byte[] byData = System.Text.Encoding.ASCII.GetBytes(Message);
            ClientSocket.Send(byData, SocketFlags.None);

            GameTimer.Enabled = false;

            // Resets game and select new random word from the text file
            clear();
            select_word();
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
                btnSelect.Visible = true;
                label1.Visible = true;             
                txtLetter.Visible = true;
                btnSelect.Visible = true;

                // Resets game and select new random word from the text file
                clear();

                String Message = 6 + lblPlayerName.Text;

                // Encodes characters of the losing message into a sequence of bytes and send the message to the server side if the player from the client side wins
                byte[] byData = System.Text.Encoding.ASCII.GetBytes(Message);
                ClientSocket.Send(byData, SocketFlags.None);

                //select_word();
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
            // Reset selected alphabet for client side
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
            lblPlayer2.Text = "";
            ScoreCounter2.Text = "";
            txtLetter.Enabled = true; // Re-enable letter entering after reset
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                // Create the socket, for TCP use
                ClientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                ClientSocket.Blocking = true; // Socket operates in Blocking mode initially
            }
            catch // Handle any exceptions
            {
                Close_Socket_and_Exit();
            }
            try
            {
                // Get the IP address from the appropriate text box
                String szIPAddress = txtIP_Address.Text;
                System.Net.IPAddress DestinationIPAddress = System.Net.IPAddress.Parse(szIPAddress);

                // Get the Port number from the appropriate text box
                String szPort = txtSendPort.Text;
                int iPort = System.Convert.ToInt16(szPort, 10);

                // Combine Address and Port to create an Endpoint
                m_remoteEndPoint = new System.Net.IPEndPoint(DestinationIPAddress, iPort);

                ClientSocket.Connect(m_remoteEndPoint);
                ClientSocket.Blocking = false; // Socket is now switched to Non-Blocking mode for send/ receive activities
                btnConnect.Text = "Player Found. Please Wait";
                btnConnect.Enabled = false;

                CommunicationActivity_Timer.Start(); // Start the timer to perform periodic checking for received messages   

                // Declaring the players name for the server side as a string
                String PlayerName2 = 1 + lblPlayerName.Text;

                // Encodes characters of the players name from the client side into a sequence of bytes and send the message to the server side to view their opponent
                byte[] byData = System.Text.Encoding.ASCII.GetBytes(PlayerName2);
                ClientSocket.Send(byData, SocketFlags.None);
            }
            catch // Catch all exceptions
            {   // If an exception occurs, display an error message
                btnConnect.Text = "Player not found ";
            }
        }

        private void PeriodicCommunicationActivityCheck(Object myObject, EventArgs myEventArgs)
        {   // On timed event that periodicaly checks whether a message has been received    
            try
            {
                EndPoint RemoteEndPoint = (EndPoint)m_remoteEndPoint;
                byte[] ReceiveBuffer = new byte[1024];
                int ReceiveBCount; // Receieve byte count
                ReceiveBCount = ClientSocket.ReceiveFrom(ReceiveBuffer, ref RemoteEndPoint);

                string ReceivedMessage;
                
                if (0 < ReceiveBCount)

                {   // Copy the number of bytes received, from the message buffer to the text control
                    ReceivedMessage = Encoding.ASCII.GetString(ReceiveBuffer, 0, ReceiveBCount);

                    // Declaring string length
                    int string_length;
                    // Split and receieve message which is the player name of the client side and the category name
                    if (ReceivedMessage.Substring(0, 1) == "1") // Gets character substring from string
                    {
                        string message = ReceivedMessage;

                        string[] strarr = message.Split('-');

                        for (int i = 0; i < strarr.Length; i++)
                        {
                            string_length = strarr[i].Count();
                            lblPlayer2.Text = strarr[i].Substring(1, string_length - 1);
                            i = i + 1;
                            lblCategory.Text = strarr[i];
                            i = i + 1;
                            GameWord = strarr[i];
                        }
                        // Select new random word and enable play button for connection with the server
                        select_word();
                        btnPlay.Enabled = true;
                    }

                    // Split and receive the second message for the receiver which is the client sides score counter
                    if (ReceivedMessage.Substring(0, 1) == "2")
                    {
                        string_length = ReceivedMessage.Count();

                        ScoreCounter2.Text = ReceivedMessage.Substring(1, string_length - 1);
                        txtLetter.Enabled = true;
                        btnPass.Enabled = true;
                        GameTimer.Enabled = true;
                    }

                    // Split and receive the third message for the receiver which is the client sides hanging process
                    if (ReceivedMessage.Substring(0, 1) == "3")
                    {
                        string_length = ReceivedMessage.Count();

                        string  rmchances = ReceivedMessage.Substring(1, string_length - 1);
                        remaining_chances2 = Convert.ToInt16(rmchances);
                        picHangman2.Invalidate();
                    }

                    // Split and receive the fourth message for the receiver which is the defeat message to the server if the client successfully gets the word first
                    if (ReceivedMessage.Substring(0, 1) == "4")
                    {
                        DialogResult win = MessageBox.Show("You Lose");
                        GameTimer.Enabled = false;
                        clear();
                    }

                    //Split and receive the 20 second game timer to the server
                    if (ReceivedMessage.Substring(0, 1) == "5")
                    {
                        TimeClock = 20;
                        GameTimer.Enabled = true;
                        txtLetter.Enabled = true;
                        btnYourTurn.Enabled = true;
                        btnPass.Enabled = true;
                    }


                    if (ReceivedMessage.Substring(0, 1) == "7")
                    {
                        DialogResult win = MessageBox.Show("Do not want to play");
                        GameWindow GameWindow = new GameWindow();
                        GameWindow.Show();
                        this.Hide();
                    }
                }
            }
            catch // Silently handle any exceptions
            {
            }
        }

        private void Close_Socket_and_Exit()
        {
            try
            {
                ClientSocket.Shutdown(SocketShutdown.Both);
            }
            catch // Silently handle any exceptions
            {
            }
            try
            {
                ClientSocket.Close();
            }
            catch // Silently handle any exceptions
            {
            }
            this.Close();
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            // Enables visibility of controls once the game begins
            lblWord.Visible = true;
            lblCategory.Visible = true;
            txtLetter.Visible = true;
            btnSelect.Visible = true;

            // Enables game timer and letter entering functionality, starting from the client side
            GameTimer.Enabled = true;
            txtLetter.Enabled = true;
            txtLetter.Focus();
        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            btnYourTurn.Enabled = true;
            TimeClock = TimeClock - 1;

            lblClock.Text = TimeClock.ToString();

            if (TimeClock == 0)
            {              
                lblClock.Text = "00"; // Reset timer label if countdown goes to 0
                btnYourTurn.Enabled = false; // Disable your turn button for visual indication that it is not the players turn anymore
                txtLetter.Enabled = false; // Disables letter entering text box
                btnPass.Enabled = true; // Disables pass button once clicked by the player

                // Declaring the pass message as a string
                string Pass;

                Pass = 5 + "Pass";

                // Encodes characters of the pass message into a sequence of bytes and send the message to the server side
                byte[] byData = System.Text.Encoding.ASCII.GetBytes(Pass);
                ClientSocket.Send(byData, SocketFlags.None);

                // Disables players countdown timer
                GameTimer.Enabled = false;
            }
        }

        private void btnPass_Click(object sender, EventArgs e)
        {
            GameTimer.Enabled = false; // Disables countedown timer if player clicks the pass button
            lblClock.Text = "00"; // Resets timer label
            TimeClock = 20; // Resets timer
            btnYourTurn.Enabled = false; // Disable your turn button for visual indication that it is not the players turn anymore
            txtLetter.Text = ""; // Empties the text box in which the player inputs their letter
            txtLetter.Enabled = false; // Empties the text box in which the player inputs their letter

            // Declaring the pass message as a string
            string Pass;

            Pass = 5 + "Pass";

            // Encodes characters of the pass message into a sequence of bytes and send the message to the server side
            byte[] byData = System.Text.Encoding.ASCII.GetBytes(Pass);
            ClientSocket.Send(byData, SocketFlags.None);
            btnPass.Enabled = false; //Disables button once it is the player from the server sides turn to enter a letter
        }

        private void btnChat_Click(object sender, EventArgs e)
        {

            new ChatRoom().Show();
        }

        private void picHangman2_Paint(object sender, PaintEventArgs e)
        {
            // Using pen to draw lines to assemble a hangman picture each time an incorrect letter is entered from the server side
            Pen pen = new Pen(Color.Blue, 12);
            pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;

            if (remaining_chances2 < lives2)
            {
                e.Graphics.DrawLine(pen, width / 10, height1 / 15, width / 10, height1 / 15 * 14);
                e.Graphics.DrawLine(pen, width / 10, height1 / 15, width / 2, height1 / 15);
            }
            if (remaining_chances2 < lives2 - 1)//head and rope
            {
                e.Graphics.DrawLine(pen, width / 2, height1 / 15, width / 2, height1 / 15 * 3);
                e.Graphics.DrawEllipse(pen, width / 2 - width / 20, height1 / 5, width / 10, height1 / 10);
            }
            if (remaining_chances2 < lives2 - 2)//body
            {
                e.Graphics.DrawLine(pen, width / 2, height1 / 10 * 3, width / 2, height1 / 10 * 6);
            }
            if (remaining_chances2 < lives2 - 3)//right arm
            {
                e.Graphics.DrawLine(pen, width / 2, height1 / 10 * 3, width / 2 + width / 10, height1 / 10 * 3 + height1 / 5);
            }
            if (remaining_chances2 < lives2 - 4)//left arm
            {
                e.Graphics.DrawLine(pen, width / 2, height1 / 10 * 3, width / 2 - width / 10, height1 / 10 * 3 + height1 / 5);
            }
            if (remaining_chances2 < lives2 - 5)//right leg 
            {
                e.Graphics.DrawLine(pen, width / 2, height1 / 10 * 6, width / 2 + width / 10, height1 / 10 * 6 + height1 / 10);
            }
            if (remaining_chances2 < lives2 - 6)//left leg
            {
                e.Graphics.DrawLine(pen, width / 2, height1 / 10 * 6, width / 2 - width / 10, height1 / 10 * 6 + height1 / 10);
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
