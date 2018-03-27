namespace WindowsFormsApp1
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
            this.label1 = new System.Windows.Forms.Label();
            this.browseButton = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.browseText = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.bfsButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dfsButton = new System.Windows.Forms.Button();
            this.nextButton = new System.Windows.Forms.Button();
            this.GraphPictureBox = new System.Windows.Forms.PictureBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GraphPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.label1.Font = new System.Drawing.Font("Bookman Old Style", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(123, 33);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(259, 39);
            this.label1.TabIndex = 0;
            this.label1.Text = "Course Planner";
            // 
            // browseButton
            // 
            this.browseButton.BackColor = System.Drawing.Color.White;
            this.browseButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.browseButton.Location = new System.Drawing.Point(370, 85);
            this.browseButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.browseButton.Name = "browseButton";
            this.browseButton.Size = new System.Drawing.Size(106, 34);
            this.browseButton.TabIndex = 1;
            this.browseButton.Text = "Browse";
            this.browseButton.UseVisualStyleBackColor = false;
            this.browseButton.Click += new System.EventHandler(this.browseButton_Click);
            // 
            // textBox1
            // 
            this.textBox1.AcceptsReturn = true;
            this.textBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(18, 121);
            this.textBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(457, 309);
            this.textBox1.TabIndex = 2;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // browseText
            // 
            this.browseText.Location = new System.Drawing.Point(18, 85);
            this.browseText.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.browseText.Name = "browseText";
            this.browseText.Size = new System.Drawing.Size(344, 26);
            this.browseText.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(152, 25);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(150, 27);
            this.label2.TabIndex = 4;
            this.label2.Text = "Create Plan";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // bfsButton
            // 
            this.bfsButton.Location = new System.Drawing.Point(94, 66);
            this.bfsButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.bfsButton.Name = "bfsButton";
            this.bfsButton.Size = new System.Drawing.Size(112, 35);
            this.bfsButton.TabIndex = 5;
            this.bfsButton.Text = "BFS";
            this.bfsButton.UseVisualStyleBackColor = true;
            this.bfsButton.Click += new System.EventHandler(this.bfsButton_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dfsButton);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.bfsButton);
            this.groupBox1.Location = new System.Drawing.Point(22, 442);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1.Size = new System.Drawing.Size(454, 123);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            // 
            // dfsButton
            // 
            this.dfsButton.Location = new System.Drawing.Point(248, 66);
            this.dfsButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dfsButton.Name = "dfsButton";
            this.dfsButton.Size = new System.Drawing.Size(112, 35);
            this.dfsButton.TabIndex = 6;
            this.dfsButton.Text = "DFS";
            this.dfsButton.UseVisualStyleBackColor = true;
            this.dfsButton.Click += new System.EventHandler(this.dfsButton_Click);
            // 
            // nextButton
            // 
            this.nextButton.Location = new System.Drawing.Point(795, 511);
            this.nextButton.Name = "nextButton";
            this.nextButton.Size = new System.Drawing.Size(90, 34);
            this.nextButton.TabIndex = 7;
            this.nextButton.Text = "Next";
            this.nextButton.UseVisualStyleBackColor = true;
            this.nextButton.Click += new System.EventHandler(this.nextButton_Click);
            // 
            // GraphPictureBox
            // 
            this.GraphPictureBox.Location = new System.Drawing.Point(544, 43);
            this.GraphPictureBox.Name = "GraphPictureBox";
            this.GraphPictureBox.Size = new System.Drawing.Size(565, 441);
            this.GraphPictureBox.TabIndex = 9;
            this.GraphPictureBox.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(1146, 583);
            this.Controls.Add(this.GraphPictureBox);
            this.Controls.Add(this.nextButton);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.browseText);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.browseButton);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Course Planner";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GraphPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button browseButton;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox browseText;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button bfsButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button dfsButton;
        private System.Windows.Forms.Button nextButton;
        private System.Windows.Forms.PictureBox GraphPictureBox;
    }
}

