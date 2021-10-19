﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;



namespace TetrisMonoGame {
    /// <summary>
    /// A class for representing the Tetris playing grid.
    /// </summary>
    public class TetrisGrid {
        /// The sprite of a single empty cell in the grid.
        static Texture2D emptyCell;

        /// The position at which this TetrisGrid should be drawn.
        Vector2 position;

        /// The number of grid elements in the x-direction.
        public static int Width { get { return 10; } }

        /// The number of grid elements in the y-direction.
        public static int Height { get { return 20; } }

        /// The grid array itself
        public static  int[,] grid = new int[Height, Width];

        private bool drawGridInConsole = true;
 


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

        public static Texture2D getSprite() {

            return emptyCell;
        }

        /// <summary>
        /// Draws the grid on the screen.
        /// </summary>
        /// <param name="gameTime">An object with information about the time that has passed in the game.</param>
        /// <param name="spriteBatch">The SpriteBatch used for drawing sprites and text.</param>
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch) {

            Vector2 pos = new Vector2(0, 0);

            for (int i = 0; i < grid.GetLength(1); i++) {

                for (int j = 0; j < grid.GetLength(0); j++) {

                    switch (grid[j, i]) {

                        case 0:
                            spriteBatch.Draw(emptyCell, new Vector2(i * emptyCell.Height, j * emptyCell.Width), Color.DarkGray);
                            break;
                        case 1:
                            spriteBatch.Draw(emptyCell, new Vector2(i * emptyCell.Height, j * emptyCell.Width), Color.Cyan);
                            break;
                        case 2:
                            spriteBatch.Draw(emptyCell, new Vector2(i * emptyCell.Height, j * emptyCell.Width), Color.Blue);
                            break;
                        case 3:
                            spriteBatch.Draw(emptyCell, new Vector2(i * emptyCell.Height, j * emptyCell.Width), Color.Orange);
                            break;
                        case 4:
                            spriteBatch.Draw(emptyCell, new Vector2(i * emptyCell.Height, j * emptyCell.Width), Color.Lime);
                            break;
                        case 5:
                            spriteBatch.Draw(emptyCell, new Vector2(i * emptyCell.Height, j * emptyCell.Width), Color.Red);
                            break;
                        case 6:
                            spriteBatch.Draw(emptyCell, new Vector2(i * emptyCell.Height, j * emptyCell.Width), Color.Yellow);
                            break;
                        case 7:
                            spriteBatch.Draw(emptyCell, new Vector2(i * emptyCell.Height, j * emptyCell.Width), Color.Magenta);
                            break;

                    }
                }
            }


        }

    }
}
    /// <summary>
    /// Clears the grid.
    /// </summary>
   // public void Clear() {

   // }
