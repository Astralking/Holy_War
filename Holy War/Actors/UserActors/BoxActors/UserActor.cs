using Holy_War.Enumerations;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Holy_War.Actors.UserActors.BoxActors
{
    public class UserActor : Actor
    {
        public Point StartingPosition { get; private set; }
        public Point GridLocation { get { return base.ScreenLocation; } }
	    private bool _turnLocked;

        public bool TurnLocked
        {
            get { return _turnLocked; }
            set
            {
                _turnLocked = value;
                SetTransparency(_turnLocked);
            }
        }

        private bool _updated;

        public bool Updated
        {
            get { return _updated; }
            set
            {
                _updated = value;

                if (_updated)
                    _newGridLocation = _location;
                else
                    _savedGridLocation = _newGridLocation;
            }
        }

        public UserActor(Texture2D texture, Point location, Layer layer)
            : base(texture, location, layer)
        {
            StartingPosition = location;
        }

		public override void Update(GameTime gameTime)
		{
		}

        public override void Move(Point direction, GameTime gameTime)
        {
            SetScreenLocation(GridLocation + direction);
        }

        public override void Action()
        {
            GameScreen.CurrentWorld.SelectUserActorAtSelectionBox();
        }

        public virtual void Back()
        {
            GameScreen.CurrentWorld.SelectSelectionBox();
        }

        public void SetScreenLocation(Point location)
        {
            base.ScreenLocation = location;
        }

        public void UpdateStartPosition()
        {
            StartingPosition = GridLocation;
        }
    }
}
