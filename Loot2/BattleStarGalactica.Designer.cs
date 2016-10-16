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
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.encTabCtrl = new System.Windows.Forms.TabControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.charBtnRight = new System.Windows.Forms.Button();
            this.charBtnLeft = new System.Windows.Forms.Button();
            this.encBtnRight = new System.Windows.Forms.Button();
            this.encBtnLeft = new System.Windows.Forms.Button();
            this.charTxt = new System.Windows.Forms.TextBox();
            this.encTxt = new System.Windows.Forms.TextBox();
            this.charSearch = new System.Windows.Forms.Button();
            this.encSearch = new System.Windows.Forms.Button();
            this.charTabCtrl.SuspendLayout();
            this.encTabCtrl.SuspendLayout();
            this.SuspendLayout();
            // 
            // charTabCtrl
            // 
            this.charTabCtrl.Controls.Add(this.tabPage1);
            this.charTabCtrl.Controls.Add(this.tabPage2);
            this.charTabCtrl.Location = new System.Drawing.Point(13, 13);
            this.charTabCtrl.Name = "charTabCtrl";
            this.charTabCtrl.SelectedIndex = 0;
            this.charTabCtrl.Size = new System.Drawing.Size(200, 344);
            this.charTabCtrl.TabIndex = 0;
            this.charTabCtrl.Click += new System.EventHandler(this.updateTextFields);
            // 
            // tabPage1
            // 
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(192, 318);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(192, 318);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // encTabCtrl
            // 
            this.encTabCtrl.Controls.Add(this.tabPage3);
            this.encTabCtrl.Controls.Add(this.tabPage4);
            this.encTabCtrl.Location = new System.Drawing.Point(528, 13);
            this.encTabCtrl.Name = "encTabCtrl";
            this.encTabCtrl.SelectedIndex = 0;
            this.encTabCtrl.Size = new System.Drawing.Size(200, 344);
            this.encTabCtrl.TabIndex = 1;
            this.encTabCtrl.Click += new System.EventHandler(this.updateTextFields);
            // 
            // tabPage3
            // 
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(192, 318);
            this.tabPage3.TabIndex = 0;
            this.tabPage3.Text = "tabPage3";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tabPage4
            // 
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(192, 318);
            this.tabPage4.TabIndex = 1;
            this.tabPage4.Text = "tabPage4";
            this.tabPage4.UseVisualStyleBackColor = true;
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
            // charTxt
            // 
            this.charTxt.Location = new System.Drawing.Point(250, 38);
            this.charTxt.Name = "charTxt";
            this.charTxt.Size = new System.Drawing.Size(100, 20);
            this.charTxt.TabIndex = 6;
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
            // BattleStarGalactica
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(995, 369);
            this.Controls.Add(this.encSearch);
            this.Controls.Add(this.charSearch);
            this.Controls.Add(this.encTxt);
            this.Controls.Add(this.charTxt);
            this.Controls.Add(this.encBtnLeft);
            this.Controls.Add(this.encBtnRight);
            this.Controls.Add(this.charBtnLeft);
            this.Controls.Add(this.charBtnRight);
            this.Controls.Add(this.encTabCtrl);
            this.Controls.Add(this.charTabCtrl);
            this.Name = "BattleStarGalactica";
            this.Text = "BattleStarGalactica";
            this.Load += new System.EventHandler(this.BattleStarGalactica_Load);
            this.charTabCtrl.ResumeLayout(false);
            this.encTabCtrl.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl charTabCtrl;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabControl encTabCtrl;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.Button charBtnRight;
        private System.Windows.Forms.Button charBtnLeft;
        private System.Windows.Forms.Button encBtnRight;
        private System.Windows.Forms.Button encBtnLeft;
        private System.Windows.Forms.TextBox charTxt;
        private System.Windows.Forms.TextBox encTxt;
        private System.Windows.Forms.Button charSearch;
        private System.Windows.Forms.Button encSearch;
    }
}