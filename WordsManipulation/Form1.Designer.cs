namespace WordsManipulation
{
    partial class WordsThread
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
            this.UI_Label = new System.Windows.Forms.Label();
            this.UI_BTN_Palindromes = new System.Windows.Forms.Button();
            this.UI_BTN_ASCII = new System.Windows.Forms.Button();
            this.UI_BTN_Substring = new System.Windows.Forms.Button();
            this.UI_TB_Substring = new System.Windows.Forms.TextBox();
            this.UI_TB_Output = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // UI_Label
            // 
            this.UI_Label.AutoSize = true;
            this.UI_Label.Location = new System.Drawing.Point(13, 13);
            this.UI_Label.Name = "UI_Label";
            this.UI_Label.Size = new System.Drawing.Size(86, 13);
            this.UI_Label.TabIndex = 0;
            this.UI_Label.Text = "Loaded 0 words!";
            // 
            // UI_BTN_Palindromes
            // 
            this.UI_BTN_Palindromes.Location = new System.Drawing.Point(16, 39);
            this.UI_BTN_Palindromes.Name = "UI_BTN_Palindromes";
            this.UI_BTN_Palindromes.Size = new System.Drawing.Size(72, 23);
            this.UI_BTN_Palindromes.TabIndex = 1;
            this.UI_BTN_Palindromes.Text = "Palindromes";
            this.UI_BTN_Palindromes.UseVisualStyleBackColor = true;
            this.UI_BTN_Palindromes.Click += new System.EventHandler(this.UI_BTN_Palindromes_Click);
            // 
            // UI_BTN_ASCII
            // 
            this.UI_BTN_ASCII.Location = new System.Drawing.Point(94, 39);
            this.UI_BTN_ASCII.Name = "UI_BTN_ASCII";
            this.UI_BTN_ASCII.Size = new System.Drawing.Size(74, 23);
            this.UI_BTN_ASCII.TabIndex = 2;
            this.UI_BTN_ASCII.Text = "ASCII Same";
            this.UI_BTN_ASCII.UseVisualStyleBackColor = true;
            this.UI_BTN_ASCII.Click += new System.EventHandler(this.UI_BTN_ASCII_Click);
            // 
            // UI_BTN_Substring
            // 
            this.UI_BTN_Substring.Location = new System.Drawing.Point(174, 39);
            this.UI_BTN_Substring.Name = "UI_BTN_Substring";
            this.UI_BTN_Substring.Size = new System.Drawing.Size(75, 23);
            this.UI_BTN_Substring.TabIndex = 3;
            this.UI_BTN_Substring.Text = "Substrings";
            this.UI_BTN_Substring.UseVisualStyleBackColor = true;
            this.UI_BTN_Substring.Click += new System.EventHandler(this.UI_BTN_Substring_Click);
            // 
            // UI_TB_Substring
            // 
            this.UI_TB_Substring.Location = new System.Drawing.Point(256, 41);
            this.UI_TB_Substring.Name = "UI_TB_Substring";
            this.UI_TB_Substring.Size = new System.Drawing.Size(157, 20);
            this.UI_TB_Substring.TabIndex = 4;
            // 
            // UI_TB_Output
            // 
            this.UI_TB_Output.Location = new System.Drawing.Point(16, 69);
            this.UI_TB_Output.Name = "UI_TB_Output";
            this.UI_TB_Output.ReadOnly = true;
            this.UI_TB_Output.Size = new System.Drawing.Size(397, 20);
            this.UI_TB_Output.TabIndex = 5;
            // 
            // WordsThread
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(432, 98);
            this.Controls.Add(this.UI_TB_Output);
            this.Controls.Add(this.UI_TB_Substring);
            this.Controls.Add(this.UI_BTN_Substring);
            this.Controls.Add(this.UI_BTN_ASCII);
            this.Controls.Add(this.UI_BTN_Palindromes);
            this.Controls.Add(this.UI_Label);
            this.Name = "WordsThread";
            this.Text = "Words Threads!";
            this.Load += new System.EventHandler(this.WordsThread_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label UI_Label;
        private System.Windows.Forms.Button UI_BTN_Palindromes;
        private System.Windows.Forms.Button UI_BTN_ASCII;
        private System.Windows.Forms.Button UI_BTN_Substring;
        private System.Windows.Forms.TextBox UI_TB_Substring;
        private System.Windows.Forms.TextBox UI_TB_Output;
    }
}

