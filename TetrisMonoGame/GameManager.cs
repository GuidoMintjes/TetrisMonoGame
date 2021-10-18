using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace TetrisMonoGame {

    public enum GameState {
        Menu,
        Playing,
        Pause,
        End
    }


    class GameManager {


        public int score;

        int screenWidth, screenHeight;

        //gravity
        int timer = 1;
        float counter = 0;
        float weight = 1;

        //randomizer
        static Random rng = new Random();
        int lastBlok = 0;

        //Blocklist
        List<Block> blockList = new List<Block>();

        // Instance of gamestate that says something about the current state our game is in
        public GameState gameState { get; set; }


        public GameManager() {

        }


        public Block GenerateBlock (bool extra) {

            Block blok = new Block();;

            switch (rng.Next(1, 8)) {

                //TODO: getting same block twice should not happen

                case 1:
                    if (lastBlok == 1) blok = GenerateBlock(extra);
                    else {
                        blok = new BlockI();
                        lastBlok = 1;
                    }
                    break;

                case 2:
                    if (lastBlok == 2) blok = GenerateBlock(extra);
                    else {
                        blok = new BlockJ();
                        lastBlok = 2;
                    }
                    break;

                case 3:
                    if (lastBlok == 3) blok = GenerateBlock(extra);
                    else {
                        blok = new BlockL();
                        lastBlok = 3;
                    }
                    break;

                case 4:
                    if (lastBlok == 4) blok = GenerateBlock(extra);
                    else {
                        blok = new BlockO();
                        lastBlok = 4;
                    }
                    break;

                case 5:
                    if (lastBlok == 5) blok = GenerateBlock(extra);
                    else {
                        blok = new BlockO();
                        lastBlok = 5;
                    }
                    break;

                case 6:
                    if (lastBlok == 6) blok = GenerateBlock(extra);
                    else {
                        blok = new BlockS();
                        lastBlok = 5;
                    }
                    break;

                case 7:
                    if (lastBlok == 7) blok = GenerateBlock(extra);
                    else {
                        blok = new BlockT();
                        lastBlok = 7;
                    }
                    break;
            }

            blockList.Add(blok);
            if (extra) blok.Pos = new Vector2(Constants.EXTRAX, Constants.EXTRAY);
            return blok;
        }

        public Block Respawn(Block mainBlok) {

            Block newBlock;

            mainBlok.AddToGrid();
            blockList.Remove(mainBlok);
            newBlock = blockList[0];
            newBlock.Pos = new Vector2(Constants.STARTX, Constants.STARTY);
            return blockList[0];
        }

    }
}