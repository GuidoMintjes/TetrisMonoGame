using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;


namespace TetrisMonoGame {
    /// <summary>
    /// A class for representing the game world.
    /// This contains the grid, the falling block, and everything else that the player can see/do.
    /// </summary>
    class GameWorld {

        // The main font of the game.
        SpriteFont font;

        // The main falling block which the player can control
        public static Block mainBlock;
        // The extra block which is drawn next to the grid
        Block extraBlock;
        // The target block which indicated where the main block will land
        public static TargetBlock targetBlok;
        public static TargetBlock targetBlok2;

        // The enemy player's block
        public static Block NetworkedBlock;
        // The enemy player's grid
        PlayerTwoGrid NetworkedGrid;

        // The game manager object
        GameManager manager;

        // Related to the gravity timer
        static float timer = 1;
        float counter = 0;

        // The main grid of the game.
        TetrisGrid grid;


        public GameWorld() {

            grid = new TetrisGrid();

            manager = new GameManager();

            mainBlock = manager.GenerateBlock(false);

            extraBlock = manager.GenerateBlock(true);

            NetworkedBlock = manager.GenerateBlock(false);
            NetworkedGrid = new PlayerTwoGrid();

            targetBlok = new TargetBlock(mainBlock.GetShape(), mainBlock.Pos);
            targetBlok2 = new TargetBlock(NetworkedBlock.GetShape(), NetworkedBlock.Pos);
            InputHelper.HandleSpace(targetBlok, true);

            font = TetrisGame.ContentManager.Load<SpriteFont>("SpelFont");
        }


        // Sets the timer for the gravity tick
        public static void SetTimer(float factor) {

            timer *= factor;
        }


