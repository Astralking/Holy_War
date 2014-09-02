#region Using Statements
using System;
using System.Collections.Generic;
using Holy_War.Actors;
using Holy_War.Actors.UserActors;
using Holy_War.Managers;
using Holy_War.Tiles;
using Holy_War.Worlds;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Holy_War.Input;
#endregion

namespace Holy_War
{
	/// <summary>
	/// This is the main type for your game
	/// </summary>
	public class MainGame : Game
	{
	    public static World CurrentWorld;
	    private GraphicsDeviceManager _graphics;
		private SpriteBatch _spriteBatch;
	    private InGameInputHandler _inputHandler;      

		public MainGame() : base()
		{
			_graphics = new GraphicsDeviceManager(this) {PreferredBackBufferHeight = 640, PreferredBackBufferWidth = 640};

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
		    this.IsFixedTimeStep = true;

		    var textureList = new List<string>
		    {
                "GrassTile",
                "BlankActor",
		        "Boxes/SelectionBox",
		        "Boxes/RedHighlightBox",
		        "Boxes/BlueHighlightBox"
		    };

            CurrentWorld = new World(13, 13, textureList);

            TextureManager.InitialiseTextures(CurrentWorld, Content);

			base.Initialize();
		}

		/// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>
		protected override void LoadContent()
		{
			// Create a new SpriteBatch, which can be used to draw textures.
			_spriteBatch = new SpriteBatch(GraphicsDevice);
            _inputHandler = new InGameInputHandler(Content);

            CurrentWorld.InitialiseMap();
		}

		/// <summary>
		/// UnloadContent will be called once per game and is the place to unload
		/// all content.
		/// </summary>
		protected override void UnloadContent()
		{
		}

		/// <summary>
		/// Allows the game to run logic such as updating the world,
		/// checking for collisions, gathering input, and playing audio.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Update(GameTime gameTime)
		{
            var command = _inputHandler.HandleInput(gameTime);

			if (command != null)
                command.Execute(CurrentWorld.SelectedUserActor, gameTime);

            CurrentWorld.Update();

			base.Update(gameTime);
		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw(GameTime gameTime)
		{		
			_spriteBatch.Begin(SpriteSortMode.FrontToBack, null, null, null, null, null, CurrentWorld.Camera.ViewMatrix);

            CurrentWorld.Draw(_spriteBatch);
      	    
			_spriteBatch.End();

			base.Draw(gameTime);
		}

	    
	}
}
