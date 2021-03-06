﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Holy_War.Actors;
using Holy_War.Actors.Stats;
using Holy_War.Actors.UserActors.BoxActors;
using Holy_War.Actors.UserActors;
using Holy_War.Actors.UserActors.UserActorImplementations;
using Holy_War.Enumerations;
using Holy_War.Enumerations.ActorStats;
using Holy_War.Managers;
using Holy_War.Menus.MenuActions;
using Holy_War.Overlay.Overlays;
using Holy_War.Overlay.Overlays.DamageOverlay;
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
        public ITerrain[,] TerrainMapArray { get; private set; }
		public IUserActor[,] GroundMapArray { get; private set; }
        public SelectionBoxActor SelectionBox { get; private set; }
        public IUserActor SelectedUserActor { get; private set; }
        public List<string> TextureNames { get; private set; }
        public TurnTrackerOverlay TurnTracker { get; private set; }
		public ActorStatusOverlay ActorStatusOverlay { get; private set; }
		public DamageOverlay DamageOverlay { get; private set; }

        public int WidthInTiles { get; private set; }
        public int HeightInTiles { get; private set; }

        public World(int height, int width, List<string> texturesNameList)
        {
			TerrainMapArray = new ITerrain[height, width];
            GroundMapArray = new IUserActor[height, width];
            WidthInTiles = width;
            HeightInTiles = height;
            TextureNames = texturesNameList;
            TurnTracker = new TurnTrackerOverlay(2);
			ActorStatusOverlay = new ActorStatusOverlay();
			DamageOverlay = new DamageOverlay();
        }

        public List<IUserActor> GetActorsInArea(Point gridLocation, int size)
        {
            var userActorList = new List<IUserActor>();

            for (int i = gridLocation.X - size; i < gridLocation.X + size; i++)
            {
                for (int j = gridLocation.Y - size; j < gridLocation.X + size; j++)
                {
                    if(i >= 0 && j >= 0)
                        userActorList.Add(GroundMapArray[i, j]);
                }
            }

            return userActorList;
        }

        public void SelectUserActorAtSelectionBox()
        {
            var actorToSelect = GroundMapArray[SelectionBox.GridLocation.X, SelectionBox.GridLocation.Y];

            if (actorToSelect != null && IsCurrentlySelectableUserActor(actorToSelect))
            {
				var actorWithStats = actorToSelect as UserActorWithStats;
				
				if(actorWithStats != null)
				{
					actorWithStats.HighlightZone(actorWithStats.MovementZone);
					actorWithStats.TargetBoxActor.OnHighlight += ActorStatusOverlay.OnTargetHightlighted;
				}

                SelectedUserActor = actorToSelect;
                SetSelectionBoxVisibile(false);
            }
        }

        public void SelectSelectionBox()
        {
            if (SelectedUserActor != null)
                SelectionBox.SetScreenLocation(SelectedUserActor.GridLocation);

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

			SelectionBox.OnHighlight += ActorStatusOverlay.OnSelectionHightlighted;

            SelectSelectionBox();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            DrawGround(spriteBatch);
            DrawActors(spriteBatch);

            TurnTracker.Draw(spriteBatch);
			ActorStatusOverlay.Draw(spriteBatch);
            DamageOverlay.Draw(spriteBatch);
            SelectionBox.Draw(spriteBatch);
        }

        public void Update(GameTime gameTime)
        {
            var actors = GroundMapArray
                .Cast<IUserActor>()
                .Where(tile => tile != null)
                .ToList();

            TurnTracker.Update(gameTime);
            DamageOverlay.Update(gameTime);
			SelectedUserActor.Update(gameTime);

            if (!actors.Any(actor => actor.Updated))
                return;

            UpdateActorTiles(actors);
        }

        private void UpdateActorTiles(IList<IUserActor> actors)
        {
            foreach (var tile in actors.Where(actor => actor.Updated))
            {
                var userActor = tile as UserActorWithStats;

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
                    var userActor = actor as UserActorWithStats;
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
                    if(j == 2)
                        TerrainMapArray[i, j] = new GrasslandPath(
                            SpriteManager.Textures["Terrain/GrassTile/GrassPathWestEast"],
                            new Point(i, j),
                            1f);
                    else
                    TerrainMapArray[i, j] = new Grassland(
                        SpriteManager.Textures["Terrain/GrassTile/GrassTile"],
                        new Point(i, j),
						1.5f);
                }
            }
        }

        private void InitialiseActors()
        {
			int x = 0, y = 0;

            GroundMapArray[x, y] = UserActorFactory.Create("Monk", Team.Blue, new Point(x++, y));
            GroundMapArray[x, y] = UserActorFactory.Create("Warrior", Team.Blue, new Point(x++, y));
            GroundMapArray[x, y] = UserActorFactory.Create("Archer", Team.Blue, new Point(x++, y));
            GroundMapArray[x, y] = UserActorFactory.Create("Sorcerer", Team.Blue, new Point(x++, y));
            GroundMapArray[x, y] = UserActorFactory.Create("Assassin", Team.Blue, new Point(x++, y));

            x = 0;
            y = 4;

            GroundMapArray[x, y] = UserActorFactory.Create("Monk", Team.Red, new Point(x++, y));
            GroundMapArray[x, y] = UserActorFactory.Create("Warrior", Team.Red, new Point(x++, y));
            GroundMapArray[x, y] = UserActorFactory.Create("Archer", Team.Red, new Point(x++, y));
            GroundMapArray[x, y] = UserActorFactory.Create("Sorcerer", Team.Red, new Point(x++, y));
            GroundMapArray[x, y] = UserActorFactory.Create("Assassin", Team.Red, new Point(x++, y));
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

					if(tile != null)
						tile.Draw(spriteBatch);
                }
            }
        }

        private void MoveTileInArray(IUserActor userActor)
        {
            var tile = userActor as Tile;

            if (tile == null)
                return;

            GroundMapArray[tile.SavedGridLocation.X, tile.SavedGridLocation.Y] = null;
			GroundMapArray[userActor.GridLocation.X, userActor.GridLocation.Y] = userActor;
        }

        private void RemoveTileInArray(IUserActor userActor)
        {
            GroundMapArray[userActor.GridLocation.X, userActor.GridLocation.Y] = null;
        }

        private bool IsCurrentlySelectableUserActor(IUserActor userActor)
	    {
	        return userActor.Team == TurnTracker.CurrentTeam && !userActor.TurnLocked;
	    }
	}
}
