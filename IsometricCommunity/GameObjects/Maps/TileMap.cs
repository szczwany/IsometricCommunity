using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace IsometricCommunity.GameObjects.Maps
{
    public class TileMap
    {
        private int mapWidth;
        private int mapHeight;

        private Tile[,] bottomLayer;

        public TileMap(int mapWidth, int mapHeight)
        {
            this.mapWidth = mapWidth;
            this.mapHeight = mapHeight;

            bottomLayer = new Tile[mapWidth, mapHeight];

            GenerateTileMap();
        }

        public void Update(GameTime gameTime)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                for (int y = 0; y < mapHeight; y++)
                {
                    bottomLayer[x, y].Update(gameTime);
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                for (int y = 0; y < mapHeight; y++)
                {
                    bottomLayer[x, y].Draw(spriteBatch);
                }
            }
        }

        private void GenerateTileMap()
        {
            for (int x = 0; x < mapWidth; x++)
            {
                for (int y = 0; y < mapHeight; y++)
                {
                    var isSolid = Globals.Random.Next(20) == 1;            
                    var texture = HelperMethods.GetTexture("desert1");

                    if (isSolid)
                    {
                        texture = HelperMethods.GetTexture("water1");
                    }

                    bottomLayer[x, y] = new Tile(new Point(x, y), texture, isSolid);
                }
            }
        }

        public List<Tile> GetAllSolidTiles()
        {
            List<Tile> solidTilesList = new List<Tile>();

            for (int x = 0; x < mapWidth; x++)
            {
                for (int y = 0; y < mapHeight; y++)
                {
                    var tile = bottomLayer[x, y];

                    if (tile.IsSolid)
                    {
                        solidTilesList.Add(tile);
                    }
                }
            }

            return solidTilesList;
        }
    }
}
