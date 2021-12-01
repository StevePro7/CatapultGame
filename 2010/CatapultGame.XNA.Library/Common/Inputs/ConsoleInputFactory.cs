using Microsoft.Xna.Framework;
using System;
using WindowsGame.Common.Inputs.Types;
using WindowsGame.Common.Interfaces;

namespace WindowsGame.Common.Inputs
{
	public class ConsoleInputFactory : BaseInputFactory, IInputFactory
	{
		public ConsoleInputFactory(IJoystickInput joystickInput)
		{
			JoystickInput = joystickInput;
		}

		public void Update(GameTime gameTime)
		{
			JoystickInput.Update(gameTime);
		}

		public Boolean Escape()
		{
			return JoyEscape();
		}

	}
}