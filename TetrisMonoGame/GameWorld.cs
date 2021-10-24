using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using System;


namespace TetrisMonoGame {
    /// <summary>
    /// A class for representing the game world.
    /// This contains the grid, the falling block, and everything else that the player can see/do.
    /// </summary>
    class GameWorld {

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
        public static Block blok;
        Block extraBlok;
        public static TargetBlock targetBlok;

        // Multiplayer related
        public static Block NetworkedBlock;
        PlayerTwoGrid NetworkedGrid;

        /// <summary>
        /// The input helper
        /// </summary>
        InputHelper inputHelper;

        // The game manager
        GameManager manager;

        // Related to gravity
        static float timer = 1;
        float counter = 0;

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

            NetworkedBlock = manager.GenerateBlock(false);
            NetworkedGrid = new PlayerTwoGrid();

            targetBlok = new TargetBlock(blok.GetShape(), blok.Pos);
            InputHelper.HandleSpace(targetBlok, true);

            font = TetrisGame.ContentManager.Load<SpriteFont>("SpelFont");
        }

        public static void SetTimer(float factor) {

            timer *= factor;
        }

        public void HandleInput(GameTime gameTime, InputHelper inputHelper) {

            if (inputHelper.KeyDown(Keys.Left) && !inputHelper.KeyDown(Keys.Right)) {

                inputHelper.HandleHold(blok, Keys.Left, gameTime);
                if (blok.CheckColliding() == 1 || blok.CheckColliding() == 2) Block.Move(blok, true);
            }

            if (inputHelper.KeyDown(Keys.Right) && !inputHelper.KeyDown(Keys.Left)) {

                inputHelper.HandleHold(blok, Keys.Right, gameTime);
                if (blok.CheckColliding() == 1 || blok.CheckColliding() == 2) Block.Move(blok, false);
            }

            if (inputHelper.KeyPressed(Keys.Up)) {

                inputHelper.HandleTurn(blok, Keys.D);
                inputHelper.HandleTurn(targetBlok, Keys.D);
            }

            if (inputHelper.KeyDown(Keys.Down)) {

                inputHelper.HandleHold(blok, Keys.Down, gameTime);
                RespawnCheck();
            }

            if (inputHelper.KeyPressed(Keys.A)) {

                inputHelper.HandleTurn(blok, Keys.A);
                inputHelper.HandleTurn(targetBlok, Keys.A);
            }

            if (inputHelper.KeyPressed(Keys.D)) {

                inputHelper.HandleTurn(blok, Keys.D);
                inputHelper.HandleTurn(targetBlok, Keys.D);
            }

            if (inputHelper.KeyPressed(Keys.Space)) {

                InputHelper.HandleSpace(blok, false);
                RespawnCheck();
            }
        }

