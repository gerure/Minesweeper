
namespace Minesweeper
{
    partial class Form1
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnSmiley = new System.Windows.Forms.Button();
            this.txtAnzahl = new System.Windows.Forms.TextBox();
            this.lblTimer = new System.Windows.Forms.Label();
            this.lblHighscoreTitle = new System.Windows.Forms.Label();
            this.lblHighscoreScore = new System.Windows.Forms.Label();
            this.lblAnzahlmarkiert = new System.Windows.Forms.Label();
            this.lblMarkiert = new System.Windows.Forms.Label();
            this.grpInfo = new System.Windows.Forms.GroupBox();
            this.grpInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSmiley
            // 
            this.btnSmiley.Location = new System.Drawing.Point(13, 19);
            this.btnSmiley.Name = "btnSmiley";
            this.btnSmiley.Size = new System.Drawing.Size(135, 135);
            this.btnSmiley.TabIndex = 0;
            this.btnSmiley.UseVisualStyleBackColor = true;
            this.btnSmiley.Click += new System.EventHandler(this.btnSmiley_Click);
            // 
            // txtAnzahl
            // 
            this.txtAnzahl.Font = new System.Drawing.Font("Microsoft Sans Serif", 35F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAnzahl.Location = new System.Drawing.Point(15, 160);
            this.txtAnzahl.Name = "txtAnzahl";
            this.txtAnzahl.Size = new System.Drawing.Size(135, 60);
            this.txtAnzahl.TabIndex = 1;
            this.txtAnzahl.Text = "15";
            this.txtAnzahl.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtAnzahl.TextChanged += new System.EventHandler(this.txtAnzahl_TextChanged);
            // 
            // lblTimer
            // 
            this.lblTimer.AutoSize = true;
            this.lblTimer.Font = new System.Drawing.Font("Microsoft Sans Serif", 35F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTimer.Location = new System.Drawing.Point(1026, 1060);
            this.lblTimer.Name = "lblTimer";
            this.lblTimer.Size = new System.Drawing.Size(101, 54);
            this.lblTimer.TabIndex = 3;
            this.lblTimer.Text = "000";
            // 
            // lblHighscoreTitle
            // 
            this.lblHighscoreTitle.AutoSize = true;
            this.lblHighscoreTitle.Cursor = System.Windows.Forms.Cursors.Default;
            this.lblHighscoreTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 22F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.lblHighscoreTitle.Location = new System.Drawing.Point(10, 237);
            this.lblHighscoreTitle.Name = "lblHighscoreTitle";
            this.lblHighscoreTitle.Size = new System.Drawing.Size(110, 26);
            this.lblHighscoreTitle.TabIndex = 4;
            this.lblHighscoreTitle.Text = "Highscore";
            this.lblHighscoreTitle.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblHighscoreScore
            // 
            this.lblHighscoreScore.AutoSize = true;
            this.lblHighscoreScore.Font = new System.Drawing.Font("Microsoft Sans Serif", 22F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHighscoreScore.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblHighscoreScore.Location = new System.Drawing.Point(9, 267);
            this.lblHighscoreScore.Name = "lblHighscoreScore";
            this.lblHighscoreScore.Size = new System.Drawing.Size(32, 36);
            this.lblHighscoreScore.TabIndex = 5;
            this.lblHighscoreScore.Text = "0";
            this.lblHighscoreScore.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblHighscoreScore.Click += new System.EventHandler(this.lblHighscoreScore_Click);
            // 
            // lblAnzahlmarkiert
            // 
            this.lblAnzahlmarkiert.AutoSize = true;
            this.lblAnzahlmarkiert.Font = new System.Drawing.Font("Microsoft Sans Serif", 22F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAnzahlmarkiert.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblAnzahlmarkiert.Location = new System.Drawing.Point(9, 336);
            this.lblAnzahlmarkiert.Name = "lblAnzahlmarkiert";
            this.lblAnzahlmarkiert.Size = new System.Drawing.Size(32, 36);
            this.lblAnzahlmarkiert.TabIndex = 6;
            this.lblAnzahlmarkiert.Text = "0";
            this.lblAnzahlmarkiert.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblMarkiert
            // 
            this.lblMarkiert.AutoSize = true;
            this.lblMarkiert.Cursor = System.Windows.Forms.Cursors.Default;
            this.lblMarkiert.Font = new System.Drawing.Font("Microsoft Sans Serif", 22F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.lblMarkiert.Location = new System.Drawing.Point(10, 307);
            this.lblMarkiert.Name = "lblMarkiert";
            this.lblMarkiert.Size = new System.Drawing.Size(90, 26);
            this.lblMarkiert.TabIndex = 7;
            this.lblMarkiert.Text = "Markiert";
            this.lblMarkiert.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // grpInfo
            // 
            this.grpInfo.Controls.Add(this.btnSmiley);
            this.grpInfo.Controls.Add(this.lblMarkiert);
            this.grpInfo.Controls.Add(this.txtAnzahl);
            this.grpInfo.Controls.Add(this.lblAnzahlmarkiert);
            this.grpInfo.Controls.Add(this.lblHighscoreTitle);
            this.grpInfo.Controls.Add(this.lblHighscoreScore);
            this.grpInfo.Location = new System.Drawing.Point(1025, 33);
            this.grpInfo.Name = "grpInfo";
            this.grpInfo.Size = new System.Drawing.Size(173, 389);
            this.grpInfo.TabIndex = 8;
            this.grpInfo.TabStop = false;
            this.grpInfo.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1342, 1145);
            this.Controls.Add(this.grpInfo);
            this.Controls.Add(this.lblTimer);
            this.Name = "Form1";
            this.Text = "Minesweeper";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.grpInfo.ResumeLayout(false);
            this.grpInfo.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSmiley;
        private System.Windows.Forms.TextBox txtAnzahl;
        private System.Windows.Forms.Label lblTimer;
        private System.Windows.Forms.Label lblHighscoreTitle;
        private System.Windows.Forms.Label lblHighscoreScore;
        private System.Windows.Forms.Label lblAnzahlmarkiert;
        private System.Windows.Forms.Label lblMarkiert;
        private System.Windows.Forms.GroupBox grpInfo;
    }
}

