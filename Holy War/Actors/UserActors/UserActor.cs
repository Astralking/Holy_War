using System;
using System.Collections.Generic;
using System.Xml.Schema;
using Holy_War.Actors.Stats;
using Holy_War.Enumerations;
using Holy_War.Helpers;
using Holy_War.Managers;
using Holy_War.Menus.ContextMenus;
using Holy_War.Menus.MenuActions;
using Holy_War.Sprites;
using Holy_War.Tiles;
using Holy_War.Zones;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Holy_War.Actors.UserActors
{
    public class UserActor : Actor, IUserActor
    {
        public Point StartingPosition { get; private set; }
        public Point ScreenLocation { get { return base.ScreenLocation; } }
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

        private bool _updated = false;

        public bool Updated
        {
            get { return _updated; }
            set
            {
                _updated = value;

                if (_updated)
                    _newGridLocation = _location;
                else
                    _currentGridLocation = _newGridLocation;
            }
        }

        public UserActor(Texture2D texture, Point location, Layer layer)
            : base(texture, location, layer)
        {
            StartingPosition = location;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

        public override void Move(Point direction, GameTime gameTime)
        {
            //if (ScreenLocation + direction == StartingPosition)
            SetScreenLocation(ScreenLocation + direction);
        }

        public override void Action()
        {
            GameScreen.CurrentWorld.SelectUserActorAtSelectionBox();
            //StartingPosition = ScreenLocation;  
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
            StartingPosition = ScreenLocation;
        }
    }
}
