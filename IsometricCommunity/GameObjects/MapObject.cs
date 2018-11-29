using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace IsometricCommunity.GameObjects
{
    public abstract class MapObject
    {
        public Point Position { get; protected set; }
        
        protected Point isometricPosition;
        protected Texture2D texture;
        protected Color tintColor;

        public MapObject(Point position, Texture2D texture)
        {
            Position = position;

            isometricPosition = HelperMethods.ToIsoScreen(Position);

            this.texture = texture;
            tintColor = Color.White;
        }

        public abstract void Update(GameTime gameTime);

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, new Rectangle(isometricPosition.X, isometricPosition.Y, Globals.TileWidth, Globals.TileHeight), tintColor);
        }
    }
}
