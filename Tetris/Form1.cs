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
    /// <summary>
    /// State of the Game (mainly for window layout).
    /// </summary>
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
        static private string scoreText = "Score: ";
        static private string loadErrorTitle = "Bad File Format";
        static private string loadErrorMessage = "Please add correct input file.";

        static private string filenameObjects = "GameBlocks/Default_Objects.txt";

        // Variables for the playground image
        private Bitmap canvasBitmap;
        private Graphics canvasGraphics;

        // Variables for the actual object image
        private Bitmap workingBitmap;
        private Graphics workingGraphics;

        // Variables for the next object image
        private Bitmap nextShapeBitmap;
        private Graphics nextShapeGraphics;

        // Timers:

        // For next block fall.
        private Timer timerFalling = new Timer();
        private int timerFallingInterval = 500;

        // For interval between user inputs.
        private Timer timerHandling = new Timer();
        private int timerHandlingInterval = 100;

        // Flag if next user input (key press) is available.
        private bool handlingAvailable = true;

        // Size of one position in playgound (one square).
        private int dotSize = 20;

        // Actual game state
        private GameState gameState;

        // Size of the map
        private int mapWidth = 16;
        private int mapHeight = 20;

        // Playgound reprezentation
        private PlayGround playGround;


        public Window()
        {
            // Init window properties
            this.KeyPreview = true;
            InitializeComponent();
            this.KeyDown += Window_KeyDown;


            // Init timers
            this.timerFalling.Tick += timerFalling_Tick;

            this.timerHandling.Tick += timerHandling_Tick;
            this.timerHandling.Interval = timerHandlingInterval;


            // Init window layout and game state
            this.gameState = GameState.START;
            this.manageLayout();


            // Use deafault settings of the map and blocks
            Reader reader = new Reader(Window.filenameObjects);
            this.playGround = new PlayGround(mapWidth, mapHeight);
            this.loadGameObjects(reader);
        }


        /// <summary>
        /// Manages layout of the window based on the acutal game state.
        /// </summary>
        private void manageLayout()
        {
            switch (this.gameState)
            {
                case GameState.START:
                    // Start Menu
                    this.pGameBoard.Hide();
                    this.pNextBlock.Hide();

                    this.lMainTitle.Show();
                    this.lPoints.Hide();
                    this.lLoadingBlocksLabel.Hide();
                    this.lLoadingMapLabel.Hide();
                    this.lNextBlock.Hide();
                    this.lGameOver.Hide();

                    this.bPlayButton.Show();
                    this.bLoadBlocks.Show();
                    this.bLoadMap.Show();
                    this.bReturn.Hide();
                    this.bEndButton.Show();

                    this.bPlayButton.Enabled = true;
                    this.bLoadBlocks.Enabled = true;
                    this.bLoadMap.Enabled = true;
                    this.bReturn.Enabled = false;
                    this.bEndButton.Enabled = true;
                    break;

                case GameState.LOADING_BLOCKS:
                    // Loading Block Reprezentation Menu
                    this.pGameBoard.Hide();
                    this.pNextBlock.Hide();

                    this.lMainTitle.Hide();
                    this.lPoints.Hide();
                    this.lLoadingBlocksLabel.Show();
                    this.lLoadingMapLabel.Hide();
                    this.lNextBlock.Hide();
                    this.lGameOver.Hide();

                    this.bPlayButton.Hide();
                    this.bLoadBlocks.Show();
                    this.bLoadMap.Hide();
                    this.bReturn.Show();
                    this.bEndButton.Hide();

                    this.bPlayButton.Enabled = false;
                    this.bLoadBlocks.Enabled = true;
                    this.bLoadMap.Enabled = false;
                    this.bReturn.Enabled = true;
                    this.bEndButton.Enabled = false;
                    break;

                case GameState.LOADING_MAP:
                    // Loading Map Reprezentation Menu
                    this.pGameBoard.Hide();
                    this.pNextBlock.Hide();

                    this.lMainTitle.Hide();
                    this.lPoints.Hide();
                    this.lLoadingBlocksLabel.Hide();
                    this.lLoadingMapLabel.Show();
                    this.lNextBlock.Hide();
                    this.lGameOver.Hide();

                    this.bPlayButton.Hide();
                    this.bLoadBlocks.Hide();
                    this.bLoadMap.Show();
                    this.bReturn.Show();
                    this.bEndButton.Hide();

                    this.bPlayButton.Enabled = false;
                    this.bLoadBlocks.Enabled = false;
                    this.bLoadMap.Enabled = true;
                    this.bReturn.Enabled = true;
                    this.bEndButton.Enabled = false;
                    break;

                case GameState.ERROR_BLOCKS:
                    // Bad Block Reprezentaion Menu
                    this.pGameBoard.Hide();
                    this.pNextBlock.Hide();

                    this.lMainTitle.Hide();
                    this.lPoints.Hide();
                    this.lLoadingBlocksLabel.Show();
                    this.lLoadingMapLabel.Hide();
                    this.lNextBlock.Hide();
                    this.lGameOver.Hide();

                    this.bPlayButton.Hide();
                    this.bLoadBlocks.Show();
                    this.bLoadMap.Hide();
                    this.bReturn.Hide();
                    this.bEndButton.Show();

                    this.bPlayButton.Enabled = false;
                    this.bLoadBlocks.Enabled = true;
                    this.bLoadMap.Enabled = false;
                    this.bReturn.Enabled = false;
                    this.bEndButton.Enabled = true;

                    MessageBox.Show(Window.loadErrorMessage, Window.loadErrorTitle,
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;

                case GameState.ERROR_MAP:
                    // Bad Map Reprezentaion Menu
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

                    this.bPlayButton.Enabled = false;
                    this.bLoadBlocks.Enabled = false;
                    this.bLoadMap.Enabled = true;
                    this.bReturn.Enabled = false;
                    this.bEndButton.Enabled = true;

                    MessageBox.Show(Window.loadErrorMessage, Window.loadErrorTitle,
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;

                case GameState.GAME:
                    // Game Playground
                    this.pGameBoard.Show();
                    this.pNextBlock.Show();

                    this.lMainTitle.Show();
                    this.lPoints.Show();
                    this.lLoadingBlocksLabel.Hide();
                    this.lLoadingMapLabel.Hide();
                    this.lNextBlock.Show();
                    this.lGameOver.Hide();

                    this.bPlayButton.Hide();
                    this.bLoadBlocks.Hide();
                    this.bLoadMap.Hide();
                    this.bReturn.Hide();
                    this.bEndButton.Hide();

                    this.bPlayButton.Enabled = false;
                    this.bLoadBlocks.Enabled = false;
                    this.bLoadMap.Enabled = false;
                    this.bReturn.Enabled = false;
                    this.bEndButton.Enabled = false;

                    this.lPoints.Text = Window.scoreText + this.playGround.map.score;
                    break;

                case GameState.END:
                    // Game Over Menu
                    this.pGameBoard.Show();
                    this.pNextBlock.Show();

                    this.lMainTitle.Show();
                    this.lPoints.Show();
                    this.lLoadingBlocksLabel.Hide();
                    this.lLoadingMapLabel.Hide();
                    this.lNextBlock.Show();
                    this.lGameOver.Show();

                    this.bPlayButton.Show();
                    this.bLoadBlocks.Hide();
                    this.bLoadMap.Hide();
                    this.bReturn.Hide();
                    this.bEndButton.Show();

                    this.bPlayButton.Enabled = true;
                    this.bLoadBlocks.Enabled = false;
                    this.bLoadMap.Enabled = false;
                    this.bReturn.Enabled = false;
                    this.bEndButton.Enabled = true;
                    break;

                default:
                    break;
            }
        }


        /// <summary>
        /// Loads canvas reprezenting the playboard.
        /// </summary>
        private void loadCanvas()
        {
            // Resize the picture box based on the dotsize and canvas size.
            this.pGameBoard.Width = this.mapWidth * this.dotSize;
            this.pGameBoard.Height = this.mapHeight * this.dotSize;

            // Create bitmap with picture box's size.
            this.canvasBitmap = new Bitmap(this.pGameBoard.Width, 
                                            this.pGameBoard.Height);

            this.canvasGraphics = Graphics.FromImage(this.canvasBitmap);

            this.canvasGraphics.FillRectangle(Brushes.LightSteelBlue, 0, 0,
                            this.canvasBitmap.Width, this.canvasBitmap.Height);

            // Load bitmap into picture box.
            this.pGameBoard.Image = this.canvasBitmap;
        }


        /// <summary>
        /// Manages loading blocks reprezentation from the specific file.
        /// </summary>
        private void manageFileLoadingBlocks()
        {
            // Get the filename from the user. 
            string filename = this.getReaderFile();

            if (filename == null)
            // Bad user input file -> Error
            {
                this.gameState = GameState.ERROR_BLOCKS;
                return;
            }

            // Loading objects succesfull -> Return to Start Menu
            if (this.loadGameObjects(new Reader(filename)))
                this.gameState = GameState.START;
        }


        /// <summary>
        /// Manages loading map reprezentation from the specific file.
        /// </summary>
        private void manageFileLoadingMap()
        {
            // Get the filename from the user. 
            string filename = this.getReaderFile();

            if (filename == null)
            // Bad user input file -> Error
            {
                this.gameState = GameState.ERROR_MAP;
                return;
            }

            // Loading map succesfull -> Return to Start Menu
            if (this.loadMap(new Reader(filename)))
                this.gameState = GameState.START;
        }


        /// <summary>
        /// Opens Dialog Window and gets filename from user.
        /// </summary>
        /// <returns>Filename from the user (`null` if bad input).</returns>
        private string getReaderFile()
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            // Show the dialog.
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            // Result Ok -> return filename
            {
                return openFileDialog1.FileName;
            }

            // Bad result.
            return null;
        }


        /// <summary>
        /// Loads Game Objects from the reader.
        /// </summary>
        /// <param name="reader">Reader to load object reprezentation.</param>
        /// <returns>Returns `true` if loading was successful, else `false`.</returns>
        private bool loadGameObjects(Reader reader)
        {
            if (reader.Error)
            // Problem with opening the file -> Error
            {
                this.gameState = GameState.ERROR_BLOCKS;
                return false;
            }

            this.playGround.LoadGameObjects(reader);

            if (reader.Error)
            // Bad file format -> Error
            {
                this.gameState = GameState.ERROR_BLOCKS;
                return false;
            }

            // Loading successful.
            return true;
        }


        /// <summary>
        /// Loads map from the reader.
        /// </summary>
        /// <param name="reader">Reader to load map reprezentation.</param>
        /// <returns>Returns `true` if loading was succesful, else `false`.</returns>
        private bool loadMap(Reader reader)
        {
            if (reader.Error)
            // Problem with opening the file -> Error
            {
                this.gameState = GameState.ERROR_MAP;
                return false;
            }

            this.playGround.map.ResetGame();

            if (!reader.ReadMap(this.playGround.map))
            // Bad file format -> Error
            {
                this.gameState = GameState.ERROR_MAP;
                return false;
            }

            this.playGround.map.SetActualDefault();

            // Loading successful.
            return true;
        }


        /// <summary>
        /// Draws playground map on the canvas.
        /// </summary>
        private void drawMap()
        {
            var bitmap = new Bitmap(this.canvasBitmap);
            var graphics = Graphics.FromImage(bitmap);

            for (int i = 0; i < this.playGround.map.Height; i++)
            {
                for (int j = 0; j < this.playGround.map.Width; j++)
                {
                    graphics.FillRectangle(
                        this.playGround.map.Bitmap[j, i] == GameObject.FullChar
                        ? Brushes.Black : Brushes.LightSteelBlue,
                        j * this.dotSize, i * this.dotSize, this.dotSize, this.dotSize);
                }
            }

            this.canvasBitmap = new Bitmap(bitmap);
        }


        /// <summary>
        /// Draws given game object on the canvas (based on the `offset`).
        /// </summary>
        /// <param name="gameObject">Block to be drawn on the canvas.</param>
        /// <param name="offset">Offset of the top-left corners of map and `gameObject`. </param>
        private void drawShape(Block gameObject, Coordinates offset)
        {
            this.workingBitmap = new Bitmap(this.canvasBitmap);
            this.workingGraphics = Graphics.FromImage(this.workingBitmap);

            for (int i = 0; i < gameObject.Height; i++)
            {
                for (int j = 0; j < gameObject.Width; j++)
                {
                    // Filled cell (draw it on the canvas).
                    if (gameObject.GetBitmapToCheck()[j,i] == GameObject.FullChar)
                        this.workingGraphics.FillRectangle(Brushes.Black, 
                                            (offset.X + j) * this.dotSize,
                                            (offset.Y + i) * this.dotSize,
                                            this.dotSize, this.dotSize);
                }
            }

            this.pGameBoard.Image = this.workingBitmap;
        }


        /// <summary>
        /// Draws next shape into its own canvas (not playground).
        /// </summary>
        /// <param name="nextBlock">Block to be drawn.</param>
        private void drawNextShape(Block nextBlock)
        {
            // Initialize canvas.
            this.nextShapeGraphics.FillRectangle(Brushes.LightSteelBlue, 0, 0,
                            this.nextShapeBitmap.Width, this.nextShapeBitmap.Height);

            // Find the ideal position for the shape in the side panel.
            var startX = (6 - nextBlock.Width) / 2;
            var startY = (6 - nextBlock.Height) / 2;

            for (int i = 0; i < nextBlock.Height; i++)
            {
                for (int j = 0; j < nextBlock.Width; j++)
                {
                    nextShapeGraphics.FillRectangle(
                        nextBlock.GetBitmapToCheck()[j, i] == GameObject.FullChar 
                        ? Brushes.Black : Brushes.LightSteelBlue,
                        (startX + j) * dotSize,
                        (startY + i) * dotSize,
                        dotSize, dotSize);
                }
            }

            this.pNextBlock.Size = this.nextShapeBitmap.Size;
            this.pNextBlock.Image = this.nextShapeBitmap;
        }


        /// <summary>
        /// Checks whether some rows are filled and updates score and falling speed.
        /// </summary>
        private void checkFilledRows()
        {
            if (this.playGround.map.ManageFilledRows())
            // Some rows were filled -> update data
            {
                this.drawMap();
                this.lPoints.Text = Window.scoreText + this.playGround.map.score;
                this.timerFalling.Interval -= 10;
            }
        }


        /// <summary>
        /// Executed when Game Over (manages layout of the window).
        /// </summary>
        private void manageGameOver()
        {
            this.gameState = GameState.END;
            this.manageLayout();
        }


        /// <summary>
        /// Manages falling of the block (is called every tick of the `timerFalling`).
        /// </summary>
        private void timerFalling_Tick(object sender, EventArgs e)
        {
            if (!this.playGround.MoveDown())
            // Reached bottom or touched any other shapes.
            {
                // Copy working image into canvas image.
                this.canvasBitmap = new Bitmap(this.workingBitmap);

                // Place object into the map.
                this.playGround.map.AddObject(this.playGround.actualBlock,
                                            this.playGround.actualOffset);

                this.checkFilledRows();

                // Get next shape or Game Over
                if (!this.playGround.CheckGameOver())
                // Game Over
                {
                    this.drawShape(this.playGround.actualBlock,
                                  this.playGround.actualOffset);
                    this.manageGameOver();
                    return;
                }

                this.drawNextShape(this.playGround.nextBlock);

                // For debuging:
                //this.playGround.PrintMap();
            }

            // Update canvas
            this.drawShape(this.playGround.actualBlock, this.playGround.actualOffset);
        }


        /// <summary>
        /// Sets used handling possible if enough time since last input 
        /// (tick of `timerHandling`).
        /// </summary>
        private void timerHandling_Tick(object sender, EventArgs e)
        {
            this.handlingAvailable = true;
        }


        /// <summary>
        /// Manages Keyboard input (game control).
        /// </summary>
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            // Not enough time since last action.
            if (!this.handlingAvailable)
                return;

            this.handlingAvailable = false;

            switch (e.KeyCode)
            {
                // Move shape left
                case Keys.Left:
                case Keys.A:
                    this.playGround.MoveLeft();
                    break;

                // Move shape right
                case Keys.Right:
                case Keys.D:
                    this.playGround.MoveRight();
                    break;

                // Move shape down faster
                case Keys.Down:
                case Keys.S:
                    this.playGround.MoveDown();
                    break;

                // Rotate the shape clockwise
                case Keys.Up:
                case Keys.W:
                case Keys.Space:
                    this.playGround.Rotate();
                    break;

                // End the game
                case Keys.Escape:
                    this.Close();
                    return;

                default:
                    return;
            }

            // Update the game layout.
            this.drawShape(this.playGround.actualBlock, this.playGround.actualOffset);

            this.timerHandling.Start();
        }


        /// <summary>
        /// Starts new game (executed after `Play` button pressed).
        /// </summary>
        private void bPlayButton_Click(object sender, EventArgs e)
        {
            // Set all variable to new game.
            this.gameState = GameState.GAME;
            this.playGround.map.ResetGame();

            this.manageLayout();
            this.loadCanvas();

            // Generate First two blocks.
            this.playGround.GenerateNextBlock();
            this.playGround.CheckGameOver();

            // Draw initial map and first object.
            this.drawMap();
            this.drawShape(this.playGround.actualBlock, this.playGround.actualOffset);

            // Codes to show the next shape in the side panel
            this.nextShapeBitmap = new Bitmap(6 * this.dotSize, 6 * this.dotSize);
            this.nextShapeGraphics = Graphics.FromImage(this.nextShapeBitmap);

            this.nextShapeGraphics.FillRectangle(Brushes.LightSteelBlue, 0, 0,
                            this.nextShapeBitmap.Width, this.nextShapeBitmap.Height);

            this.drawNextShape(this.playGround.nextBlock);

            // Reset the timers.
            this.timerFalling.Interval = this.timerFallingInterval;
            this.timerFalling.Start();

            this.timerHandling.Start();
        }


        /// <summary>
        /// Does proper operations when the Load Blocks button is pressed.
        /// Either changes the window or loads the blocks.
        /// </summary>
        private void bLoadBlocks_Click(object sender, EventArgs e)
        {
            switch (this.gameState)
            {
                // Move to the Loading Blocks Menu.
                case GameState.START:
                    this.gameState = GameState.LOADING_BLOCKS;
                    break;

                // Load the Blocks.
                case GameState.LOADING_BLOCKS:
                case GameState.ERROR_BLOCKS:
                    this.manageFileLoadingBlocks();
                    break;

                default:
                    break;
            }

            this.manageLayout();
        }


        /// <summary>
        /// Does proper operations when the Load Map button is pressed.
        /// Either changes the window or loads the map.
        /// </summary>
        private void bLoadMap_Click(object sender, EventArgs e)
        {
            switch (this.gameState)
            {
                // Move to the Loading Map Menu.
                case GameState.START:
                    this.gameState = GameState.LOADING_MAP;
                    break;

                // Load the Map.
                case GameState.LOADING_MAP:
                case GameState.ERROR_MAP:
                    this.manageFileLoadingMap();
                    break;

                default:
                    break;
            }

            this.manageLayout();
        }


        /// <summary>
        /// Returns to the Start Menu (when Return button pressed).
        /// </summary>
        private void bReturn_Click(object sender, EventArgs e)
        {
            this.gameState = GameState.START;
            this.manageLayout();
        }


        /// <summary>
        /// Ends the whole program (when End Game button pressed).
        /// </summary>
        private void bEndButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}