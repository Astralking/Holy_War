using Holy_War.Enumerations;
using Holy_War.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Holy_War.Screens
{
	public interface IScreen
	{
		ScreenState ScreenState { get; set; }
		bool IsPopup { get; set; }
		bool IsActive { get; }
		ScreenManager ScreenManager { get; set; }

		void Update(GameTime gameTime, IInputCommand command, bool screenIsCovered);

		void HandleInput();
		void LoadContent();
		void UnloadContent();
		void Initialize();

		void Draw(GameTime gameTime, SpriteBatch spriteBatch);
	}
}
