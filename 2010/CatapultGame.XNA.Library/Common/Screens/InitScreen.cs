using System;
using Microsoft.Xna.Framework;
using WindowsGame.Common.Interfaces;
using WindowsGame.Common.Static;
using WindowsGame.Master;

namespace WindowsGame.Common.Screens
{
	public class InitScreen : BaseScreen, IScreen
	{
		private ScreenType nextScreen;
		private UInt16 splashDelay;
		private Boolean join;

		public override void Initialize()
		{
			Single wide = (Constants.ScreenWide - Assets.SplashTexture.Width) / 2.0f;
			Single high = (Constants.ScreenHigh - Assets.SplashTexture.Height) / 2.0f;
			BannerPosition = new Vector2(wide, high);

			nextScreen = GetNextScreen();
			splashDelay = MyGame.Manager.ConfigManager.GlobalConfigData.SplashDelay;
			join = false;
		}

		public override void LoadContent()
		{
			base.LoadContent();
			MyGame.Manager.ThreadManager.LoadContentAsync();
		}

		public ScreenType Update(GameTime gameTime)
		{
			UpdateTimer(gameTime);

			// Do not attempt to progress until join.
			join = MyGame.Manager.ThreadManager.Join(1);
			if (!join)
			{
				return ScreenType.Init;
			}

			if (Timer > splashDelay)
			{
				return nextScreen;
			}

			Boolean fullScreen = MyGame.Manager.InputManager.FullScreen();
			Boolean advance = false;
#if WINDOWS
			advance = MyGame.Manager.InputManager.Advance();
#endif
			
			return fullScreen || advance ? nextScreen : ScreenType.Init;
		}

		public override void Draw()
		{
			Engine.SpriteBatch.Draw(Assets.SplashTexture, BannerPosition, Color.White);
		}

		private static ScreenType GetNextScreen()
		{
			ScreenType screenType = MyGame.Manager.ConfigManager.GlobalConfigData.ScreenType;
			if (ScreenType.Splash == screenType || ScreenType.Init == screenType)
			{
				screenType = ScreenType.Title;
			}

			return screenType;
		}

	}
}