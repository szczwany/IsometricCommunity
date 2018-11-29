using IsometricCommunity.Components;
using IsometricCommunity.GameObjects.Maps;
using IsometricCommunity.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace IsometricCommunity
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class IsometricCommunity : Game
    {
        public static Dictionary<string, Texture2D> TextureDictionary;

        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        private Camera camera;
        private Overworld overworld;

        public IsometricCommunity()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            graphics.PreferredBackBufferWidth = Globals.ScreenWidth;
            graphics.PreferredBackBufferHeight = Globals.ScreenHeight;
            graphics.ApplyChanges();

            IsMouseVisible = true;
            // IsFixedTimeStep = true;
            // TargetElapsedTime = TimeSpan.FromSeconds(1d / 30d);

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            camera = new Camera(graphics.GraphicsDevice.Viewport);

            // TODO: use this.Content to load your game content here
            TextureDictionary = new Dictionary<string, Texture2D>()
            {
                { "tile1", Content.Load<Texture2D>("Terrain/tile1") },
                { "desert1", Content.Load<Texture2D>("Terrain/desert1") },
                { "water1", Content.Load<Texture2D>("Terrain/water1") },

                { "overlay1", Content.Load<Texture2D>("overlay1") },

                { "gui1", Content.Load<Texture2D>("Resources/gui1") },
                { "wood1", Content.Load<Texture2D>("Resources/wood1") },

                { "zeus1", Content.Load<Texture2D>("Structures/Buildings/zeus1") },

                { "forge1", Content.Load<Texture2D>("Structures/Buildings/Industry/forge1") },
                { "forge2", Content.Load<Texture2D>("Structures/Buildings/Industry/forge2") },
                { "woodmill1", Content.Load<Texture2D>("Structures/Buildings/Industry/woodmill1") },
                { "grapemill1", Content.Load<Texture2D>("Structures/Buildings/Industry/grapemill1") },
                { "oilmill1", Content.Load<Texture2D>("Structures/Buildings/Industry/oilmill1") },
                { "mill2", Content.Load<Texture2D>("Structures/Buildings/Industry/mill2") },
                { "stoner1", Content.Load<Texture2D>("Structures/Buildings/Industry/stoner1") },
                { "stoner2", Content.Load<Texture2D>("Structures/Buildings/Industry/stoner2") },

                { "farm1", Content.Load<Texture2D>("Structures/Buildings/Farming/farm1") },

                { "grass1", Content.Load<Texture2D>("Structures/Vegetation/grass1") },
                { "tree1", Content.Load<Texture2D>("Structures/Vegetation/tree1") },
                { "tree2", Content.Load<Texture2D>("Structures/Vegetation/tree2") },

                { "house11", Content.Load<Texture2D>("Structures/Buildings/Houses/house11") },
                { "house12", Content.Load<Texture2D>("Structures/Buildings/Houses/house12") },
                { "house21", Content.Load<Texture2D>("Structures/Buildings/Houses/house21") },
                { "house22", Content.Load<Texture2D>("Structures/Buildings/Houses/house22") },
                { "house71", Content.Load<Texture2D>("Structures/Buildings/Houses/house71") },
                { "house72", Content.Load<Texture2D>("Structures/Buildings/Houses/house72") },

                { "woodstore1", Content.Load<Texture2D>("Structures/Buildings/Store/woodstore1") },
                { "woodstore2", Content.Load<Texture2D>("Structures/Buildings/Store/woodstore2") },
                { "woodstore3", Content.Load<Texture2D>("Structures/Buildings/Store/woodstore3") },
                { "woodstore4", Content.Load<Texture2D>("Structures/Buildings/Store/woodstore4") },

                { "sawmill10", Content.Load<Texture2D>("Structures/Buildings/Industry/sawmill10") },
                { "sawmill11", Content.Load<Texture2D>("Structures/Buildings/Industry/sawmill11") },
                { "sawmill12", Content.Load<Texture2D>("Structures/Buildings/Industry/sawmill12") },
                { "sawmill13", Content.Load<Texture2D>("Structures/Buildings/Industry/sawmill13") },
                { "sawmill14", Content.Load<Texture2D>("Structures/Buildings/Industry/sawmill14") },
                { "sawmill15", Content.Load<Texture2D>("Structures/Buildings/Industry/sawmill15") },
                { "sawmill16", Content.Load<Texture2D>("Structures/Buildings/Industry/sawmill16") },
                { "sawmill17", Content.Load<Texture2D>("Structures/Buildings/Industry/sawmill17") },
                { "sawmill18", Content.Load<Texture2D>("Structures/Buildings/Industry/sawmill18") },
                { "sawmill19", Content.Load<Texture2D>("Structures/Buildings/Industry/sawmill19") },
            };

            overworld = new Overworld();
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            InputManager.Update();

            // TODO: Add your update logic here
            overworld.Update(gameTime);

            camera.Update();

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, transformMatrix: camera.Transform);

            overworld.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
