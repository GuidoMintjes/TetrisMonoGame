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
        GraphicsDeviceManager graphics;
        static GameManager manager;

        // Menu texture
        public static Texture2D tetrisArt;

        // Sound/music 
        public Song bgSong;
        public static SoundEffect soundEffect;

        /// <summary>
        /// A static reference to the ContentManager object, used for loading assets.
        /// </summary>
        public static ContentManager ContentManager { get; private set; }


        /// <summary>
        /// A static reference to the width and height of the screen.
        /// </summary>
        public static Vector2 ScreenSize { get; private set; }

        [STAThread]
        static void Main(string[] args) {
            TetrisGame game = new TetrisGame();
            game.Run();
        }

        public TetrisGame() {
            // initialize the graphics device
            GraphicsDeviceManager graphics = new GraphicsDeviceManager(this);

            // store a static reference to the content manager, so other objects can use it
            ContentManager = Content;

            // set the directory where game assets are located
            Content.RootDirectory = "Content";

            // set the desired window size
            ScreenSize = Constants.SCREENSIZE;
            graphics.PreferredBackBufferWidth = (int)ScreenSize.X;
            graphics.PreferredBackBufferHeight = (int)ScreenSize.Y;

            // create the input helper object
            inputHelper = new InputHelper();

            IsMouseVisible = true;
        }

        protected override void Initialize() {
            base.Initialize();

            manager = new GameManager();
        }

        protected override void LoadContent() {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            tetrisArt = ContentManager.Load<Texture2D>("TetrisArt");
            bgSong = Content.Load<Song>("Audio/Met_OD_nu");
            soundEffect = Content.Load<SoundEffect>("Audio/sound");

            MediaPlayer.IsRepeating = true;

            MediaPlayer.Play(bgSong);

            // create and reset the game world
            gameWorld = new GameWorld();
            gameWorld.Reset();
        }

        protected override void Update(GameTime gameTime) {

            if (GameManager.gameState == GameState.Menu) {

                inputHelper.Update(gameTime);

                if (inputHelper.KeyPressed(Keys.Space)) {

                    GameManager.gameState = GameState.Playing;
                }

                if (inputHelper.KeyPressed(Keys.M)) {

                    GameManager.gameState = GameState.Multiplayer;
                    NetworkManager.Connect();
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

        public static GameManager Manager {

            get { return manager; }
        }
    }
}