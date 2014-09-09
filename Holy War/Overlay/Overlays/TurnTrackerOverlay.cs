using System;
using System.Collections.Generic;
using System.Linq;
using Holy_War.Helpers;
using Holy_War.Screens;
using Holy_War.Tiles;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Holy_War.Enumerations;
using Holy_War.Managers;

namespace Holy_War.Overlay.Overlays
{
    public class TurnTrackerOverlay : Overlay
    {
        public int CurrentTurn { get; private set; }
        public Team CurrentTeam { get; private set; }

		private Queue<string> _textToDisplayQueue;
        private readonly int _totalTeams;
		private float _mAlphaValue;
        private float _mFadeIncrement = 0.05f;
        private double _mFadeDelay = .035;

        public TurnTrackerOverlay()
        {
            CurrentTurn = 1;
            CurrentTeam = (Team)1;

            _totalTeams = Enum.GetNames(typeof(Team)).Length;
			_textToDisplayQueue = new Queue<string>();
        }

        public bool NextTurn()
        {
			if ((int)CurrentTeam != _totalTeams)
			{
				CurrentTeam++;
				_textToDisplayQueue.Enqueue("Team " + CurrentTeam + "\'s Turn");
			}
			else
			{
				CurrentTurn++;
				_textToDisplayQueue.Enqueue("Next Turn");
				CurrentTeam = (Team)1;
				_textToDisplayQueue.Enqueue("Team " + CurrentTeam + " - Turn " + CurrentTurn);
			}

            return true;
        }

		public override void Draw(SpriteBatch spriteBatch)
		{
			if (_textToDisplayQueue.Any() && !String.IsNullOrEmpty(_textToDisplayQueue.Peek()))
			{
				var spriteFont = SpriteManager.Fonts["Fonts/HeadingFont"];
				var spriteFontSize = spriteFont.MeasureString(_textToDisplayQueue.Peek());

				spriteBatch.DrawString(
					spriteFont,
					_textToDisplayQueue.Peek(),
					new Vector2(
                        (MainGame.GameOptions.ResolutionX / GameScreen.Camera.Zoom) / 2,
                        (MainGame.GameOptions.ResolutionY / GameScreen.Camera.Zoom) / 2),
					Color.White * _mAlphaValue,
					0f,
					new Vector2(spriteFontSize.X / 2, spriteFontSize.Y / 2),
                    1f * GameScreen.Camera.Zoom,
					SpriteEffects.None,
					Converter.LayerToFloat(Layer.Master));
			}
		}

		public override void Update(GameTime gameTime)
		{
			if (_textToDisplayQueue.Any() && !String.IsNullOrEmpty(_textToDisplayQueue.Peek()))
			{
				//Decrement the delay by the number of seconds that have elapsed since
				//the last time that the Update method was called
				_mFadeDelay -= gameTime.ElapsedGameTime.TotalSeconds;

				//If the Fade delays has dropped below zero, then it is time to 
				//fade in/fade out the image a little bit more.
				if (_mFadeDelay <= 0)
				{
					//Reset the Fade delay
					_mFadeDelay = .035;

					//Increment/Decrement the fade value for the image
					_mAlphaValue += _mFadeIncrement;

					//If the AlphaValue is equal or above the max Alpha value or
					//has dropped below or equal to the min Alpha value, then 
					//reverse the fade
					if (_mAlphaValue >= 1.5f)
						_mFadeIncrement *= -1;

					if (_mAlphaValue <= 0)
					{
						_mFadeIncrement *= -1;
						_textToDisplayQueue.Dequeue();
					}
				}
			}
		}
    }
}
