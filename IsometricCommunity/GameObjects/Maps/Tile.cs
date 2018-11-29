using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace IsometricCommunity.GameObjects.Maps
{
    public class Tile : CollisionObject
    {
        public bool IsSolid { get; private set; }

        public Tile(Point position, Texture2D texture, bool isSolid)
            : base(position, texture, new Point(1, 1))
        {
                IsSolid = isSolid;
        }

        public override void Update(GameTime gameTime) { }
    }
}
