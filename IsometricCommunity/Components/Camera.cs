using IsometricCommunity.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace IsometricCommunity.Components
{
    public class Camera
    {
        public Matrix Transform;
        public static Matrix InvertedMatrix;

        public static Point position;
        private Viewport viewport;
        private MouseState mouseState;
        private KeyboardState keyboardState;

        private float zoom;
        private int scroll;

        public Camera(Viewport viewport)
        {
            position = Point.Zero;
            this.viewport = viewport;
                        
            zoom = 1f;
            scroll = 0;
        }

        public void Update()
        {
            Input();

            zoom = MathHelper.Clamp(zoom, 0.5f, 1.5f);

            Transform = Matrix.CreateTranslation(position.X, position.Y, 0) * Matrix.CreateScale(zoom, zoom, 1);
            InvertedMatrix = Matrix.Invert(Transform);
        }

        protected virtual void Input()
        {
            mouseState = InputManager.GetCurrentMouseState();
            keyboardState = InputManager.GetCurrentKeyboardState();

            if (mouseState.ScrollWheelValue > scroll)
            {
                zoom += 0.5f;
                scroll = mouseState.ScrollWheelValue;
            }
            else if (mouseState.ScrollWheelValue < scroll)
            {
                zoom -= 0.5f;
                scroll = mouseState.ScrollWheelValue;
            }

            if (keyboardState.IsKeyDown(Keys.A))
            {
                position.X += Globals.CameraSpeed;
            }

            if (keyboardState.IsKeyDown(Keys.D))
            {
                position.X -= Globals.CameraSpeed;
            }

            if (keyboardState.IsKeyDown(Keys.W))
            {
                position.Y += Globals.CameraSpeed;
            }

            if (keyboardState.IsKeyDown(Keys.S))
            {
                position.Y -= Globals.CameraSpeed;
            }
        }
    }
}
