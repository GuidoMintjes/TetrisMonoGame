using System;
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
        float weight = 1;

        // Instance of gamestate that says something about the current state our game is in
        public GameState gameState { get; set; }


        public GameManager() {

        }


        //Function that makes the block fall down
        public void Gravity(Block blok, float deltaTime) {

            counter += deltaTime;

            if (counter >= timer) {
                blok.Pos += new Vector2 (0 , weight);
                counter = 0;
            
            }
        }
    }
}