using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using System;


namespace TetrisMonoGame {
    class TetrisGame : Game {

        SpriteBatch spriteBatch;
        InputHelper inputHelper;
        GameWorld gameWorld;

        // Menu texture
        public static Texture2D tetrisArt;

        // Sound/music 
        public Song bgSong;
        public static SoundEffect placeSound;
        public static SoundEffect scoreSound;
        public static SoundEffect levelUp;

        // A static reference to the ContentManager object, used for loading assets.
        public static ContentManager ContentManager { get; private set; }

        // A static reference to the width and height of the screen.
        public static Vector2 ScreenSize { get; private set; }


        [STAThread]
        static void Main(string[] args) {
            TetrisGame game = new TetrisGame();
            game.Run();
        }


        public TetrisGame() {

            // Initialize the graphics device
            GraphicsDeviceManager graphics = new GraphicsDeviceManager(this);

            // Store a static reference to the content manager, so other objects can use it
            ContentManager = Content;

            // Set the directory where game assets are located
            Content.RootDirectory = "Content";

            // Set the desired window size
            ScreenSize = Constants.SCREENSIZE;
            graphics.PreferredBackBufferWidth = (int)ScreenSize.X;
            graphics.PreferredBackBufferHeight = (int)ScreenSize.Y;

            // Create the input helper object
            inputHelper = new InputHelper();

            IsMouseVisible = false;
        }

        protected override void Initialize() { 

            base.Initialize();
        }

        protected override void LoadContent() {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // Load the splash art and sounds
            tetrisArt = ContentManager.Load<Texture2D>("TetrisArt");
            bgSong = Content.Load<Song>("Audio/Tetris3");
            placeSound = Content.Load<SoundEffect>("Audio/placeSound");
            scoreSound = Content.Load<SoundEffect>("Audio/score");
            levelUp = Content.Load<SoundEffect>("Audio/levelUp");

            // Set the background music to loop and start it
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(bgSong);

            // Create and reset the game world
            gameWorld = new GameWorld();
            gameWorld.Reset();
        }


        // Update the Gamestate depending on which key is pressed in which Gamestate, or exit the game
        protected override void Update(GameTime gameTime) {

            if (GameManager.gameState == GameState.Menu) {

                inputHelper.Update(gameTime);

                if (inputHelper.KeyPressed(Keys.Space)) {

                    GameManager.gameState = GameState.Playing;
                }

                if (inputHelper.KeyPressed(Keys.M)) {

                    GameManager.gameState = GameState.Multiplayer; 
                }
            }

            if (GameManager.gameState == GameState.Playing) {

                inputHelper.Update(gameTime);

                gameWorld.HandleInput(gameTime, inputHelper);
                gameWorld.Update(gameTime);
            }

            if (GameManager.gameState == GameState.End) {

                inputHelper.Update(gameTime);

                if (inputHelper.KeyPressed(Keys.Space)) {

                    GameManager.gameState = GameState.Playing;
                    gameWorld.Reset();
                }
            }


            if (GameManager.gameState == GameState.Multiplayer) {

                inputHelper.Update(gameTime);

                gameWorld.HandleInput(gameTime, inputHelper);
                gameWorld.Update(gameTime);
            }

            if (inputHelper.KeyPressed(Keys.Escape)) {

                Exit();
            }
        }

        protected override void Draw(GameTime gameTime) {

            GraphicsDevice.Clear(Color.White);
            gameWorld.Draw(gameTime, spriteBatch);
        }
    }
}