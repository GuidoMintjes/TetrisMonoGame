using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TetrisMonoGame {

    public static class Constants {

        //related to blocks
        public const int DEFAULTBLOCKWIDTH = 30;
        public const int DEFAULTBLOCKHEIGHT = 30;

        //startpos
        public const int STARTX = 4;
        public const int STARTY = 0;

        //extra block position next to grid
        public const int EXTRAX = 10;
        public const int EXTRAY = 1;

        //Colors
        public static Color CyberYellow = new Color(255, 213, 0);
        public static Color CobaltBlue = new Color(3, 65, 174);
        public static Color Apple = new Color(114, 203, 59);
        public static Color Beer = new Color(255, 151, 28);
        public static Color RYBRed = new Color(255, 50, 19);
        public static Color VeryDarkGray = new Color(30, 30, 30);

    }
}
