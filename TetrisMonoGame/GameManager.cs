using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TetrisMonoGame {

    // Enum of the four different Gamestates
    public enum GameState {
        Menu,
        Playing,
        End,
        Multiplayer
    }

    class GameManager {

        // Score related integers
        public int Score { get; private set; }
        public int Level { get; private set; }
        int scoreLimit = Constants.SCORESTART;


        // Randomizer object
        static Random rng = new Random();

        // Blocklist to store the currently active block and the next block to load
        List<Block> blockList = new List<Block>();

        // Instance of gamestate that says something about the current state our game is in
        public static GameState gameState { get; set; }

        public GameManager() {

            // On game startup, the game will be in the menu state
            gameState = GameState.Menu;
        }
        

        // Generates a random block
        public Block GenerateBlock(bool extra) {

            Block blok = new Block();

            // Cheat! Press I while a new block is being generated to turn it into a I shaped block
            if (Keyboard.GetState().IsKeyDown(Keys.I)){

                Console.WriteLine("Cheat active, generating I block");
                blok = new BlockI();
            } 

            else {
                switch (rng.Next(1, 8)) {

                    case 1:

                        blok = new BlockI();
                        break;

                    case 2:

                        blok = new BlockJ();
                        break;

                    case 3:

                        blok = new BlockL();
                        break;

                    case 4:

                        blok = new BlockO();
                        break;

                    case 5:

                        blok = new BlockZ();
                        break;

                    case 6:

                        blok = new BlockS();
                        break;

                    case 7:

                        blok = new BlockT();
                        break;
                }
            }
            
            blockList.Add(blok);
            if (extra) blok.Pos = new Vector2(Constants.EXTRAX, Constants.EXTRAY);
            return blok;
        }

        // Moves on to the next block in blockList, and starts checking for full lines
        public Block NextBlock(Block mainBlok) {

            Block newBlock;

            mainBlok.AddToGrid();

            blockList.Remove(mainBlok);

            newBlock = blockList[0];

            newBlock.Pos = new Vector2(Constants.STARTX, Constants.STARTY);

            // Gets the Y coordinates of the mainBlok
            List<int> yList = mainBlok.GetYList();

            float scored = 0;

            for (int i = yList[0] - 1 ; i <= yList.Last() - 1; i++) {

                // If there is a full line, increase scored, play the sound and clear the line
                if (CheckLineClear(i)) {

                    scored++;
                    TetrisGame.scoreSound.Play();
                    TetrisGrid.ClearLine(i);

                }
            }

            // If no line is cleared, 0 points will be added
            Score += (int)(scored * 100);

            // If more than one line is cleared at once, extra points will be added
            if (scored > 1) Score += (int)(scored * 50);

            ScoreCheck();

            // If a new block is colliding, this means you lost
            if (newBlock.CheckColliding() == 2) {

                if (gameState == GameState.Multiplayer) {

                    
                }

                gameState = GameState.End;
            }

            return newBlock;
        }


        // Checks whether a level up is reached, if so increases the scoreLimit, plays a sound and speeds up the gravity
        public void ScoreCheck() {

            if (Score >= scoreLimit) {

                Score = 0;
                scoreLimit += 500;
                Level++;

                TetrisGame.levelUp.Play();

                GameWorld.SetTimer(0.8f);
            }
        }


        // Checks if a line is full or not, returns a boolean
        public bool CheckLineClear(int line) {

            if (line == 20) return false;

            for (int j = 0; j <= TetrisGrid.Width - 1; j++) {

                try {
                    if (TetrisGrid.grid[line, j] == 0) {

                        return false;
                    }
                } catch { break; }
            }

            return true;
        }


        // Updates the target block
        public static void UpdateTarget() {

            GameWorld.targetBlok.SetShape(GameWorld.mainBlock.GetShape());

            Vector2 blokPos = GameWorld.mainBlock.Pos;

            GameWorld.targetBlok.Pos = blokPos;

            InputHelper.HandleSpace(GameWorld.targetBlok, true);

            GameWorld.targetBlok2.SetShape(GameWorld.mainBlock.GetShape());

            Vector2 blokPos2 = GameWorld.NetworkedBlock.Pos;

            GameWorld.targetBlok2.Pos = blokPos;

            InputHelper.HandleSpace(GameWorld.targetBlok2, true);
        }
    }
}