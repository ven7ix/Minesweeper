namespace Minesweeper
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
            picField = new PictureBox();
            lblMines = new Label();
            lblTimer = new Label();
            comboDifficulty = new ComboBox();
            menuStrip = new MenuStrip();
            ((System.ComponentModel.ISupportInitialize)picField).BeginInit();
            SuspendLayout();
            // 
            // picField
            // 
            picField.Location = new Point(10, 40);
            picField.Name = "picField";
            picField.Size = new Size(600, 400);
            picField.TabIndex = 0;
            picField.TabStop = false;
            picField.MouseClick += PicField_MouseClick;
            // 
            // lblMines
            // 
            lblMines.AutoSize = true;
            lblMines.Location = new Point(10, 15);
            lblMines.Name = "lblMines";
            lblMines.Size = new Size(38, 15);
            lblMines.TabIndex = 1;
            lblMines.Text = "label1";
            // 
            // lblTimer
            // 
            lblTimer.AutoSize = true;
            lblTimer.Location = new Point(73, 15);
            lblTimer.Name = "lblTimer";
            lblTimer.Size = new Size(38, 15);
            lblTimer.TabIndex = 2;
            lblTimer.Text = "label2";
            // 
            // comboDifficulty
            // 
            comboDifficulty.FormattingEnabled = true;
            comboDifficulty.Location = new Point(145, 12);
            comboDifficulty.Name = "comboDifficulty";
            comboDifficulty.Size = new Size(120, 23);
            comboDifficulty.TabIndex = 3;
            comboDifficulty.SelectedIndexChanged += ComboDifficulty_SelectedIndexChanged;
            // 
            // menuStrip
            // 
            menuStrip.Location = new Point(0, 0);
            menuStrip.Name = "menuStrip";
            menuStrip.Size = new Size(800, 24);
            menuStrip.TabIndex = 4;
            menuStrip.Text = "menuStrip1";
            menuStrip.Click += MenuStrip_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(comboDifficulty);
            Controls.Add(lblTimer);
            Controls.Add(lblMines);
            Controls.Add(picField);
            Controls.Add(menuStrip);
            MainMenuStrip = menuStrip;
            MinimumSize = new Size(650, 430);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)picField).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox picField;
        private Label lblMines;
        private Label lblTimer;
        private ComboBox comboDifficulty;
        private MenuStrip menuStrip;
    }
}
