using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Holy_War.Actors;
using Holy_War.Actors.Stats;
using Holy_War.Actors.UserActors;
using Holy_War.Actors.UserActors.UserActorImplementations;
using Holy_War.Enumerations;
using Holy_War.Enumerations.ActorStats;
using Holy_War.Managers;
using Holy_War.Menus.MenuActions;
using Holy_War.Overlay.Overlays;
using Holy_War.Tiles;
using Holy_War.Tiles.Terrain;
using Microsoft.Win32;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Holy_War.Worlds
{
    public class World
    {
        public Tile[,] TerrainMapArray { get; private set; }
        public Tile[,] GroundMapArray { get; private set; }
        public SelectionBoxActor SelectionBox { get; private set; }
        public IUserActor SelectedUserActor { get; private set; }
        public List<string> TextureNames { get; private set; }
        public TurnTrackerOverlay TurnTracker { get; private set; }

        public int WidthInTiles { get; private set; }
        public int HeightInTiles { get; private set; }

        private ContentManager _contentManager;

        public World(int height, int width, List<string> texturesNameList)
        {
            TerrainMapArray = new Tile[height, width];
            GroundMapArray = new Tile[height, width];
            WidthInTiles = width;
            HeightInTiles = height;
            TextureNames = texturesNameList;
            TurnTracker = new TurnTrackerOverlay();
        }


        public void SelectUserActorAtSelectionBox()
        {
            var actorToSelect =
                GroundMapArray[SelectionBox.ScreenLocation.X, SelectionBox.ScreenLocation.Y] as UserActorWithZones;

            if (actorToSelect != null && IsCurrentlySelectableUserActor(actorToSelect))
            {
                actorToSelect.HighlightZone(actorToSelect.MovementZone);
                SelectedUserActor = actorToSelect;
                SetSelectionBoxVisibile(false);
            }
        }

        public void SelectSelectionBox()
        {
            if (SelectedUserActor != null)
                SelectionBox.SetScreenLocation(SelectedUserActor.ScreenLocation);

            SelectedUserActor = SelectionBox;
            SetSelectionBoxVisibile(true);
        }

        public void SetSelectionBoxVisibile(bool visible)
        {
            SelectionBox.Visible = visible;
        }

        public void InitialiseMap()
        {
            InitialiseGround();
            InitialiseActors();

            SelectionBox = new SelectionBoxActor(
                SpriteManager.Textures["Boxes/SelectionBox"],
                new Point(0, 0),
                Layer.Zones);

            SelectSelectionBox();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            DrawGround(spriteBatch);
            DrawActors(spriteBatch);

            TurnTracker.Draw(spriteBatch);
            SelectionBox.Draw(spriteBatch);
        }

        public void Update(GameTime gameTime)
        {
            var actors = GroundMapArray
                .Cast<IUserActor>()
                .Where(tile => tile != null)
                .ToList();

            TurnTracker.Update(gameTime);

            if (!actors.Any(actor => actor.Updated))
                return;

            UpdateActorTiles(actors);
        }

        private void UpdateActorTiles(IList<IUserActor> actors)
        {
            foreach (var tile in actors.Where(actor => actor.Updated))
            {
                var userActor = tile as UserActorWithZones;

                if (userActor != null && userActor.Dead)
                    RemoveTileInArray(tile);
                else
                {
                    MoveTileInArray(tile);

                    tile.Updated = false;
                    tile.TurnLocked = true;
                }
            }

            if (ActorsAllTurnLocked(actors))
            {
                TurnTracker.NextTurn();
                UnlockActors(actors);
            }
        }

        private bool ActorsAllTurnLocked(IEnumerable<IUserActor> actors)
        {
            return actors
                .Where(actor =>
                {
                    var userActor = actor as UserActorWithZones;
                    return userActor != null && userActor.Stats.Team == TurnTracker.CurrentTeam;
                })
                .All(actor => actor.TurnLocked);
        }

        private void UnlockActors(IEnumerable<IUserActor> actors)
        {
            foreach (var actor in actors)
                actor.TurnLocked = false;
        }

        private void InitialiseGround()
        {
            for (var i = 0; i < WidthInTiles; i++)
            {
                for (var j = 0; j < HeightInTiles; j++)
                {
                    TerrainMapArray[i, j] = new Grassland(
                        SpriteManager.Textures["Terrain/GrassTile"],
                        new Point(i, j));
                }
            }
        }

        private void InitialiseActors()
        {
            var defaultMenuActionList = new List<IMenuAction>
            {
                new EndTurnMenuAction("End Turn"),
                new AttackMenuAction("Attack")
            };

            GroundMapArray[0, 0] = new Monk(
                defaultMenuActionList,
                new ActorStats(
                    primaryStat: PrimaryStat.Dexterity, 
                    attackType: AttackType.Blunt, 
                    armorType: ArmorType.None, 
                    team: Team.Blue, 
                    hp:10,
                    strength: 3, 
                    dexterity: 6, 
                    intelligence: 5, 
                    attackRange: 1, 
                    movement: 4),
                new Point(0, 0),
                Layer.Ground);

            GroundMapArray[1, 0] = new Warrior(
                defaultMenuActionList,
                new ActorStats(
                    primaryStat: PrimaryStat.Strength, 
                    attackType: AttackType.Slashing, 
                    armorType: ArmorType.Medium, 
                    team: Team.Blue, 
                    hp: 10, 
                    strength: 5, 
                    dexterity: 5, 
                    intelligence: 5, 
                    attackRange: 1, 
                    movement: 4),
                new Point(1, 0),
                Layer.Ground); 

            GroundMapArray[2, 0] = new Archer(
                defaultMenuActionList,
                new ActorStats(
                    primaryStat: PrimaryStat.Dexterity, 
                    attackType: AttackType.Piercing, 
                    armorType: ArmorType.Light, 
                    team: Team.Blue, 
                    hp: 10, 
                    strength: 5, 
                    dexterity: 5, 
                    intelligence: 5, 
                    attackRange: 2, 
                    movement: 4),
                new Point(2, 0),
                Layer.Ground); 

            GroundMapArray[3, 0] = new Sorcerer(
                defaultMenuActionList,
                new ActorStats(
                    primaryStat: PrimaryStat.Dexterity, 
                    attackType: AttackType.Magical, 
                    armorType: ArmorType.None, 
                    team: Team.Blue, 
                    hp: 10, 
                    strength: 5, 
                    dexterity: 5, 
                    intelligence: 7, 
                    attackRange: 1, 
                    movement: 4),
                new Point(3, 0),
                Layer.Ground);

            GroundMapArray[3, 3] = new Monk(
                defaultMenuActionList,
                new ActorStats(
                    primaryStat: PrimaryStat.Strength, 
                    attackType: AttackType.Blunt, 
                    armorType: ArmorType.None, 
                    team: Team.Red, 
                    hp: 10, 
                    strength: 5, 
                    dexterity: 5, 
                    intelligence: 5, 
                    attackRange: 1, 
                    movement: 4),
                new Point(3, 3),
                Layer.Ground);

            GroundMapArray[3, 4] = new Warrior(
                defaultMenuActionList,
                new ActorStats(
                    primaryStat: PrimaryStat.Strength, 
                    attackType: AttackType.Blunt, 
                    armorType: ArmorType.None, 
                    team: Team.Red, 
                    hp: 10, 
                    strength: 5, 
                    dexterity: 5, 
                    intelligence: 5, 
                    attackRange: 1, 
                    movement: 4),
                new Point(3, 4),
                Layer.Ground);

            GroundMapArray[3, 5] = new Archer(
                defaultMenuActionList,
                new ActorStats(
                    primaryStat: PrimaryStat.Strength, 
                    attackType: AttackType.Blunt, 
                    armorType: ArmorType.None, 
                    team: Team.Red, 
                    hp: 10, 
                    strength: 5, 
                    dexterity: 5, 
                    intelligence: 5, 
                    attackRange: 1, 
                    movement: 4),
                new Point(3, 5),
                Layer.Ground);

            GroundMapArray[3, 5] = new Sorcerer(
                defaultMenuActionList,
                new ActorStats(
                    primaryStat: PrimaryStat.Strength, 
                    attackType: AttackType.Blunt, 
                    armorType: ArmorType.None, 
                    team: Team.Red, 
                    hp: 10, 
                    strength: 5, 
                    dexterity: 5, 
                    intelligence: 5, 
                    attackRange: 1, 
                    movement: 4),
                new Point(3, 5),
                Layer.Ground);
        }

        private void DrawGround(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < WidthInTiles; i++)
            {
                for (int j = 0; j < HeightInTiles; j++)
                {
                    var tile = TerrainMapArray[i, j];

                    tile.Draw(spriteBatch);
                }
            }
        }

        private void DrawActors(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < WidthInTiles; i++)
            {
                for (int j = 0; j < HeightInTiles; j++)
                {
                    var tile = GroundMapArray[i, j];

                    if (tile != null)
                        tile.Draw(spriteBatch);
                }
            }
        }

        private void MoveTileInArray(IUserActor userActor)
        {
            var tile = userActor as Tile;

            if (tile == null)
                return;

            GroundMapArray[tile.CurrentGridLocation.X, tile.CurrentGridLocation.Y] = null;
            GroundMapArray[tile.ScreenLocation.X, tile.ScreenLocation.Y] = tile;
        }

        private void RemoveTileInArray(IUserActor userActor)
        {
            GroundMapArray[userActor.ScreenLocation.X, userActor.ScreenLocation.Y] = null;
        }

        private bool IsCurrentlySelectableUserActor(UserActorWithZones userActor)
	    {
	        return userActor.Stats.Team == TurnTracker.CurrentTeam && !userActor.TurnLocked;
	    }
	}
}
