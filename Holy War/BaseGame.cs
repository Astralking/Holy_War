#region Using Statements
using System;
using System.Collections.Generic;
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
		GraphicsDeviceManager _graphics;
		SpriteBatch _spriteBatch;
		InGameInputHandler _inputHandler = new InGameInputHandler();
		Map _map = new Maps.Map(10, 10);

		public MainGame()
			: base()
		{
			_graphics = new GraphicsDeviceManager(this);
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
			_map.PopulateMap(Content);
			// TODO: IMPLEMENT PROPER CONTENT MANAGER
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
			IInputCommand command = _inputHandler.HandleInput();

			if (command != null)
				command.Execute();

			base.Update(gameTime);
		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.CornflowerBlue);
			const int tileHeightConstant = 64;
			const int tileWidthConstant = 64;

			_spriteBatch.Begin();

			int x = 0, y = 0;

			for (int i = 0; i < _map.Width; i++)
			{
				for (int j = 0; j < _map.Height; j++)
				{
					var tile = _map.MapArray[i, j];

					_spriteBatch.Draw(_map.MapArray[i, j].Texture, new Rectangle(x, y, tile.Width, tile.Height), Color.White);
					y += tileHeightConstant;
				}

				x += tileWidthConstant;
				y = 0;
			}
			_spriteBatch.End();

			// TODO: Add your drawing code here

			base.Draw(gameTime);
		}
	}
}
