using Microsoft.Xna.Framework;
using WindowsGame.Common.Static;

namespace WindowsGame.Common.Interfaces
{
	public interface IScreen
	{
		void Initialize();
		void LoadContent();
		ScreenType Update(GameTime gameTime);
		void Draw();
	}
}