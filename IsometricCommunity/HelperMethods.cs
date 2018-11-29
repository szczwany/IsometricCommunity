using IsometricCommunity.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace IsometricCommunity
{
    public static class HelperMethods
    {
        internal static Texture2D GetTexture(string name)
        {
            return IsometricCommunity.TextureDictionary[name];
        }

        internal static Point ToIsoScreen(Point position)
        {
            Vector3 positionVector3 = new Vector3(position.X, position.Y, 0);
            float angle = MathHelper.ToRadians(45);
            float scaleFactor = Globals.TileHeight * (float)Math.Sqrt(2);
            Vector3 scale = new Vector3(1 * scaleFactor, 0.5f * scaleFactor, 1);

            Matrix transformMatrix = Matrix.CreateRotationZ(angle) * Matrix.CreateScale(scale);

            var resultMatrix = Matrix.CreateTranslation(positionVector3) * transformMatrix;

            return new Point((int)resultMatrix.M41, (int)resultMatrix.M42);
        }

        internal static Point ToGrid(Vector2 mousePosition)
        {
            Vector3 positionVector3 = new Vector3(mousePosition.X - Globals.TileHeight, mousePosition.Y, 0);
            float angle = MathHelper.ToRadians(45);
            float scaleFactor = (float)Math.Sqrt(2);
            Vector3 scale = new Vector3(1 * scaleFactor, 0.5f * scaleFactor, 1);

            Matrix transformMatrix = Matrix.CreateRotationZ(angle) * Matrix.CreateScale(scale);

            var invertedMatrix = Matrix.Invert(transformMatrix);

            float tileScale = 1f / Globals.TileHeight;

            var resultMatrix = Matrix.CreateTranslation(positionVector3) * Camera.InvertedMatrix * invertedMatrix * Matrix.CreateScale(tileScale);

            return new Point((int)resultMatrix.M41, (int)resultMatrix.M42);
        }
    }
}
