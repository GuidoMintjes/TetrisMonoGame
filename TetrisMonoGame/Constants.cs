using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TetrisMonoGame {

    public static class Constants {

        // Related to blocks
        public const int DEFAULTBLOCKWIDTH = 30;
        public const int DEFAULTBLOCKHEIGHT = 30;

        // Startpos
        public const int STARTX = 4;
        public const int STARTY = 0;

        // Extra block position next to grid
        public const int EXTRAX = 10;
        public const int EXTRAY = 1;

        // Score display
        public const int SCOREX = 4;
        public const int SCOREYOFFSET = 2;
        public const int LEVELYOFFSET = 1;

        //Colors
        public static Color CyberYellow = new Color(255, 213, 0);
        public static Color CobaltBlue = new Color(3, 65, 174);
        public static Color Apple = new Color(114, 203, 59);
        public static Color Beer = new Color(255, 151, 28);
        public static Color RYBRed = new Color(255, 50, 19);
        public static Color VeryDarkGray = new Color(30, 30, 30);

        // Screensizes
        public static Vector2 SCREENSIZE = new Vector2 (1280, 720);
        public const int HEIGHTOFFSET = 100;

        // Player one offset
        public static Vector2 PLAYERONEOFFSET = new Vector2(2 * DEFAULTBLOCKWIDTH,
                    (SCREENSIZE.Y - TetrisGrid.grid.GetLength(0) * DEFAULTBLOCKHEIGHT) / 2);

    }
}
