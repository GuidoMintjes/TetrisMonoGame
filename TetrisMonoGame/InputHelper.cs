using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace TetrisMonoGame {
    /// <summary>
    /// A class for helping out with input-related tasks, such as checking if a key or mouse button has been pressed.
    /// </summary>
    class InputHelper {
        // The current and previous mouse/keyboard states.
        MouseState currentMouseState, previousMouseState;
        KeyboardState currentKeyboardState, previousKeyboardState;

        //related to cooldown
        float counter = 0;
        float timer = 0.45f;
        float cooldownTimer = 0.1f;
        float cooldownCounter;
        bool moved = false;

        /// <summary>
        /// Updates the InputHelper object by retrieving the new mouse/keyboard state, and keeping the previous state as a back-up.
        /// </summary>
        /// <param name="gameTime">An object with information about the time that has passed in the game.</param>
        public void Update(GameTime gameTime) {
            // update the mouse and keyboard states
            previousMouseState = currentMouseState;
            previousKeyboardState = currentKeyboardState;
            currentMouseState = Mouse.GetState();
            currentKeyboardState = Keyboard.GetState();

        }

        /// <summary>
        /// Gets the current position of the mouse cursor.
        /// </summary>
        public Vector2 MousePosition {
            get { return new Vector2(currentMouseState.X, currentMouseState.Y); }
        }

        /// <summary>
        /// Returns whether or not the left mouse button has just been pressed.
        /// </summary>
        /// <returns></returns>
        public bool MouseLeftButtonPressed() {
            return currentMouseState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Released;
        }

        /// <summary>
        /// Returns whether or not a given keyboard key has just been pressed.
        /// </summary>
        /// <param name="k">The key to check.</param>
        /// <returns>true if the given key has just been pressed in this frame; false otherwise.</returns>
        public bool KeyPressed(Keys k) {
            return currentKeyboardState.IsKeyDown(k) && previousKeyboardState.IsKeyUp(k);
        }

        /// <summary>
        /// Returns whether or not a given keyboard key is currently being held down.
        /// </summary>
        /// <param name="k">The key to check.</param>
        /// <returns>true if the given key is being held down; false otherwise.</returns>
        public bool KeyDown(Keys k) {
            return currentKeyboardState.IsKeyDown(k);
        }

        //function that handles moving left or right
        public void HandleHold(Block blok, Keys key, GameTime gameTime) {

            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Booleans for going right or left, or down
            bool right = false;
            bool down = false;

            if (key == Keys.Right || key == Keys.D) right = true;

            if (key == Keys.Down || key == Keys.S) down = true;


            // The initial block movement on key press
            if (KeyPressed(key)) {

                if (down) Block.MoveUp(blok, right);
                else Block.Move(blok, right);

                GameManager.UpdateTarget();
            }

            // Start the counter while holding down the key
            counter += deltaTime;

            // If the counter reaches the timer, and there is no movement cooldown, move
            if (counter >= timer && moved == false) {

                if (down) Block.MoveUp(blok, right);
                else Block.Move(blok, right);
                moved = true;

                GameManager.UpdateTarget();
            }

            // Check the cooldown
            Cooldown(gameTime);

            // Reset the counter when you release the key
            if (previousKeyboardState.IsKeyUp(key)) {

                counter = 0;
            }
        }

        // Function to handle the turning process
        public void HandleTurn(Block blok, Keys key) {

            bool rightTurn = (key == Keys.D);
            bool LeftSide = (blok.Pos.X < 5);

            blok.SetShape(Block.Rotate(blok.GetShape(), rightTurn));

            GameManager.UpdateTarget();

            // If colliding, move one block
            if (blok.CheckColliding() != 0) {

                Block.Move(blok, LeftSide);
                // If still colliding, move one more block
                if (blok.CheckColliding() != 0) {

                    Block.Move(blok, LeftSide);
                    // If still colliding now, this position doesn't work. Revert to original position
                    if (blok.CheckColliding() != 0) {

                        blok.SetShape(Block.Rotate(blok.GetShape(), !rightTurn));
                        Block.Move(blok, !LeftSide);
                        Block.Move(blok, !LeftSide);
                    }
                }
            }
        }


        public static void HandleSpace(Block blok, bool target) {

            int toMove = TetrisGrid.Height - (int)blok.Pos.Y;

            for (int i = 0; i < toMove; i++) {

                blok.Pos += new Vector2(0, 1);
                if (blok.CheckColliding() == 2 || blok.CheckColliding() == 3) {

                    if (target) Block.MoveUp(blok, true);
                    break;
                }
            }
        }


        // A cooldown timer for moving the blocks
        public void Cooldown(GameTime gameTime) {

            cooldownCounter += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (cooldownCounter >= cooldownTimer && moved == true) {

                moved = false;
                cooldownCounter = 0;
            }
        }
    }
}