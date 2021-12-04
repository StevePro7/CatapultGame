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

		public Boolean LeftArrow()
		{
			//if (MouseScreenInput.ButtonHold())
			//{
			//    return MyGame.Manager.CollisionManager.LeftArrow(MouseScreenInput.CurrMouseX, MouseScreenInput.CurrMouseY);
			//}

			//if (KeyboardInput.KeyPress(Keys.Left))
			if (KeyboardInput.KeyHold(Keys.Left))
			{
				return true;
			}

			return false;
		}

		public Boolean RghtArrow()
		{
			//if (MouseScreenInput.ButtonHold())
			//{
			//    return MyGame.Manager.CollisionManager.RghtArrow(MouseScreenInput.CurrMouseX, MouseScreenInput.CurrMouseY);
			//}

			//if (KeyboardInput.KeyPress(Keys.Right))
			if (KeyboardInput.KeyHold(Keys.Right))
			{
				return true;
			}

			return false;
		}

	}
}