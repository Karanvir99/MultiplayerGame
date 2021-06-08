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
            this.SendPort_textBox = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.Message_textBox = new System.Windows.Forms.TextBox();
            this.Send_button = new System.Windows.Forms.Button();
            this.txtSendingMessage = new System.Windows.Forms.TextBox();
            this.Auto_Received_timer = new System.Windows.Forms.Timer(this.components);
            this.label4 = new System.Windows.Forms.Label();
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
            this.ReceivePort_textBox.Location = new System.Drawing.Point(158, 53);
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
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(287, 130);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Configuration";
            // 
            // SendPort_textBox
            // 
            this.SendPort_textBox.Location = new System.Drawing.Point(158, 83);
            this.SendPort_textBox.Name = "SendPort_textBox";
            this.SendPort_textBox.Size = new System.Drawing.Size(69, 20);
            this.SendPort_textBox.TabIndex = 16;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.Message_textBox);
            this.groupBox2.Location = new System.Drawing.Point(12, 148);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(287, 223);
            this.groupBox2.TabIndex = 14;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Message";
            // 
            // Message_textBox
            // 
            this.Message_textBox.AccessibleRole = System.Windows.Forms.AccessibleRole.ScrollBar;
            this.Message_textBox.Location = new System.Drawing.Point(10, 19);
            this.Message_textBox.Multiline = true;
            this.Message_textBox.Name = "Message_textBox";
            this.Message_textBox.Size = new System.Drawing.Size(265, 198);
            this.Message_textBox.TabIndex = 9;
            this.Message_textBox.TextChanged += new System.EventHandler(this.Message_textBox_TextChanged);
            this.Message_textBox.VisibleChanged += new System.EventHandler(this.Message_textBox_VisibleChanged);
            // 
            // Send_button
            // 
            this.Send_button.Location = new System.Drawing.Point(75, 410);
            this.Send_button.Name = "Send_button";
            this.Send_button.Size = new System.Drawing.Size(141, 37);
            this.Send_button.TabIndex = 15;
            this.Send_button.Text = "Send";
            this.Send_button.UseVisualStyleBackColor = true;
            this.Send_button.Click += new System.EventHandler(this.Send_button_Click);
            // 
            // txtSendingMessage
            // 
            this.txtSendingMessage.Location = new System.Drawing.Point(22, 384);
            this.txtSendingMessage.Name = "txtSendingMessage";
            this.txtSendingMessage.Size = new System.Drawing.Size(265, 20);
            this.txtSendingMessage.TabIndex = 16;
            // 
            // Auto_Received_timer
            // 
            this.Auto_Received_timer.Enabled = true;
            this.Auto_Received_timer.Tick += new System.EventHandler(this.Auto_Received_timer_Tick);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(23, 83);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(83, 13);
            this.label4.TabIndex = 17;
            this.label4.Text = "Port for Sending";
            // 
            // ChatRoom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(305, 478);
            this.Controls.Add(this.txtSendingMessage);
            this.Controls.Add(this.Send_button);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Name = "ChatRoom";
            this.Text = "ChatRoom  (Server Side)";
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
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button Send_button;
        private System.Windows.Forms.TextBox SendPort_textBox;
        private System.Windows.Forms.TextBox Message_textBox;
        private System.Windows.Forms.TextBox txtSendingMessage;
        private System.Windows.Forms.Timer Auto_Received_timer;
        private System.Windows.Forms.Label label4;
    }
}