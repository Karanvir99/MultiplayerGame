using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace HangmanGame
{
    public partial class MainMenu : Form
    {
        //Referencing for textfield
        public static string Playername;

        public MainMenu()
        {
            InitializeComponent();
        }

        private void btnProceed_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtPlayerName.Text)) 
            {
                MessageBox.Show("Please enter your player name!"); //If textfield is empty then show this message 
            }
            else
            {
                //If field is not empty then display the GameWindow
                Playername = txtPlayerName.Text;

                new GameWindow().Show();
                Hide();
            }
        }
    }
}
    

