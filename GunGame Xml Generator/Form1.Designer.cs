namespace GunGame_Xml_Generator
{
    partial class GunGameGenerator
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
            this.ChooseZItem = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.ChooseString = new System.Windows.Forms.Button();
            this.StartProcess = new System.Windows.Forms.Button();
            this.Status = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // ChooseZItem
            // 
            this.ChooseZItem.Location = new System.Drawing.Point(21, 29);
            this.ChooseZItem.Name = "ChooseZItem";
            this.ChooseZItem.Size = new System.Drawing.Size(111, 46);
            this.ChooseZItem.TabIndex = 1;
            this.ChooseZItem.Text = "Choose file";
            this.ChooseZItem.UseVisualStyleBackColor = true;
            this.ChooseZItem.Click += new System.EventHandler(this.ChooseZItem_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ChooseZItem);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(377, 97);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Zitem.xml";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.ChooseString);
            this.groupBox2.Location = new System.Drawing.Point(12, 115);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(377, 97);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "string.xml";
            // 
            // ChooseString
            // 
            this.ChooseString.Location = new System.Drawing.Point(21, 29);
            this.ChooseString.Name = "ChooseString";
            this.ChooseString.Size = new System.Drawing.Size(111, 46);
            this.ChooseString.TabIndex = 1;
            this.ChooseString.Text = "Choose file";
            this.ChooseString.UseVisualStyleBackColor = true;
            this.ChooseString.Click += new System.EventHandler(this.ChooseString_Click);
            // 
            // StartProcess
            // 
            this.StartProcess.Location = new System.Drawing.Point(104, 228);
            this.StartProcess.Name = "StartProcess";
            this.StartProcess.Size = new System.Drawing.Size(213, 46);
            this.StartProcess.TabIndex = 2;
            this.StartProcess.Text = "Get gungame.xml";
            this.StartProcess.UseVisualStyleBackColor = true;
            this.StartProcess.Click += new System.EventHandler(this.StartProcess_Click);
            // 
            // Status
            // 
            this.Status.AutoSize = true;
            this.Status.ForeColor = System.Drawing.Color.DarkCyan;
            this.Status.Location = new System.Drawing.Point(29, 307);
            this.Status.Name = "Status";
            this.Status.Size = new System.Drawing.Size(340, 20);
            this.Status.TabIndex = 4;
            this.Status.Text = "**** Successfully Generated GunGame.xml ****";
            // 
            // GunGameGenerator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(402, 355);
            this.Controls.Add(this.Status);
            this.Controls.Add(this.StartProcess);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "GunGameGenerator";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "GunGame Generator";
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ChooseZItem;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button ChooseString;
        private System.Windows.Forms.Button StartProcess;
        private System.Windows.Forms.Label Status;
    }
}

