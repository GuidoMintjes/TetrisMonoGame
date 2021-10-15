﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;



namespace TetrisMonoGame {
    /// <summary>
    /// A class for representing the game world.
    /// This contains the grid, the falling block, and everything else that the player can see/do.
    /// </summary>
    class GameWorld {
        /// <summary>
        /// An enum for the different game states that the game can have.
        /// </summary>
        enum GameState {
            Playing,
            GameOver
        }

        /// <summary>
        /// The random-number generator of the game.
        /// </summary>
        public static Random Random { get { return random; } }
        static Random random;

        /// <summary>
        /// The main font of the game.
        /// </summary>
        SpriteFont font;

        //the block
        Block blok;

        /// <summary>
        /// The input helper
        /// </summary>
        InputHelper inputHelper;

    /// <summary>
    /// The current game state.
    /// </summary>
    GameState gameState;

        /// <summary>
        /// The main grid of the game.
        /// </summary>
        TetrisGrid grid;

        public GameWorld() {
            random = new Random();
            inputHelper = new InputHelper();
            gameState = GameState.Playing;

            font = TetrisGame.ContentManager.Load<SpriteFont>("SpelFont");

            grid = new TetrisGrid();

            blok = new BlockI(grid);

        }

        public void HandleInput(GameTime gameTime, InputHelper inputHelper) {


            if (inputHelper.KeyDown(Keys.Left) && !inputHelper.KeyDown(Keys.Right) ){

                inputHelper.MoveHold(blok, Keys.Left, gameTime);
                if (blok.CheckColliding()) Block.Move(blok, true);
                
            }

            if (inputHelper.KeyDown(Keys.Right) && !inputHelper.KeyDown(Keys.Left) ){

                inputHelper.MoveHold(blok, Keys.Right , gameTime);
                if (blok.CheckColliding()) Block.Move(blok, false);
            }

            if (inputHelper.KeyPressed(Keys.Up)) {

                //blok.SetShape(Block.Rotate(blok.GetShape(), true));
                Block.MoveUp(blok, true);

            }

            if (inputHelper.KeyPressed(Keys.Down)) {

                Block.MoveUp(blok, false);
                if(blok.CheckColliding()) Block.MoveUp(blok, true);

            }

            if (inputHelper.KeyPressed(Keys.A)) {

                blok.SetShape(Block.Rotate(blok.GetShape(), false));
                if (blok.CheckColliding()) blok.SetShape(Block.Rotate(blok.GetShape(), true));
            }

            if (inputHelper.KeyPressed(Keys.D)) {

                blok.SetShape(Block.Rotate(blok.GetShape(), true));
                if (blok.CheckColliding()) blok.SetShape(Block.Rotate(blok.GetShape(), false));
            }

        }

        public void Update(GameTime gameTime) {

            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            TetrisGame.Manager.Gravity(blok, deltaTime);
          
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch) {
            spriteBatch.Begin();
            grid.Draw(gameTime, spriteBatch);
            blok.Draw(gameTime, spriteBatch);
            //spriteBatch.DrawString(font, "Hello!", Vector2.Zero, Color.Black);
            spriteBatch.End();
        }

        public void Reset() {
        }

    }
}