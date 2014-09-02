using Microsoft.Xna.Framework;
using Holy_War.Tiles;
using Holy_War.Helpers;

namespace Holy_War
{
    public class Camera
    {
        private Vector2 _position;
        private readonly int _heightInTiles;
        private readonly int _widthInTiles;
        private const int _defaultMapWidth = 10;
        private const int _defaultMapHeight = 10;

        public Matrix ViewMatrix { get; private set; }
        public int ScreenWidth { get { return GraphicsDeviceManager.DefaultBackBufferWidth; } }
        public int ScreenHeight { get { return GraphicsDeviceManager.DefaultBackBufferWidth; } }

        public Camera()
        {
            _heightInTiles = _defaultMapHeight;
            _widthInTiles = _defaultMapWidth;

            ViewMatrix = Matrix.CreateTranslation(new Vector3(Vector2.Zero, 0));
        }

        public Camera(int tileHeight, int tileWidth)
        {
            _heightInTiles = tileHeight;
            _widthInTiles = tileWidth;
            ViewMatrix = Matrix.CreateTranslation(new Vector3(Vector2.Zero, 0));
        }

        public void Update(Point selectedActorPosition)
        {
            if (_position.X < 0)
                _position.X = 0;
            if (_position.Y < 0)
                _position.Y = 0;
            if (selectedActorPosition.X >= _widthInTiles + _position.X)
                _position.X += 1;
            if (selectedActorPosition.Y >= _heightInTiles + _position.Y)
                _position.Y += 1;
            if (selectedActorPosition.X < _position.X)
                _position.X -= 1;
            if (selectedActorPosition.Y < _position.Y)
                _position.Y -= 1;

            ViewMatrix = Matrix.CreateTranslation(
            -_position.X * Tile.GetDefaultTileWidth(),
            -_position.Y * Tile.GetDefaultTileHeight(),
            0);
        }
    }
}