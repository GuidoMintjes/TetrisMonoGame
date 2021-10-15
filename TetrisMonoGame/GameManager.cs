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

        //timer for gravity
        int timer = 30;

        // Instance of gamestate that says something about the current state our game is in
        public GameState gameState { get; set; }


        public GameManager() {

        }


        public void Gravity(Block blok, float deltaTime) {

            float time = 0;
            time += deltaTime;

            if (time == timer) {
               //blok.Pos = (30,30); //VERANDER DIT LATER

            }
        }



    }




}
