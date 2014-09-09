#region Using Statements
using System.Collections.Generic;
using System.Windows.Forms;
using Holy_War.Overlay.Overlays;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Holy_War.Managers;
using Holy_War.Options;
using Holy_War.Worlds;
using Holy_War.Input;
using Screen = Holy_War.Screens.Screen;

#endregion

namespace Holy_War
{
	/// <summary>
	/// This is the main type for your game
	/// </summary>
	public class GameScreen : Screen
	{
	    public static World CurrentWorld;
        public static Camera Camera { get; private set; }

	    private GraphicsDeviceManager _graphics;
	    private ContentManager _contentManager;

        public GameScreen(ContentManager contentManager)
	    {
	        _contentManager = contentManager;
	    }

	    /// <summary>
		/// Allows the game to perform any initialization it needs to before starting to run.
		/// This is where it can query for any required services and load any non-graphic
		/// related content.  Calling base.Initialize will enumerate through any components
		/// and initialize them as well.
		/// </summary>
		public override void Initialize()
		{
			base.Initialize();
		}

		/// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>
		public override void LoadContent()
        {
            Camera = new Camera(1f);

            var textureList = new List<string>
		    {
                "Terrain/GrassTile",
                "Actors/UserActors/BlueActor",
                "Actors/UserActors/RedActor",
                "Actors/UserActors/BlankActor",
                "Actors/UserActors/ArcherRed",
                "Actors/UserActors/WarriorRed",
                "Actors/UserActors/SorcererRed",
                "Actors/UserActors/MonkRed",
                "Actors/UserActors/AssassinRed",
                "Actors/UserActors/ArcherBlue",
                "Actors/UserActors/WarriorBlue",
                "Actors/UserActors/SorcererBlue",
                "Actors/UserActors/MonkBlue",
                "Actors/UserActors/AssassinBlue",
		        "Boxes/SelectionBox",
		        "Boxes/TargetBox",
		        "Boxes/RedHighlightBox",
		        "Boxes/BlueHighlightBox",
                "Menus/ContextMenu/ContextMenuBottom",
                "Menus/ContextMenu/ContextMenuSection",
                "Menus/ContextMenu/ContextMenuTop",
                "Menus/SelectionArrow"
		    };

            var fontList = new List<string>
		    {
                "Fonts/HeadingFont",
                "Fonts/ContextMenuFont"
		    };

            CurrentWorld = new World(20, 20, textureList);

            SpriteManager.InitialiseTextures(CurrentWorld, _contentManager);
            SpriteManager.InitialiseFonts(fontList, _contentManager);
            
            CurrentWorld.InitialiseMap();
		}

		/// <summary>
		/// UnloadContent will be called once per game and is the place to unload
		/// all content.
		/// </summary>
		public override void UnloadContent()
		{
		}

		/// <summary>
		/// Allows the game to run logic such as updating the world,
		/// checking for collisions, gathering input, and playing audio.
		/// </summary>
        public override void Update(GameTime gameTime, IInputCommand command, bool covered)
		{
            if (command != null && !CurrentWorld.SelectedUserActor.TurnLocked)
		        command.Execute(CurrentWorld.SelectedUserActor, gameTime);

		    CurrentWorld.Update(gameTime);
            Camera.Update(CurrentWorld.SelectedUserActor.ScreenLocation);

			base.Update(gameTime, command, covered);
		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.FrontToBack, null, null, null, null, null, Camera.ViewMatrix);

            CurrentWorld.Draw(spriteBatch);

            spriteBatch.End();
        }

	    
	}
}
