﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TetrisMonoGame {
    public class Block {

        protected bool[,] shape;

        Vector2 pos;

        Vector2 blockSize;

        Texture2D sprite; 

        public Block(TetrisGrid grid) {

            blockSize = new Vector2(Constants.DEFAULTBLOCKWIDTH,Constants.DEFAULTBLOCKHEIGHT);
            sprite = grid.emptyCell;
            pos = new Vector2(Constants.GRIDCENTERX, 0);

        }


        public virtual void Gravitate() {

        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch) {
            float x = pos.X;
            float y = pos.Y;

            for (int i = 0; i < shape.GetLength(0); i++) {

                y += i*blockSize.Y;
                for (int j = 0; j < shape.GetLength(1); j++) {

                    x += j*blockSize.X;
                    if (shape[i,j] == true) {

                        spriteBatch.Draw(sprite, new Vector2 (x,y), Color.Red);
                    }
                    x = pos.X;
                }
                y = pos.Y;
            }
        }


        public void Rotate(bool turnRight) {

            bool[,] tmp = shape;

            if (turnRight) {

                for (int i = 0; i < tmp.GetLength(0); i++) {
                    for (int j = 0; j < tmp.GetLength(1); j++) {

                        int length = tmp.GetLength(0);      // Because this is a square any side length will do
                        int xMiddle = length / 2;           // Get the coordinates of the middle block (pivot)
                        int yMiddle = length / 2;

                        int xSquare = xMiddle - i, ySquare = yMiddle - j;

                        shape[ySquare, -xSquare] = tmp[i, j];
                    }
                }
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



        public override void Gravitate() {



            base.Gravitate();
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