        // Handles all the inputs, and checks for collisions
        public void HandleInput(GameTime gameTime, InputHelper inputHelper) {

            if (GameManager.gameState == GameState.Playing) {
                // If the correct key is pressed, start moving the block
                if (inputHelper.KeyDown(Keys.Left) && !inputHelper.KeyDown(Keys.Right)) {

                    inputHelper.HandleHold(mainBlock, Keys.Left, gameTime);
                    // If colliding, move the block back
                    if (mainBlock.CheckColliding() == 1 || mainBlock.CheckColliding() == 2) Block.Move(mainBlock, true);
                }

                if (inputHelper.KeyDown(Keys.Right) && !inputHelper.KeyDown(Keys.Left)) {

                    inputHelper.HandleHold(mainBlock, Keys.Right, gameTime);
                    if (mainBlock.CheckColliding() == 1 || mainBlock.CheckColliding() == 2) Block.Move(mainBlock, false);
                }

                // Up rotated the block right
                if (inputHelper.KeyPressed(Keys.Up)) {

                    inputHelper.HandleTurn(mainBlock, Keys.D);
                    inputHelper.HandleTurn(targetBlok, Keys.D);
                }

                // Down moves the block down, and checks if the bottom has been reached
                if (inputHelper.KeyDown(Keys.Down)) {

                    inputHelper.HandleHold(mainBlock, Keys.Down, gameTime);
                    RespawnCheck();
                }

                if (inputHelper.KeyPressed(Keys.A)) {

                    inputHelper.HandleTurn(mainBlock, Keys.A);
                    inputHelper.HandleTurn(targetBlok, Keys.A);
                }

                if (inputHelper.KeyPressed(Keys.D)) {

                    inputHelper.HandleTurn(mainBlock, Keys.D);
                    inputHelper.HandleTurn(targetBlok, Keys.D);
                }

                // Space moves the block all the way down instantly
                if (inputHelper.KeyPressed(Keys.Space)) {

                    InputHelper.HandleSpace(mainBlock, false);
                    RespawnCheck();
                }

            }

           if (GameManager.gameState == GameState.Multiplayer) {

                // Player One
                // If the correct key is pressed, start moving the block
                if (inputHelper.KeyDown(Keys.A) && !inputHelper.KeyDown(Keys.D)) {

                    inputHelper.HandleHold(mainBlock, Keys.A, gameTime);
                    // If colliding, move the block back
                    if (mainBlock.CheckColliding() == 1 || mainBlock.CheckColliding() == 2) Block.Move(mainBlock, true);
                }

                if (inputHelper.KeyDown(Keys.D) && !inputHelper.KeyDown(Keys.A)) {

                    inputHelper.HandleHold(mainBlock, Keys.D, gameTime);
                    if (mainBlock.CheckColliding() == 1 || mainBlock.CheckColliding() == 2) Block.Move(mainBlock, false);
                }

                // Up rotated the block right
                if (inputHelper.KeyPressed(Keys.W)) {

                    inputHelper.HandleTurn(mainBlock, Keys.D);
                    inputHelper.HandleTurn(targetBlok, Keys.D);
                }

                // Down moves the block down, and checks if the bottom has been reached
                if (inputHelper.KeyDown(Keys.S)) {

                    inputHelper.HandleHold(mainBlock, Keys.Down, gameTime);
                    RespawnCheck();
                }

                if (inputHelper.KeyPressed(Keys.R)) {

                    inputHelper.HandleTurn(mainBlock, Keys.A);
                    inputHelper.HandleTurn(targetBlok, Keys.A);
                }

                if (inputHelper.KeyPressed(Keys.T)) {

                    inputHelper.HandleTurn(mainBlock, Keys.D);
                    inputHelper.HandleTurn(targetBlok, Keys.D);
                }

                // Space moves the block all the way down instantly
                if (inputHelper.KeyPressed(Keys.Space)) {

                    InputHelper.HandleSpace(mainBlock, false);
                    RespawnCheck();
                }


                // Playet Two

                // If the correct key is pressed, start moving the block
                if (inputHelper.KeyDown(Keys.Left) && !inputHelper.KeyDown(Keys.Right)) {

                    inputHelper.HandleHold(NetworkedBlock, Keys.Left, gameTime);
                    // If colliding, move the block back
                    if (mainBlock.CheckColliding() == 1 || mainBlock.CheckColliding() == 2) Block.Move(mainBlock, true);
                }

                if (inputHelper.KeyDown(Keys.Right) && !inputHelper.KeyDown(Keys.Left)) {

                    inputHelper.HandleHold(NetworkedBlock, Keys.Right, gameTime);
                    if (mainBlock.CheckColliding() == 1 || mainBlock.CheckColliding() == 2) Block.Move(mainBlock, false);
                }

                // Up rotated the block right
                if (inputHelper.KeyPressed(Keys.Up)) {

                    inputHelper.HandleTurn(NetworkedBlock, Keys.D);
                    inputHelper.HandleTurn(targetBlok2, Keys.D);
                }

                // Down moves the block down, and checks if the bottom has been reached
                if (inputHelper.KeyDown(Keys.Down)) {

                    inputHelper.HandleHold(NetworkedBlock, Keys.Down, gameTime);
                    RespawnCheck();
                }

                if (inputHelper.KeyPressed(Keys.O)) {

                    inputHelper.HandleTurn(NetworkedBlock, Keys.A);
                    inputHelper.HandleTurn(targetBlok2, Keys.A);
                }

                if (inputHelper.KeyPressed(Keys.P)) {

                    inputHelper.HandleTurn(NetworkedBlock, Keys.D);
                    inputHelper.HandleTurn(targetBlok2, Keys.D);
                }

                // Space moves the block all the way down instantly
                if (inputHelper.KeyPressed(Keys.L)) {

                    InputHelper.HandleSpace(NetworkedBlock, false);
                    RespawnCheck();
                }


            }


        }


        // Updates the gravity, and sets the blocks' last position
        public void Update(GameTime gameTime) {

            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            Gravity(deltaTime);

            mainBlock.LastPos = mainBlock.Pos;
        }


