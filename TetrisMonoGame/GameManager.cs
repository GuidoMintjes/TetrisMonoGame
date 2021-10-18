﻿using System;
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
 
                    blok = new BlockO();
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