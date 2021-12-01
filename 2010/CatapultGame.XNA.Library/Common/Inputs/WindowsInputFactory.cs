using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using WindowsGame.Common.Inputs.Types;
using WindowsGame.Common.Interfaces;
using WindowsGame.Common.Static;

namespace WindowsGame.Common.Inputs
{
	public class WindowsInputFactory : BaseInputFactory, IInputFactory
	{
		public WindowsInputFactory(IJoystickInput joystickInput, IKeyboardInput keyboardInput, IMouseScreenInput mouseScreenInput)
		{
			JoystickInput = joystickInput;
			KeyboardInput = keyboardInput;
			MouseScreenInput = mouseScreenInput;
		}

		public void Update(GameTime gameTime)
		{
			JoystickInput.Update(gameTime);
			KeyboardInput.Update(gameTime);
			MouseScreenInput.Update(gameTime);
		}

		public Boolean Escape()
		{
			return KeyboardInput.KeyHold(Keys.Escape) || JoyEscape();
		}

		public Boolean Advance()
		{
			if (MouseScreenInput.CurrButtonState == ButtonState.Pressed)
			{
				return true;
			}

			return KeyboardInput.KeyHold(Keys.Space);
		}

	}
}