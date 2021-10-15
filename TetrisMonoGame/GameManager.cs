﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

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

        // Instance of gamestate that says something about the current state our game is in
        public GameState gameState { get; set; }


        public GameManager() {

        }


        public void Gravity(Block blok, float deltaTime) {

            counter += deltaTime;
            Console.WriteLine(counter + "dt");
            Console.WriteLine(timer + "timer");

            if (counter >= timer) {
                blok.Pos += new Vector2 (0 , Constants.DEFAULTBLOCKHEIGHT);
                counter = 0;
            
            }
        }
    }
}