#region Using Statements
using System;
using System.Collections.Generic;
using Holy_War.Actors;
using Holy_War.Tiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Holy_War.Maps;
using Holy_War.Input;
#endregion

namespace Holy_War
{
	/// <summary>
	/// This is the main type for your game
	/// </summary>
	public class MainGame : Game
	{
        public static Map CurrentMap = new Maps.Map(10, 10);
	    public static UserActor SelectedUserActor { get; private set; }

	    GraphicsDeviceManager _graphics;
		SpriteBatch _spriteBatch;
	    InGameInputHandler _inputHandler;      

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
			// TODO: Add your initialization logic here

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

            CurrentMap.InitialiseMap(Content);
		}

		/// <summary>
		/// UnloadContent will be called once per game and is the place to unload
		/// all content.
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
            if (SelectedUserActor == null)
                SelectedUserActor = CurrentMap.SelectionBox;

            var command = _inputHandler.HandleInput(gameTime);

			if (command != null)
                command.Execute(SelectedUserActor);

		    CurrentMap.Update();

			base.Update(gameTime);
		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.CornflowerBlue);

			_spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend);

            CurrentMap.Draw(_spriteBatch);
      	
			_spriteBatch.End();

			base.Draw(gameTime);
		}

	    public static void SelectActor(Point location)
	    {
	        var selectedActor = CurrentMap.ActorMapArray[location.X, location.Y] as UserActor;

	        if (selectedActor != null)
                SelectedUserActor = selectedActor;
	    }

        public static void SelectSelectionBox()
        {
            SelectedUserActor = CurrentMap.SelectionBox;
        }
	}
}
