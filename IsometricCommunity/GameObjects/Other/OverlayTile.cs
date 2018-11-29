using Microsoft.Xna.Framework;

namespace IsometricCommunity.GameObjects.Other
{
    public class OverlayTile : MapObject
    {
        public OverlayTile(Point position, Color tintColor) 
            : base(position, HelperMethods.GetTexture("overlay1"))
        {
            this.tintColor = tintColor;
        }

        public override void Update(GameTime gameTime) { }
    }
}
