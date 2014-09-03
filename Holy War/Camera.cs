using System;
using Microsoft.Xna.Framework;
using Holy_War.Tiles;

namespace Holy_War
{
    public class Camera
    {
        private Vector2 _position;
        private readonly int _heightInTiles;
        private readonly int _widthInTiles;
        private const float _defaultHeightInTiles = 10;
        private const float _defaultWidthInTiles = 10;

        public Matrix ViewMatrix { get; private set; }
        public float Zoom { get; private set; }

        public Camera(float zoom)
        {
            Zoom = zoom;

            _heightInTiles = (int)Math.Round(_defaultHeightInTiles / Zoom);
            _widthInTiles = (int)Math.Round(_defaultWidthInTiles / Zoom);

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

            ViewMatrix = 
                Matrix.CreateTranslation(
                    new Vector3(-_position.X * Tile.GetDefaultTileWidth(),-_position.Y * Tile.GetDefaultTileHeight(),0)) * 
                Matrix.CreateScale(
                    new Vector3(Zoom, Zoom, 1));
        }
    }
}