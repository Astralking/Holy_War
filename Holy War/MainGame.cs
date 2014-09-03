#region Using Statements
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Holy_War.Managers;
using Holy_War.Options;
using Holy_War.Worlds;
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
        public static GameOptions GameOptions;

	    private TurnTracker _turnTracker;
	    private GraphicsDeviceManager _graphics;
		private SpriteBatch _spriteBatch;
	    private InGameInputHandler _inputHandler;


		public MainGame()
		{
            GameOptions = new GameOptions(640, 640);
			_graphics = new GraphicsDeviceManager(this)
			{
                PreferredBackBufferHeight = GameOptions.ResolutionY,
                PreferredBackBufferWidth = GameOptions.ResolutionX
			};

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
                "Actors/UserActors/BlueActor",
                "Actors/UserActors/RedActor",
                "Actors/UserActors/BlankActor",
		        "Boxes/SelectionBox",
		        "Boxes/RedHighlightBox",
		        "Boxes/BlueHighlightBox"
		    };

            CurrentWorld = new World(20, 20, textureList);
            

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

            if (command != null && !CurrentWorld.SelectedUserActor.TurnLocked)
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
