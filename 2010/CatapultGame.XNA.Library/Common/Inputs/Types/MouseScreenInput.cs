using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace WindowsGame.Common.Inputs.Types
{
	public interface IMouseScreenInput
	{
		void Update(GameTime gameTime);
		Boolean ButtonHold();

		Int32 CurrMouseX { get; }
		Int32 CurrMouseY { get; }
		ButtonState CurrButtonState { get; }
		ButtonState PrevButtonState { get; }
	}

	public class MouseScreenInput : IMouseScreenInput
	{
		private MouseState currMouseState;

		public void Update(GameTime gameTime)
		{
			PrevButtonState = CurrButtonState;

			currMouseState = Mouse.GetState();
			CurrMouseX = currMouseState.X;
			CurrMouseY = currMouseState.Y;

			CurrButtonState = currMouseState.LeftButton;
		}

		public Boolean ButtonHold()
		{
			return ButtonState.Pressed == CurrButtonState && ButtonState.Released == PrevButtonState;
		}

		public Int32 CurrMouseX { get; private set; }
		public Int32 CurrMouseY { get; private set; }
		public ButtonState CurrButtonState { get; private set; }
		public ButtonState PrevButtonState { get; private set; }
	}
}