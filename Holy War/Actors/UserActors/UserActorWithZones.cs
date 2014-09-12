using System.Collections.Generic;
using Holy_War.Actors.Stats;
using Holy_War.Actors.UserActors.BoxActors;
using Holy_War.Enumerations;
using Holy_War.Enumerations.ActorStats;
using Holy_War.Helpers;
using Holy_War.Managers;
using Holy_War.Menus.ContextMenus;
using Holy_War.Menus.MenuActions;
using Holy_War.Zones;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Holy_War.Actors.UserActors
{
    public class UserActorWithStats : UserActor
    {
        private readonly List<IMenuAction> _menuActions;
        private ContextMenu _contextMenu;
        private UserActorState _state;
        private IZone _activeZone;

        public bool Dead { get; private set; }
        public ActorStats Stats { get; private set; }
		public TargetBoxActor TargetBoxActor { get; private set; }
        public IZone MovementZone { get; private set; }
        public IZone AttackZone { get; private set; }

        public UserActorWithStats(List<IMenuAction> menuActions, ActorStats stats, Texture2D texture, Point location, Layer layer)
            : base(texture, location, layer)
        {
            Stats = stats;

            _menuActions = menuActions;

            MovementZone = new MovementZone(stats.Movement, location);
            AttackZone = new AttackZone(stats.AttackRange, location);

			TargetBoxActor = new BoxActors.TargetBoxActor(
				SpriteManager.Textures["Boxes/TargetBox"],
				ScreenLocation,
				Layer.Zones);
        }

        public void ResetZoneOrigins(Point newZoneOrigin)
        {
            MovementZone.ResetOrigin(newZoneOrigin);
            AttackZone.ResetOrigin(newZoneOrigin);
            _state = UserActorState.Moving;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            DrawZones(spriteBatch);

            if (TargetBoxActor != null)
                TargetBoxActor.Draw(spriteBatch);

            if (_contextMenu != null)
                _contextMenu.Draw(spriteBatch);

            base.Draw(spriteBatch);
        }

		public override void Update(GameTime gameTime)
		{
			if (TargetBoxActor != null && TargetBoxActor.Visible)
				TargetBoxActor.Update(gameTime);

			base.Update(gameTime);
		}

        public override void Move(Point direction, GameTime gameTime)
        {
            switch (_state)
            {
                case UserActorState.Moving:
                    if (ScreenLocation + direction == StartingPosition || MovementZone.PositionIsInZone(ScreenLocation + direction))
                        SetScreenLocation(ScreenLocation + direction);
                    break;
                case UserActorState.Attacking:
                    if (TargetBoxActor.ScreenLocation + direction == TargetBoxActor.StartingPosition || AttackZone.PositionIsInZone(TargetBoxActor.ScreenLocation + direction))
                        TargetBoxActor.SetScreenLocation(TargetBoxActor.ScreenLocation + direction);
                    break;
                case UserActorState.ContextMenu:
                    _contextMenu.Move(direction, gameTime);
                    break;
            }
        }

        public override void Action()
        {
            switch (_state)
            {
                case UserActorState.Moving:
                    {
                        if (IsValidPosition())
                            ShowContextMenu();
                    }
                    break;
                case UserActorState.ContextMenu:
                    {
                        var command = _contextMenu.GetMenuAction();
                        command.Execute(this);

                        _contextMenu.Hide();
                    }
                    break;
                case UserActorState.Attacking:
                    {
                        var userActorToAttack =
                            GameScreen.CurrentWorld.GroundMapArray[
                                TargetBoxActor.ScreenLocation.X, TargetBoxActor.ScreenLocation.Y] as UserActorWithStats;

                        if (userActorToAttack != null)
                        {
                            Attack(userActorToAttack);
                            EndTurn();
                        }
                    }
                    break;
            }
        }

        public override void Back()
        {
            SetScreenLocation(StartingPosition);
            ResetZoneOrigins(StartingPosition);
            _activeZone = null;

            if (_contextMenu != null)
                _contextMenu.Hide();

			if (TargetBoxActor != null)
				TargetBoxActor.Visible = false;

            SetState(UserActorState.Moving);
        
            base.Back();
        }

        private void DrawZones(SpriteBatch spriteBatch)
        {
            if(_activeZone != null)
                _activeZone.Draw(spriteBatch);
        }

        private bool IsValidPosition()
        {
            return ScreenLocation == StartingPosition || 
                   GameScreen.CurrentWorld.GroundMapArray[ScreenLocation.X, ScreenLocation.Y] == null;
        }

        public void HighlightZone(IZone zone)
        {
            _activeZone = zone;
        }

        public void SetState(UserActorState userActorState)
        {
            _state = userActorState;

            switch (userActorState)
            {
                case UserActorState.Moving:
                    break;
                case UserActorState.Attacking:
					TargetBoxActor.SetScreenLocation(ScreenLocation);
					TargetBoxActor.Visible = true;
                    break;
                case UserActorState.ContextMenu:
                    _contextMenu.Show();
                    break;
            }

        }

        public void EndTurn()
        {
            Updated = true;

			if (TargetBoxActor != null)
				TargetBoxActor.Visible = false;

            HighlightZone(null);
            ResetZoneOrigins(ScreenLocation);
            UpdateStartPosition();

            GameScreen.CurrentWorld.SelectSelectionBox();
        }

        private void ShowContextMenu()
        {
            if (_contextMenu == null)
                _contextMenu = ContextMenuFactory.CreateContextMenu(_menuActions, Converter.PointToVector(ScreenLocation));

            SetState(UserActorState.ContextMenu);
        }

        private int Attack(UserActorWithStats userActorToAttack)
        {
            var fullAttackPower = Stats.GetAttackPower();

            var attackPowerAfterReductions = userActorToAttack.Stats.ArmorType.PerformReduction(
                Stats.AttackType,
                fullAttackPower);

            return userActorToAttack.InflictDamage(attackPowerAfterReductions);
        }

        private int InflictDamage(int damage)
        {
            int resultingHp = Stats.ReduceHp(damage);

            if (resultingHp <= 0)
            {
                Updated = true;
                Dead = true;
            }

            return resultingHp;
        }
    }
}
