using Microsoft.Xna.Framework;

namespace TetrisMonoGame {

    public static class Constants {

        // Related to blocks
        public const int DEFAULTBLOCKWIDTH = 30;
        public const int DEFAULTBLOCKHEIGHT = 30;

        // Startpos
        public const int STARTX = 4;
        public const int STARTY = 0;

        // Menu text
        public static Point MENUTEXTOFFSET = new Point(-165, 50);

        // Extra block position next to grid
        public const int EXTRAX = 10;
        public const int EXTRAY = 1;

        // Score display and start limit
        public const int SCOREX = 4;
        public const int SCOREYOFFSET = 2;
        public const int LEVELYOFFSET = 1;
        public const int ENDX = 2;
        public const int ENDY = 3;
        public const int SCORESTART = 1000;

        // Colors
        public static Color CyberYellow = new Color(255, 213, 0);
        public static Color CobaltBlue = new Color(3, 65, 174);
        public static Color Apple = new Color(114, 203, 59);
        public static Color Beer = new Color(255, 151, 28);
        public static Color RYBRed = new Color(255, 50, 19);
        public static Color VeryDarkGray = new Color(30, 30, 30);

        // Screensizes
        public static Vector2 SCREENSIZE = new Vector2(1280, 720);
        public const int HEIGHTOFFSET = 100;

        // Player offsets
        public static Vector2 PLAYERONEOFFSET = new Vector2(2 * DEFAULTBLOCKWIDTH,
                    (SCREENSIZE.Y - TetrisGrid.Height * DEFAULTBLOCKHEIGHT) / 2);

        public static Vector2 PLAYERTWOOFFSET = new Vector2(SCREENSIZE.X - 2 * DEFAULTBLOCKWIDTH -
                    TetrisGrid.Width * DEFAULTBLOCKWIDTH,
                    (SCREENSIZE.Y - TetrisGrid.Height * DEFAULTBLOCKHEIGHT) / 2);



        // Network related
        public const string ipAddress = "127.0.0.1";
        public const int port = 8900;
        public const string defaultUsername = "tetrispeler3000";

    }
}
