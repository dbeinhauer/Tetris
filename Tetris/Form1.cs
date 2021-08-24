using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tetris
{
    enum GameState
    {
        START,
        LOADING_BLOCKS,
        LOADING_MAP,
        ERROR_BLOCKS,
        ERROR_MAP,
        GAME,
        END
    }

    public partial class Window : Form
    {
        static string scoreText = "Score: ";

        GameState gameState;

        Bitmap canvasBitmap;
        Graphics canvasGraphics;
        int canvasWidth = 16;
        int canvasHeight = 20;

        int dotSize = 20;

        int mapWidth = 16;
        int mapHeight = 20;
        PlayGround playGround;
        Coordinates mapOffset;


        Timer timer = new Timer();
        Timer timerHandling = new Timer();
        private bool handlingAvailable = true;

        Bitmap workingBitmap;
        Graphics workingGraphics;

        Bitmap nextShapeBitmap;
        Graphics nextShapeGraphics;

        public Window()
        {
            this.KeyPreview = true;
            InitializeComponent();
            this.KeyDown += Window_KeyDown;


            this.gameState = GameState.START;

            this.mapOffset = new Coordinates(0, 0, this.mapWidth);

            this.handleDisplay();


            Reader reader = new Reader("GameBlocks/Default_Objects.txt");

            this.playGround = new PlayGround(mapWidth, mapHeight);

            this.loadGameObjects(reader);
        }

        private void handleDisplay()
        {
            switch (this.gameState)
            {
                case GameState.START:
                    this.pGameBoard.Hide();
                    this.pNextBlock.Hide();
                    this.lMainTitle.Show();
                    this.lPoints.Hide();
                    this.lLoadingBlocksLabel.Hide();
                    this.lLoadingMapLabel.Hide();
                    this.lNextBlock.Hide();
                    //this.lErrorMessage.Hide();
                    this.lGameOver.Hide();
                    this.bPlayButton.Show();
                    this.bLoadBlocks.Show();
                    this.bLoadMap.Show();
                    this.bReturn.Hide();
                    this.bEndButton.Show();
                    //this.tbLoadingBlocksTextBox.Hide();
                    break;
                case GameState.LOADING_BLOCKS:
                    this.pGameBoard.Hide();
                    this.pNextBlock.Hide();
                    this.lMainTitle.Hide();
                    this.lPoints.Hide();
                    this.lLoadingBlocksLabel.Show();
                    this.lLoadingMapLabel.Hide();
                    this.lNextBlock.Hide();
                    //this.lErrorMessage.Hide();
                    this.lGameOver.Hide();
                    this.bPlayButton.Hide();
                    this.bLoadBlocks.Show();
                    this.bLoadMap.Hide();
                    this.bReturn.Show();
                    this.bEndButton.Hide();
                    //this.tbLoadingBlocksTextBox.Show();
                    break;
                case GameState.LOADING_MAP:
                    this.pGameBoard.Hide();
                    this.pNextBlock.Hide();
                    this.lMainTitle.Hide();
                    this.lPoints.Hide();
                    this.lLoadingBlocksLabel.Hide();
                    this.lLoadingMapLabel.Show();
                    this.lNextBlock.Hide();
                    //this.lErrorMessage.Hide();
                    this.lGameOver.Hide();
                    this.bPlayButton.Hide();
                    this.bLoadBlocks.Hide();
                    this.bLoadMap.Show();
                    this.bReturn.Show();
                    this.bEndButton.Hide();
                    //this.tbLoadingBlocksTextBox.Show();
                    break;
                case GameState.ERROR_BLOCKS:
                    this.pGameBoard.Hide();
                    this.pNextBlock.Hide();
                    this.lMainTitle.Hide();
                    this.lPoints.Hide();
                    this.lLoadingBlocksLabel.Show();
                    this.lLoadingMapLabel.Hide();
                    this.lNextBlock.Hide();
                    //this.lErrorMessage.Show();
                    this.lGameOver.Hide();
                    this.bPlayButton.Hide();
                    this.bLoadBlocks.Show();
                    this.bLoadMap.Hide();
                    this.bReturn.Hide();
                    this.bEndButton.Show();
                    //this.tbLoadingBlocksTextBox.Show();

                    MessageBox.Show("Please add correct input file.", "Bad File Format", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case GameState.ERROR_MAP:
                    this.pGameBoard.Hide();
                    this.pNextBlock.Hide();
                    this.lMainTitle.Hide();
                    this.lPoints.Hide();
                    this.lLoadingBlocksLabel.Hide();
                    this.lLoadingMapLabel.Show();
                    this.lNextBlock.Hide();
                    //this.lErrorMessage.Show();
                    this.lGameOver.Hide();
                    this.bPlayButton.Hide();
                    this.bLoadBlocks.Hide();
                    this.bLoadMap.Show();
                    this.bReturn.Hide();
                    this.bEndButton.Show();
                    //this.tbLoadingBlocksTextBox.Show();

                    MessageBox.Show("Please add correct input file.", "Bad File Format", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case GameState.GAME:
                    this.pGameBoard.Show();
                    this.pNextBlock.Show();
                    this.lMainTitle.Show();
                    this.lPoints.Show();
                    this.lLoadingBlocksLabel.Hide();
                    this.lLoadingMapLabel.Hide();
                    this.lNextBlock.Show();
                    //this.lErrorMessage.Hide();
                    this.lGameOver.Hide();
                    this.bPlayButton.Hide();
                    this.bLoadBlocks.Hide();
                    this.bLoadMap.Hide();
                    this.bReturn.Hide();
                    this.bEndButton.Hide();
                    //this.tbLoadingBlocksTextBox.Hide();

                    this.lPoints.Text = Window.scoreText + this.playGround.map.score;
                    break;
                case GameState.END:
                    this.pGameBoard.Show();
                    this.pNextBlock.Show();
                    this.lMainTitle.Show();
                    this.lPoints.Show();
                    this.lLoadingBlocksLabel.Hide();
                    this.lLoadingMapLabel.Hide();
                    this.lNextBlock.Show();
                    //this.lErrorMessage.Hide();
                    this.lGameOver.Show();
                    this.bPlayButton.Show();
                    this.bLoadBlocks.Hide();
                    this.bLoadMap.Hide();
                    this.bReturn.Hide();
                    this.bEndButton.Show();
                    //this.tbLoadingBlocksTextBox.Hide();
                    break;
                default:
                    break;
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            // if shape reached bottom or touched any other shapes
            if (!this.playGround.MoveDown())
            {
                // copy working image into canvas image
                this.canvasBitmap = new Bitmap(this.workingBitmap);

                this.playGround.map.AddObject(this.playGround.actualBlock, this.playGround.actualOffset);

                if (this.playGround.map.ManageFilledRows())
                {
                    this.drawMap();
                    this.lPoints.Text = Window.scoreText + this.playGround.map.score; 
                    this.timer.Interval -= 10;
                }

                // get next shape
                if (!this.playGround.CheckGameOver())
                {
                    this.drawShape(this.playGround.actualBlock, this.playGround.actualOffset);
                    this.handleGameOver();
                    return;
                }

                this.drawNextShape(this.playGround.nextBlock);
                
                this.playGround.PrintMap();
            }

            this.drawShape(this.playGround.actualBlock, this.playGround.actualOffset);
        }

        private void TimerHandling_Tick(object sender, EventArgs e)
        {
            this.handlingAvailable = true;
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (!this.handlingAvailable)
                return;

            this.handlingAvailable = false;
            this.timerHandling.Start();
            // calculate the vertical and horizontal move values
            // based on the key pressed
            switch (e.KeyCode)
            {
                // move shape left
                case Keys.Left:
                    this.playGround.MoveLeft();
                    break;

                // move shape right
                case Keys.Right:
                    this.playGround.MoveRight();
                    break;

                // move shape down faster
                case Keys.Down:
                    this.playGround.MoveDown();
                    break;

                // rotate the shape clockwise
                case Keys.Up:
                    this.playGround.Rotate();
                    break;

                default:
                    return;
            }

            this.drawShape(this.playGround.actualBlock, this.playGround.actualOffset);
        }


        private void loadcanvas()
        {
            // resize the picture box based on the dotsize and canvas size
            pGameBoard.Width = canvasWidth * dotSize;
            pGameBoard.Height = canvasHeight * dotSize;

            // create bitmap with picture box's size
            canvasBitmap = new Bitmap(pGameBoard.Width, pGameBoard.Height);

            canvasGraphics = Graphics.FromImage(canvasBitmap);

            canvasGraphics.FillRectangle(Brushes.LightSteelBlue, 0, 0, canvasBitmap.Width, canvasBitmap.Height);

            // load bitmap into picture box
            pGameBoard.Image = canvasBitmap;

        }

        private void drawShape(Block gameObject, Coordinates offset)
        {
            workingBitmap = new Bitmap(canvasBitmap);
            workingGraphics = Graphics.FromImage(workingBitmap);

            for (int i = 0; i < gameObject.Height; i++)
            {
                for (int j = 0; j < gameObject.Width; j++)
                {
                    if (gameObject.GetBitmapToCheck()[j,i] == GameObject.FullChar)
                        workingGraphics.FillRectangle(Brushes.Black, (offset.X + j) * dotSize, (offset.Y + i) * dotSize, dotSize, dotSize);
                }
            }

            pGameBoard.Image = workingBitmap;
        }

        private void drawMap()
        {
            var bitmap = new Bitmap(canvasBitmap);
            var graphics = Graphics.FromImage(bitmap);

            for (int i = 0; i < this.playGround.map.Height; i++)
            {
                for (int j = 0; j < this.playGround.map.Width; j++)
                {
                    graphics.FillRectangle(
                        this.playGround.map.Bitmap[j, i] == GameObject.FullChar ? Brushes.Black : Brushes.LightSteelBlue,
                        j * dotSize, i * dotSize, dotSize, dotSize);
                }
            }

            this.canvasBitmap = new Bitmap(bitmap);
        }

        private void drawNextShape(Block nextBlock)
        {
            nextShapeGraphics.FillRectangle(Brushes.LightSteelBlue, 0, 0, nextShapeBitmap.Width, nextShapeBitmap.Height);

            // Find the ideal position for the shape in the side panel
            var startX = (6 - nextBlock.Width) / 2;
            var startY = (6 - nextBlock.Height) / 2;

            for (int i = 0; i < nextBlock.Height; i++)
            {
                for (int j = 0; j < nextBlock.Width; j++)
                {
                    nextShapeGraphics.FillRectangle(
                        nextBlock.GetBitmapToCheck()[j, i] == GameObject.FullChar ? Brushes.Black : Brushes.LightSteelBlue,
                        (startX + j) * dotSize, (startY + i) * dotSize, dotSize, dotSize);
                }
            }

            pNextBlock.Size = nextShapeBitmap.Size;
            pNextBlock.Image = nextShapeBitmap;
        }

        private void handleFileLoadingBlocks()
        {
            string filename = this.getReaderFile();
            if (filename == null)
            {
                this.gameState = GameState.ERROR_BLOCKS;
                return;
            }

            if (this.loadGameObjects(new Reader(filename)))
            {
                this.gameState = GameState.START;
            }
        }

        private void handleFileLoadingMap()
        {
            string filename = this.getReaderFile();
            if (filename == null)
            {
                this.gameState = GameState.ERROR_MAP;
                return;
            }

            if (this.loadMap(new Reader(filename)))//new Reader(this.tbLoadingBlocksTextBox.Text)))
            {
                this.gameState = GameState.START;
            }

            //this.tbLoadingBlocksTextBox.Clear();
        }

        private string getReaderFile()
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            DialogResult result = openFileDialog1.ShowDialog(); // Show the dialog.
            if (result == DialogResult.OK) // Test result.
            {
                return openFileDialog1.FileName;
            }

            return null;
        }

        private bool loadGameObjects(Reader reader)
        {
            if (reader.Error)
            {
                this.gameState = GameState.ERROR_BLOCKS;
                //this.handleDisplay();
                return false;
            }

            //this.playGround = new PlayGround(mapWidth, mapHeight);
            this.playGround.LoadGameObjects(reader);

            if (reader.Error)
            {
                this.gameState = GameState.ERROR_BLOCKS;
                //this.handleDisplay();
                return false;
            }

            return true;
        }

        private bool loadMap(Reader reader)
        {
            if (reader.Error)
            {
                this.gameState = GameState.ERROR_MAP;
                return false;
            }

            this.playGround.map.ResetGame();

            if (!reader.ReadMap(this.playGround.map))
            {
                this.gameState = GameState.ERROR_MAP;
                return false;
            }

            this.playGround.map.SetActualDefault();

            return true;
        }

        private void lPoints_Click(object sender, EventArgs e)
        {

        }

        private void bPlayButton_Click(object sender, EventArgs e)
        {
            this.gameState = GameState.GAME;
            this.playGround.map.ResetGame();
            this.handleDisplay();

            this.loadcanvas();

            this.playGround.GenerateNextBlock();
            this.playGround.CheckGameOver();

            this.drawMap();
            this.drawShape(this.playGround.actualBlock, this.playGround.actualOffset);

            // Codes to show the next shape in the side panel
            nextShapeBitmap = new Bitmap(6 * dotSize, 6 * dotSize);
            nextShapeGraphics = Graphics.FromImage(nextShapeBitmap);

            nextShapeGraphics.FillRectangle(Brushes.LightSteelBlue, 0, 0, nextShapeBitmap.Width, nextShapeBitmap.Height);

            //this.drawShape(this.playGround.actualBlock, this.playGround.actualOffset);
            this.drawNextShape(this.playGround.nextBlock);

            timer.Tick += Timer_Tick;
            timer.Interval = 500;
            timer.Start();

            timerHandling.Tick += TimerHandling_Tick;
            timerHandling.Interval = 100;
            timerHandling.Start();
        }

        private void handleGameOver()
        {
            this.gameState = GameState.END;
            this.handleDisplay();
        }

        private void bLoadBlocks_Click(object sender, EventArgs e)
        {
            switch (this.gameState)
            {
                case GameState.START:
                    this.gameState = GameState.LOADING_BLOCKS;
                    break;
                //case GameState.END:
                //    this.gameState = GameState.LOADING_BLOCKS;
                //    break;
                case GameState.LOADING_BLOCKS:
                    this.handleFileLoadingBlocks();
                    break;
                case GameState.ERROR_BLOCKS:
                    this.handleFileLoadingBlocks();
                    break;
                default:
                    break;
            }
            this.handleDisplay();
        }

        private void bReturn_Click(object sender, EventArgs e)
        {
            this.gameState = GameState.START;
            this.handleDisplay();
        }

        private void bEndButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }




        private void tbLoadingBlocksTextBox_Enter(object sender, EventArgs e)
        {
        }

        private void lGameOver_Click(object sender, EventArgs e)
        {

        }

        private void bLoadMap_Click(object sender, EventArgs e)
        {
            switch (this.gameState)
            {
                case GameState.START:
                    this.gameState = GameState.LOADING_MAP;
                    break;
                case GameState.LOADING_MAP:
                    this.handleFileLoadingMap();
                    break;
                case GameState.ERROR_MAP:
                    this.handleFileLoadingMap();
                    break;
                default:
                    break;
            }
            this.handleDisplay();
        }
    }
}
