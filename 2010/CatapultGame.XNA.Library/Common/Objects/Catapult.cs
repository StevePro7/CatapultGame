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
		}

		public void Update(GameTime gameTime)
		{
			
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
