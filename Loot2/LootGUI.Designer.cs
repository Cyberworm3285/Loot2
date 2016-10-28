namespace Loot2
{
    partial class LootGUI
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
            this.saveBtn = new System.Windows.Forms.Button();
            this.itemLBx = new System.Windows.Forms.ListBox();
            this.untenTBx = new System.Windows.Forms.TextBox();
            this.obenTBx = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lootBtn = new System.Windows.Forms.Button();
            this.loadBtn = new System.Windows.Forms.Button();
            this.logLBx = new System.Windows.Forms.ListBox();
            this.traverseBtn = new System.Windows.Forms.Button();
            this.NFilterTBx = new System.Windows.Forms.TextBox();
            this.blackWhiteTBx = new System.Windows.Forms.TextBox();
            this.blackWhiteListBtn = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.clearBtn = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.xmlJsonBtn = new System.Windows.Forms.Button();
            this.aktDataTypeLbl = new System.Windows.Forms.Label();
            this.dummyCreatorBtn = new System.Windows.Forms.Button();
            this.lootModeBtn = new System.Windows.Forms.Button();
            this.smoothProbBtn = new System.Windows.Forms.Button();
            this.smoothTxt = new System.Windows.Forms.TextBox();
            this.smoothLbl = new System.Windows.Forms.Label();
            this.AlgCheckBtn = new System.Windows.Forms.Button();
            this.rarOutput = new System.Windows.Forms.Button();
            this.battleSysBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // saveBtn
            // 
            this.saveBtn.Location = new System.Drawing.Point(95, 41);
            this.saveBtn.Name = "saveBtn";
            this.saveBtn.Size = new System.Drawing.Size(75, 23);
            this.saveBtn.TabIndex = 7;
            this.saveBtn.Text = "Save";
            this.saveBtn.UseVisualStyleBackColor = true;
            this.saveBtn.Click += new System.EventHandler(this.dummyCreatorBtnr_Click);
            // 
            // itemLBx
            // 
            this.itemLBx.FormattingEnabled = true;
            this.itemLBx.Location = new System.Drawing.Point(12, 99);
            this.itemLBx.Name = "itemLBx";
            this.itemLBx.Size = new System.Drawing.Size(425, 316);
            this.itemLBx.TabIndex = 13;
            // 
            // untenTBx
            // 
            this.untenTBx.Location = new System.Drawing.Point(319, 13);
            this.untenTBx.Name = "untenTBx";
            this.untenTBx.Size = new System.Drawing.Size(118, 20);
            this.untenTBx.TabIndex = 0;
            this.untenTBx.Text = "0";
            this.untenTBx.KeyDown += new System.Windows.Forms.KeyEventHandler(this.untenTBx_KeyDown);
            // 
            // obenTBx
            // 
            this.obenTBx.Location = new System.Drawing.Point(535, 13);
            this.obenTBx.Name = "obenTBx";
            this.obenTBx.Size = new System.Drawing.Size(118, 20);
            this.obenTBx.TabIndex = 2;
            this.obenTBx.Text = "1000";
            this.obenTBx.KeyDown += new System.Windows.Forms.KeyEventHandler(this.obenTBx_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(279, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 16;
            this.label1.Text = "unten";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(486, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 13);
            this.label2.TabIndex = 19;
            this.label2.Text = "oben";
            // 
            // lootBtn
            // 
            this.lootBtn.Location = new System.Drawing.Point(12, 13);
            this.lootBtn.Name = "lootBtn";
            this.lootBtn.Size = new System.Drawing.Size(75, 23);
            this.lootBtn.TabIndex = 4;
            this.lootBtn.Text = "Loot";
            this.lootBtn.UseVisualStyleBackColor = true;
            this.lootBtn.Click += new System.EventHandler(this.lootBtn_Click);
            // 
            // loadBtn
            // 
            this.loadBtn.Location = new System.Drawing.Point(95, 13);
            this.loadBtn.Name = "loadBtn";
            this.loadBtn.Size = new System.Drawing.Size(75, 23);
            this.loadBtn.TabIndex = 6;
            this.loadBtn.Text = "load";
            this.loadBtn.UseVisualStyleBackColor = true;
            this.loadBtn.Click += new System.EventHandler(this.loadBtn_Click);
            // 
            // logLBx
            // 
            this.logLBx.FormattingEnabled = true;
            this.logLBx.Location = new System.Drawing.Point(443, 99);
            this.logLBx.Name = "logLBx";
            this.logLBx.Size = new System.Drawing.Size(210, 316);
            this.logLBx.TabIndex = 14;
            // 
            // traverseBtn
            // 
            this.traverseBtn.Location = new System.Drawing.Point(176, 13);
            this.traverseBtn.Name = "traverseBtn";
            this.traverseBtn.Size = new System.Drawing.Size(75, 23);
            this.traverseBtn.TabIndex = 8;
            this.traverseBtn.Text = "Traverse";
            this.traverseBtn.UseVisualStyleBackColor = true;
            this.traverseBtn.Click += new System.EventHandler(this.traverseBtn_Click);
            // 
            // NFilterTBx
            // 
            this.NFilterTBx.Location = new System.Drawing.Point(319, 44);
            this.NFilterTBx.Name = "NFilterTBx";
            this.NFilterTBx.Size = new System.Drawing.Size(118, 20);
            this.NFilterTBx.TabIndex = 1;
            // 
            // blackWhiteTBx
            // 
            this.blackWhiteTBx.Location = new System.Drawing.Point(535, 44);
            this.blackWhiteTBx.Name = "blackWhiteTBx";
            this.blackWhiteTBx.Size = new System.Drawing.Size(118, 20);
            this.blackWhiteTBx.TabIndex = 3;
            // 
            // blackWhiteListBtn
            // 
            this.blackWhiteListBtn.Location = new System.Drawing.Point(454, 42);
            this.blackWhiteListBtn.Name = "blackWhiteListBtn";
            this.blackWhiteListBtn.Size = new System.Drawing.Size(75, 23);
            this.blackWhiteListBtn.TabIndex = 12;
            this.blackWhiteListBtn.Text = "Blacklist";
            this.blackWhiteListBtn.UseVisualStyleBackColor = true;
            this.blackWhiteListBtn.Click += new System.EventHandler(this.blackWhiteListBtn_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(253, 49);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 13);
            this.label3.TabIndex = 17;
            this.label3.Text = "Namenfilter";
            // 
            // clearBtn
            // 
            this.clearBtn.Location = new System.Drawing.Point(176, 42);
            this.clearBtn.Name = "clearBtn";
            this.clearBtn.Size = new System.Drawing.Size(75, 23);
            this.clearBtn.TabIndex = 9;
            this.clearBtn.Text = "Clear";
            this.clearBtn.UseVisualStyleBackColor = true;
            this.clearBtn.Click += new System.EventHandler(this.clearBtn_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(271, 75);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(42, 13);
            this.label4.TabIndex = 18;
            this.label4.Text = "Modus:";
            // 
            // xmlJsonBtn
            // 
            this.xmlJsonBtn.Location = new System.Drawing.Point(319, 70);
            this.xmlJsonBtn.Name = "xmlJsonBtn";
            this.xmlJsonBtn.Size = new System.Drawing.Size(75, 23);
            this.xmlJsonBtn.TabIndex = 11;
            this.xmlJsonBtn.Text = "-";
            this.xmlJsonBtn.UseVisualStyleBackColor = true;
            this.xmlJsonBtn.Click += new System.EventHandler(this.xmlJsonBtn_Click);
            // 
            // aktDataTypeLbl
            // 
            this.aktDataTypeLbl.AutoSize = true;
            this.aktDataTypeLbl.Location = new System.Drawing.Point(419, 75);
            this.aktDataTypeLbl.Name = "aktDataTypeLbl";
            this.aktDataTypeLbl.Size = new System.Drawing.Size(0, 13);
            this.aktDataTypeLbl.TabIndex = 20;
            // 
            // dummyCreatorBtn
            // 
            this.dummyCreatorBtn.Location = new System.Drawing.Point(12, 41);
            this.dummyCreatorBtn.Name = "dummyCreatorBtn";
            this.dummyCreatorBtn.Size = new System.Drawing.Size(75, 23);
            this.dummyCreatorBtn.TabIndex = 5;
            this.dummyCreatorBtn.Text = "Dummy";
            this.dummyCreatorBtn.UseVisualStyleBackColor = true;
            this.dummyCreatorBtn.Click += new System.EventHandler(this.dummyCreatorBtn_Click);
            // 
            // lootModeBtn
            // 
            this.lootModeBtn.Location = new System.Drawing.Point(12, 70);
            this.lootModeBtn.Name = "lootModeBtn";
            this.lootModeBtn.Size = new System.Drawing.Size(239, 23);
            this.lootModeBtn.TabIndex = 10;
            this.lootModeBtn.Text = "LootModus:";
            this.lootModeBtn.UseVisualStyleBackColor = true;
            this.lootModeBtn.Click += new System.EventHandler(this.lootModeBtn_Click);
            // 
            // smoothProbBtn
            // 
            this.smoothProbBtn.Location = new System.Drawing.Point(659, 13);
            this.smoothProbBtn.Name = "smoothProbBtn";
            this.smoothProbBtn.Size = new System.Drawing.Size(114, 23);
            this.smoothProbBtn.TabIndex = 21;
            this.smoothProbBtn.Text = "Smooth Probs";
            this.smoothProbBtn.UseVisualStyleBackColor = true;
            this.smoothProbBtn.Click += new System.EventHandler(this.smoothProbBtn_Click);
            // 
            // smoothTxt
            // 
            this.smoothTxt.Location = new System.Drawing.Point(739, 44);
            this.smoothTxt.Name = "smoothTxt";
            this.smoothTxt.Size = new System.Drawing.Size(32, 20);
            this.smoothTxt.TabIndex = 22;
            // 
            // smoothLbl
            // 
            this.smoothLbl.AutoSize = true;
            this.smoothLbl.Location = new System.Drawing.Point(673, 47);
            this.smoothLbl.Name = "smoothLbl";
            this.smoothLbl.Size = new System.Drawing.Size(60, 13);
            this.smoothLbl.TabIndex = 23;
            this.smoothLbl.Text = "Iterationen:";
            // 
            // AlgCheckBtn
            // 
            this.AlgCheckBtn.Location = new System.Drawing.Point(659, 70);
            this.AlgCheckBtn.Name = "AlgCheckBtn";
            this.AlgCheckBtn.Size = new System.Drawing.Size(114, 23);
            this.AlgCheckBtn.TabIndex = 24;
            this.AlgCheckBtn.Text = "Check Algs";
            this.AlgCheckBtn.UseVisualStyleBackColor = true;
            this.AlgCheckBtn.Click += new System.EventHandler(this.AlgCheckBtn_Click);
            // 
            // rarOutput
            // 
            this.rarOutput.Location = new System.Drawing.Point(660, 100);
            this.rarOutput.Name = "rarOutput";
            this.rarOutput.Size = new System.Drawing.Size(113, 49);
            this.rarOutput.TabIndex = 25;
            this.rarOutput.Text = "-";
            this.rarOutput.UseVisualStyleBackColor = true;
            // 
            // battleSysBtn
            // 
            this.battleSysBtn.Location = new System.Drawing.Point(659, 155);
            this.battleSysBtn.Name = "battleSysBtn";
            this.battleSysBtn.Size = new System.Drawing.Size(114, 23);
            this.battleSysBtn.TabIndex = 26;
            this.battleSysBtn.Text = "New BattleSys";
            this.battleSysBtn.UseVisualStyleBackColor = true;
            this.battleSysBtn.Click += new System.EventHandler(this.battleSysBtn_Click);
            // 
            // LootGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(780, 431);
            this.Controls.Add(this.battleSysBtn);
            this.Controls.Add(this.rarOutput);
            this.Controls.Add(this.AlgCheckBtn);
            this.Controls.Add(this.smoothLbl);
            this.Controls.Add(this.smoothTxt);
            this.Controls.Add(this.smoothProbBtn);
            this.Controls.Add(this.lootModeBtn);
            this.Controls.Add(this.dummyCreatorBtn);
            this.Controls.Add(this.aktDataTypeLbl);
            this.Controls.Add(this.xmlJsonBtn);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.clearBtn);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.blackWhiteListBtn);
            this.Controls.Add(this.blackWhiteTBx);
            this.Controls.Add(this.NFilterTBx);
            this.Controls.Add(this.traverseBtn);
            this.Controls.Add(this.logLBx);
            this.Controls.Add(this.loadBtn);
            this.Controls.Add(this.lootBtn);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.obenTBx);
            this.Controls.Add(this.untenTBx);
            this.Controls.Add(this.itemLBx);
            this.Controls.Add(this.saveBtn);
            this.Name = "LootGUI";
            this.Text = "xxXLootLordXxx";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button saveBtn;
        private System.Windows.Forms.ListBox itemLBx;
        private System.Windows.Forms.TextBox untenTBx;
        private System.Windows.Forms.TextBox obenTBx;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button lootBtn;
        private System.Windows.Forms.Button loadBtn;
        private System.Windows.Forms.ListBox logLBx;
        private System.Windows.Forms.Button traverseBtn;
        private System.Windows.Forms.TextBox NFilterTBx;
        private System.Windows.Forms.TextBox blackWhiteTBx;
        private System.Windows.Forms.Button blackWhiteListBtn;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button clearBtn;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button xmlJsonBtn;
        private System.Windows.Forms.Label aktDataTypeLbl;
        private System.Windows.Forms.Button dummyCreatorBtn;
        private System.Windows.Forms.Button lootModeBtn;
        private System.Windows.Forms.Button smoothProbBtn;
        private System.Windows.Forms.TextBox smoothTxt;
        private System.Windows.Forms.Label smoothLbl;
        private System.Windows.Forms.Button AlgCheckBtn;
        private System.Windows.Forms.Button rarOutput;
        private System.Windows.Forms.Button battleSysBtn;
    }
}

