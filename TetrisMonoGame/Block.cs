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

        public Block(TetrisGrid grid) {

            blockSize = new Vector2(Constants.DEFAULTBLOCKWIDTH, Constants.DEFAULTBLOCKHEIGHT);
            sprite = grid.emptyCell;
            this.Pos = new Vector2(Constants.STARTX, Constants.STARTY);
        }


        public bool[,] GetShape() {

            return shape;
        }

        public void SetShape(bool[,] newShape) {

            shape = newShape;
        }


        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch) {
            float x = this.Pos.X;
            float y = this.Pos.Y;

            for (int i = 0; i < shape.GetLength(0); i++) {

                y += i;
                for (int j = 0; j < shape.GetLength(1); j++) {

                    x += j;
                    if (shape[i, j] == true) {

                        spriteBatch.Draw(sprite, new Vector2(x * blockSize.X, y * blockSize.Y), Color.Red);
                        //TetrisGrid.grid[(int)y, (int)x] = 1;
                    }
                    x = this.Pos.X;
                }
                y = this.Pos.Y;
            }
        }

        public bool CheckColliding() {
            float x = this.Pos.X;
            float y = this.Pos.Y;
            bool collide = false;

            for (int i = 0; i < shape.GetLength(0); i++) {

                y += i;
                for (int j = 0; j < shape.GetLength(1); j++) {

                    x += j;
                    if (shape[i, j] == true) {

                        if (y >= TetrisGrid.grid.GetLength(0) || x >= TetrisGrid.grid.GetLength(1) || x < 0) {
                            collide = true;
                            Console.WriteLine("buiten");
                        }
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

            Console.WriteLine("move");

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

        public BlockI(TetrisGrid grid) : base(grid) {
        shape =new bool[4,4] {   {false, true, false, false},
                                {false, true, false, false},
                                {false, true, false, false},
                                {false, true, false, false}      };

        }


    }

    class BlockJ : Block {

        public BlockJ(TetrisGrid grid) : base(grid) {
            shape = new bool [,] {   {false, true, false},
                                    {false, true, false},
                                    {true, true, false}     };
        }


        
    }

    class BlockL : Block {

        public BlockL(TetrisGrid grid) : base(grid) {
            shape = new bool [,] {   {false, true, false},
                                    {false, true, false},
                                    {false, true, true}     };

        }
    }

    class BlockS : Block {

        public BlockS(TetrisGrid grid) : base(grid) {
            shape = new bool [,] {   {false, false, false},
                                    {false, true, true},
                                    {true, true, false}     };
        }

    }

    class BlockZ : Block {

        public BlockZ(TetrisGrid grid) : base(grid) {
            shape = new bool [,] {   {false, false, false},
                                    {true, true, false},
                                    {false, true, true}      };
        }

    }

    class BlockO : Block {

        public BlockO(TetrisGrid grid) : base(grid) {
            shape = new bool [,] {   {true, true},
                                    {true, true}     };
        }
    }

    class BlockT : Block {

        public BlockT(TetrisGrid grid) : base(grid) {
            shape = new bool [,] {   {false, false, false},
                                    {false, true, false},
                                    {true, true, true}      };
        }
    }
}
