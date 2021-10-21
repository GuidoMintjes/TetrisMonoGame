using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace TetrisMonoGame {

    public enum GameState {
        Menu,
        Playing,
        End,
        Multiplayer
    }


    class GameManager {

        // Score related
        public int Score { get; private set; }
        public int Level { get; private set; }
        int scoreLimit = Constants.SCORESTART;


        // Randomizer
        static Random rng = new Random();

        // Blocklist
        List<Block> blockList = new List<Block>();

        // Instance of gamestate that says something about the current state our game is in
        public static GameState gameState { get; set; }


        public GameManager() {

            gameState = GameState.Menu;

        }


        public Block GenerateBlock (bool extra) {

            Block blok = new Block();;

            switch (rng.Next(1, 8)) {

                //TODO: getting same block twice should not happen

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

            blockList.Add(blok);
            if (extra) blok.Pos = new Vector2(Constants.EXTRAX, Constants.EXTRAY);
            return blok;
        }

        //'resets' the block, by adding the old block to the grid, removing it from the blockList and returning the next block in the list
        public Block NextBlock(Block mainBlok) {

            Block newBlock;
            List<int> yList = mainBlok.GetYList();
            float scored = 0;

            mainBlok.AddToGrid();
            blockList.Remove(mainBlok);
            newBlock = blockList[0];
            newBlock.Pos = new Vector2(Constants.STARTX, Constants.STARTY);

            for (int i = yList[0] - 1; i <= yList.Last() - 1; i++) {

                Console.WriteLine("rijen gechecked: " + i);

                if (CheckLineClear(i)) {

                    scored ++;
                    TetrisGrid.ClearLine(i);
                }
            }

            Score += (int)(scored * 100);
            if (scored > 1) {
                Score += (int)(0.5 * scored * 100);
            }

            ScoreCheck();

            if (newBlock.CheckColliding() == 2) {

                gameState = GameState.End;
            }

            return newBlock;
        }

        public void ScoreCheck() {

            if (Score >= scoreLimit) {

                Score = 0;
                scoreLimit += 500;
                Level ++;

                GameWorld.SetTimer(0.8f);
            }
        }


        public bool CheckLineClear(int line) {

            if (line == 20) return false;

            for (int j = 0; j <= TetrisGrid.grid.GetLength(1) - 1 ; j++) {

                try {
                    if (TetrisGrid.grid[line, j] == 0) {

                        Console.WriteLine("No clear on: " + line);
                        return false;
                    }
                } catch { break; }
            }

            Console.WriteLine("Check return true");
            return true;
        }


        public static void MoveTarget() {

            Console.WriteLine();
            Console.WriteLine("Moved target!");
            Console.WriteLine();

            GameWorld.targetBlok.SetShape(GameWorld.blok.GetShape());
            GameWorld.targetBlok.Pos = GameWorld.blok.Pos;
            InputHelper.HandleSpace(GameWorld.targetBlok, true);
        }
    }
}