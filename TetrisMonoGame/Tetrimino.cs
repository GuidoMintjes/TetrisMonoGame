using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TetrisMonoGame {
    public class Tetrimino {

        bool[,] shape = new bool[4, 4];

        Vector2 Size { get; set };

        public Tetrimino(TetrisGrid grid) {

            size = grid.GetSize();

        }


        public virtual void Gravitate() {

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


    class BlockI : Tetrimino {

        public BlockI(TetrisGrid grid) : base(grid) {

        }

        bool[,] shape = {   {false, true, false, false},
                            {false, true, false, false},
                            {false, true, false, false},
                            {false, true, false, false}      };


        public override void Gravitate() {



            base.Gravitate();
        }
    }

    class BlockJ : Tetrimino {

        public BlockJ(TetrisGrid grid) : base(grid) {

        }

        bool[,] shape = {   {false, true, false},
                            {false, true, false},
                            {true, true, false}      };

        
    }

    class BlockL : Tetrimino {

        public BlockL(TetrisGrid grid) : base(grid) {

        }

        bool[,] shape = {   {false, true, false},
                            {false, true, false},
                            {false, true, true}      };

    }

    class BlockS : Tetrimino {

        public BlockS(TetrisGrid grid) : base(grid) {

        }

        bool[,] shape = {   {false, false, false},
                            {false, true, true},
                            {true, true, false}     };

    }

    class BlockZ : Tetrimino {

        public BlockZ(TetrisGrid grid) : base(grid) {

        }

        bool[,] shape = {   {false, false, false},
                            {true, true, false},
                            {false, true, true}      };


    }

    class BlockO : Tetrimino {

        public BlockO(TetrisGrid grid) : base(grid) {

        }

        bool[,] shape = {   {true, true},
                            {true, true}     };


    }

    class BlockT : Tetrimino {

        public BlockT(TetrisGrid grid) : base(grid) {

        }

        bool[,] shape = {   {false, false, false},
                            {false, true, false},
                            {true, true, true}      };
    }
}
