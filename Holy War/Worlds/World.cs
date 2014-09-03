using System.Collections.Generic;
using System.Linq;
using Holy_War.Actors;
using Holy_War.Actors.Stats;
using Holy_War.Actors.UserActors;
using Holy_War.Enumerations;
using Holy_War.Input;
using Holy_War.Managers;
using Holy_War.Tiles;
using Holy_War.Tiles.Terrain;
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
        public UserActor SelectedUserActor { get; private set; }
        public List<string> TextureNames { get; private set; }
        public Camera Camera { get; private set; }
        public TurnTracker TurnTracker { get; private set; }

		public int WidthInTiles { get; private set; }
        public int HeightInTiles { get; private set; }

	    private ContentManager _contentManager;

		public World(int height, int width, List<string> texturesNameList)
		{
            TerrainMapArray = new Tile[height, width];
            GroundMapArray = new Tile[height, width];
            Camera = new Camera(0.6f);
			WidthInTiles = width;
			HeightInTiles = height;
		    TextureNames = texturesNameList;
            TurnTracker = new TurnTracker(2);
		}


        public void SelectUserActorAtSelectionBox()
        {
            var actorToSelect = GroundMapArray[SelectionBox.ScreenLocation.X, SelectionBox.ScreenLocation.Y] as UserActor;

            if (actorToSelect != null && IsCurrentlySelectableUserActor(actorToSelect))
            {
                SelectedUserActor = actorToSelect;
                SetSelectionBoxVisibile(false);
            }
        }

        public void SelectSelectionBox()
        {
            if (SelectedUserActor != null)
                SelectionBox.ScreenLocation = SelectedUserActor.ScreenLocation;

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
		        TextureManager.Texture["Boxes/SelectionBox"],
                new Point(0, 0),
                Layer.Master);

            SelectSelectionBox();
		}

        public void Draw(SpriteBatch spriteBatch)
        {
            DrawGround(spriteBatch);
            DrawActors(spriteBatch);

            SelectionBox.Draw(spriteBatch);
        }

        public void Update()
        {
            var actors = GroundMapArray
                .Cast<Actor>()
                .Where(tile => tile != null)
                .ToList();

            Camera.Update(SelectedUserActor.ScreenLocation);

            if (!actors.Any(actor => actor.Updated))
                return;

            UpdateActorTiles(actors);
        }

	    private void UpdateActorTiles(IList<Actor> actors)
	    {
            foreach (var tile in actors.Where(actor => actor.Updated))
            {
                MoveTileInArray(tile);
                tile.Updated = false;
                tile.TurnLocked = true;
            }

            if (ActorsAllTurnLocked(actors))
	        {
	            TurnTracker.NextTurn();
                UnlockActors(actors);
	        }
	    }

        private bool ActorsAllTurnLocked(IEnumerable<Actor> actors)
        {
            return actors
                .Where(actor => (actor as UserActor).Stats.Team == TurnTracker.CurrentTeam)
                .All(actor => actor.TurnLocked);
        }

        private void UnlockActors(IEnumerable<Actor> actors)
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
                        TextureManager.Texture["GrassTile"], 
                        new Point(i, j));
                }
            }
	    }

        private void InitialiseActors()
        {
            GroundMapArray[0, 0] = new UserActor(
                TextureManager.Texture["Actors/UserActors/BlueActor"], 
                new Point(0, 0), 
                Layer.Ground,
                new ActorStats(1, 4));

            GroundMapArray[1, 0] = new UserActor(
                TextureManager.Texture["Actors/UserActors/BlueActor"],
                new Point(1, 0),
                Layer.Ground,
                new ActorStats(1, 4));

            GroundMapArray[10, 10] = new UserActor(
                TextureManager.Texture["Actors/UserActors/RedActor"],
                new Point(10, 10),
                Layer.Ground,
                new ActorStats(2, 4));

            GroundMapArray[10, 11] = new UserActor(
                TextureManager.Texture["Actors/UserActors/RedActor"],
                new Point(10, 11),
                Layer.Ground,
                new ActorStats(2, 4));
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

        private void MoveTileInArray(Actor tile)
	    {
            GroundMapArray[tile.CurrentGridLocation.X, tile.CurrentGridLocation.Y] = null;
            GroundMapArray[tile.ScreenLocation.X, tile.ScreenLocation.Y] = tile;
	    }

	    private bool IsCurrentlySelectableUserActor(UserActor userActor)
	    {
	        return userActor.Stats.Team == TurnTracker.CurrentTeam && !userActor.TurnLocked;
	    }
	}
}
