using IsometricCommunity.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace IsometricCommunity.GUI
{
    public class GuiIcon
    {
        public string Name;
        private Texture2D guiTexture;
        private Texture2D texture;
        private Vector2 position;

        public GuiIcon(string name, Texture2D texture, Vector2 position)
        {
            Name = name;
            guiTexture = HelperMethods.GetTexture("gui1");
            this.texture = texture;
            this.position = position;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(guiTexture, new Rectangle((int)position.X - Camera.position.X, (int)position.Y - Camera.position.Y, 32, 32), Color.White);
            spriteBatch.Draw(texture, new Rectangle((int)position.X - Camera.position.X, (int)position.Y - Camera.position.Y, 32, 32), Color.White);
        }

        public Rectangle GetBounds()
        {
            return new Rectangle((int)position.X, (int)position.Y, 32, 32);
        }
    }
}
