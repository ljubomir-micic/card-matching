namespace lab03
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.lab03ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.konfiguracijaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.igraToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.predajaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sacuvajToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ucitajToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.restartToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lab03ToolStripMenuItem,
            this.igraToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // lab03ToolStripMenuItem
            // 
            this.lab03ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem,
            this.konfiguracijaToolStripMenuItem});
            this.lab03ToolStripMenuItem.Name = "lab03ToolStripMenuItem";
            this.lab03ToolStripMenuItem.Size = new System.Drawing.Size(80, 20);
            this.lab03ToolStripMenuItem.Text = "menuStrip1";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.aboutToolStripMenuItem.Text = "About";
            // 
            // konfiguracijaToolStripMenuItem
            // 
            this.konfiguracijaToolStripMenuItem.Name = "konfiguracijaToolStripMenuItem";
            this.konfiguracijaToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.konfiguracijaToolStripMenuItem.Text = "Konfiguracija";
            // 
            // igraToolStripMenuItem
            // 
            this.igraToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.predajaToolStripMenuItem,
            this.sacuvajToolStripMenuItem,
            this.ucitajToolStripMenuItem,
            this.restartToolStripMenuItem});
            this.igraToolStripMenuItem.Name = "igraToolStripMenuItem";
            this.igraToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.igraToolStripMenuItem.Text = "Igra";
            // 
            // predajaToolStripMenuItem
            // 
            this.predajaToolStripMenuItem.BackColor = System.Drawing.Color.IndianRed;
            this.predajaToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.predajaToolStripMenuItem.Name = "predajaToolStripMenuItem";
            this.predajaToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.predajaToolStripMenuItem.Text = "Predaja";
            this.predajaToolStripMenuItem.Click += new System.EventHandler(this.predajaToolStripMenuItem_Click);
            // 
            // sacuvajToolStripMenuItem
            // 
            this.sacuvajToolStripMenuItem.Name = "sacuvajToolStripMenuItem";
            this.sacuvajToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.sacuvajToolStripMenuItem.Text = "Sacuvaj";
            this.sacuvajToolStripMenuItem.Click += new System.EventHandler(this.sacuvajToolStripMenuItem_Click);
            // 
            // ucitajToolStripMenuItem
            // 
            this.ucitajToolStripMenuItem.Name = "ucitajToolStripMenuItem";
            this.ucitajToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.ucitajToolStripMenuItem.Text = "Ucitaj";
            this.ucitajToolStripMenuItem.Click += new System.EventHandler(this.ucitajToolStripMenuItem_Click);
            // 
            // restartToolStripMenuItem
            // 
            this.restartToolStripMenuItem.BackColor = System.Drawing.Color.IndianRed;
            this.restartToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.restartToolStripMenuItem.Name = "restartToolStripMenuItem";
            this.restartToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.restartToolStripMenuItem.Text = "Restart";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Text = "Form1";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem lab03ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem konfiguracijaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem restartToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem predajaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem igraToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ucitajToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sacuvajToolStripMenuItem;
    }
}

