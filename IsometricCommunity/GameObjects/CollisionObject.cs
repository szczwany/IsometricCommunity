using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace IsometricCommunity.GameObjects
{
    public abstract class CollisionObject : MapObject, IComparable<CollisionObject>
    {
        protected Rectangle collisionRectangle;
        protected Point collisionSize;

        private int depth => Position.X + collisionSize.X + Position.Y + collisionSize.Y;

        public CollisionObject(Point position, Texture2D texture, Point collisionSize) 
            : base(position, texture)
        {
            collisionRectangle = new Rectangle(Position, collisionSize);
            this.collisionSize = collisionSize;
        }

        public bool IsColliding(CollisionObject other)
        {
            if (collisionRectangle.Intersects(other.collisionRectangle))
            {
                return true;
            }

            return false;
        }

        protected void UpdateCollisionRectangle()
        {
            collisionRectangle = new Rectangle(Position, collisionSize);
        }

        public int CompareTo(CollisionObject other)
        {
            if (depth > other.depth)
            {
                return 1;
            }
            else if (depth < other.depth)
            {
                return -1;
            }

            return 0;
        }
    }
}