        // Draw the correct things depending on the gameSta
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch) {

            spriteBatch.Begin();

            if (GameManager.gameState == GameState.Menu) {

                // Draw the splash art and the text that displays the controls
                spriteBatch.Draw(TetrisGame.tetrisArt, new Vector2(Constants.SCREENSIZE.X / 2 - TetrisGame.tetrisArt.Width / 2,
                    Constants.SCREENSIZE.Y / 2 - TetrisGame.tetrisArt.Height / 2 - Constants.ARTYOFFSET), Color.White);
                spriteBatch.DrawString(font, "Use arrow keys to move, up to turn right\npress A for left rotation, D for right rotation\nPress space to place block immediately" +
                    "\nPress M for multiplayer\n\nPress space to start", new Vector2(Constants.SCREENSIZE.X / 2 + Constants.MENUTEXTOFFSET.X,
                    Constants.SCREENSIZE.Y / 2 + Constants.MENUTEXTOFFSET.Y), Color.Black);
            }

            if (GameManager.gameState == GameState.Playing) {

                // Draw all the game objects which should be visible to the player, and some text
                grid.Draw(gameTime, spriteBatch);
                targetBlok.Draw(gameTime, spriteBatch, Constants.PLAYERONEOFFSET);
                mainBlock.Draw(gameTime, spriteBatch, Constants.PLAYERONEOFFSET);
                extraBlock.Draw(gameTime, spriteBatch, Constants.PLAYERONEOFFSET);
                spriteBatch.DrawString(font, "Next block:", new Vector2(Constants.EXTRAX * Constants.DEFAULTBLOCKWIDTH + Constants.PLAYERONEOFFSET.X,
                    Constants.STARTY + Constants.PLAYERONEOFFSET.Y), Color.Black);
                spriteBatch.DrawString(font, "Score: " + manager.Score.ToString(), new Vector2(Constants.SCOREX * Constants.DEFAULTBLOCKWIDTH + Constants.PLAYERONEOFFSET.X,
                    Constants.PLAYERONEOFFSET.Y - Constants.SCOREYOFFSET * Constants.DEFAULTBLOCKHEIGHT), Color.Black);
                spriteBatch.DrawString(font, "Level: " + manager.Level.ToString(), new Vector2(Constants.SCOREX * Constants.DEFAULTBLOCKWIDTH + Constants.PLAYERONEOFFSET.X,
                    Constants.PLAYERONEOFFSET.Y - Constants.LEVELYOFFSET * Constants.DEFAULTBLOCKHEIGHT), Color.Black);
            }

            if (GameManager.gameState == GameState.End) {

                // Draw the gameover message
                spriteBatch.DrawString(font, "U ded \n\nScore: " + manager.Score.ToString() + "\nLevel: " + manager.Level.ToString() + "\n\nPress space to restart",
                    new Vector2(Constants.PLAYERONEOFFSET.X + Constants.ENDX * Constants.DEFAULTBLOCKWIDTH,
                    Constants.PLAYERONEOFFSET.Y + Constants.ENDY * Constants.DEFAULTBLOCKHEIGHT), Color.Black);
            }


            if (GameManager.gameState == GameState.Multiplayer) {

                // Player one
                grid.Draw(gameTime, spriteBatch);
                targetBlok.Draw(gameTime, spriteBatch, Constants.PLAYERONEOFFSET);
                mainBlock.Draw(gameTime, spriteBatch, Constants.PLAYERONEOFFSET);
                extraBlock.Draw(gameTime, spriteBatch, Constants.PLAYERONEOFFSET);

                // Player two
                NetworkedGrid.Draw(gameTime, spriteBatch);
                NetworkedBlock.Draw(gameTime, spriteBatch, Constants.PLAYERTWOOFFSET);
                targetBlok2.Draw(gameTime, spriteBatch, Constants.PLAYERTWOOFFSET);

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


        // Makes the main block fall down depending on the GameWorld timer
        public void Gravity(float deltaTime) {

            counter += deltaTime;

            if (counter >= timer) {

                Block.MoveUp(mainBlock, false);
                Block.MoveUp(NetworkedBlock, false);
                counter = 0;
                RespawnCheck();

                if (GameManager.gameState == GameState.Multiplayer) {

                    // In multiplayer, sends the block info every tick of gravity
                    
                }
            }
        }


        // Checks whether the block has reached the bottom (another block or the grid), and respawns if so
        public void RespawnCheck() {

            if (mainBlock.CheckColliding() == 2 || mainBlock.CheckColliding() == 3) {

                TetrisGame.placeSound.Play();

                // Move on to new blocks
                mainBlock = manager.NextBlock(mainBlock);
                extraBlock = manager.GenerateBlock(true);

                // Updates the target Blocks
                targetBlok.SetShape(mainBlock.GetShape());
                targetBlok.Pos = mainBlock.Pos;
                InputHelper.HandleSpace(targetBlok, true);
                targetBlok2.SetShape(mainBlock.GetShape());
                targetBlok2.Pos = mainBlock.Pos;
                InputHelper.HandleSpace(targetBlok2, true);

                if (GameManager.gameState == GameState.Multiplayer) {

                    //NetworkedGrid.UpdateNetworkedGrid();
                }
            }
        }


        // Reset the Game World Objects
        public void Reset() {

            grid = new TetrisGrid();

            manager = new GameManager();

            mainBlock = manager.GenerateBlock(false);

            extraBlock = manager.GenerateBlock(true);

            targetBlok = new TargetBlock(mainBlock.GetShape(), mainBlock.Pos);
            InputHelper.HandleSpace(targetBlok, true);
        }
    }
}