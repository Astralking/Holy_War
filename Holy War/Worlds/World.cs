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
		public Tile[,] GroundMapArray { get; private set; }
		public Actor[,] ActorMapArray { get; private set; }
        public SelectionBoxActor SelectionBox { get; private set; }
        public UserActor SelectedUserActor { get; private set; }
        public List<string> TextureNames { get; private set; }
        public Camera Camera { get; private set; }

		public int WidthInTiles { get; private set; }
        public int HeightInTiles { get; private set; }

	    private ContentManager _contentManager;

		public World(int height, int width, List<string> texturesNameList)
		{
            GroundMapArray = new Tile[height, width];
            ActorMapArray = new Actor[height, width];
            Camera = new Camera();
			WidthInTiles = width;
			HeightInTiles = height;
		    TextureNames = texturesNameList;
		}

        public void SelectActor(UserActor userActor)
        {
            SelectedUserActor = userActor;
            SetSelectionBoxVisibile(false);
        }

        public void SelectUserActorAtSelectionBox()
        {
            var actorToSelect = ActorMapArray[SelectionBox.ScreenLocation.X, SelectionBox.ScreenLocation.Y] as UserActor;

            if (actorToSelect == null)
                return;

            SelectedUserActor = actorToSelect;
            SetSelectionBoxVisibile(false);
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
            if (InputHandler.ActiveDevice == Device.Mouse)
                SelectionBox.SetLocation(new Point(InputHandler.CurrentMouseState.X, InputHandler.CurrentMouseState.Y));

            var actorsToUpdate = ActorMapArray.Cast<Actor>()
                .Where(tile => tile != null && tile.Updated)
                .ToList();

            Camera.Update(SelectedUserActor.ScreenLocation);

            if (!actorsToUpdate.Any())
                return;

            foreach (var tile in actorsToUpdate)
            {
               MoveTileInArray(tile);

                tile.Updated = false;
            }
        }

	    private void InitialiseGround()
	    {
            for (var i = 0; i < WidthInTiles; i++)
            {
                for (var j = 0; j < HeightInTiles; j++)
                {
                    GroundMapArray[i, j] = new Grassland(
                        TextureManager.Texture["GrassTile"], 
                        new Point(i, j));
                }
            }
	    }

        private void InitialiseActors()
        {
            int x = 0;
            int y = 0;

            ActorMapArray[x, y] = new UserActor(
                TextureManager.Texture["BlankActor"], 
                new Point(x, y), 
                Layer.Actor,
                new ActorStats(_contentManager, 4));
        }

        private void DrawGround(SpriteBatch spriteBatch)
	    {
            for (int i = 0; i < WidthInTiles; i++)
            {
                for (int j = 0; j < HeightInTiles; j++)
                {
                    var tile = GroundMapArray[i, j];

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
                    var tile = ActorMapArray[i, j];

	                if (tile != null)
                        tile.Draw(spriteBatch);
	            }
	        }
	    }

        private void MoveTileInArray(Actor tile)
	    {
            ActorMapArray[tile.CurrentGridLocation.X, tile.CurrentGridLocation.Y] = null;
            ActorMapArray[tile.ScreenLocation.X, tile.ScreenLocation.Y] = tile;
	    }
	}
}
