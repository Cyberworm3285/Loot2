namespace Loot2
{
    partial class BattleStarGalactica
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
            this.charTabCtrl = new System.Windows.Forms.TabControl();
            this.encTabCtrl = new System.Windows.Forms.TabControl();
            this.charBtnRight = new System.Windows.Forms.Button();
            this.charBtnLeft = new System.Windows.Forms.Button();
            this.encBtnRight = new System.Windows.Forms.Button();
            this.encBtnLeft = new System.Windows.Forms.Button();
            this.charSearchTxt = new System.Windows.Forms.TextBox();
            this.encTxt = new System.Windows.Forms.TextBox();
            this.charSearch = new System.Windows.Forms.Button();
            this.encSearch = new System.Windows.Forms.Button();
            this.enBtnSearch = new System.Windows.Forms.Button();
            this.enSearchTxt = new System.Windows.Forms.TextBox();
            this.enBtnLeft = new System.Windows.Forms.Button();
            this.enBtnRight = new System.Windows.Forms.Button();
            this.attackCharToNPC = new System.Windows.Forms.Button();
            this.logOutputTxtBx = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // charTabCtrl
            // 
            this.charTabCtrl.Location = new System.Drawing.Point(13, 13);
            this.charTabCtrl.Name = "charTabCtrl";
            this.charTabCtrl.SelectedIndex = 0;
            this.charTabCtrl.Size = new System.Drawing.Size(200, 344);
            this.charTabCtrl.TabIndex = 0;
            this.charTabCtrl.Click += new System.EventHandler(this.updateTextFields);
            // 
            // encTabCtrl
            // 
            this.encTabCtrl.Location = new System.Drawing.Point(528, 13);
            this.encTabCtrl.Name = "encTabCtrl";
            this.encTabCtrl.SelectedIndex = 0;
            this.encTabCtrl.Size = new System.Drawing.Size(200, 344);
            this.encTabCtrl.TabIndex = 1;
            this.encTabCtrl.TabIndexChanged += new System.EventHandler(this.encTabCtrl_TabIndexChanged);
            this.encTabCtrl.Click += new System.EventHandler(this.updateTextFields);
            // 
            // charBtnRight
            // 
            this.charBtnRight.Location = new System.Drawing.Point(356, 35);
            this.charBtnRight.Name = "charBtnRight";
            this.charBtnRight.Size = new System.Drawing.Size(25, 25);
            this.charBtnRight.TabIndex = 2;
            this.charBtnRight.Text = ">";
            this.charBtnRight.UseVisualStyleBackColor = true;
            this.charBtnRight.Click += new System.EventHandler(this.charBtnRight_Click);
            // 
            // charBtnLeft
            // 
            this.charBtnLeft.Location = new System.Drawing.Point(219, 35);
            this.charBtnLeft.Name = "charBtnLeft";
            this.charBtnLeft.Size = new System.Drawing.Size(25, 25);
            this.charBtnLeft.TabIndex = 3;
            this.charBtnLeft.Text = "<";
            this.charBtnLeft.UseVisualStyleBackColor = true;
            this.charBtnLeft.Click += new System.EventHandler(this.charBtnLeft_Click);
            // 
            // encBtnRight
            // 
            this.encBtnRight.Location = new System.Drawing.Point(867, 35);
            this.encBtnRight.Name = "encBtnRight";
            this.encBtnRight.Size = new System.Drawing.Size(25, 25);
            this.encBtnRight.TabIndex = 4;
            this.encBtnRight.Text = ">";
            this.encBtnRight.UseVisualStyleBackColor = true;
            this.encBtnRight.Click += new System.EventHandler(this.encBtnRight_Click);
            // 
            // encBtnLeft
            // 
            this.encBtnLeft.Location = new System.Drawing.Point(730, 35);
            this.encBtnLeft.Name = "encBtnLeft";
            this.encBtnLeft.Size = new System.Drawing.Size(25, 25);
            this.encBtnLeft.TabIndex = 5;
            this.encBtnLeft.Text = "<";
            this.encBtnLeft.UseVisualStyleBackColor = true;
            this.encBtnLeft.Click += new System.EventHandler(this.encBtnLeft_Click);
            // 
            // charSearchTxt
            // 
            this.charSearchTxt.Location = new System.Drawing.Point(250, 38);
            this.charSearchTxt.Name = "charSearchTxt";
            this.charSearchTxt.Size = new System.Drawing.Size(100, 20);
            this.charSearchTxt.TabIndex = 6;
            this.charSearchTxt.KeyDown += new System.Windows.Forms.KeyEventHandler(this.charTxt_KeyDown);
            // 
            // encTxt
            // 
            this.encTxt.Location = new System.Drawing.Point(761, 38);
            this.encTxt.Name = "encTxt";
            this.encTxt.Size = new System.Drawing.Size(100, 20);
            this.encTxt.TabIndex = 7;
            // 
            // charSearch
            // 
            this.charSearch.Location = new System.Drawing.Point(250, 64);
            this.charSearch.Name = "charSearch";
            this.charSearch.Size = new System.Drawing.Size(100, 23);
            this.charSearch.TabIndex = 8;
            this.charSearch.Text = "Search";
            this.charSearch.UseVisualStyleBackColor = true;
            this.charSearch.Click += new System.EventHandler(this.charSearch_Click);
            // 
            // encSearch
            // 
            this.encSearch.Location = new System.Drawing.Point(761, 64);
            this.encSearch.Name = "encSearch";
            this.encSearch.Size = new System.Drawing.Size(100, 23);
            this.encSearch.TabIndex = 9;
            this.encSearch.Text = "Search";
            this.encSearch.UseVisualStyleBackColor = true;
            this.encSearch.Click += new System.EventHandler(this.encSearch_Click);
            // 
            // enBtnSearch
            // 
            this.enBtnSearch.Location = new System.Drawing.Point(761, 119);
            this.enBtnSearch.Name = "enBtnSearch";
            this.enBtnSearch.Size = new System.Drawing.Size(100, 23);
            this.enBtnSearch.TabIndex = 13;
            this.enBtnSearch.Text = "Search";
            this.enBtnSearch.UseVisualStyleBackColor = true;
            this.enBtnSearch.Click += new System.EventHandler(this.enBtnSearch_Click);
            // 
            // enSearchTxt
            // 
            this.enSearchTxt.Location = new System.Drawing.Point(761, 93);
            this.enSearchTxt.Name = "enSearchTxt";
            this.enSearchTxt.Size = new System.Drawing.Size(100, 20);
            this.enSearchTxt.TabIndex = 12;
            // 
            // enBtnLeft
            // 
            this.enBtnLeft.Location = new System.Drawing.Point(730, 90);
            this.enBtnLeft.Name = "enBtnLeft";
            this.enBtnLeft.Size = new System.Drawing.Size(25, 25);
            this.enBtnLeft.TabIndex = 11;
            this.enBtnLeft.Text = "<";
            this.enBtnLeft.UseVisualStyleBackColor = true;
            this.enBtnLeft.Click += new System.EventHandler(this.enBtnLeft_Click);
            // 
            // enBtnRight
            // 
            this.enBtnRight.Location = new System.Drawing.Point(867, 90);
            this.enBtnRight.Name = "enBtnRight";
            this.enBtnRight.Size = new System.Drawing.Size(25, 25);
            this.enBtnRight.TabIndex = 10;
            this.enBtnRight.Text = ">";
            this.enBtnRight.UseVisualStyleBackColor = true;
            this.enBtnRight.Click += new System.EventHandler(this.enBtnRight_Click);
            // 
            // attackCharToNPC
            // 
            this.attackCharToNPC.Location = new System.Drawing.Point(404, 35);
            this.attackCharToNPC.Name = "attackCharToNPC";
            this.attackCharToNPC.Size = new System.Drawing.Size(75, 23);
            this.attackCharToNPC.TabIndex = 14;
            this.attackCharToNPC.Text = "Attack ->";
            this.attackCharToNPC.UseVisualStyleBackColor = true;
            this.attackCharToNPC.Click += new System.EventHandler(this.attackCharToNPC_Click);
            // 
            // logOutputTxtBx
            // 
            this.logOutputTxtBx.FormattingEnabled = true;
            this.logOutputTxtBx.Location = new System.Drawing.Point(730, 165);
            this.logOutputTxtBx.Name = "logOutputTxtBx";
            this.logOutputTxtBx.Size = new System.Drawing.Size(162, 95);
            this.logOutputTxtBx.TabIndex = 15;
            // 
            // BattleStarGalactica
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(995, 369);
            this.Controls.Add(this.logOutputTxtBx);
            this.Controls.Add(this.attackCharToNPC);
            this.Controls.Add(this.enBtnSearch);
            this.Controls.Add(this.enSearchTxt);
            this.Controls.Add(this.enBtnLeft);
            this.Controls.Add(this.enBtnRight);
            this.Controls.Add(this.encSearch);
            this.Controls.Add(this.charSearch);
            this.Controls.Add(this.encTxt);
            this.Controls.Add(this.charSearchTxt);
            this.Controls.Add(this.encBtnLeft);
            this.Controls.Add(this.encBtnRight);
            this.Controls.Add(this.charBtnLeft);
            this.Controls.Add(this.charBtnRight);
            this.Controls.Add(this.encTabCtrl);
            this.Controls.Add(this.charTabCtrl);
            this.Name = "BattleStarGalactica";
            this.Text = "BattleStarGalactica";
            this.Load += new System.EventHandler(this.BattleStarGalactica_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl charTabCtrl;
        private System.Windows.Forms.TabControl encTabCtrl;
        private System.Windows.Forms.Button charBtnRight;
        private System.Windows.Forms.Button charBtnLeft;
        private System.Windows.Forms.Button encBtnRight;
        private System.Windows.Forms.Button encBtnLeft;
        private System.Windows.Forms.TextBox charSearchTxt;
        private System.Windows.Forms.TextBox encTxt;
        private System.Windows.Forms.Button charSearch;
        private System.Windows.Forms.Button encSearch;
        private System.Windows.Forms.Button enBtnSearch;
        private System.Windows.Forms.TextBox enSearchTxt;
        private System.Windows.Forms.Button enBtnLeft;
        private System.Windows.Forms.Button enBtnRight;
        private System.Windows.Forms.Button attackCharToNPC;
        private System.Windows.Forms.ListBox logOutputTxtBx;
    }
}