namespace HangmanGame
{
    partial class ChatRoom
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
            this.IP_Address_textBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ReceivePort_textBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.SendPort_textBox = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.Message_textBox = new System.Windows.Forms.TextBox();
            this.txtSendingMessage = new System.Windows.Forms.TextBox();
            this.Send_button = new System.Windows.Forms.Button();
            this.Auto_Received_timer = new System.Windows.Forms.Timer(this.components);
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // IP_Address_textBox
            // 
            this.IP_Address_textBox.Location = new System.Drawing.Point(158, 24);
            this.IP_Address_textBox.Name = "IP_Address_textBox";
            this.IP_Address_textBox.ReadOnly = true;
            this.IP_Address_textBox.Size = new System.Drawing.Size(107, 20);
            this.IP_Address_textBox.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Local IP Address";
            // 
            // ReceivePort_textBox
            // 
            this.ReceivePort_textBox.Location = new System.Drawing.Point(160, 50);
            this.ReceivePort_textBox.Name = "ReceivePort_textBox";
            this.ReceivePort_textBox.Size = new System.Drawing.Size(69, 20);
            this.ReceivePort_textBox.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Port for receiving";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.SendPort_textBox);
            this.groupBox1.Controls.Add(this.ReceivePort_textBox);
            this.groupBox1.Controls.Add(this.IP_Address_textBox);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(2, 1);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(287, 130);
            this.groupBox1.TabIndex = 19;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Configuration";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(23, 83);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(81, 13);
            this.label4.TabIndex = 17;
            this.label4.Text = "Port for sending";
            // 
            // SendPort_textBox
            // 
            this.SendPort_textBox.Location = new System.Drawing.Point(160, 76);
            this.SendPort_textBox.Name = "SendPort_textBox";
            this.SendPort_textBox.Size = new System.Drawing.Size(69, 20);
            this.SendPort_textBox.TabIndex = 16;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.Message_textBox);
            this.groupBox2.Location = new System.Drawing.Point(2, 137);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(287, 205);
            this.groupBox2.TabIndex = 20;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Message";
            // 
            // Message_textBox
            // 
            this.Message_textBox.AccessibleRole = System.Windows.Forms.AccessibleRole.ScrollBar;
            this.Message_textBox.Location = new System.Drawing.Point(6, 19);
            this.Message_textBox.Multiline = true;
            this.Message_textBox.Name = "Message_textBox";
            this.Message_textBox.Size = new System.Drawing.Size(275, 171);
            this.Message_textBox.TabIndex = 9;
            // 
            // txtSendingMessage
            // 
            this.txtSendingMessage.Location = new System.Drawing.Point(6, 348);
            this.txtSendingMessage.Name = "txtSendingMessage";
            this.txtSendingMessage.Size = new System.Drawing.Size(277, 20);
            this.txtSendingMessage.TabIndex = 22;
            // 
            // Send_button
            // 
            this.Send_button.Location = new System.Drawing.Point(62, 374);
            this.Send_button.Name = "Send_button";
            this.Send_button.Size = new System.Drawing.Size(141, 37);
            this.Send_button.TabIndex = 21;
            this.Send_button.Text = "Send ";
            this.Send_button.UseVisualStyleBackColor = true;
            this.Send_button.Click += new System.EventHandler(this.Send_button_Click);
            // 
            // Auto_Received_timer
            // 
            this.Auto_Received_timer.Tick += new System.EventHandler(this.Auto_Received_timer_Tick);
            // 
            // ChatRoom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(305, 433);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.txtSendingMessage);
            this.Controls.Add(this.Send_button);
            this.Name = "ChatRoom";
            this.Text = "ChatRoom";
            this.Load += new System.EventHandler(this.ChatRoom_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox IP_Address_textBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox ReceivePort_textBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox SendPort_textBox;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox Message_textBox;
        private System.Windows.Forms.TextBox txtSendingMessage;
        private System.Windows.Forms.Button Send_button;
        private System.Windows.Forms.Timer Auto_Received_timer;
        private System.Windows.Forms.Label label4;
    }
}