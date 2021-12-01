using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace WindowsGame.Common.Inputs.Types
{
	public interface IJoystickInput
	{
		void Initialize();
		void Update(GameTime gameTime);

		Boolean JoyHold(Buttons button);
		Boolean JoyMove(Buttons button);

		//void SetMotors(Single leftMotor, Single rightMotor);
		//void ResetMotors();
	}

	public class JoystickInput : IJoystickInput
	{
		private Single tolerance;

		private GamePadState currGamePadState;
		private GamePadState prevGamePadState;

		const float Deadzone = 0.8f;
		const float DiagonalAvoidance = 0.2f;

		public void Initialize()
		{
			Single joyStickTolerance = 0.4f;
			tolerance = joyStickTolerance;
		}

		public void Update(GameTime gameTime)
		{
			// http://xona.com/2010/05/03.html.
			prevGamePadState = currGamePadState;
			currGamePadState = GamePad.GetState(PlayerIndex.One, GamePadDeadZone.IndependentAxes);
		}

		public Boolean JoyHold(Buttons button)
		{
			return currGamePadState.IsButtonDown(button) && prevGamePadState.IsButtonUp(button);

		}
		public Boolean JoyMove(Buttons button)
		{
			return currGamePadState.IsButtonDown(button);
		}

		//public void SetMotors(Single leftMotor, Single rightMotor)
		//{
		//	if (!currGamePadState.IsConnected)
		//	{
		//		return;
		//	}

		//	GamePad.SetVibration(PlayerIndex.One, leftMotor, rightMotor);
		//}

		//public void ResetMotors()
		//{
		//	SetMotors(0, 0);
		//}
	}
}