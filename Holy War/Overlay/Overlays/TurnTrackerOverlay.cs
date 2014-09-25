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

        public TurnTrackerOverlay(int totalTeams)
        {
            CurrentTurn = 1;
            CurrentTeam = (Team)1;

			_totalTeams = totalTeams;
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
					Converter.LayerTofloat(Layer.Master));
			}
		}

		public override void Update(GameTime gameTime)
		{
		    if (!_textToDisplayQueue.Any() || String.IsNullOrEmpty(_textToDisplayQueue.Peek())) 
                return;

		    _mFadeDelay -= gameTime.ElapsedGameTime.TotalSeconds;

		    if (_mFadeDelay <= 0)
		    {
		        _mFadeDelay = .035;

		        _mAlphaValue += _mFadeIncrement;

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
