namespace Tetris
{
    partial class Window
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
            this.pGameBoard = new System.Windows.Forms.PictureBox();
            this.lMainTitle = new System.Windows.Forms.Label();
            this.lGameOver = new System.Windows.Forms.Label();
            this.bPlayButton = new System.Windows.Forms.Button();
            this.bEndButton = new System.Windows.Forms.Button();
            this.bLoadBlocks = new System.Windows.Forms.Button();
            this.tbLoadingBlocksTextBox = new System.Windows.Forms.TextBox();
            this.lPoints = new System.Windows.Forms.Label();
            this.lLoadingBlocksLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pGameBoard)).BeginInit();
            this.SuspendLayout();
            // 
            // pGameBoard
            // 
            this.pGameBoard.Location = new System.Drawing.Point(292, 151);
            this.pGameBoard.Name = "pGameBoard";
            this.pGameBoard.Size = new System.Drawing.Size(125, 18);
            this.pGameBoard.TabIndex = 0;
            this.pGameBoard.TabStop = false;
            // 
            // lMainTitle
            // 
            this.lMainTitle.AutoSize = true;
            this.lMainTitle.Location = new System.Drawing.Point(335, 33);
            this.lMainTitle.Name = "lMainTitle";
            this.lMainTitle.Size = new System.Drawing.Size(44, 20);
            this.lMainTitle.TabIndex = 1;
            this.lMainTitle.Text = "Tetris";
            // 
            // lGameOver
            // 
            this.lGameOver.AutoSize = true;
            this.lGameOver.Location = new System.Drawing.Point(590, 198);
            this.lGameOver.Name = "lGameOver";
            this.lGameOver.Size = new System.Drawing.Size(83, 20);
            this.lGameOver.TabIndex = 2;
            this.lGameOver.Text = "Game Over";
            // 
            // bPlayButton
            // 
            this.bPlayButton.Location = new System.Drawing.Point(48, 189);
            this.bPlayButton.Name = "bPlayButton";
            this.bPlayButton.Size = new System.Drawing.Size(94, 29);
            this.bPlayButton.TabIndex = 3;
            this.bPlayButton.Text = "Play";
            this.bPlayButton.UseVisualStyleBackColor = true;
            // 
            // bEndButton
            // 
            this.bEndButton.Location = new System.Drawing.Point(602, 302);
            this.bEndButton.Name = "bEndButton";
            this.bEndButton.Size = new System.Drawing.Size(94, 29);
            this.bEndButton.TabIndex = 4;
            this.bEndButton.Text = "End Game";
            this.bEndButton.UseVisualStyleBackColor = true;
            // 
            // bLoadBlocks
            // 
            this.bLoadBlocks.Location = new System.Drawing.Point(315, 302);
            this.bLoadBlocks.Name = "bLoadBlocks";
            this.bLoadBlocks.Size = new System.Drawing.Size(94, 29);
            this.bLoadBlocks.TabIndex = 5;
            this.bLoadBlocks.Text = "Load Blocks";
            this.bLoadBlocks.UseVisualStyleBackColor = true;
            // 
            // tbLoadingBlocksTextBox
            // 
            this.tbLoadingBlocksTextBox.Location = new System.Drawing.Point(292, 380);
            this.tbLoadingBlocksTextBox.Name = "tbLoadingBlocksTextBox";
            this.tbLoadingBlocksTextBox.Size = new System.Drawing.Size(125, 27);
            this.tbLoadingBlocksTextBox.TabIndex = 6;
            // 
            // lPoints
            // 
            this.lPoints.AutoSize = true;
            this.lPoints.Location = new System.Drawing.Point(73, 251);
            this.lPoints.Name = "lPoints";
            this.lPoints.Size = new System.Drawing.Size(51, 20);
            this.lPoints.TabIndex = 7;
            this.lPoints.Text = "Points:";
            // 
            // lLoadingBlocksLabel
            // 
            this.lLoadingBlocksLabel.AutoSize = true;
            this.lLoadingBlocksLabel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lLoadingBlocksLabel.Location = new System.Drawing.Point(221, 348);
            this.lLoadingBlocksLabel.Name = "lLoadingBlocksLabel";
            this.lLoadingBlocksLabel.Size = new System.Drawing.Size(331, 20);
            this.lLoadingBlocksLabel.TabIndex = 8;
            this.lLoadingBlocksLabel.Text = "Insert name of file where block reprezentation is:";
            // 
            // Window
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(782, 753);
            this.Controls.Add(this.lLoadingBlocksLabel);
            this.Controls.Add(this.lPoints);
            this.Controls.Add(this.tbLoadingBlocksTextBox);
            this.Controls.Add(this.bLoadBlocks);
            this.Controls.Add(this.bEndButton);
            this.Controls.Add(this.bPlayButton);
            this.Controls.Add(this.lGameOver);
            this.Controls.Add(this.lMainTitle);
            this.Controls.Add(this.pGameBoard);
            this.Name = "Window";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pGameBoard)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pGameBoard;
        private System.Windows.Forms.Label lMainTitle;
        private System.Windows.Forms.Label lGameOver;
        private System.Windows.Forms.Button bPlayButton;
        private System.Windows.Forms.Button bEndButton;
        private System.Windows.Forms.Button bLoadBlocks;
        private System.Windows.Forms.TextBox tbLoadingBlocksTextBox;
        private System.Windows.Forms.Label lPoints;
        private System.Windows.Forms.Label lLoadingBlocksLabel;
    }
}

