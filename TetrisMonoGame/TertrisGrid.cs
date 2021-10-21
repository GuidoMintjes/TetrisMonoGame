using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;



namespace TetrisMonoGame {
    /// <summary>
    /// A class for representing the Tetris playing grid.
    /// </summary>
    public class TetrisGrid {
        /// The sprite of a single cell in the grid.
        static Texture2D cell;

        // The sprite a a cell that's being targeted
        static Texture2D targetCell;

        /// The position at which this TetrisGrid should be drawn.
        Vector2 position;

        /// The number of grid elements in the x-direction.
        public static int Width { get { return 10; } }

        /// The number of grid elements in the y-direction.
        public static int Height { get { return 20; } }

        /// The grid array itself
        public static int[,] grid = new int[Height, Width];

        private bool drawGridInConsole = true;
 


        /// <summary>
        /// Creates a new TetrisGrid.
        /// </summary>
        /// <param name="b"></param>
        public TetrisGrid() {

            cell = TetrisGame.ContentManager.Load<Texture2D>("block");

            position = Vector2.Zero;

            for (int i = 0; i < grid.GetLength(0); i++) {
                for (int j = 0; j < grid.GetLength(1); j++) {
                    grid[i, j] = 0;

                }
            }

            //Clear();
        }

        public static Texture2D getSprite() {

            return cell;
        }

        /// <summary>
        /// Draws the grid on the screen.
        /// </summary>
        /// <param name="gameTime">An object with information about the time that has passed in the game.</param>
        /// <param name="spriteBatch">The SpriteBatch used for drawing sprites and text.</param>
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Vector2 playerOneOffset) {

            for (int i = 0; i < grid.GetLength(1); i++) {

                for (int j = 0; j < grid.GetLength(0); j++) {

                    switch (grid[j, i]) {

                        case 0:
                            spriteBatch.Draw(cell, new Vector2(i * cell.Height + playerOneOffset.X, j * cell.Width + playerOneOffset.Y), Constants.VeryDarkGray);
                            break;
                        case 1:
                            spriteBatch.Draw(cell, new Vector2(i * cell.Height + playerOneOffset.X, j * cell.Width + playerOneOffset.Y), Color.Cyan);
                            break;
                        case 2:
                            spriteBatch.Draw(cell, new Vector2(i * cell.Height + playerOneOffset.X, j * cell.Width + playerOneOffset.Y), Constants.CobaltBlue);
                            break;
                        case 3:
                            spriteBatch.Draw(cell, new Vector2(i * cell.Height + playerOneOffset.X, j * cell.Width + playerOneOffset.Y), Constants.Beer);
                            break;
                        case 4:
                            spriteBatch.Draw(cell, new Vector2(i * cell.Height + playerOneOffset.X, j * cell.Width + playerOneOffset.Y), Constants.Apple);
                            break;
                        case 5:
                            spriteBatch.Draw(cell, new Vector2(i * cell.Height + playerOneOffset.X, j * cell.Width + playerOneOffset.Y), Constants.RYBRed);
                            break;
                        case 6:
                            spriteBatch.Draw(cell, new Vector2(i * cell.Height + playerOneOffset.X, j * cell.Width + playerOneOffset.Y), Constants.CyberYellow);
                            break;
                        case 7:
                            spriteBatch.Draw(cell, new Vector2(i * cell.Height + playerOneOffset.X, j * cell.Width + playerOneOffset.Y), Color.Magenta);
                            break;

                    }
                }
            }
        }


        public static void ClearLine(int line) {

            try {
                for (int i = 0; i < Width; i++) {

                    grid[line, i] = 0;
                }
            } catch { }

            MoveLineDown(line);

            Console.WriteLine("LOL WEG IS LIJNTJE OP: " + line);

        }


        public static void MoveLineDown(int line) {

            if (line < Height) { 

                int[,] tempGridStart = new int[Height - (Height - line - 1), Width];
                int[,] tempGridEnd = new int[Height - line, Width];

                for (int i = 0; i < Height; i++) {

                    for (int j = 0; j < Width; j++) {

                        try {

                            if (i < line) {

                                tempGridStart[i, j] = grid[i, j];
                            } else if (i > line) {

                                tempGridEnd[i, j] = grid[i, j];
                            }
                        } catch { }
                    }
                }

                for (int i = 0; i < Height; i++) {

                    for (int j = 0; j < Width; j++) {

                        try {

                            if (i < line) {

                                grid[i + 1, j] = tempGridStart[i, j];
                            } else if (i >= line) {

                                grid[i, j] = tempGridEnd[i, j];
                            }
                        } catch { }
                    }
                }

                Console.WriteLine(tempGridStart);
                Console.WriteLine(tempGridEnd);
            }
        }
    }
}
    /// <summary>
    /// Clears the grid.
    /// </summary>
   // public void Clear() {

   // }
