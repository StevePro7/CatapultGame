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
			const int gameTimeElapsedGameTimeMilliseconds = 16;

			CatapultState currentState = MyGame.Manager.StateManager.CatapultState;
			if (currentState == CatapultState.Rolling)
			{
			}
			// Are we in the firing state
			else if (currentState == CatapultState.Firing)
			{
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
				// We are done rotating so send the pumpkin flying
				else
				{
					MyGame.Manager.StateManager.SetState(CatapultState.ProjectileFlying);

					pumpkinVelocity.X = baseSpeed * 2.0f + 1;
					pumpkinVelocity.Y = -baseSpeed * 0.75f;

					float rightTriggerAmt = 0.5f;
					rightTriggerAmt *= 2;

					pumpkinVelocity *= 1.0f + rightTriggerAmt;

					// Check for extra boost power
					if (basePosition.X > 620)
					{
						boostPower = 3;
						pumpkinVelocity *= 2.0f;
						//curGame.SoundBank.PlayCue("Boost");
					}
					else if (basePosition.X > 600)
					{
						boostPower = 2;
						pumpkinVelocity *= 1.6f;
						//curGame.SoundBank.PlayCue("Boost");
					}
					else if (basePosition.X > 580)
					{
						boostPower = 1;
						pumpkinVelocity *= 1.3f;
						//curGame.SoundBank.PlayCue("Boost");
					}
				}
			}
			// Pumpkin is in the flying state
            else if (currentState == CatapultState.ProjectileFlying)
            {
				// Update the position of the pumpkin
				//pumpkinPosition += pumpkinVelocity * gameTime.ElapsedGameTime.Milliseconds;
				//pumpkinVelocity += pumpkinAcceleration * gameTime.ElapsedGameTime.Milliseconds;
				pumpkinPosition += pumpkinVelocity * gameTimeElapsedGameTimeMilliseconds;
				pumpkinVelocity += pumpkinAcceleration * gameTimeElapsedGameTimeMilliseconds;

				// Move the catapult away from the pumpkin
				//basePosition.X -= pumpkinVelocity.X * gameTime.ElapsedGameTime.Milliseconds;
				basePosition.X -= pumpkinVelocity.X * gameTimeElapsedGameTimeMilliseconds;

				// Rotate the pumpkin as it flys
				pumpkinRotation += MathHelper.ToRadians(pumpkinVelocity.X * 3.5f);

				// Is the pumpkin hitting the ground
				if (pumpkinPosition.Y > 630)
				{
					// Stop playing any sounds
					//if (playingCue != null && playingCue.IsPlaying)
					//{
					//    playingCue.Stop(AudioStopOptions.Immediate);
					//    playingCue.Dispose();
					//    playingCue = null;
					//}

					// Play the bounce sound
					//curGame.SoundBank.PlayCue("Bounce");

					// Move the pumpkin out of the ground and Change the pumkin velocity
					pumpkinPosition.Y = 630;
					pumpkinVelocity.Y *= -0.8f;
					pumpkinVelocity.X *= 0.7f;

					// Stop the pumpkin if the speed is too low
					if (pumpkinVelocity.X < 0.1f)
					{
						CatapultState nextState = CatapultState.ProjectileHit;
						MyGame.Manager.StateManager.SetState(nextState);
						//curGame.SoundBank.PlayCue("Hit");

						// TODO - highscore depends on PumpkinDistance which are variables in main game!
						//if (curGame.HighScore == (int) curGame.PumpkinDistance && curGame.HighScore > 1000)
						//{
						//    //curGame.SoundBank.PlayCue("HighScore");
						//}
					}
				}
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

			// Position.
			Engine.SpriteBatch.DrawString(Assets.EmulogicFont, pumpkinPosition.X.ToString(), new Vector2(10, 20), Color.White);
			Engine.SpriteBatch.DrawString(Assets.EmulogicFont, pumpkinPosition.Y.ToString(), new Vector2(10, 70), Color.White);

			// Velocity.
			Engine.SpriteBatch.DrawString(Assets.EmulogicFont, pumpkinVelocity.X.ToString(), new Vector2(310, 20), Color.White);
			Engine.SpriteBatch.DrawString(Assets.EmulogicFont, pumpkinVelocity.Y.ToString(), new Vector2(310, 70), Color.White);

			// Acceleration.
			Engine.SpriteBatch.DrawString(Assets.EmulogicFont, pumpkinAcceleration.X.ToString(), new Vector2(610, 20), Color.White);
			Engine.SpriteBatch.DrawString(Assets.EmulogicFont, pumpkinAcceleration.Y.ToString(), new Vector2(610, 70), Color.White);
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
