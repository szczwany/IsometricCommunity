using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace IsometricCommunity.Managers
{
    public static class InputManager
    {
        // Store current and previous states for comparison. 
        private static MouseState previousMouseState;
        private static MouseState currentMouseState;

        // Some keyboard states for later use.
        private static KeyboardState previousKeyboardState;
        private static KeyboardState currentKeyboardState;
        
        public static MouseState GetCurrentMouseState()
        {
            return currentMouseState;
        }

        public static KeyboardState GetCurrentKeyboardState()
        {
            return currentKeyboardState;
        }

        // Update the states so that they contain the right data.
        public static void Update()
        {
            previousMouseState = currentMouseState;
            currentMouseState = Mouse.GetState();

            previousKeyboardState = currentKeyboardState;
            currentKeyboardState = Keyboard.GetState();
        }

        public static Point GetMousePosition()
        {
            return new Point(currentMouseState.X, currentMouseState.Y);
        }

        public static Rectangle GetMouseBounds(bool currentState)
        {
            // Return a 1x1 squre representing the mouse click's bounding box.
            if (currentState)
                return new Rectangle(currentMouseState.X, currentMouseState.Y, 1, 1);
            else
                return new Rectangle(previousMouseState.X, previousMouseState.Y, 1, 1);
        }

        public static bool GetIsMouseButtonUp(MouseButton btn, bool currentState)
        {
            if (currentState)
                switch (btn)
                {
                    case MouseButton.Left:
                        return currentMouseState.LeftButton == ButtonState.Released;
                    case MouseButton.Middle:
                        return currentMouseState.MiddleButton == ButtonState.Released;
                    case MouseButton.Right:
                        return currentMouseState.RightButton == ButtonState.Released;
                }
            else
                switch (btn)
                {
                    case MouseButton.Left:
                        return previousMouseState.LeftButton == ButtonState.Released;
                    case MouseButton.Middle:
                        return previousMouseState.MiddleButton == ButtonState.Released;
                    case MouseButton.Right:
                        return previousMouseState.RightButton == ButtonState.Released;
                }

            return false;
        }

        public static bool GetIsMouseButtonDown(MouseButton btn, bool currentState)
        {
            return !GetIsMouseButtonUp(btn, currentState);
        }

        // TODO: Keyboard input stuff goes here
        public static bool GetIsKeyPressedOnce(Keys key)
        {
            if (currentKeyboardState.IsKeyDown(key) && previousKeyboardState.IsKeyUp(key))
            {
                return true;
            }

            return false;
        }
    }

    // A simple enum for any mouse buttons - could just pass mouseState.ButtonState instead 
    public enum MouseButton
    {
        Left,
        Middle,
        Right
    }
}
