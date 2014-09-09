using Holy_War.Options;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Holy_War
{
    public class MainGame : Game
    {
        private ScreenManager _screenManager;
        private GraphicsDeviceManager _graphics;
        
        public static GameOptions GameOptions;

        public MainGame()
        {
            Content.RootDirectory = "Content";
            
            GameOptions = new GameOptions(
              resolutionX: 640,
              resolutionY: 640);

            _graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferHeight = GameOptions.ResolutionY,
                PreferredBackBufferWidth = GameOptions.ResolutionX
            };

            IsFixedTimeStep = true;
        }

        protected override void LoadContent()
        {
            _screenManager = new ScreenManager(this);
            Components.Add(_screenManager);

            _screenManager.AddScreen(new GameScreen(Content));
        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            base.Draw(gameTime);
        }
    }
}
