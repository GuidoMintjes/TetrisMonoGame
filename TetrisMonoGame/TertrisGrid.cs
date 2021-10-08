using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

/// <summary>
/// A class for representing the Tetris playing grid.
/// </summary>
class TetrisGrid {
    /// The sprite of a single empty cell in the grid.
    Texture2D emptyCell;

    /// The position at which this TetrisGrid should be drawn.
    Vector2 position;

    /// The number of grid elements in the x-direction.
    public static int Width { get { return 10; } }

    /// The number of grid elements in the y-direction.
    public static int Height { get { return 20; } }



    public int[,] grid = new int[Height, Width];
    
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

        for (int i = 0; i < grid.GetLength(0); i++) {

            for (int j = 0; j < grid.GetLength(1); j++) {
                Console.Write(grid[i, j]);
            }
            Console.Write("\n");
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


        for (int i = 0; i < grid.GetLength(1); i++) {

            for (int j = 0; j < grid.GetLength(0); j++) {


                spriteBatch.Draw(emptyCell, new Vector2(i * emptyCell.Height, j * emptyCell.Width), Color.White);

            }
        }
        }

    /*
        for (Vector2 pos = Vector2.Zero; i < Width ; pos.Y += emptyCell.Height, i++) {

            for (int t = 0; 
            }
            pos.X = 0;
    }

t < Width; pos.X += emptyCell.Width, t++) {
                spriteBatch.Draw(emptyCell, pos, Color.White);

    */
    }

    /// <summary>
    /// Clears the grid.
    /// </summary>
   // public void Clear() {

   // }
