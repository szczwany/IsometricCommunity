using IsometricCommunity.GameObjects;
using IsometricCommunity.GameObjects.Other;
using IsometricCommunity.GameObjects.Structures;
using IsometricCommunity.GUI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace IsometricCommunity.Managers
{
    public class BuildingManager
    {
        private Dictionary<string, Structure> availableStructures;

        private List<CollisionObject> mapObjectsListReference;
        private List<CollisionObject> currentCollisionObjectsListRefence;

        private Structure selectedStructure;
        private bool isBuildingAllowed;
        private bool isOnMap;
        private bool isBuilding;

        private Overlay overlay;

        int index = 0;
        int timer = 0;
        Texture2D testTexture;

        // Test
        private List<GuiIcon> icons;

        public BuildingManager(List<CollisionObject> mapObjectsList, List<CollisionObject> collisions)
        {
            availableStructures = new Dictionary<string, Structure>();

            mapObjectsListReference = mapObjectsList;
            currentCollisionObjectsListRefence = collisions;

            isBuildingAllowed = false;
            isBuilding = false;

            icons = new List<GuiIcon>();

            InitializeAvailableStructures();
        }

        public void Update(GameTime gameTime)
        {
            SetSelectedStructure();

            if (InputManager.GetIsKeyPressedOnce(Keys.B) && selectedStructure != null)
            {
                isBuilding = !isBuilding;
            }

            if (isBuilding)
            {
                selectedStructure.Update(gameTime);
                isBuildingAllowed = selectedStructure.IsBuildingAllowed(currentCollisionObjectsListRefence);
                isOnMap = selectedStructure.IsOnMap();

                CreateOverlay();

                var leftMouseButtonPressed = InputManager.GetIsMouseButtonDown(MouseButton.Left, true);

                if (isBuildingAllowed && isOnMap && leftMouseButtonPressed)
                {
                    BuildSelectedStructure();
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (isBuilding)
            {
                selectedStructure.Draw(spriteBatch);

                if (!selectedStructure.IsBuilt)
                {
                    overlay.Draw(spriteBatch);
                }
            }

            foreach (var icon in icons)
            {
                icon.Draw(spriteBatch);
            }

            if (index < 9)
            {
                timer++;

                if (timer > 5)
                {
                    index++;
                    timer = 0;
                }
            }
            else
            {
                index = 0;
            }

            testTexture = HelperMethods.GetTexture("sawmill1" + index.ToString());
            var testDrawOffset = new Vector2(1, 7);
            var test2 = new Vector2(Globals.TileHeight, 102 - 2 * Globals.TileHeight);
            spriteBatch.Draw(testTexture, HelperMethods.ToIsoScreen(new Point(3, 3)).ToVector2() - testDrawOffset - test2, Color.White);
        }

        private void CreateOverlay()
        {     
            var selectedCollisionRectangle = selectedStructure.GetCollisionRectangle();
            overlay = new Overlay(selectedCollisionRectangle, isBuildingAllowed && isOnMap);
        }

        private void BuildSelectedStructure()
        {
            var builtStructure = selectedStructure.Clone();

            builtStructure.IsBuilt = true;
            mapObjectsListReference.Add(builtStructure);

            mapObjectsListReference.Sort();
        }

        private void InitializeAvailableStructures()
        {
            // Industry
            availableStructures.Add("stoner1", new Structure(Point.Zero, HelperMethods.GetTexture("stoner1"), new Point(2, 2)));
            availableStructures.Add("stoner2", new Structure(Point.Zero, HelperMethods.GetTexture("stoner2"), new Point(2, 2)));
            availableStructures.Add("forge1", new Structure(Point.Zero, HelperMethods.GetTexture("forge1"), new Point(2, 2)));
            availableStructures.Add("woodmill1", new Structure(Point.Zero, HelperMethods.GetTexture("woodmill1"), new Point(2, 2)));
            availableStructures.Add("oilmill1", new Structure(Point.Zero, HelperMethods.GetTexture("oilmill1"), new Point(2, 2)));
            availableStructures.Add("grapemill1", new Structure(Point.Zero, HelperMethods.GetTexture("grapemill1"), new Point(2, 2)));
            availableStructures.Add("mill2", new Structure(Point.Zero, HelperMethods.GetTexture("mill2"), new Point(2, 2)));
            availableStructures.Add("forge2", new Structure(Point.Zero, HelperMethods.GetTexture("forge2"), new Point(2, 2)));

            // Houses
            availableStructures.Add("house11", new Structure(Point.Zero, HelperMethods.GetTexture("house11"), new Point(2, 2)));
            availableStructures.Add("house12", new Structure(Point.Zero, HelperMethods.GetTexture("house12"), new Point(2, 2)));
            availableStructures.Add("house21", new Structure(Point.Zero, HelperMethods.GetTexture("house21"), new Point(2, 2)));
            availableStructures.Add("house22", new Structure(Point.Zero, HelperMethods.GetTexture("house22"), new Point(2, 2)));
            availableStructures.Add("house71", new Structure(Point.Zero, HelperMethods.GetTexture("house71"), new Point(2, 2)));
            availableStructures.Add("house72", new Structure(Point.Zero, HelperMethods.GetTexture("house72"), new Point(2, 2)));

            // Store
            availableStructures.Add("woodstore1", new Structure(Point.Zero, HelperMethods.GetTexture("woodstore1"), new Point(1, 1)));
            availableStructures.Add("woodstore2", new Structure(Point.Zero, HelperMethods.GetTexture("woodstore2"), new Point(1, 1)));
            availableStructures.Add("woodstore3", new Structure(Point.Zero, HelperMethods.GetTexture("woodstore3"), new Point(1, 1)));
            availableStructures.Add("woodstore4", new Structure(Point.Zero, HelperMethods.GetTexture("woodstore4"), new Point(1, 1)));

            // Other
            availableStructures.Add("zeus1", new Structure(Point.Zero, HelperMethods.GetTexture("zeus1"), new Point(1, 1)));

            // Vegetation
            availableStructures.Add("grass1", new Structure(Point.Zero, HelperMethods.GetTexture("grass1"), new Point(1, 1)));
            availableStructures.Add("tree1", new Structure(Point.Zero, HelperMethods.GetTexture("tree1"), new Point(1, 1)));
            availableStructures.Add("tree2", new Structure(Point.Zero, HelperMethods.GetTexture("tree2"), new Point(1, 1)));

            // Farming
            availableStructures.Add("farm1", new Structure(Point.Zero, HelperMethods.GetTexture("farm1"), new Point(3, 3)));

            foreach (var s in availableStructures)
            {
                int i = icons.Count;

                var posY = 10 * (i + 1) + 32 * i;

                icons.Add(new GuiIcon(s.Key, HelperMethods.GetTexture(s.Key), new Vector2(Globals.ScreenWidth - 42, posY)));
            }
        }

        private void SetSelectedStructure()
        {
            foreach (var icon in icons)
            {
                if (icon.GetBounds().Contains(InputManager.GetMousePosition()) && InputManager.GetIsMouseButtonDown(MouseButton.Right, true))
                {
                    selectedStructure = availableStructures[icon.Name];
                }
            }
        }
    }
}
