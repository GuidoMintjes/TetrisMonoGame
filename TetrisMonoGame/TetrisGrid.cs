using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;


namespace TetrisMonoGame {
    /// <summary>
    /// A class for representing the Tetris playing grid.
    /// </summary>
    public class TetrisGrid {
        /// The sprite of a single cell in the grid.
        protected static Texture2D cell;
        protected static Texture2D whiteCell;

        /// The number of grid elements in the x-direction.
        public static int Width { get { return 10; } }

        /// The number of grid elements in the y-direction.
        public static int Height { get { return 20; } }

        /// The grid array itself
        public static int[,] grid = new int[Height, Width];

        static List<int> drawAnimation = new List<int>();
        float animationCounter = 0;
        static float animationTimer = 0.4f;


        /// <summary>
        /// Creates a new TetrisGrid.
        /// </summary>
        /// <param name="b"></param>
        public TetrisGrid() {

            cell = TetrisGame.ContentManager.Load<Texture2D>("block");
            whiteCell = TetrisGame.ContentManager.Load<Texture2D>("blockWhite");

            for (int i = 0; i < grid.GetLength(0); i++) {
                for (int j = 0; j < grid.GetLength(1); j++) {
                    grid[i, j] = 0;

                }
            }
        }

        public static Texture2D getSprite() {

            return cell;
        }

        public static void setAnimation (int line) {

            drawAnimation.Add(line);
        }

        /// <summary>
        /// Draws the grid on the screen.
        /// </summary>
        /// <param name="gameTime">An object with information about the time that has passed in the game.</param>
        /// <param name="spriteBatch">The SpriteBatch used for drawing sprites and text.</param>
        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch) {

            Vector2 playerOneOffset = Constants.PLAYERONEOFFSET;

            for (int i = 0; i < Width; i++) {

                for (int j = 0; j < Height; j++) {

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

            for (int i = 0; i < drawAnimation.Count; i++) {

                DrawAnimation(spriteBatch, drawAnimation[i]);
                AnimationTimer(gameTime, drawAnimation[i]);
            }
        }

        private void DrawAnimation(SpriteBatch spriteBatch, int line) {

            Vector2 playerOneOffset = Constants.PLAYERONEOFFSET;

            Rectangle box = new Rectangle(Constants.STARTX * cell.Width + (int)playerOneOffset.X, line * cell.Height + (int)playerOneOffset.Y, 2*cell.Width, cell.Height);


            if (animationCounter < animationTimer) {

                if (animationCounter < (animationTimer / 5)) { // 1/5th animation

                }
                else if (animationCounter < (2 * animationTimer / 5)) { // 2/5th animation

                    box.X -= cell.Width;
                    box.Width += 2*cell.Width;
                }
                else if (animationCounter < (3 * animationTimer / 5)) { // 3/5th animation etc.

                    box.X -= 2*cell.Width;
                    box.Width += 4*cell.Width;
                }
                else if (animationCounter < (4 * animationTimer / 5)) {

                    box.X -= 3*cell.Width;
                    box.Width += 6*cell.Width;
                }
                else {

                    box.X -= 4*cell.Width;
                    box.Width += 8*cell.Width;
                }

                spriteBatch.Draw(whiteCell, box, Color.White);
            }
        }

        private void AnimationTimer(GameTime gameTime, int line) {

            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            animationCounter += deltaTime;

            if (animationCounter >= animationTimer) {

                Console.WriteLine("clear op line: " + line);

                for (int i = 0; i < drawAnimation.Count; i++) {

                    MoveLineDown(drawAnimation[i]);
                }

                animationCounter = 0;
                drawAnimation.Clear();
            }
        }

        public static void ClearLine(int line) {

            setAnimation(line);

            Console.WriteLine("LOL WEG IS LIJNTJE OP: " + line);
        }

        // Function that 'moves' the grid one position down
        public static void MoveLineDown(int line) {

            // If not the bottom line
            if (line < Height) {

                // Create a temporary grid, one that ends before the cleared line, one that starts after
                int[,] tempGridStart = new int[Height - (Height - line - 1), Width];

                for (int i = 0; i < Height; i++) {
                    for (int j = 0; j < Width; j++) {

                        try {

                            // Copy the grid start into its temp grid
                            if (i < line) {

                                tempGridStart[i, j] = grid[i, j];
                            }
                        } catch { }
                    }
                }

                for (int i = 0; i < Height; i++) {
                    for (int j = 0; j < Width; j++) {

                        try {

                            //now 'shift' the grid, by copying the temp grid start into the grid, moved one place
                            if (i < line) {

                                grid[i + 1, j] = tempGridStart[i, j];
                            }
                        } catch { }
                    }
                }

                Console.WriteLine(tempGridStart);
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