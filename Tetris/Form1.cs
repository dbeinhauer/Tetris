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
    public partial class Window : Form
    {
        Bitmap canvasBitmap;
        Graphics canvasGraphics;
        int canvasWidth = 16;
        int canvasHeight = 20;
        //int[,] canvsDotArray;
        int dotSize = 20;

        int mapWidth = 16;
        int mapHeight = 20;
        PlayGround playGround;
        Coordinates mapOffset;


        Timer timer = new Timer();
        Timer timerHandling = new Timer();
        public Window()
        {
            this.KeyPreview = true;
            InitializeComponent();

            this.mapOffset = new Coordinates(0, 0, this.mapWidth);

            Reader reader = new Reader("GameBlocks/Default_objects.txt");


            this.playGround = new PlayGround(mapWidth, mapHeight);
            this.playGround.LoadGameObjects(reader);
            this.playGround.GenerateNextBlock();
            this.playGround.CheckGameOver();

            this.loadcanvas();
            timer.Tick += Timer_Tick;
            timer.Interval = 500;
            timer.Start();

            timerHandling.Tick += TimerHandling_Tick;

            this.KeyDown += Window_KeyDown;

            //this.drawShape(this.playGround.actualBlock, this.playGround.actualOffset);
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            //var isMoveSuccess = this.playGround.MoveDown();

            // if shape reached bottom or touched any other shapes
            if (!this.playGround.MoveDown())
            {
                // copy working image into canvas image
                this.canvasBitmap = new Bitmap(this.workingBitmap);

                //updateCanvasDotArrayWithCurrentShape();
                this.playGround.map.AddObject(this.playGround.actualBlock, this.playGround.actualOffset);
                

                // get next shape
                this.playGround.GenerateNextBlock();
                this.playGround.CheckGameOver();

                //this.playGround.PrintMap();

                //this.drawShape(this.playGround.map, this.mapOffset);
                //currentShape = getRandomShapeWithCenterAligned();
            }
            this.drawShape(this.playGround.actualBlock, this.playGround.actualOffset);
        }

        private void TimerHandling_Tick(object sender, EventArgs e)

        {

        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {

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
                    this.playGround.MoveRight();
                    break;

                // rotate the shape clockwise
                case Keys.Up:
                    this.playGround.Rotate();
                    break;

                default:
                    return;
            }

            //var isMoveSuccess = moveShapeIfPossible(horizontalMove, verticalMove);

            //// if the player was trying to rotate the shape, but
            //// that move was not possible - rollback the shape
            //if (!isMoveSuccess && e.KeyCode == Keys.Up)
            //    currentShape.rollback();
        }


        private void loadcanvas()
        {
            // resize the picture box based on the dotsize and canvas size
            pGameBoard.Width = canvasWidth * dotSize;
            pGameBoard.Height = canvasHeight * dotSize;

            // create bitmap with picture box's size
            canvasBitmap = new Bitmap(pGameBoard.Width, pGameBoard.Height);

            canvasGraphics = Graphics.FromImage(canvasBitmap);

            canvasGraphics.FillRectangle(Brushes.LightGray, 0, 0, canvasBitmap.Width, canvasBitmap.Height);

            // load bitmap into picture box
            pGameBoard.Image = canvasBitmap;

        }

        Bitmap workingBitmap;
        Graphics workingGraphics;

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
                    //else
                    //    workingGraphics.FillRectangle(Brushes.LightGray, (offset.X + i) * dotSize, (offset.Y + j) * dotSize, dotSize, dotSize);
                }
            }

            pGameBoard.Image = workingBitmap;
        }

        //private void drawMap()

    }
}
