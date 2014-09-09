using System;
using System.Collections.Generic;
using Holy_War.Enumerations;
using Holy_War.Input;
using Holy_War.Options;
using Holy_War.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Holy_War
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class ScreenManager : DrawableGameComponent
    {
        #region Fields
        private List<Screen> _screens = new List<Screen>();
        private List<Screen> _screensToUpdate = new List<Screen>();
        private SpriteBatch _spriteBatch;
        private InputHandler _inputHandler;

        private bool isInitialized;

        #endregion

        #region Properties

        public SpriteBatch SpriteBatch { get { return _spriteBatch; } }
        public ContentManager Content { get { return Game.Content; } }
        public Viewport Viewport { get { return GraphicsDevice.Viewport; } }

        #endregion

        #region Initialization
        /// <summary>
        /// Constructor, initializes the manager
        /// </summary>
        public ScreenManager(Game game)
            : base(game)
        {
            base.Initialize();

            isInitialized = true;
        }

        /// <summary>
        /// Initialize the spriteBatch and screen dedicated content.
        /// </summary>
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _inputHandler = new InGameInputHandler(Content);

            //load screen dedicated content
            foreach (Screen screen in _screens)
                screen.LoadContent();
        }

        /// <summary>
        /// Unload screen dedicated content
        /// </summary>
        protected override void UnloadContent()
        {
            //Tells the screen to unload their content.
            foreach (Screen screen in _screens)
                screen.UnloadContent();
        }
        #endregion

        #region Update and Draw

        /// <summary>
        /// Update manager and screens
        /// </summary>
        public override void Update(GameTime gameTime)
        {
            _screensToUpdate.Clear();

            var command = _inputHandler.HandleInput(gameTime);

            if (_screens.Count == 0)
                this.Game.Exit();

            foreach (Screen screen in _screens)
                _screensToUpdate.Add(screen);

            bool screenIsCovered = false;
            bool firstScreen = true;

            if (!Game.IsActive)
            {
                //Pause logic, delete this if you want to remove
                //the functionality
            }
            else
            {
                while (_screensToUpdate.Count > 0)
                {
                    Screen screen = _screensToUpdate[_screensToUpdate.Count - 1];

                    _screensToUpdate.RemoveAt(_screensToUpdate.Count - 1);

                    //Update the screen unless its frozen or inactive
                    if (screen.ScreenState != ScreenState.Frozen
                        && screen.ScreenState != ScreenState.Inactive)
                    {
                        screen.Update(gameTime, command, screenIsCovered);
                    }

                    if (screen.IsActive)
                    {
                        if (firstScreen)
                        {
                            screen.HandleInput();
                            firstScreen = false;
                        }

                        if (!screen.IsPopup)
                            screenIsCovered = true;
                    }
                }
            }
        }

        /// <summary>
        /// Tells each screen to draw
        /// </summary>
        /// <param name="gameTime">Time object to pass to the screens</param>
        public override void Draw(GameTime gameTime)
        {
            foreach (Screen screen in _screens)
            {           
                if (screen.ScreenState == ScreenState.Hidden)
                    continue;

                screen.Draw(gameTime, _spriteBatch);

                _spriteBatch.End();
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Adds a screen to the manager
        /// </summary>
        /// <param name="screen">The screen to be added</param>
        public void AddScreen(Screen screen)
        {
            //Sets the reference to the screen manager on the screen
            screen.ScreenManager = this;

            //If the screen manager is initialized, perform initialize operations 
            //for the screens.
            if (this.isInitialized)
            {
                screen.LoadContent();
                screen.Initialize();
            }

            //Finally, add the screen to the list.
            _screens.Add(screen);
        }

        /// <summary>
        /// Removed the desired screen from the system
        /// </summary>
        /// <param name="screen">The screen we wish to remove</param>
        public void RemoveScreen(Screen screen)
        {
            //If the screen manager is initialized, unload the screen content.
            if (this.isInitialized)
            {
                screen.UnloadContent();
            }

            //Finally, remove the screen from both lists.
            _screens.Remove(screen);
            _screensToUpdate.Remove(screen);
        }
        #endregion
    }
}
