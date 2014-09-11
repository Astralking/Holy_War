using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Holy_War.Enumerations;
using Holy_War.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Holy_War.Screens
{
    public abstract class Screen : IScreen
    {
        #region Fields and Properties

        public bool IsPopup { get; set; }

        public TimeSpan TransitionOnTime
        {
            get { return _transitionOnTime; }
            protected set { _transitionOnTime = value; }
        }

       
        public TimeSpan TransitionOffTime 
        { 
            get { return transitionOffTime; }
            protected set { transitionOffTime = value; }
        }

        public float TransitionPercent { get; private set; }
        public float TransitionSpeed {get { return transitionSpeed; }}

        private const float transitionSpeed = 1.5f;
        private const int transitionDirection = 1;
        private TimeSpan _transitionOnTime = TimeSpan.Zero;
        private TimeSpan transitionOffTime = TimeSpan.Zero;


        public byte ScreenAlpha { get { return (byte)(TransitionPercent * 255); } }

        /// <summary>
        /// What is the screen doing currently?
        /// </summary>
        public ScreenState ScreenState
        {
            get { return _screenState; }
            set { _screenState = value; }
        }
        ScreenState _screenState = ScreenState.TransitionOn;

        /// <summary>
        /// The screen manager that controls the screen.
        /// </summary>
        public ScreenManager ScreenManager
        {
            get { return screenManager; }
            set { screenManager = value; }
        }
        ScreenManager screenManager;

        /// <summary>
        /// Is the screen currently exiting?
        /// </summary>
        public bool IsExiting
        {
            get { return isExiting; }
            protected set
            {
                isExiting = value;
                if (isExiting && (Exiting != null))
                {
                    Exiting(this, EventArgs.Empty);
                }
            }
        }
        bool isExiting = false;

        public Screen()
        {
            TransitionPercent = 0.00f;
            IsPopup = false;
        }

        public bool IsActive
        {
            get
            {
                return (_screenState == ScreenState.TransitionOn
                    || _screenState == ScreenState.Active);
            }
        }

        /// <summary>
        /// Event Handlers for the screen entering, exiting, and being removed
        /// </summary>
        public event EventHandler Entering;
        public event EventHandler Exiting;
        public event EventHandler Removed;

        /// <summary>
        /// Is the screen currently being covered by another?
        /// </summary>
        #endregion

        #region Initialization

        public virtual void LoadContent() { }

        public virtual void UnloadContent() { }
        #endregion

        #region Update and Draw

        public virtual void Initialize() { }

        public virtual void Update(GameTime gameTime, IInputCommand command, bool covered)
        {
            //If the screen state is either frozen or inactive, do not do any updating.
            //This is needed in case a screen sets the status before base.Update();
            if (_screenState == ScreenState.Frozen || _screenState == ScreenState.Inactive)
            {
                return;
            }
            if (IsExiting)
            {
                _screenState = ScreenState.TransitionOff;

                if (!ScreenTransition(gameTime, transitionOffTime, -1))
                {
                    this.Remove();
                }
            }
            else if (covered)
            {
                if (ScreenTransition(gameTime, transitionOffTime, 1))
                {
                    _screenState = ScreenState.TransitionOff;
                }
                else
                {
                    _screenState = ScreenState.Hidden;
                }
            }
            else if (_screenState == ScreenState.TransitionOn)
            {
                if (ScreenTransition(gameTime, _transitionOnTime, 1))
                {
                    _screenState = ScreenState.TransitionOn;
                }
                else
                {
                    _screenState = ScreenState.Active;
                }
            }
            else if (_screenState == ScreenState.TransitionOn)
            {
                if (ScreenTransition(gameTime, transitionOffTime, -1))
                {
                    _screenState = ScreenState.TransitionOff;
                }
                else
                {
                    _screenState = ScreenState.Inactive;
                }
            }
        }

        public virtual void Remove()
        {
            screenManager.RemoveScreen(this);
            if (Removed != null)
                Removed(this, EventArgs.Empty);
        }

        private bool ScreenTransition(GameTime gameTime, TimeSpan transitionTime, int direction)
        {
            //Check the direction to make sure it is either 1 or -1
            if (direction > 0 && direction != 1)
                direction = 1;
            else if (direction < 0 && direction != -1)
                direction = -1;

            float transitionDelta;

            //If the time is 0, there is no transition
            if (transitionTime == TimeSpan.Zero)
                transitionDelta = 1;
            else
                transitionDelta = (float)(gameTime.ElapsedGameTime.TotalMilliseconds / transitionTime.TotalMilliseconds);

            //Update the percent value of the transition
            TransitionPercent += transitionDelta * direction * transitionSpeed;

            if ((TransitionPercent <= 0) || (TransitionPercent >= 1))
            {
                //We are done transitioning
                TransitionPercent = MathHelper.Clamp(TransitionPercent, 0, 1);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Allows the screen to handle input
        /// </summary>
        public virtual void HandleInput()
        {
            //if the screen is not active, do nothing
            if (_screenState != ScreenState.Active)
                return;
        }

        /// <summary>
        /// Draw stuff to the game window.
        /// </summary>
        /// <param name="gameTime">GameTime object to perform timed elements</param>
        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);

        /// <summary>
        /// This method will fade the screen behind it by
        /// creating a large texture over the current screen.
        /// spriteBatch HAS to be in between begin and end calls.
        /// </summary>
        /// <param name="spriteBatch">The active sprite batch object</param>
        /// <param name="fade">The texture to use for the fading (generally a single pixel).</param>
        /// <param name="color">The color you want the screen to fade</param>
        /// <param name="opaticy">Amount to fade by (between 0 and 1)</param>
        public void FadeScreen(SpriteBatch spriteBatch, Texture2D fade, Color color, float opacity)
        {
            opacity = MathHelper.Clamp(opacity, 0, 1);
            Viewport viewport = ScreenManager.Viewport;
            spriteBatch.Draw(fade, new Rectangle(0, 0, viewport.Width, viewport.Height),
                new Color(color.A, color.G, color.B, (byte)(opacity * 255)));
        }
        #endregion

        #region Methods
        /// <summary>
        /// Preforms logic for when we want to exit the screen.
        /// Override this to perform special actions (new screen).
        /// If you add a new screen here, you will not get a transition off.
        /// </summary>
        public virtual void ExitScreen()
        {
            IsExiting = true;
            if (transitionOffTime == TimeSpan.Zero)
                this.Remove();
        }
        public void FreezeScreen()
        {
            //Screen will be drawn but not updated
            _screenState = ScreenState.Frozen;
        }
        #endregion
    }
}
