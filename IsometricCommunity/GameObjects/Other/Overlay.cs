using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace IsometricCommunity.GameObjects.Other
{
    public class Overlay
    {
        private List<OverlayTile> overlayTilesList;

        public Overlay(Rectangle selectedCollisionRectangle, bool isAllowed)
        {
            overlayTilesList = new List<OverlayTile>();

            GenerateOverlayTiles(selectedCollisionRectangle, isAllowed);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (overlayTilesList.Count > 0)
            {
                foreach (var tile in overlayTilesList)
                {
                    tile.Draw(spriteBatch);
                }
            }
        }

        private void GenerateOverlayTiles(Rectangle selectedCollisionRectangle, bool isAllowed)
        {
            var tintColor = isAllowed ? new Color(Color.Green, 0.005f) : new Color(Color.Red, 0.005f);

            var startX = selectedCollisionRectangle.X;
            var startY = selectedCollisionRectangle.Y;

            var endX = startX + selectedCollisionRectangle.Width;
            var endY = startY + selectedCollisionRectangle.Height;

            for (int x = startX; x < endX; x++)
            {
                for (int y = startY; y < endY; y++)
                {
                    var overlayTile = new OverlayTile(new Point(x, y), tintColor);

                    overlayTilesList.Add(overlayTile);
                }
            }
        }
    }
}
