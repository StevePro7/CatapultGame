using System;
using System.IO;
using WindowsGame.Master;
using Microsoft.Xna.Framework;
using WindowsGame.Common.Interfaces;
using WindowsGame.Common.Static;
using Microsoft.Xna.Framework.Graphics;

namespace WindowsGame.Common.Screens
{
	public class TestScreen : BaseScreen, IScreen
	{
		RenderTarget2D renderTarget;
		int width;
		int height;

		public override void Initialize()
		{
			PresentationParameters pp = Engine.GraphicsDevice.PresentationParameters;
			width = pp.BackBufferWidth;
			height = pp.BackBufferHeight;
			//renderTarget = new RenderTarget2D(GraphicsDevice, width, height, 1, GraphicsDevice.DisplayMode.Format);
			renderTarget = new RenderTarget2D(Engine.GraphicsDevice, width, height, false, SurfaceFormat.Color, DepthFormat.Depth24);
		}

		public override void LoadContent()
		{
			//MyGame.Manager.ImageManager.GenerateNextActor();
		}

		public ScreenType Update(GameTime gameTime)
		{
			return ScreenType.Test;
		}

		//Title.
		public override void Draw()
		{
			Engine.SpriteBatch.Draw(Assets.SplashTexture, BannerPosition, Color.White);
		}

	}
}