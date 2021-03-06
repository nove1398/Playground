﻿
namespace TimerTest
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.statusListBox = new System.Windows.Forms.ListBox();
            this.startServiceButton = new System.Windows.Forms.Button();
            this.stopServiceButton = new System.Windows.Forms.Button();
            this.statusButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioButton4 = new System.Windows.Forms.RadioButton();
            this.serviceRunningRadio = new System.Windows.Forms.RadioButton();
            this.serviceStartingRadio = new System.Windows.Forms.RadioButton();
            this.serviceStoppedRadio = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox
            // 
            this.pictureBox.Location = new System.Drawing.Point(8, 8);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(168, 112);
            this.pictureBox.TabIndex = 0;
            this.pictureBox.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Calibri", 25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(248, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(284, 41);
            this.label1.TabIndex = 1;
            this.label1.Text = "MTM Email Service";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.statusListBox);
            this.panel1.Location = new System.Drawing.Point(32, 448);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(760, 216);
            this.panel1.TabIndex = 2;
            // 
            // statusListBox
            // 
            this.statusListBox.FormattingEnabled = true;
            this.statusListBox.ItemHeight = 15;
            this.statusListBox.Location = new System.Drawing.Point(0, 0);
            this.statusListBox.Name = "statusListBox";
            this.statusListBox.Size = new System.Drawing.Size(760, 214);
            this.statusListBox.TabIndex = 0;
            // 
            // startServiceButton
            // 
            this.startServiceButton.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.startServiceButton.Location = new System.Drawing.Point(640, 64);
            this.startServiceButton.Name = "startServiceButton";
            this.startServiceButton.Size = new System.Drawing.Size(112, 32);
            this.startServiceButton.TabIndex = 3;
            this.startServiceButton.Text = "Start Service";
            this.startServiceButton.UseVisualStyleBackColor = true;
            this.startServiceButton.Click += new System.EventHandler(this.StartServiceButton_Click);
            // 
            // stopServiceButton
            // 
            this.stopServiceButton.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.stopServiceButton.Location = new System.Drawing.Point(640, 104);
            this.stopServiceButton.Name = "stopServiceButton";
            this.stopServiceButton.Size = new System.Drawing.Size(112, 32);
            this.stopServiceButton.TabIndex = 4;
            this.stopServiceButton.Text = "Stop Service";
            this.stopServiceButton.UseVisualStyleBackColor = true;
            this.stopServiceButton.Click += new System.EventHandler(this.StopServiceButton_Click);
            // 
            // statusButton
            // 
            this.statusButton.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.statusButton.Location = new System.Drawing.Point(640, 144);
            this.statusButton.Name = "statusButton";
            this.statusButton.Size = new System.Drawing.Size(112, 32);
            this.statusButton.TabIndex = 5;
            this.statusButton.Text = "Service Status";
            this.statusButton.UseVisualStyleBackColor = true;
            this.statusButton.Click += new System.EventHandler(this.StatusButton_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioButton4);
            this.groupBox1.Controls.Add(this.serviceRunningRadio);
            this.groupBox1.Controls.Add(this.serviceStartingRadio);
            this.groupBox1.Controls.Add(this.serviceStoppedRadio);
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.groupBox1.Location = new System.Drawing.Point(296, 208);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 168);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Service Status";
            // 
            // radioButton4
            // 
            this.radioButton4.AutoSize = true;
            this.radioButton4.Enabled = false;
            this.radioButton4.Location = new System.Drawing.Point(8, 128);
            this.radioButton4.Name = "radioButton4";
            this.radioButton4.Size = new System.Drawing.Size(119, 25);
            this.radioButton4.TabIndex = 3;
            this.radioButton4.Text = "radioButton4";
            this.radioButton4.UseVisualStyleBackColor = true;
            // 
            // serviceRunningRadio
            // 
            this.serviceRunningRadio.AutoSize = true;
            this.serviceRunningRadio.Enabled = false;
            this.serviceRunningRadio.Location = new System.Drawing.Point(8, 96);
            this.serviceRunningRadio.Name = "serviceRunningRadio";
            this.serviceRunningRadio.Size = new System.Drawing.Size(141, 25);
            this.serviceRunningRadio.TabIndex = 2;
            this.serviceRunningRadio.Text = "Service Running";
            this.serviceRunningRadio.UseVisualStyleBackColor = true;
            // 
            // serviceStartingRadio
            // 
            this.serviceStartingRadio.AutoSize = true;
            this.serviceStartingRadio.Enabled = false;
            this.serviceStartingRadio.Location = new System.Drawing.Point(8, 64);
            this.serviceStartingRadio.Name = "serviceStartingRadio";
            this.serviceStartingRadio.Size = new System.Drawing.Size(136, 25);
            this.serviceStartingRadio.TabIndex = 1;
            this.serviceStartingRadio.Text = "Service Starting";
            this.serviceStartingRadio.UseVisualStyleBackColor = true;
            // 
            // serviceStoppedRadio
            // 
            this.serviceStoppedRadio.AutoSize = true;
            this.serviceStoppedRadio.BackColor = System.Drawing.SystemColors.Control;
            this.serviceStoppedRadio.Checked = true;
            this.serviceStoppedRadio.Enabled = false;
            this.serviceStoppedRadio.Location = new System.Drawing.Point(8, 32);
            this.serviceStoppedRadio.Name = "serviceStoppedRadio";
            this.serviceStoppedRadio.Size = new System.Drawing.Size(139, 25);
            this.serviceStoppedRadio.TabIndex = 0;
            this.serviceStoppedRadio.TabStop = true;
            this.serviceStoppedRadio.Text = "Service Stopped";
            this.serviceStoppedRadio.UseVisualStyleBackColor = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(824, 681);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.statusButton);
            this.Controls.Add(this.stopServiceButton);
            this.Controls.Add(this.startServiceButton);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ListBox statusListBox;
        private System.Windows.Forms.Button startServiceButton;
        private System.Windows.Forms.Button stopServiceButton;
        private System.Windows.Forms.Button statusButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioButton4;
        private System.Windows.Forms.RadioButton serviceRunningRadio;
        private System.Windows.Forms.RadioButton serviceStartingRadio;
        private System.Windows.Forms.RadioButton serviceStoppedRadio;
    }
}

