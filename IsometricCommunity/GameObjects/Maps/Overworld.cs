using System.Collections.Generic;
using System.Linq;
using IsometricCommunity.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace IsometricCommunity.GameObjects.Maps
{
    public class Overworld
    {
        private TileMap terrain;

        private List<CollisionObject> mapObjectsList;
        private List<CollisionObject> currentCollisionObjects;

        private BuildingManager buildingManager;

        public Overworld()
        {
            terrain = new TileMap(Globals.MapWidth, Globals.MapHeight);

            mapObjectsList = new List<CollisionObject>();

            currentCollisionObjects = new List<CollisionObject>();

            buildingManager = new BuildingManager(mapObjectsList, currentCollisionObjects);
        }

        public void Update(GameTime gameTime)
        {
            terrain.Update(gameTime);                      
            
            foreach (var mapObject in mapObjectsList.Where(o => o != null))
            {
                mapObject.Update(gameTime);
            }

            UpdateCollisionObjects();

            buildingManager.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            terrain.Draw(spriteBatch);
            
            foreach (var mapObject in mapObjectsList.Where(o => o != null))
            {
                mapObject.Draw(spriteBatch);
            }

            buildingManager.Draw(spriteBatch);
        }

        private void UpdateCollisionObjects()
        {
            currentCollisionObjects.AddRange(terrain.GetAllSolidTiles());
            currentCollisionObjects.AddRange(mapObjectsList.Where(o => o != null));
        }
    }
}
