using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;



namespace TetrisMonoGame {
    /// <summary>
    /// A class for representing the Tetris playing grid.
    /// </summary>
    public class TetrisGrid {
        /// The sprite of a single empty cell in the grid.
        public Texture2D emptyCell;

        /// The position at which this TetrisGrid should be drawn.
        Vector2 position;

        /// The number of grid elements in the x-direction.
        public static int Width { get { return 10; } }

        /// The number of grid elements in the y-direction.
        public static int Height { get { return 20; } }

        /// The grid array itself
        public int[,] grid = new int[Height, Width];

        private bool drawGridInConsole = true;
 

        /// blocks enum
        enum Blocks {
            blockI,
            blockJ,
            blockL,
            blockO,
            blockS,
            blockT,
            blockZ
        }


        Blocks blocks;

        /// <summary>
        /// Creates a new TetrisGrid.
        /// </summary>
        /// <param name="b"></param>
        public TetrisGrid() {
            emptyCell = TetrisGame.ContentManager.Load<Texture2D>("block");
            position = Vector2.Zero;

            for (int i = 0; i < grid.GetLength(0); i++) {
                for (int j = 0; j < grid.GetLength(1); j++) {
                    grid[i, j] = 0;
                }
            }



            //Clear();
        }

        /// <summary>
        /// Draws the grid on the screen.
        /// </summary>
        /// <param name="gameTime">An object with information about the time that has passed in the game.</param>
        /// <param name="spriteBatch">The SpriteBatch used for drawing sprites and text.</param>
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch) {
            //int i = 0;

            Vector2 pos = new Vector2(0, 0);

            grid[1, 1] = 1;


            for (int i = 0; i < grid.GetLength(1); i++) {

                for (int j = 0; j < grid.GetLength(0); j++) {

                    switch (grid[j, i]) {

                        case 0:
                            spriteBatch.Draw(emptyCell, new Vector2(i * emptyCell.Height, j * emptyCell.Width), Color.White);
                            break;
                        case 1:
                            spriteBatch.Draw(emptyCell, new Vector2(i * emptyCell.Height, j * emptyCell.Width), Color.Orange);
                            break;
                        case 2:
                            spriteBatch.Draw(emptyCell, new Vector2(i * emptyCell.Height, j * emptyCell.Width), Color.Blue);
                            break;
                    }
                }
            }

            if (drawGridInConsole) {
                //Console.Clear();

                for (int i = 0; i < grid.GetLength(0); i++) {

                    for (int j = 0; j < grid.GetLength(1); j++) {
                        Console.Write(grid[i, j]);
                    }
                    Console.Write("\n");
                }

                drawGridInConsole = false;
            } else {
                drawGridInConsole = true;
            }
        }

    }
}
    /// <summary>
    /// Clears the grid.
    /// </summary>
   // public void Clear() {

   // }
