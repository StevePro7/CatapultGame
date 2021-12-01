using Microsoft.Xna.Framework;
using WindowsGame.Common.Interfaces;
using WindowsGame.Common.Static;

namespace WindowsGame.Common.Devices
{
	public class MobilesDeviceFactory : IDeviceFactory
	{
		private Vector2 leftArrowText, rghtArrowText;

		public void Initialize()
		{
			leftArrowText = MyGame.Manager.TextManager.GetTextPosition(0, 19);
			leftArrowText.X = 0;
			rghtArrowText = MyGame.Manager.TextManager.GetTextPosition(32, 19);
		}

		// Left arrow.
		public void DrawBackButton()
		{
			DrawLeft(Globalize.BUTTON_BACK);
			//MyGame.Manager.SpriteManager.DrawLeftArrow();
			//MyGame.Manager.TextManager.DrawText(Globalize.BUTTON_BACK, leftArrowPos);
		}

		public void DrawOverButton()
		{
			DrawLeft(Globalize.BUTTON_OVER);
		}

		public void DrawQuitButton()
		{
			DrawLeft(Globalize.BUTTON_QUIT);
			//MyGame.Manager.SpriteManager.DrawLeftArrow();
		}

		// Right
		public void DrawPlayButton()
		{
			DrawRght(Globalize.BUTTON_PLAY);
		}

		public void DrawQuizButton()
		{
			DrawRght(Globalize.BUTTON_QUIZ);
			//MyGame.Manager.TextManager.DrawText(Globalize.BUTTON_QUIZ, rghtArrowPos);
			//MyGame.Manager.SpriteManager.DrawRghtArrow();
		}

		public void DrawViewButton()
		{
			DrawRght(Globalize.BUTTON_VIEW);
		}

		private void DrawLeft(string text)
		{
			MyGame.Manager.TextManager.DrawText(text, leftArrowText);
			MyGame.Manager.SpriteManager.DrawLeftArrow();
		}
		private void DrawRght(string text)
		{
			MyGame.Manager.TextManager.DrawText(text, rghtArrowText);
			MyGame.Manager.SpriteManager.DrawRghtArrow();
		}

	}
}