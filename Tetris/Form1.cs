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
        public Window()
        {
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

            this.drawShape(this.playGround.actualBlock, this.playGround.actualOffset);
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
                this.drawShape(this.playGround.map, this.mapOffset);
                //currentShape = getRandomShapeWithCenterAligned();
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

            canvasGraphics.FillRectangle(Brushes.LightGray, 0, 0, canvasBitmap.Width, canvasBitmap.Height);

            // load bitmap into picture box
            pGameBoard.Image = canvasBitmap;

        }

        Bitmap workingBitmap;
        Graphics workingGraphics;

        private void drawShape(GameObject gameObject, Coordinates offset)
        {
            workingBitmap = new Bitmap(canvasBitmap);
            workingGraphics = Graphics.FromImage(workingBitmap);

            for (int i = 0; i < gameObject.Height; i++)
            {
                for (int j = 0; j < gameObject.Width; j++)
                {
                    if (gameObject.Bitmap[j, i] == GameObject.FullChar)
                        workingGraphics.FillRectangle(Brushes.Black, (offset.X + i) * dotSize, (offset.Y + j) * dotSize, dotSize, dotSize);
                    //else
                    //    workingGraphics.FillRectangle(Brushes.LightGray, (offset.X + i) * dotSize, (offset.Y + j) * dotSize, dotSize, dotSize);
                }
            }

            pGameBoard.Image = workingBitmap;
        }

    }
}
