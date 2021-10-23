using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;



namespace TetrisMonoGame {
    /// <summary>
    /// A class for representing the Tetris playing grid.
    /// </summary>
    public class TetrisGrid {
        /// The sprite of a single cell in the grid.
        protected static Texture2D cell;

        /// The number of grid elements in the x-direction.
        public static int Width { get { return 10; } }

        /// The number of grid elements in the y-direction.
        public static int Height { get { return 20; } }

        /// The grid array itself
        public static int[,] grid = new int[Height, Width];


        /// <summary>
        /// Creates a new TetrisGrid.
        /// </summary>
        /// <param name="b"></param>
        public TetrisGrid() {

            cell = TetrisGame.ContentManager.Load<Texture2D>("block");

            for (int i = 0; i < grid.GetLength(0); i++) {
                for (int j = 0; j < grid.GetLength(1); j++) {
                    grid[i, j] = 0;

                }
            }
        }

        public static Texture2D getSprite() {

            return cell;
        }

        public static void setGrid(int x, int y, int value) {

            grid[x, y] = value;
        }

        /// <summary>
        /// Draws the grid on the screen.
        /// </summary>
        /// <param name="gameTime">An object with information about the time that has passed in the game.</param>
        /// <param name="spriteBatch">The SpriteBatch used for drawing sprites and text.</param>
        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch) {

            Vector2 playerOneOffset = Constants.PLAYERONEOFFSET;

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

    public class PlayerTwoGrid : TetrisGrid {

        int[,] grid2 = new int[Height, Width];

        public PlayerTwoGrid() {

            for (int i = 0; i < grid.GetLength(0); i++) {
                for (int j = 0; j < grid.GetLength(1); j++) {
                    grid2[i, j] = 0;

                }
            }
        }

        public void UpdateNetworkedGrid() {


        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch) {

            Vector2 playerTwoOffset = Constants.PLAYERTWOOFFSET;

            for (int i = 0; i < grid2.GetLength(1); i++) {

                for (int j = 0; j < grid2.GetLength(0); j++) {

                    switch (grid2[j, i]) {

                        case 0:
                            spriteBatch.Draw(cell, new Vector2(i * cell.Height + playerTwoOffset.X, j * cell.Width + playerTwoOffset.Y), Constants.VeryDarkGray);
                            break;
                        case 1:
                            spriteBatch.Draw(cell, new Vector2(i * cell.Height + playerTwoOffset.X, j * cell.Width + playerTwoOffset.Y), Color.Cyan);
                            break;
                        case 2:
                            spriteBatch.Draw(cell, new Vector2(i * cell.Height + playerTwoOffset.X, j * cell.Width + playerTwoOffset.Y), Constants.CobaltBlue);
                            break;
                        case 3:
                            spriteBatch.Draw(cell, new Vector2(i * cell.Height + playerTwoOffset.X, j * cell.Width + playerTwoOffset.Y), Constants.Beer);
                            break;
                        case 4:
                            spriteBatch.Draw(cell, new Vector2(i * cell.Height + playerTwoOffset.X, j * cell.Width + playerTwoOffset.Y), Constants.Apple);
                            break;
                        case 5:
                            spriteBatch.Draw(cell, new Vector2(i * cell.Height + playerTwoOffset.X, j * cell.Width + playerTwoOffset.Y), Constants.RYBRed);
                            break;
                        case 6:
                            spriteBatch.Draw(cell, new Vector2(i * cell.Height + playerTwoOffset.X, j * cell.Width + playerTwoOffset.Y), Constants.CyberYellow);
                            break;
                        case 7:
                            spriteBatch.Draw(cell, new Vector2(i * cell.Height + playerTwoOffset.X, j * cell.Width + playerTwoOffset.Y), Color.Magenta);
                            break;

                    }
                }
            }
        }

    }


}