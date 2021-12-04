using System;
using System.IO;
using WindowsGame.Common.Objects;
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

		// Current distance of the pumpkin
		float pumpkinDistance;

		public override void Initialize()
		{
		}

		public override void LoadContent()
		{
		}

		public ScreenType Update(GameTime gameTime)
		{
			CatapultState currentState = MyGame.Manager.StateManager.CatapultState;
			Catapult playerCatapult = MyGame.Manager.SpriteManager.PlayerCatapult;

			if (currentState == CatapultState.Firing)
			{
				MyGame.Manager.SpriteManager.Update(gameTime);
			}
			else
			{
				Boolean right = MyGame.Manager.InputManager.RghtArrow();
				if (right)
				{
					MyGame.Manager.SpriteManager.Update(gameTime);

					if (currentState == CatapultState.Reset)
					{
						// reset background and log
						screenPosition = Vector2.Zero;

						endObjectPos.X = 1000;
						endObjectPos.Y = 500;
					}

					// Move background
					if (currentState == CatapultState.ProjectileFlying)
					{
						screenPosition.X = (playerCatapult.PumpkinPosition.X - playerCatapult.PumpkinLaunchPosition)*-1.0f;
						endObjectPos.X = (playerCatapult.PumpkinPosition.X - playerCatapult.PumpkinLaunchPosition)*-1.0f + 1000;
					}

					// Calculate the pumpkin flying distance
					if (currentState == CatapultState.ProjectileFlying || currentState == CatapultState.ProjectileHit)
					{
						pumpkinDistance = playerCatapult.PumpkinPosition.X - playerCatapult.PumpkinLaunchPosition;
						pumpkinDistance /= 15.0f;
					}
				}
				else
				{
					Boolean left = MyGame.Manager.InputManager.LeftArrow();
					if (left)
					{
						MyGame.Manager.SpriteManager.UpdateLeft(gameTime);

						//if (currentState == CatapultState.Reset)
						//{
						//    // reset background and log
						//    screenPosition = Vector2.Zero;

						//    endObjectPos.X = 1000;
						//    endObjectPos.Y = 500;
						//}

						// Move background
						//if (currentState == CatapultState.ProjectileFlying)
						//{
						//    endObjectPos.X = (playerCatapult.PumpkinPosition.X - playerCatapult.PumpkinLaunchPosition) * 1.0f - 1000;
						//    screenPosition.X = (playerCatapult.PumpkinPosition.X - playerCatapult.PumpkinLaunchPosition) * 1.0f;
						//}

						//// Calculate the pumpkin flying distance
						//if (currentState == CatapultState.ProjectileFlying || currentState == CatapultState.ProjectileHit)
						//{
						//    pumpkinDistance = playerCatapult.PumpkinPosition.X + playerCatapult.PumpkinLaunchPosition;
						//    pumpkinDistance /= 15.0f;
						//}
					}
				}
			}

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


			Engine.SpriteBatch.DrawString(Assets.EmulogicFont, MyGame.Manager.StateManager.CatapultState.ToString(), new Vector2(1000, 20), Color.White);
			Engine.SpriteBatch.DrawString(Assets.EmulogicFont, pumpkinDistance.ToString(), new Vector2(1000, 70), Color.White);

			Engine.SpriteBatch.End();
		}

	}
}