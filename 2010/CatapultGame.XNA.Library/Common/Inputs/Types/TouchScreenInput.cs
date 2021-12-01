using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input.Touch;

namespace WindowsGame.Common.Inputs.Types
{
	public interface ITouchScreenInput
	{
		void Initialize();
		void Update(GameTime gameTime);

		Vector2 TouchPosition { get; }
		Int32 CurrTouchX { get; }
		Int32 CurrTouchY { get; }
		TouchLocationState TouchState { get; }
		Boolean Tap { get; }
		Boolean Hold { get; }
		Boolean DoubleTap { get; }
		Boolean HorizontalDrag { get; }
		Boolean VerticalDrag { get; }
	}

	public class TouchScreenInput : ITouchScreenInput
	{
		public void Initialize()
		{
			TouchPanel.EnabledGestures = GestureType.Tap | GestureType.DoubleTap | GestureType.Hold | GestureType.HorizontalDrag | GestureType.VerticalDrag;
		}

		public void Update(GameTime gameTime)
		{
			var location = GetTouchLocation();
			if (null != location)
			{
				TouchLocation touchLocation = (TouchLocation)location;
				
				TouchPosition = GetTouchPosition(touchLocation.Position);
				CurrTouchX = (Int32)(TouchPosition.X);
				CurrTouchY = (Int32)(TouchPosition.Y);
				TouchState = touchLocation.State;
			}
			else
			{
				TouchPosition = Vector2.Zero;
				CurrTouchX = 0;
				CurrTouchY = 0;
				TouchState = TouchLocationState.Invalid;
			}

			Tap = Hold = DoubleTap = HorizontalDrag = VerticalDrag = false;
			if (!TouchPanel.IsGestureAvailable)
			{
				return;
			}

			GestureSample gesture = TouchPanel.ReadGesture();
			Tap = gesture.GestureType == GestureType.Tap;
			Hold = gesture.GestureType == GestureType.Hold;
			DoubleTap = gesture.GestureType == GestureType.DoubleTap;
			HorizontalDrag = gesture.GestureType == GestureType.HorizontalDrag;
			VerticalDrag = gesture.GestureType == GestureType.VerticalDrag;
		}

		private static TouchLocation? GetTouchLocation()
		{
			TouchCollection touchCollection = TouchPanel.GetState();
			if (touchCollection.Count > 0)
			{
				return touchCollection[0];
			}

			return null;
		}

		private static Vector2 GetTouchPosition(Vector2 touchPosition)
		{
			// http://www.david-amador.com/2010/03/xna-2d-independent-resolution-rendering.
			Vector2 deltaPosition = touchPosition - MyGame.Manager.ResolutionManager.ViewPortVector2;
			return Vector2.Transform(deltaPosition, MyGame.Manager.ResolutionManager.InvertTransformationMatrix);
		}

		public Vector2 TouchPosition { get; private set; }
		public Int32 CurrTouchX { get; private set; }
		public Int32 CurrTouchY { get; private set; }
		public TouchLocationState TouchState { get; private set; }
		public Boolean Tap { get; private set; }
		public Boolean Hold { get; private set; }
		public Boolean DoubleTap { get; private set; }
		public Boolean HorizontalDrag { get; private set; }
		public Boolean VerticalDrag { get; private set; }
	}
}