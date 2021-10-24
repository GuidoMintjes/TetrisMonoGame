using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace TetrisMonoGame {
    public class Block {

        // An array of booleans to store the shape
        protected bool[,] shape;

        // Vectors to store the position and previous position
        public Vector2 Pos { get; set; }
        public Vector2 LastPos { get; set; }

        // A vector to store the size
        Vector2 blockSize;

        protected Texture2D sprite;

        // Int to change the color of the grid
        public int ColorInt { get; protected set; }

        // Color used to draw active blocks
        public Color Colour { get; set; }

        public Block() {

            blockSize = new Vector2(Constants.DEFAULTBLOCKWIDTH, Constants.DEFAULTBLOCKHEIGHT);
            sprite = TetrisGrid.getSprite();
            Pos = new Vector2(Constants.STARTX, Constants.STARTY);
        }


        public bool[,] GetShape() {

            return shape;
        }


        public void SetShape(bool[,] newShape) {

            shape = newShape;
        }


        // Function that returns a list of the Y coordinates the current block is occupying
        public List<int> GetYList() {

            List<int> yList = new List<int>();
            for (int k = (int)Pos.Y - 1; k <= shape.GetLength(0) + Pos.Y - 1; k++) {
                yList.Add((int)k);

            }

            return yList;
        }


        // Draws the correct blocks according to the shape of the blok
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Vector2 playerOffset) {
            float x = this.Pos.X;
            float y = this.Pos.Y;

            for (int i = 0; i < shape.GetLength(0); i++) {

                y += i;
                for (int j = 0; j < shape.GetLength(1); j++) {

                    x += j;
                    if (shape[i, j] == true) {

                        spriteBatch.Draw(sprite, new Vector2(x * blockSize.X + playerOffset.X, y * blockSize.Y + playerOffset.Y), this.Colour);
                    }
                    x = this.Pos.X;
                }
                y = this.Pos.Y;
            }
        }


        // Adds the block to the grid, by using the ColorInt
        public void AddToGrid() {

            float x = this.Pos.X;
            float y = this.Pos.Y;

            for (int i = 0; i < shape.GetLength(0); i++) {

                y += i;
                for (int j = 0; j < shape.GetLength(1); j++) {

                    x += j;
                    if (shape[i, j] == true) {

                        try {

                            TetrisGrid.grid[(int)y - 1, (int)x] = this.ColorInt; // y - 1 to make sure it draws inside the grid
                        } catch { }
                    }

                    x = this.Pos.X;
                }

                y = this.Pos.Y;
            }
        }


        // Checks if the current position is colliding with anything, and returns an int according to the situation
        public int CheckColliding() {
            float x = this.Pos.X;
            float y = this.Pos.Y;
            int collide = 0;

            for (int i = 0; i < shape.GetLength(0); i++) {

                y += i;
                for (int j = 0; j < shape.GetLength(1); j++) {

                    x += j;
                    if (shape[i, j] == true) {

                        //colliside = 1 is a wall collision
                        if (x >= TetrisGrid.Width || x < 0) {

                            collide = 1;
                            return collide;
                        }

                        //this is in a try-catch because bumping into the side of the grid will cause an error
                        //collide = 2 is a block collision
                        try {

                            if ((TetrisGrid.grid[(int)y, (int)x] != 0) || PlayerTwoGrid.grid2[(int)y, (int)x] != 0) {

                                collide = 2;
                                return collide;
                            }
                        } catch { }

                        //collide = 3 is a floor collision
                        if (y >= TetrisGrid.Height) {

                            collide = 3;
                            return collide;
                        }
                    }

                    x = this.Pos.X;
                }

                y = this.Pos.Y;
            }

            return collide;
        }


        // Rotates the block, left or right depending on the value of the bool
        public static bool[,] Rotate(bool[,] shape, bool turnRight) {

            if (turnRight) {

                bool[,] tmp = new bool[shape.GetLength(0), shape.GetLength(1)];

                for (int i = 0; i <= tmp.GetLength(0) - 1; i++) {

                    for (int j = tmp.GetLength(1) - 1; j >= 0; j--) {

                        tmp[i, j] = shape[shape.GetLength(1) - 1 - j, i];
                    }
                }

                return tmp;

            } else {

                bool[,] tmp = new bool[shape.GetLength(0), shape.GetLength(1)];

                for (int i = 0; i <= tmp.GetLength(0) - 1; i++) {

                    for (int j = tmp.GetLength(1) - 1; j >= 0; j--) {

                        tmp[i, j] = shape[j, shape.GetLength(1) - 1 - i];
                    }
                }

                return tmp;
            }
        }


        // Moves the block right/left and updates the target block
        public static void Move(Block blok, bool moveRight) {

            if (moveRight) {

                try {

                    blok.Pos += new Vector2(1, 0);

                    GameManager.UpdateTarget();

                } catch { }

            } else {

                try {

                    blok.Pos -= new Vector2(1, 0);

                    GameManager.UpdateTarget();

                } catch { }
            }
        }


        // Moves the block up/down
        public static void MoveUp(Block blok, bool moveUp) {

            if (moveUp) {

                try {

                    blok.Pos -= new Vector2(0, 1);

                } catch { }

            } else {

                try {

                    blok.Pos += new Vector2(0, 1);

                } catch { }
            }
        }
    }

    class BlockI : Block {

        public BlockI() : base() {
            shape = new bool[4, 4] {   {false, true, false, false},
                                    {false, true, false, false},
                                    {false, true, false, false},
                                    {false, true, false, false}      };
            this.ColorInt = 1; //Cyan
            this.Colour = Color.Cyan;
        }
    }

    class BlockJ : Block {

        public BlockJ() : base() {
            shape = new bool[,] {   {false, true, false},
                                    {false, true, false},
                                    {true, true, false}     };
            this.ColorInt = 2; //Blue
            this.Colour = Constants.CobaltBlue;
        }
    }

    class BlockL : Block {

        public BlockL() : base() {
            shape = new bool[,] {   {false, true, false},
                                    {false, true, false},
                                    {false, true, true}     };
            this.ColorInt = 3; //Orange
            this.Colour = Constants.Beer;

        }
    }

    class BlockS : Block {

        public BlockS() : base() {
            shape = new bool[,] {   {false, false, false},
                                    {false, true, true},
                                    {true, true, false}     };
            this.ColorInt = 4; //Green
            this.Colour = Constants.Apple;
        }
    }

    class BlockZ : Block {

        public BlockZ() : base() {
            shape = new bool[,] {   {false, false, false},
                                    {true, true, false},
                                    {false, true, true}      };
            this.ColorInt = 5; //Red
            this.Colour = Constants.RYBRed;
        }
    }

    class BlockO : Block {

        public BlockO() : base() {
            shape = new bool[,] {   {true, true},
                                    {true, true}     };
            this.ColorInt = 6; //Yellow
            this.Colour = Constants.CyberYellow;
        }
    }

    class BlockT : Block {

        public BlockT() : base() {
            shape = new bool[,] {   {false, false, false},
                                    {false, true, false},
                                    {true, true, true}      };
            this.ColorInt = 7; //Purple
            this.Colour = Color.Magenta;
        }
    }


    // The target block, which changes shape and position depending on the active block
    class TargetBlock : Block {

        public TargetBlock(bool[,] newShape, Vector2 newPos) : base() {

            shape = newShape;
            Pos = newPos;
            sprite = TetrisGame.ContentManager.Load<Texture2D>("blockClearing");

            this.ColorInt = 0; //grid color
            this.Colour = Color.White;
        }
    }
}
