using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;


namespace TetrisMonoGame {
    /// <summary>
    /// A class for representing the game world.
    /// This contains the grid, the falling block, and everything else that the player can see/do.
    /// </summary>
    class GameWorld {
        /// <summary>
        /// An enum for the different game states that the game can have.
        /// </summary>

        /// <summary>
        /// The random-number generator of the game.
        /// </summary>
        public static Random Random { get { return random; } }
        static Random random;

        /// <summary>
        /// The main font of the game.
        /// </summary>
        SpriteFont font;

        // The blocks
        Block blok;
        Block extraBlok;
        TargetBlock targetBlok;

        /// <summary>
        /// The input helper
        /// </summary>
        InputHelper inputHelper;

        // The game manager
        GameManager manager;

        // Related to gravity
        int timer = 1;
        float counter = 0;
        float weight = 1;

        /// <summary>
        /// The main grid of the game.
        /// </summary>
        TetrisGrid grid;

        public GameWorld() {

            random = new Random();

            inputHelper = new InputHelper();

            grid = new TetrisGrid();

            manager = new GameManager();

            blok = manager.GenerateBlock(false);

            extraBlok = manager.GenerateBlock(true);

            targetBlok = new TargetBlock(blok.GetShape(), blok.Pos);
            inputHelper.HandleSpace(targetBlok, true);

            font = TetrisGame.ContentManager.Load<SpriteFont>("SpelFont");
        }

        public void HandleInput(GameTime gameTime, InputHelper inputHelper) {

            bool changed = false;

            if (inputHelper.KeyDown(Keys.Left) && !inputHelper.KeyDown(Keys.Right) ){

                inputHelper.HandleHold(blok, Keys.Left, gameTime);
                if (blok.CheckColliding() == 1 || blok.CheckColliding() == 2) Block.Move(blok, true);

                changed = true;
            }

            if (inputHelper.KeyDown(Keys.Right) && !inputHelper.KeyDown(Keys.Left) ){

                inputHelper.HandleHold(blok, Keys.Right , gameTime);
                if (blok.CheckColliding() == 1 || blok.CheckColliding() == 2) Block.Move(blok, false);

                changed = true;
            }

            if (inputHelper.KeyPressed(Keys.Up)) {

                inputHelper.HandleTurn(blok, Keys.D);

                changed = true;
                // Block.MoveUp(blok, true);
            }

            if (inputHelper.KeyDown(Keys.Down)) {

                inputHelper.HandleHold(blok, Keys.Down, gameTime);
                RespawnCheck();
            }

            if (inputHelper.KeyPressed(Keys.A)) {

                inputHelper.HandleTurn(blok, Keys.A);

                changed = true;
            }

            if (inputHelper.KeyPressed(Keys.D)) {

                inputHelper.HandleTurn(blok, Keys.D);

                changed = true;
            }

            if (inputHelper.KeyPressed(Keys.Space)) {

                inputHelper.HandleSpace(blok, false);
                RespawnCheck();
            }

            if (changed) {

                targetBlok.SetShape(blok.GetShape());
                targetBlok.Pos = blok.Pos;
                inputHelper.HandleSpace(targetBlok, true);
            }
        }

        public void Update(GameTime gameTime) {

            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            Gravity(deltaTime);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch) {

            spriteBatch.Begin();

            if (GameManager.gameState == GameState.Menu) {

                spriteBatch.Draw(TetrisGame.tetrisArt, new Vector2(Constants.SCREENSIZE.X / 2 - TetrisGame.tetrisArt.Width / 2, 
                    Constants.SCREENSIZE.Y / 2 - TetrisGame.tetrisArt.Height / 2 - Constants.HEIGHTOFFSET), Color.White);
            }

            if (GameManager.gameState == GameState.Playing) {

                grid.Draw(gameTime, spriteBatch, Constants.PLAYERONEOFFSET);
                targetBlok.Draw(gameTime, spriteBatch, Constants.PLAYERONEOFFSET);
                blok.Draw(gameTime, spriteBatch, Constants.PLAYERONEOFFSET);
                extraBlok.Draw(gameTime, spriteBatch, Constants.PLAYERONEOFFSET); 
                spriteBatch.DrawString(font, "Next block:", new Vector2(Constants.EXTRAX * Constants.DEFAULTBLOCKWIDTH + Constants.PLAYERONEOFFSET.X,
                    Constants.STARTY + Constants.PLAYERONEOFFSET.Y), Color.Black);
                spriteBatch.DrawString(font, manager.Score.ToString(), new Vector2(Constants.DEFAULTBLOCKWIDTH * 5 + Constants.PLAYERONEOFFSET.X, Constants.PLAYERONEOFFSET.Y - Constants.DEFAULTBLOCKHEIGHT), Color.Black);
            }
            spriteBatch.End();
        }

        //function to make the blocks fall down every second
        public void Gravity(float deltaTime) {

            counter += deltaTime;

            if (counter >= timer) {

                Block.MoveUp(blok, false);
                counter = 0;
                //Console.WriteLine("gravity");
                RespawnCheck();
            }
        }
    

        //function that checks if the block is at the bottom and respawns if so
        public void RespawnCheck() {
            if (blok.CheckColliding() == 2 || blok.CheckColliding() == 3) {

                blok = manager.NextBlock(blok);
                extraBlok = manager.GenerateBlock(true);

                targetBlok.SetShape(blok.GetShape());
                targetBlok.Pos = blok.Pos;
                inputHelper.HandleSpace(targetBlok, true);
            }
        }

        public void Reset() {

        }

    }
}