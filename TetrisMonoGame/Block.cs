using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TetrisMonoGame {
    public class Block {

        protected bool[,] shape;

        public Vector2 Pos { get; set; }

        Vector2 blockSize;

        Texture2D sprite;

        //Int to change the color of the grid
        public int ColorInt { get; protected set; }

        //Color used internally
        public Color Colour { get; set; }

        public Block() {

            blockSize = new Vector2(Constants.DEFAULTBLOCKWIDTH, Constants.DEFAULTBLOCKHEIGHT);
            sprite = TetrisGrid.getSprite();
            this.Pos = new Vector2(Constants.STARTX, Constants.STARTY);
        }


        public bool[,] GetShape() {

            return shape;
        }

        public void SetShape(bool[,] newShape) {

            shape = newShape;
        }

        public void Respawn() {
            this.Pos = new Vector2(Constants.STARTX, Constants.STARTY + 1); // +1 to make sure it spawns inside screen bounds
            Console.WriteLine(this.GetType());

        }


        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch) {
            float x = this.Pos.X;
            float y = this.Pos.Y;

            for (int i = 0; i < shape.GetLength(0); i++) {

                y += i;
                for (int j = 0; j < shape.GetLength(1); j++) {

                    x += j;
                    if (shape[i, j] == true) {

                        spriteBatch.Draw(sprite, new Vector2(x * blockSize.X, y * blockSize.Y), this.Colour);
                        //TetrisGrid.grid[(int)y, (int)x] = 1;
                    }
                    x = this.Pos.X;
                }
                y = this.Pos.Y;
            }
        }

        public void AddToGrid() {

            float x = this.Pos.X;
            float y = this.Pos.Y;

            for (int i = 0; i < shape.GetLength(0); i++) {

                y += i;
                for (int j = 0; j < shape.GetLength(1); j++) {

                    x += j;
                    if (shape[i, j] == true) {

                        TetrisGrid.grid[(int)y - 1, (int)x] = this.ColorInt; //y - 1 to make sure it draws in the grid
                    }

                    x = this.Pos.X;
                }

                y = this.Pos.Y;
            }

        }


        public int CheckColliding() {
            float x = this.Pos.X;
            float y = this.Pos.Y;
            int collide = 0; //1 for collision, 2 for score
 

            for (int i = 0; i < shape.GetLength(0); i++) {

                y += i;
                for (int j = 0; j < shape.GetLength(1); j++) {

                    x += j;
                    if (shape[i, j] == true) {

                        if (x >= TetrisGrid.grid.GetLength(1) || x < 0) {

                            collide = 1;
                            Console.WriteLine("buitenrand");
                        }

                        //this is in a try-catch because bouncing against the side of the grid will cause an error
                        try {

                            if (y >= TetrisGrid.grid.GetLength(0) || (TetrisGrid.grid[(int)y, (int)x] != 0)) {

                                collide = 2;
                                this.AddToGrid();

                            }
                        } catch { }
                    }

                    x = this.Pos.X;
                }

                y = this.Pos.Y;
            }

            return collide;
        }



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


        public static void Move(Block blok, bool moveRight) {

            if (moveRight) {

                try {

                    blok.Pos += new Vector2(1, 0);
                } catch { }

            } else {

                try {

                    blok.Pos -= new Vector2(1, 0);
                } catch { }

            }
        }

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
            shape =new bool[4,4] {   {false, true, false, false},
                                    {false, true, false, false},
                                    {false, true, false, false},
                                    {false, true, false, false}      };
            this.ColorInt = 1; //Red
            this.Colour = Color.Red;

        }


    }

    class BlockJ : Block {

        public BlockJ() : base() {
            shape = new bool [,] {   {false, true, false},
                                    {false, true, false},
                                    {true, true, false}     };
            this.ColorInt = 2; //Orange
            this.Colour = Color.Orange;
        }


        
    }

    class BlockL : Block {

        public BlockL() : base() {
            shape = new bool [,] {   {false, true, false},
                                    {false, true, false},
                                    {false, true, true}     };
            this.ColorInt = 3; //Yellow
            this.Colour = Color.Yellow;

        }
    }

    class BlockS : Block {

        public BlockS() : base() {
            shape = new bool [,] {   {false, false, false},
                                    {false, true, true},
                                    {true, true, false}     };
            this.ColorInt = 4; //Green
            this.Colour = Color.Green;
        }

    }

    class BlockZ : Block {

        public BlockZ() : base() {
            shape = new bool [,] {   {false, false, false},
                                    {true, true, false},
                                    {false, true, true}      };
            this.ColorInt = 5; //Light blue
            this.Colour = Color.LightBlue;
        }

    }

    class BlockO : Block {

        public BlockO() : base() {
            shape = new bool [,] {   {true, true},
                                    {true, true}     };
            this.ColorInt = 6; //Dark blue
            this.Colour = Color.DarkBlue;
        }
    }

    class BlockT : Block {

        public BlockT() : base() {
            shape = new bool [,] {   {false, false, false},
                                    {false, true, false},
                                    {true, true, true}      };
            this.ColorInt = 7; //Purple
            this.Colour = Color.Purple;
        }
    }
}
