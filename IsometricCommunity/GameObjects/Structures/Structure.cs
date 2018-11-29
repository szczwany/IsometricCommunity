using System.Collections.Generic;
using IsometricCommunity.GameObjects.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace IsometricCommunity.GameObjects.Structures
{
    public class Structure : CollisionObject, ICloneable<Structure>
    {
        public bool IsBuilt { get; set; }

        private Point drawOffset;

        public Structure(Point position, Texture2D texture, Point collisionSize)
            : base(position, texture, collisionSize)
        {
            drawOffset = new Point((collisionSize.X - 1) * Globals.TileHeight, texture.Height - collisionSize.Y * Globals.TileHeight);

            IsBuilt = false;

            UpdateIsometricPosition();
        }

        public override void Update(GameTime gameTime)
        {
            if (!IsBuilt)
            {
                var newPosition = HelperMethods.ToGrid(new Vector2(Mouse.GetState().X, Mouse.GetState().Y));

                UpdatePosition(newPosition);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, isometricPosition.ToVector2() - drawOffset.ToVector2(), tintColor);
        }

        public bool IsBuildingAllowed(List<CollisionObject> collisionObjectsList)
        {
            foreach (var other in collisionObjectsList)
            {
                if (IsColliding(other))
                {
                    return false;
                }
            }

            return true;
        }

        public bool IsOnMap()
        {
            int minX = Position.X;
            int minY = Position.Y;

            int maxX = Globals.MapWidth - collisionSize.X;
            int maxY = Globals.MapHeight - collisionSize.Y;

            if (minX < 0 || minY < 0 || minX > maxX || minY > maxY)
            {   
                return false;
            }

            return true;
        }

        public Structure Clone()
        {
            return new Structure(Position, texture, collisionSize);
        }

        public Rectangle GetCollisionRectangle()
        {
            return collisionRectangle;
        }

        private void UpdatePosition(Point newPosition)
        {
            if (Position != newPosition)
            {
                Position = newPosition;

                UpdateIsometricPosition();
                UpdateCollisionRectangle();
            }
        }

        private void UpdateIsometricPosition()
        {
            isometricPosition = HelperMethods.ToIsoScreen(Position);
        }
    }
}
