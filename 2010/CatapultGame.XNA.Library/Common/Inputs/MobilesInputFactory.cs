using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input.Touch;
using WindowsGame.Common.Inputs.Types;
using WindowsGame.Common.Interfaces;
using WindowsGame.Common.Static;

namespace WindowsGame.Common.Inputs
{
	public class MobilesInputFactory : BaseInputFactory, IInputFactory
	{
		public MobilesInputFactory(IJoystickInput joystickInput, ITouchScreenInput touchScreenInput)
		{
			JoystickInput = joystickInput;
			TouchScreenInput = touchScreenInput;
		}

		public override void Initialize()
		{
			TouchScreenInput.Initialize();
		}

		public void Update(GameTime gameTime)
		{
			JoystickInput.Update(gameTime);
			TouchScreenInput.Update(gameTime);
		}

		public Boolean Escape()
		{
			return JoyEscape();
		}

		public Boolean Advance()
		{
			return TouchScreenInput.Tap;
		}

		public Boolean FullScreen()
		{
			if (TouchLocationState.Pressed != TouchScreenInput.TouchState)
			{
				return false;
			}

			return MyGame.Manager.CollisionManager.FullScreen(TouchScreenInput.CurrTouchX, TouchScreenInput.CurrTouchY);
		}

		public OptionType GetOptionType()
		{
			if (TouchLocationState.Pressed != TouchScreenInput.TouchState)
			{
				return OptionType.None;
			}

			return MyGame.Manager.CollisionManager.GetOptionType(TouchScreenInput.CurrTouchX, TouchScreenInput.CurrTouchY);
		}

		public Boolean LeftArrow()
		{
			if (TouchLocationState.Pressed != TouchScreenInput.TouchState)
			{
				return false;
			}

			return MyGame.Manager.CollisionManager.LeftArrow(TouchScreenInput.CurrTouchX, TouchScreenInput.CurrTouchY);
		}

		public Boolean RghtArrow()
		{
			if (TouchLocationState.Pressed != TouchScreenInput.TouchState)
			{
				return false;
			}

			return MyGame.Manager.CollisionManager.RghtArrow(TouchScreenInput.CurrTouchX, TouchScreenInput.CurrTouchY);
		}

		public Boolean VolumeIcon()
		{
			if (TouchLocationState.Pressed != TouchScreenInput.TouchState)
			{
				return false;
			}

			return MyGame.Manager.CollisionManager.VolumeIcon(TouchScreenInput.CurrTouchX, TouchScreenInput.CurrTouchY);
		}

		public Boolean CheatMode()
		{
			if (TouchLocationState.Pressed != TouchScreenInput.TouchState)
			{
				return false;
			}

			return MyGame.Manager.CollisionManager.CheatMode(TouchScreenInput.CurrTouchX, TouchScreenInput.CurrTouchY);
		}

		public Boolean Character()
		{
			if (TouchLocationState.Pressed != TouchScreenInput.TouchState)
			{
				return false;
			}

			return MyGame.Manager.CollisionManager.Character(TouchScreenInput.CurrTouchX, TouchScreenInput.CurrTouchY);
		}

	}
}