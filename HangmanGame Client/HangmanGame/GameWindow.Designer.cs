namespace HangmanGame
{
    partial class GameWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.lblWord = new System.Windows.Forms.Label();
            this.picHangman = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnChat = new System.Windows.Forms.Button();
            this.btnYourTurn = new System.Windows.Forms.Button();
            this.lblClock = new System.Windows.Forms.Label();
            this.btnPass = new System.Windows.Forms.Button();
            this.lblLettersUsed = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtSendPort = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.btnPlay = new System.Windows.Forms.Button();
            this.txtIP_Address = new System.Windows.Forms.TextBox();
            this.btnConnect = new System.Windows.Forms.Button();
            this.picHangman2 = new System.Windows.Forms.PictureBox();
            this.ScoreCounter2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblPlayer2 = new System.Windows.Forms.Label();
            this.ScoreCounter = new System.Windows.Forms.Label();
            this.lblScore = new System.Windows.Forms.Label();
            this.lblPlayerName = new System.Windows.Forms.Label();
            this.btnSelect = new System.Windows.Forms.Button();
            this.txtLetter = new System.Windows.Forms.TextBox();
            this.lblCategory = new System.Windows.Forms.Label();
            this.GameTimer = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.picHangman)).BeginInit();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picHangman2)).BeginInit();
            this.SuspendLayout();
            // 
            // lblWord
            // 
            this.lblWord.AutoSize = true;
            this.lblWord.Font = new System.Drawing.Font("Segoe Print", 15F, System.Drawing.FontStyle.Bold);
            this.lblWord.ForeColor = System.Drawing.Color.White;
            this.lblWord.Location = new System.Drawing.Point(574, 87);
            this.lblWord.Name = "lblWord";
            this.lblWord.Size = new System.Drawing.Size(0, 35);
            this.lblWord.TabIndex = 0;
            this.lblWord.Visible = false;
            // 
            // picHangman
            // 
            this.picHangman.Location = new System.Drawing.Point(83, 125);
            this.picHangman.Name = "picHangman";
            this.picHangman.Size = new System.Drawing.Size(455, 527);
            this.picHangman.TabIndex = 1;
            this.picHangman.TabStop = false;
            this.picHangman.Visible = false;
            this.picHangman.Paint += new System.Windows.Forms.PaintEventHandler(this.picHangman_Paint);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.btnChat);
            this.panel1.Controls.Add(this.btnYourTurn);
            this.panel1.Controls.Add(this.lblClock);
            this.panel1.Controls.Add(this.btnPass);
            this.panel1.Controls.Add(this.lblLettersUsed);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.picHangman2);
            this.panel1.Controls.Add(this.ScoreCounter2);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.lblPlayer2);
            this.panel1.Controls.Add(this.ScoreCounter);
            this.panel1.Controls.Add(this.lblScore);
            this.panel1.Controls.Add(this.lblPlayerName);
            this.panel1.Controls.Add(this.btnSelect);
            this.panel1.Controls.Add(this.txtLetter);
            this.panel1.Controls.Add(this.lblCategory);
            this.panel1.Controls.Add(this.picHangman);
            this.panel1.Controls.Add(this.lblWord);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1229, 913);
            this.panel1.TabIndex = 2;
            // 
            // btnChat
            // 
            this.btnChat.FlatAppearance.BorderSize = 3;
            this.btnChat.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnChat.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Black;
            this.btnChat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnChat.Font = new System.Drawing.Font("Segoe Print", 15F, System.Drawing.FontStyle.Bold);
            this.btnChat.ForeColor = System.Drawing.Color.White;
            this.btnChat.Location = new System.Drawing.Point(385, 698);
            this.btnChat.Name = "btnChat";
            this.btnChat.Size = new System.Drawing.Size(90, 45);
            this.btnChat.TabIndex = 35;
            this.btnChat.Text = "Chat";
            this.btnChat.UseVisualStyleBackColor = true;
            this.btnChat.Click += new System.EventHandler(this.btnChat_Click);
            // 
            // btnYourTurn
            // 
            this.btnYourTurn.Enabled = false;
            this.btnYourTurn.FlatAppearance.BorderSize = 0;
            this.btnYourTurn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnYourTurn.Font = new System.Drawing.Font("Segoe Print", 15F, System.Drawing.FontStyle.Bold);
            this.btnYourTurn.ForeColor = System.Drawing.Color.White;
            this.btnYourTurn.Location = new System.Drawing.Point(552, 470);
            this.btnYourTurn.Name = "btnYourTurn";
            this.btnYourTurn.Size = new System.Drawing.Size(127, 55);
            this.btnYourTurn.TabIndex = 32;
            this.btnYourTurn.Text = "Your turn";
            this.btnYourTurn.UseVisualStyleBackColor = true;
            // 
            // lblClock
            // 
            this.lblClock.AutoSize = true;
            this.lblClock.Font = new System.Drawing.Font("Segoe Print", 15F, System.Drawing.FontStyle.Bold);
            this.lblClock.ForeColor = System.Drawing.Color.White;
            this.lblClock.Location = new System.Drawing.Point(597, 375);
            this.lblClock.Name = "lblClock";
            this.lblClock.Size = new System.Drawing.Size(41, 35);
            this.lblClock.TabIndex = 31;
            this.lblClock.Text = "--";
            // 
            // btnPass
            // 
            this.btnPass.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPass.FlatAppearance.BorderSize = 3;
            this.btnPass.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnPass.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Black;
            this.btnPass.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPass.Font = new System.Drawing.Font("Segoe Print", 15F, System.Drawing.FontStyle.Bold);
            this.btnPass.ForeColor = System.Drawing.Color.White;
            this.btnPass.Location = new System.Drawing.Point(264, 698);
            this.btnPass.Name = "btnPass";
            this.btnPass.Size = new System.Drawing.Size(90, 45);
            this.btnPass.TabIndex = 30;
            this.btnPass.Text = "Pass";
            this.btnPass.UseVisualStyleBackColor = true;
            this.btnPass.Click += new System.EventHandler(this.btnPass_Click);
            // 
            // lblLettersUsed
            // 
            this.lblLettersUsed.AutoSize = true;
            this.lblLettersUsed.Font = new System.Drawing.Font("Segoe Print", 15F, System.Drawing.FontStyle.Bold);
            this.lblLettersUsed.ForeColor = System.Drawing.Color.White;
            this.lblLettersUsed.Location = new System.Drawing.Point(220, 758);
            this.lblLettersUsed.Name = "lblLettersUsed";
            this.lblLettersUsed.Size = new System.Drawing.Size(0, 35);
            this.lblLettersUsed.TabIndex = 6;
            this.lblLettersUsed.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe Print", 15F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(77, 758);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(141, 35);
            this.label1.TabIndex = 7;
            this.label1.Text = "Letters used:";
            this.label1.Visible = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtSendPort);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.btnPlay);
            this.groupBox1.Controls.Add(this.txtIP_Address);
            this.groupBox1.Controls.Add(this.btnConnect);
            this.groupBox1.Font = new System.Drawing.Font("Segoe Print", 15F, System.Drawing.FontStyle.Bold);
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(712, 710);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(482, 172);
            this.groupBox1.TabIndex = 28;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Communication";
            // 
            // txtSendPort
            // 
            this.txtSendPort.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txtSendPort.Location = new System.Drawing.Point(12, 138);
            this.txtSendPort.Name = "txtSendPort";
            this.txtSendPort.Size = new System.Drawing.Size(69, 20);
            this.txtSendPort.TabIndex = 5;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 39);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(268, 35);
            this.label6.TabIndex = 1;
            this.label6.Text = "IP Address of Destination";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 100);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(145, 35);
            this.label7.TabIndex = 4;
            this.label7.Text = "Sending port";
            // 
            // btnPlay
            // 
            this.btnPlay.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPlay.Enabled = false;
            this.btnPlay.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnPlay.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Black;
            this.btnPlay.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPlay.Font = new System.Drawing.Font("Segoe Print", 15F, System.Drawing.FontStyle.Bold);
            this.btnPlay.ForeColor = System.Drawing.Color.White;
            this.btnPlay.Location = new System.Drawing.Point(310, 120);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(166, 48);
            this.btnPlay.TabIndex = 29;
            this.btnPlay.Text = "Play";
            this.btnPlay.UseVisualStyleBackColor = true;
            this.btnPlay.Click += new System.EventHandler(this.btnPlay_Click);
            // 
            // txtIP_Address
            // 
            this.txtIP_Address.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txtIP_Address.Location = new System.Drawing.Point(12, 77);
            this.txtIP_Address.Name = "txtIP_Address";
            this.txtIP_Address.Size = new System.Drawing.Size(165, 20);
            this.txtIP_Address.TabIndex = 0;
            // 
            // btnConnect
            // 
            this.btnConnect.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnConnect.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnConnect.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Black;
            this.btnConnect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConnect.Font = new System.Drawing.Font("Segoe Print", 15F, System.Drawing.FontStyle.Bold);
            this.btnConnect.ForeColor = System.Drawing.Color.White;
            this.btnConnect.Location = new System.Drawing.Point(310, 66);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(166, 48);
            this.btnConnect.TabIndex = 9;
            this.btnConnect.Text = "Send invite";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // picHangman2
            // 
            this.picHangman2.Location = new System.Drawing.Point(732, 125);
            this.picHangman2.Name = "picHangman2";
            this.picHangman2.Size = new System.Drawing.Size(455, 527);
            this.picHangman2.TabIndex = 14;
            this.picHangman2.TabStop = false;
            this.picHangman2.Visible = false;
            this.picHangman2.Paint += new System.Windows.Forms.PaintEventHandler(this.picHangman2_Paint);
            // 
            // ScoreCounter2
            // 
            this.ScoreCounter2.AutoSize = true;
            this.ScoreCounter2.Font = new System.Drawing.Font("Segoe Print", 15F, System.Drawing.FontStyle.Bold);
            this.ScoreCounter2.ForeColor = System.Drawing.Color.White;
            this.ScoreCounter2.Location = new System.Drawing.Point(946, 87);
            this.ScoreCounter2.Name = "ScoreCounter2";
            this.ScoreCounter2.Size = new System.Drawing.Size(30, 35);
            this.ScoreCounter2.TabIndex = 13;
            this.ScoreCounter2.Text = "0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe Print", 15F, System.Drawing.FontStyle.Bold);
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(879, 87);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 35);
            this.label3.TabIndex = 12;
            this.label3.Text = "Score:";
            // 
            // lblPlayer2
            // 
            this.lblPlayer2.AutoSize = true;
            this.lblPlayer2.Font = new System.Drawing.Font("Segoe Print", 15F, System.Drawing.FontStyle.Bold);
            this.lblPlayer2.ForeColor = System.Drawing.Color.White;
            this.lblPlayer2.Location = new System.Drawing.Point(1109, 28);
            this.lblPlayer2.Name = "lblPlayer2";
            this.lblPlayer2.Size = new System.Drawing.Size(94, 35);
            this.lblPlayer2.TabIndex = 11;
            this.lblPlayer2.Text = "Player2";
            // 
            // ScoreCounter
            // 
            this.ScoreCounter.AutoSize = true;
            this.ScoreCounter.Font = new System.Drawing.Font("Segoe Print", 15F, System.Drawing.FontStyle.Bold);
            this.ScoreCounter.ForeColor = System.Drawing.Color.White;
            this.ScoreCounter.Location = new System.Drawing.Point(325, 87);
            this.ScoreCounter.Name = "ScoreCounter";
            this.ScoreCounter.Size = new System.Drawing.Size(30, 35);
            this.ScoreCounter.TabIndex = 10;
            this.ScoreCounter.Text = "0";
            // 
            // lblScore
            // 
            this.lblScore.AutoSize = true;
            this.lblScore.Font = new System.Drawing.Font("Segoe Print", 15F, System.Drawing.FontStyle.Bold);
            this.lblScore.ForeColor = System.Drawing.Color.White;
            this.lblScore.Location = new System.Drawing.Point(258, 87);
            this.lblScore.Name = "lblScore";
            this.lblScore.Size = new System.Drawing.Size(75, 35);
            this.lblScore.TabIndex = 9;
            this.lblScore.Text = "Score:";
            // 
            // lblPlayerName
            // 
            this.lblPlayerName.AutoSize = true;
            this.lblPlayerName.Font = new System.Drawing.Font("Segoe Print", 15F, System.Drawing.FontStyle.Bold);
            this.lblPlayerName.ForeColor = System.Drawing.Color.White;
            this.lblPlayerName.Location = new System.Drawing.Point(29, 28);
            this.lblPlayerName.Name = "lblPlayerName";
            this.lblPlayerName.Size = new System.Drawing.Size(137, 35);
            this.lblPlayerName.TabIndex = 8;
            this.lblPlayerName.Text = "PlayerName";
            // 
            // btnSelect
            // 
            this.btnSelect.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSelect.FlatAppearance.BorderSize = 3;
            this.btnSelect.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnSelect.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Black;
            this.btnSelect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSelect.Font = new System.Drawing.Font("Segoe Print", 15F, System.Drawing.FontStyle.Bold);
            this.btnSelect.ForeColor = System.Drawing.Color.White;
            this.btnSelect.Location = new System.Drawing.Point(134, 698);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(102, 45);
            this.btnSelect.TabIndex = 5;
            this.btnSelect.Text = "Enter";
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Visible = false;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // txtLetter
            // 
            this.txtLetter.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLetter.Location = new System.Drawing.Point(83, 699);
            this.txtLetter.MaxLength = 1;
            this.txtLetter.Name = "txtLetter";
            this.txtLetter.Size = new System.Drawing.Size(45, 44);
            this.txtLetter.TabIndex = 4;
            this.txtLetter.Visible = false;
            // 
            // lblCategory
            // 
            this.lblCategory.AutoSize = true;
            this.lblCategory.Font = new System.Drawing.Font("Segoe Print", 15F, System.Drawing.FontStyle.Bold);
            this.lblCategory.ForeColor = System.Drawing.Color.White;
            this.lblCategory.Location = new System.Drawing.Point(563, 46);
            this.lblCategory.Name = "lblCategory";
            this.lblCategory.Size = new System.Drawing.Size(104, 35);
            this.lblCategory.TabIndex = 3;
            this.lblCategory.Text = "Category";
            this.lblCategory.Visible = false;
            // 
            // GameTimer
            // 
            this.GameTimer.Interval = 1000;
            this.GameTimer.Tick += new System.EventHandler(this.GameTimer_Tick);
            // 
            // GameWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::HangmanGame.Properties.Resources.Chalkboard;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1253, 937);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.Name = "GameWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Game Window (Client side)";
            this.Load += new System.EventHandler(this.GameWindow_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picHangman)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picHangman2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblWord;
        private System.Windows.Forms.PictureBox picHangman;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblCategory;
        private System.Windows.Forms.Button btnSelect;
        private System.Windows.Forms.TextBox txtLetter;
        private System.Windows.Forms.Label lblLettersUsed;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblPlayerName;
        private System.Windows.Forms.Label ScoreCounter;
        private System.Windows.Forms.Label lblScore;
        private System.Windows.Forms.Label ScoreCounter2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblPlayer2;
        private System.Windows.Forms.PictureBox picHangman2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.TextBox txtSendPort;
        private System.Windows.Forms.TextBox txtIP_Address;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnPlay;
        private System.Windows.Forms.Label lblClock;
        private System.Windows.Forms.Button btnPass;
        private System.Windows.Forms.Timer GameTimer;
        private System.Windows.Forms.Button btnYourTurn;
        private System.Windows.Forms.Button btnChat;
    }
}

