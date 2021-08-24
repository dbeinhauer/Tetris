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
            this.lPoints = new System.Windows.Forms.Label();
            this.lLoadingBlocksLabel = new System.Windows.Forms.Label();
            this.pNextBlock = new System.Windows.Forms.PictureBox();
            this.lNextBlock = new System.Windows.Forms.Label();
            this.bReturn = new System.Windows.Forms.Button();
            this.bLoadMap = new System.Windows.Forms.Button();
            this.lLoadingMapLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pGameBoard)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pNextBlock)).BeginInit();
            this.SuspendLayout();
            // 
            // pGameBoard
            // 
            this.pGameBoard.BackColor = System.Drawing.Color.LightSteelBlue;
            this.pGameBoard.Location = new System.Drawing.Point(218, 114);
            this.pGameBoard.Name = "pGameBoard";
            this.pGameBoard.Size = new System.Drawing.Size(125, 18);
            this.pGameBoard.TabIndex = 0;
            this.pGameBoard.TabStop = false;
            // 
            // lMainTitle
            // 
            this.lMainTitle.AutoSize = true;
            this.lMainTitle.Font = new System.Drawing.Font("Showcard Gothic", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lMainTitle.ForeColor = System.Drawing.Color.Lime;
            this.lMainTitle.Location = new System.Drawing.Point(249, 20);
            this.lMainTitle.Name = "lMainTitle";
            this.lMainTitle.Size = new System.Drawing.Size(237, 74);
            this.lMainTitle.TabIndex = 1;
            this.lMainTitle.Text = "Tetris";
            // 
            // lGameOver
            // 
            this.lGameOver.AutoSize = true;
            this.lGameOver.BackColor = System.Drawing.Color.Transparent;
            this.lGameOver.Font = new System.Drawing.Font("Showcard Gothic", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lGameOver.ForeColor = System.Drawing.Color.Red;
            this.lGameOver.Location = new System.Drawing.Point(137, 283);
            this.lGameOver.Name = "lGameOver";
            this.lGameOver.Size = new System.Drawing.Size(483, 98);
            this.lGameOver.TabIndex = 2;
            this.lGameOver.Text = "Game Over";
            this.lGameOver.Click += new System.EventHandler(this.lGameOver_Click);
            // 
            // bPlayButton
            // 
            this.bPlayButton.BackColor = System.Drawing.Color.Salmon;
            this.bPlayButton.Font = new System.Drawing.Font("Showcard Gothic", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.bPlayButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.bPlayButton.Location = new System.Drawing.Point(244, 174);
            this.bPlayButton.Name = "bPlayButton";
            this.bPlayButton.Size = new System.Drawing.Size(250, 70);
            this.bPlayButton.TabIndex = 3;
            this.bPlayButton.Text = "Play";
            this.bPlayButton.UseVisualStyleBackColor = false;
            this.bPlayButton.Click += new System.EventHandler(this.bPlayButton_Click);
            // 
            // bEndButton
            // 
            this.bEndButton.BackColor = System.Drawing.Color.Firebrick;
            this.bEndButton.Font = new System.Drawing.Font("Showcard Gothic", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.bEndButton.Location = new System.Drawing.Point(244, 417);
            this.bEndButton.Name = "bEndButton";
            this.bEndButton.Size = new System.Drawing.Size(250, 70);
            this.bEndButton.TabIndex = 4;
            this.bEndButton.Text = "End Game";
            this.bEndButton.UseVisualStyleBackColor = false;
            this.bEndButton.Click += new System.EventHandler(this.bEndButton_Click);
            // 
            // bLoadBlocks
            // 
            this.bLoadBlocks.BackColor = System.Drawing.Color.PaleGreen;
            this.bLoadBlocks.Font = new System.Drawing.Font("Showcard Gothic", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.bLoadBlocks.Location = new System.Drawing.Point(244, 258);
            this.bLoadBlocks.Name = "bLoadBlocks";
            this.bLoadBlocks.Size = new System.Drawing.Size(250, 70);
            this.bLoadBlocks.TabIndex = 5;
            this.bLoadBlocks.Text = "Load Blocks";
            this.bLoadBlocks.UseVisualStyleBackColor = false;
            this.bLoadBlocks.Click += new System.EventHandler(this.bLoadBlocks_Click);
            // 
            // lPoints
            // 
            this.lPoints.AutoSize = true;
            this.lPoints.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lPoints.Location = new System.Drawing.Point(25, 114);
            this.lPoints.Name = "lPoints";
            this.lPoints.Size = new System.Drawing.Size(106, 38);
            this.lPoints.TabIndex = 7;
            this.lPoints.Text = "Points:";
            this.lPoints.Click += new System.EventHandler(this.lPoints_Click);
            // 
            // lLoadingBlocksLabel
            // 
            this.lLoadingBlocksLabel.AutoSize = true;
            this.lLoadingBlocksLabel.BackColor = System.Drawing.Color.Transparent;
            this.lLoadingBlocksLabel.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lLoadingBlocksLabel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lLoadingBlocksLabel.Location = new System.Drawing.Point(100, 201);
            this.lLoadingBlocksLabel.Name = "lLoadingBlocksLabel";
            this.lLoadingBlocksLabel.Size = new System.Drawing.Size(565, 41);
            this.lLoadingBlocksLabel.TabIndex = 8;
            this.lLoadingBlocksLabel.Text = "Choose file with object reprezentation:";
            // 
            // pNextBlock
            // 
            this.pNextBlock.BackColor = System.Drawing.Color.LightSteelBlue;
            this.pNextBlock.Location = new System.Drawing.Point(588, 150);
            this.pNextBlock.Name = "pNextBlock";
            this.pNextBlock.Size = new System.Drawing.Size(125, 62);
            this.pNextBlock.TabIndex = 9;
            this.pNextBlock.TabStop = false;
            // 
            // lNextBlock
            // 
            this.lNextBlock.AutoSize = true;
            this.lNextBlock.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lNextBlock.Location = new System.Drawing.Point(576, 106);
            this.lNextBlock.Name = "lNextBlock";
            this.lNextBlock.Size = new System.Drawing.Size(169, 38);
            this.lNextBlock.TabIndex = 10;
            this.lNextBlock.Text = "Next Block:";
            // 
            // bReturn
            // 
            this.bReturn.BackColor = System.Drawing.Color.Violet;
            this.bReturn.Font = new System.Drawing.Font("Showcard Gothic", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.bReturn.ForeColor = System.Drawing.Color.Black;
            this.bReturn.Location = new System.Drawing.Point(588, 455);
            this.bReturn.Name = "bReturn";
            this.bReturn.Size = new System.Drawing.Size(121, 37);
            this.bReturn.TabIndex = 11;
            this.bReturn.Text = "Return";
            this.bReturn.UseVisualStyleBackColor = false;
            this.bReturn.Click += new System.EventHandler(this.bReturn_Click);
            // 
            // bLoadMap
            // 
            this.bLoadMap.BackColor = System.Drawing.Color.DarkTurquoise;
            this.bLoadMap.Font = new System.Drawing.Font("Showcard Gothic", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.bLoadMap.Location = new System.Drawing.Point(244, 336);
            this.bLoadMap.Name = "bLoadMap";
            this.bLoadMap.Size = new System.Drawing.Size(250, 70);
            this.bLoadMap.TabIndex = 12;
            this.bLoadMap.Text = "Load Map";
            this.bLoadMap.UseVisualStyleBackColor = false;
            this.bLoadMap.Click += new System.EventHandler(this.bLoadMap_Click);
            // 
            // lLoadingMapLabel
            // 
            this.lLoadingMapLabel.AutoSize = true;
            this.lLoadingMapLabel.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lLoadingMapLabel.Location = new System.Drawing.Point(109, 272);
            this.lLoadingMapLabel.Name = "lLoadingMapLabel";
            this.lLoadingMapLabel.Size = new System.Drawing.Size(539, 41);
            this.lLoadingMapLabel.TabIndex = 13;
            this.lLoadingMapLabel.Text = "Choose file with map reprezentation:";
            // 
            // Window
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.RoyalBlue;
            this.ClientSize = new System.Drawing.Size(782, 553);
            this.Controls.Add(this.lLoadingMapLabel);
            this.Controls.Add(this.bLoadMap);
            this.Controls.Add(this.bReturn);
            this.Controls.Add(this.lNextBlock);
            this.Controls.Add(this.pNextBlock);
            this.Controls.Add(this.lLoadingBlocksLabel);
            this.Controls.Add(this.lPoints);
            this.Controls.Add(this.bLoadBlocks);
            this.Controls.Add(this.bEndButton);
            this.Controls.Add(this.bPlayButton);
            this.Controls.Add(this.lGameOver);
            this.Controls.Add(this.lMainTitle);
            this.Controls.Add(this.pGameBoard);
            this.Name = "Window";
            this.Text = "Tetris";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Window_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.pGameBoard)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pNextBlock)).EndInit();
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
        private System.Windows.Forms.Label lPoints;
        private System.Windows.Forms.Label lLoadingBlocksLabel;
        private System.Windows.Forms.PictureBox pNextBlock;
        private System.Windows.Forms.Label lNextBlock;
        private System.Windows.Forms.Button bReturn;
        private System.Windows.Forms.Button bLoadMap;
        private System.Windows.Forms.Label lLoadingMapLabel;
    }
}

