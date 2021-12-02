using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing;
using System.Linq;
using System.Text;
using WindowsGame.Common.Static;
using WindowsGame.Master;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WindowsGame.Common.Objects
{
	public class Catapult
	{
		// Position and speed of catapult base
		Vector2 basePosition = Vector2.Zero;
		float baseSpeed;

		// Position and rotation of catapult arm
		Vector2 armCenter = new Vector2(200, 27);
		Vector2 armOffset = new Vector2(280, 100);
		float armRotation;

		// Position, speed, and rotation of pumpkin
		Vector2 pumpkinPosition = Vector2.Zero;

		Vector2 pumpkinVelocity = Vector2.Zero;
		Vector2 pumpkinAcceleration = new Vector2(0, 0.001f);
		Vector2 pumpkinRotationPosition = Vector2.Zero;
		float pumpkinLaunchPosition;

		float pumpkinRotation;

		// Level of boost power
		int boostPower;

		public void Initialize()
		{
			ResetCatapult();
			Fire();
		}

		public void Update(GameTime gameTime)
		{
			int gameTimeElapsedGameTimeMilliseconds = 16;

			// Rotate the arm
			if (armRotation < MathHelper.ToRadians(81))
			{
				//armRotation += MathHelper.ToRadians(gameTime.ElapsedGameTime.Milliseconds);
				armRotation += MathHelper.ToRadians(gameTimeElapsedGameTimeMilliseconds);

				Matrix matTranslate, matTranslateBack, matRotate, matFinal;
				matTranslate = Matrix.CreateTranslation((-pumpkinRotationPosition.X)
							   - 170, -pumpkinRotationPosition.Y, 0);
				matTranslateBack =
					Matrix.CreateTranslation(pumpkinRotationPosition.X + 170,
											 pumpkinRotationPosition.Y, 0);
				matRotate = Matrix.CreateRotationZ(armRotation);
				matFinal = matTranslate * matRotate * matTranslateBack;

				Vector2.Transform(ref pumpkinRotationPosition, ref matFinal,
								  out pumpkinPosition);
				pumpkinLaunchPosition = pumpkinPosition.X;

				//pumpkinRotation += MathHelper.ToRadians(gameTime.ElapsedGameTime.Milliseconds / 10.0f);
				pumpkinRotation += MathHelper.ToRadians(gameTimeElapsedGameTimeMilliseconds / 10.0f);
			}
		}

		public void UpdateX(GameTime gameTime)
		{
			var currentState = MyGame.Manager.StateManager.CatapultState;

			// Do we need to reset
			if (currentState == CatapultState.Reset)
				ResetCatapult();

			// Are we currently rolling?
			if (currentState == CatapultState.Rolling)
			{
				// Add to current speed
				float speedAmt = 1.0f;//curGame.CurrentGamePadState.Triggers.Left;
				//if (curGame.CurrentKeyboardState.IsKeyDown(Keys.Right))
					//speedAmt = 1.0f;

				baseSpeed += speedAmt * gameTime.ElapsedGameTime.Milliseconds * 0.001f;

				// Move catapult based on speed
				basePosition.X += baseSpeed * gameTime.ElapsedGameTime.Milliseconds;

				// Move pumpkin to match catapult
				pumpkinPosition.X = pumpkinLaunchPosition = basePosition.X + 120;
				pumpkinPosition.Y = basePosition.Y + 80;

				// Play moving sound
				//if (playingCue == null && baseSpeed > 0)
				//{
				//    playingCue = curGame.SoundBank.GetCue("Move");
				//    playingCue.Play();
				//}

				// Check to see if we fire the pumpkin
				//if ((curGame.CurrentGamePadState.Buttons.A == ButtonState.Pressed &&
				//    curGame.LastGamePadState.Buttons.A != ButtonState.Pressed) ||
				//    (curGame.CurrentKeyboardState.IsKeyDown(Keys.Space) &&
				//    curGame.LastKeyboardState.IsKeyUp(Keys.Space)))
				//{
				//    Fire();
				//    if (playingCue != null && playingCue.IsPlaying)
				//    {
				//        playingCue.Stop(AudioStopOptions.Immediate);
				//        playingCue.Dispose();
				//        playingCue = curGame.SoundBank.GetCue("Flying");
				//        playingCue.Play();
				//    }
				//}
			}
		}

		public void Draw()
		{
			var currentState = MyGame.Manager.StateManager.CatapultState;
			if (currentState == CatapultState.Crash)
			{
				Engine.SpriteBatch.Draw(Assets.baseTextureBack, basePosition, null,
					Color.White, MathHelper.ToRadians(-5),
					Vector2.Zero, 1.0f, SpriteEffects.None, 0);
				Engine.SpriteBatch.Draw(Assets.armTexture, basePosition + armOffset, null,
				   Color.White, armRotation, armCenter, 1.0f, SpriteEffects.None, 0.0f);
				Engine.SpriteBatch.Draw(Assets.baseTexture, basePosition, null, Color.White,
					MathHelper.ToRadians(5), Vector2.Zero, 1.0f, SpriteEffects.None, 0);
			}
			else
			{
				Engine.SpriteBatch.Draw(Assets.baseTextureBack, basePosition, Color.White);
				Engine.SpriteBatch.Draw(Assets.armTexture, basePosition + armOffset, null,
				   Color.White, armRotation, armCenter, 1.0f, SpriteEffects.None, 0.0f);
				Engine.SpriteBatch.Draw(Assets.baseTexture, basePosition, Color.White);
			}

			if (currentState != CatapultState.ProjectileHit &&
			    currentState != CatapultState.Crash)
			{
				Engine.SpriteBatch.Draw(Assets.pumpkinTexture,
					new Vector2(pumpkinLaunchPosition, pumpkinPosition.Y),
					null, Color.White, pumpkinRotation,
					new Vector2(32, 32), 1.0f, SpriteEffects.None, 0.0f);
			}
			else
			{
				Engine.SpriteBatch.Draw(Assets.pumpkinSmashTexture,
					new Vector2(pumpkinLaunchPosition, pumpkinPosition.Y),
					null, Color.White, 0,
					new Vector2(50, 32), 1.0f, SpriteEffects.None, 0.0f);
			}
		}

		// Reset the catapult and pumpkin to default positions
		private void ResetCatapult()
		{
			basePosition.X = -100;
			//basePosition.X = 300;
			basePosition.Y = 430;
			baseSpeed = 0;

			pumpkinPosition = Vector2.Zero;
			armRotation = MathHelper.ToRadians(0);

			//done in StateMgr init
			//currentState = CatapultState.Rolling;

			pumpkinPosition = Vector2.Zero;
			pumpkinVelocity = Vector2.Zero;
			pumpkinPosition.X = pumpkinLaunchPosition = basePosition.X + 120;
			pumpkinPosition.Y = basePosition.Y + 80;
			pumpkinRotation = 0;

			boostPower = 0;
		}

		// Change state to firing and play fire sound
		private void Fire()
		{
			// TODO - write this before call to Fire
			//currentState = CatapultState.Firing;
			MyGame.Manager.StateManager.SetState(CatapultState.Firing);
			pumpkinRotationPosition = pumpkinPosition;
			//curGame.SoundBank.PlayCue("ThrowSound");
		}

		public Vector2 PumpkinPosition
		{
			get { return pumpkinPosition; }
//			set { pumpkinPosition = value; }
		}

		public float PumpkinLaunchPosition
		{
			get { return pumpkinLaunchPosition; }
			//set { pumpkinLaunchPosition = value; }
		}

		public int BoostPower
		{
			get { return boostPower; }
//			set { boostPower = value; }
		}
	}
}