        public void Update(GameTime gameTime) {

            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            Gravity(deltaTime);

            blok.LastPos = blok.Pos;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch) {

            spriteBatch.Begin();

            if (GameManager.gameState == GameState.Menu) {

                spriteBatch.Draw(TetrisGame.tetrisArt, new Vector2(Constants.SCREENSIZE.X / 2 - TetrisGame.tetrisArt.Width / 2,
                    Constants.SCREENSIZE.Y / 2 - TetrisGame.tetrisArt.Height / 2 - Constants.HEIGHTOFFSET), Color.White);
                spriteBatch.DrawString(font, "Use arrow keys to move, up to turn right\npress A for left rotation, D for right rotation\nPress space to place block immediately" +
                    "\nPress M for multiplayer\n\nPress space to start", new Vector2(Constants.SCREENSIZE.X / 2 + Constants.MENUTEXTOFFSET.X,
                    Constants.SCREENSIZE.Y / 2 + Constants.MENUTEXTOFFSET.Y), Color.Black);
            }

            if (GameManager.gameState == GameState.Playing) {

                grid.Draw(gameTime, spriteBatch);
                targetBlok.Draw(gameTime, spriteBatch, Constants.PLAYERONEOFFSET);
                blok.Draw(gameTime, spriteBatch, Constants.PLAYERONEOFFSET);
                extraBlok.Draw(gameTime, spriteBatch, Constants.PLAYERONEOFFSET);
                spriteBatch.DrawString(font, "Next block:", new Vector2(Constants.EXTRAX * Constants.DEFAULTBLOCKWIDTH + Constants.PLAYERONEOFFSET.X,
                    Constants.STARTY + Constants.PLAYERONEOFFSET.Y), Color.Black);
                spriteBatch.DrawString(font, "Score: " + manager.Score.ToString(), new Vector2(Constants.SCOREX * Constants.DEFAULTBLOCKWIDTH + Constants.PLAYERONEOFFSET.X,
                    Constants.PLAYERONEOFFSET.Y - Constants.SCOREYOFFSET * Constants.DEFAULTBLOCKHEIGHT), Color.Black);
                spriteBatch.DrawString(font, "Level: " + manager.Level.ToString(), new Vector2(Constants.SCOREX * Constants.DEFAULTBLOCKWIDTH + Constants.PLAYERONEOFFSET.X,
                    Constants.PLAYERONEOFFSET.Y - Constants.LEVELYOFFSET * Constants.DEFAULTBLOCKHEIGHT), Color.Black);
            }

            if (GameManager.gameState == GameState.End) {

                spriteBatch.DrawString(font, "U ded \n\nScore: " + manager.Score.ToString() + "\nLevel: " + manager.Level.ToString() + "\n\nPress space to restart",
                    new Vector2(Constants.PLAYERONEOFFSET.X + Constants.ENDX * Constants.DEFAULTBLOCKWIDTH,
                    Constants.PLAYERONEOFFSET.Y + Constants.ENDY * Constants.DEFAULTBLOCKHEIGHT), Color.Black);
            }


            if (GameManager.gameState == GameState.Multiplayer) {

                // Player one
                grid.Draw(gameTime, spriteBatch);
                targetBlok.Draw(gameTime, spriteBatch, Constants.PLAYERONEOFFSET);
                blok.Draw(gameTime, spriteBatch, Constants.PLAYERONEOFFSET);
                extraBlok.Draw(gameTime, spriteBatch, Constants.PLAYERONEOFFSET);

                // Player two
                NetworkedGrid.Draw(gameTime, spriteBatch);
                NetworkedBlock.Draw(gameTime, spriteBatch, Constants.PLAYERTWOOFFSET);

                // Player one text
                spriteBatch.DrawString(font, "Next block:", new Vector2(Constants.EXTRAX * Constants.DEFAULTBLOCKWIDTH + Constants.PLAYERONEOFFSET.X,
                    Constants.STARTY + Constants.PLAYERONEOFFSET.Y), Color.Black);
                spriteBatch.DrawString(font, "Score: " + manager.Score.ToString(), new Vector2(Constants.SCOREX * Constants.DEFAULTBLOCKWIDTH + Constants.PLAYERONEOFFSET.X,
                    Constants.PLAYERONEOFFSET.Y - Constants.SCOREYOFFSET * Constants.DEFAULTBLOCKHEIGHT), Color.Black);
                spriteBatch.DrawString(font, "Level: " + manager.Level.ToString(), new Vector2(Constants.SCOREX * Constants.DEFAULTBLOCKWIDTH + Constants.PLAYERONEOFFSET.X,
                    Constants.PLAYERONEOFFSET.Y - Constants.LEVELYOFFSET * Constants.DEFAULTBLOCKHEIGHT), Color.Black);
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

                TetrisGame.soundEffect.Play();

                blok = manager.NextBlock(blok);
                extraBlok = manager.GenerateBlock(true);

                targetBlok.SetShape(blok.GetShape());
                targetBlok.Pos = blok.Pos;
                InputHelper.HandleSpace(targetBlok, true);

                if (GameManager.gameState == GameState.Multiplayer) {

                    //NetworkedGrid.UpdateNetworkedGrid();
                }
            }
        }

        public void Reset() {

            grid = new TetrisGrid();

            manager = new GameManager();

            blok = manager.GenerateBlock(false);

            extraBlok = manager.GenerateBlock(true);

            targetBlok = new TargetBlock(blok.GetShape(), blok.Pos);
            InputHelper.HandleSpace(targetBlok, true);
        }

    }
}