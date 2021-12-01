using System;
using Microsoft.Xna.Framework;
using WindowsGame.Common.Interfaces;
using WindowsGame.Common.Static;
using WindowsGame.Master;

namespace WindowsGame.Common.Screens
{
	public class SplashScreen : BaseScreen, IScreen
	{
		private Boolean flag;

		public override void Initialize()
		{
			Single wide = (Constants.ScreenWide - Assets.SplashTexture.Width) / 2.0f;
			Single high = (Constants.ScreenHigh - Assets.SplashTexture.Height) / 2.0f;

			BannerPosition = new Vector2(wide, high);
			flag = false;
		}

		public ScreenType Update(GameTime gameTime)
		{
			return flag ? ScreenType.Init : ScreenType.Splash;
		}

		public override void Draw()
		{
			Engine.SpriteBatch.Draw(Assets.SplashTexture, BannerPosition, Color.White);
			flag = true;
		}

	}
}