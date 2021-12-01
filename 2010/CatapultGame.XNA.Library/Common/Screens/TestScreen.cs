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
		//public override void Draw()
		//{
		//    Engine.GraphicsDevice.SetRenderTarget(renderTarget);
		//    Engine.GraphicsDevice.Clear(ClearOptions.Target | ClearOptions.DepthBuffer, Color.B, 1, 0);

		//    // Draw
		//    MyGame.Manager.ImageManager.DrawTitle();
		//    // Draw

		//    Engine.GraphicsDevice.SetRenderTarget(null);
		//    //Texture2D resolvedTexture = renderTarget.GetTexture();
		//    Texture2D resolvedTexture = (Texture2D)renderTarget;
		//    //resolvedTexture.Save("00.jpg", ImageFileFormat.Jpg);
		//    var file = "Images/title.jpg";
		//    Stream stream = File.Create(file);
		//    resolvedTexture.SaveAsJpeg(stream, width, height);

		//    Engine.Game.Exit();
		//}

		//Actor.
		public override void Draw()
		{
			//for (Byte index = 0; index < Constants.NUMBER_CHARACTERS; index++)
			for (Byte index = 0; index < 1; index++)
			{
				Engine.GraphicsDevice.SetRenderTarget(renderTarget);
				Engine.GraphicsDevice.Clear(ClearOptions.Target | ClearOptions.DepthBuffer, Color.Black, 1, 0);

				// Draw
				MyGame.Manager.ImageManager.DrawHeader();
				MyGame.Manager.ImageManager.DrawActor(index);
				// Draw

				Engine.GraphicsDevice.SetRenderTarget(null);
				//Texture2D resolvedTexture = renderTarget.GetTexture();
				Texture2D resolvedTexture = (Texture2D)renderTarget;
				//resolvedTexture.Save("00.jpg", ImageFileFormat.Jpg);
				var file = "Images/" + index + ".jpg";
				Stream stream = File.Create(file);
				resolvedTexture.SaveAsJpeg(stream, width, height);
			}

			Engine.Game.Exit();
		}

	}
}