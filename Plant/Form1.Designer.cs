namespace Plant
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
            components = new System.ComponentModel.Container();
            timer1 = new System.Windows.Forms.Timer(components);
            menuStrip1 = new MenuStrip();
            plant1ToolStripMenuItem = new ToolStripMenuItem();
            plant2ToolStripMenuItem = new ToolStripMenuItem();
            plant3ToolStripMenuItem = new ToolStripMenuItem();
            plant4ToolStripMenuItem = new ToolStripMenuItem();
            plant5ToolStripMenuItem = new ToolStripMenuItem();
            plant6ToolStripMenuItem = new ToolStripMenuItem();
            plant7ToolStripMenuItem = new ToolStripMenuItem();
            plant8ToolStripMenuItem = new ToolStripMenuItem();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { plant1ToolStripMenuItem, plant2ToolStripMenuItem, plant3ToolStripMenuItem, plant4ToolStripMenuItem, plant5ToolStripMenuItem, plant6ToolStripMenuItem, plant7ToolStripMenuItem, plant8ToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(800, 25);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // plant1ToolStripMenuItem
            // 
            plant1ToolStripMenuItem.Name = "plant1ToolStripMenuItem";
            plant1ToolStripMenuItem.Size = new Size(56, 21);
            plant1ToolStripMenuItem.Text = "plant1";
            plant1ToolStripMenuItem.Click += plant1_Click;
            // 
            // plant2ToolStripMenuItem
            // 
            plant2ToolStripMenuItem.Name = "plant2ToolStripMenuItem";
            plant2ToolStripMenuItem.Size = new Size(56, 21);
            plant2ToolStripMenuItem.Text = "plant2";
            plant2ToolStripMenuItem.Click += plant2_Click;
            // 
            // plant3ToolStripMenuItem
            // 
            plant3ToolStripMenuItem.Name = "plant3ToolStripMenuItem";
            plant3ToolStripMenuItem.Size = new Size(56, 21);
            plant3ToolStripMenuItem.Text = "plant3";
            plant3ToolStripMenuItem.Click += plant3_Click;
            // 
            // plant4ToolStripMenuItem
            // 
            plant4ToolStripMenuItem.Name = "plant4ToolStripMenuItem";
            plant4ToolStripMenuItem.Size = new Size(56, 21);
            plant4ToolStripMenuItem.Text = "plant4";
            plant4ToolStripMenuItem.Click += plant4_Click;
            // 
            // plant5ToolStripMenuItem
            // 
            plant5ToolStripMenuItem.Name = "plant5ToolStripMenuItem";
            plant5ToolStripMenuItem.Size = new Size(56, 21);
            plant5ToolStripMenuItem.Text = "plant5";
            plant5ToolStripMenuItem.Click += plant5_Click;
            // 
            // plant6ToolStripMenuItem
            // 
            plant6ToolStripMenuItem.Name = "plant6ToolStripMenuItem";
            plant6ToolStripMenuItem.Size = new Size(56, 21);
            plant6ToolStripMenuItem.Text = "plant6";
            plant6ToolStripMenuItem.Click += plant6_Click;
            // 
            // plant7ToolStripMenuItem
            // 
            plant7ToolStripMenuItem.Name = "plant7ToolStripMenuItem";
            plant7ToolStripMenuItem.Size = new Size(56, 21);
            plant7ToolStripMenuItem.Text = "plant7";
            plant7ToolStripMenuItem.Click += plant7_Click;
            // 
            // plant8ToolStripMenuItem
            // 
            plant8ToolStripMenuItem.Name = "plant8ToolStripMenuItem";
            plant8ToolStripMenuItem.Size = new Size(56, 21);
            plant8ToolStripMenuItem.Text = "plant8";
            plant8ToolStripMenuItem.Click += plant8_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "Form1";
            Text = "Form1";
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem plant1ToolStripMenuItem;
        private ToolStripMenuItem plant2ToolStripMenuItem;
        private ToolStripMenuItem plant3ToolStripMenuItem;
        private ToolStripMenuItem plant4ToolStripMenuItem;
        private ToolStripMenuItem plant5ToolStripMenuItem;
        private ToolStripMenuItem plant6ToolStripMenuItem;
        private ToolStripMenuItem plant7ToolStripMenuItem;
        private ToolStripMenuItem plant8ToolStripMenuItem;
    }
}