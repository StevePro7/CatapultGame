using System;
using System.IO;
using WindowsGame.Master;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using WindowsGame.Common.Interfaces;
using WindowsGame.Common.Static;
using Microsoft.Xna.Framework.Graphics;

namespace WindowsGame.Common.Screens
{
	public class TestScreen : BaseScreen, IScreen
	{
		// Track where the player is viewing
		Vector2 screenPosition = Vector2.Zero;

		// Position of the object at the end of the rolling area
		Vector2 endObjectPos = new Vector2(1000, 500);

		public override void Initialize()
		{
		}

		public override void LoadContent()
		{
		}

		public ScreenType Update(GameTime gameTime)
		{
			Boolean left = MyGame.Manager.InputManager.LeftArrow();
			if (left)
			{
				MyGame.Manager.StateManager.SetState(CatapultState.Crash);
			}
			else
			{
				MyGame.Manager.StateManager.SetState(CatapultState.Rolling);
			}

			MyGame.Manager.SpriteManager.Update(gameTime);
			return ScreenType.Test;
		}

		//Title.
		public override void Draw()
		{
			// Where to draw the Sky
			Vector2 skyDrawPos = Vector2.Zero;
			skyDrawPos.Y -= 50;
			skyDrawPos.X = (screenPosition.X / 6) % 3840;

			// Where to draw the background hills
			Vector2 backgroundDrawPos = Vector2.Zero;
			backgroundDrawPos.Y += 225;
			backgroundDrawPos.X = screenPosition.X % 1920;

			string printString;
			Vector2 FontOrigin;

			// Start Drawing
			//spriteBatch.Begin(SpriteBlendMode.AlphaBlend);
			//Engine.SpriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend);
			Engine.SpriteBatch.Begin();
			

			// Draw the sky
			Engine.SpriteBatch.Draw(Assets.skyTexture, skyDrawPos, Color.White);
			Engine.SpriteBatch.Draw(Assets.skyTexture, skyDrawPos + new Vector2(Assets.skyTexture.Width, 0),
				null, Color.White, 0, Vector2.Zero, 1,
				SpriteEffects.FlipHorizontally, 0);

			// Check to see if we need to draw another sky
			if (skyDrawPos.X <= -(Assets.skyTexture.Width * 2) + Constants.screenWidth)
			{
				skyDrawPos.X += Assets.skyTexture.Width * 2;
				Engine.SpriteBatch.Draw(Assets.skyTexture, skyDrawPos, Color.White);
				Engine.SpriteBatch.Draw(Assets.skyTexture, skyDrawPos +
					new Vector2(Assets.skyTexture.Width, 0),
					null, Color.White, 0, Vector2.Zero, 1,
					SpriteEffects.FlipHorizontally, 0);
			}

			// Draw the background once
			Engine.SpriteBatch.Draw(Assets.backgroundTexture, backgroundDrawPos, Color.White);
			Engine.SpriteBatch.Draw(Assets.backgroundTexture, backgroundDrawPos +
				new Vector2(Assets.backgroundTexture.Width, 0),
				null, Color.White, 0, Vector2.Zero, 1,
				SpriteEffects.FlipHorizontally, 0);

			// Check to see if we need to draw another background
			if (backgroundDrawPos.X <= -(Assets.backgroundTexture.Width * 2) + Constants.screenWidth)
			{
				backgroundDrawPos.X += Assets.backgroundTexture.Width * 2;
				Engine.SpriteBatch.Draw(Assets.backgroundTexture, backgroundDrawPos, Color.White);
				Engine.SpriteBatch.Draw(Assets.backgroundTexture, backgroundDrawPos +
				new Vector2(Assets.backgroundTexture.Width, 0),
				null, Color.White, 0, Vector2.Zero, 1,
				SpriteEffects.FlipHorizontally, 0);
			}

			// Draw the log at the end
			Engine.SpriteBatch.Draw(Assets.endObjectTexture, endObjectPos, Color.White);

			// Draw the Catapult
			MyGame.Manager.SpriteManager.Draw();

			Engine.SpriteBatch.End();
		}

	}
}