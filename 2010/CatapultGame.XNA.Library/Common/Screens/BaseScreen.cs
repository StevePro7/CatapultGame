using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using WindowsGame.Common.Objects;

namespace WindowsGame.Common.Screens
{
	public abstract class BaseScreen
	{
		protected UInt16 Timer { get; set; }
		protected Vector2 BannerPosition { get; set; }
		protected IList<TextData> TextDataList { get; private set; }
		protected IList<Vector2> TextPositions { get; set; }
		protected Vector2 BuildPosition { get; private set; }
		private IList<Vector2> CheatPositions { get; set; }

		public virtual void Initialize()
		{
			BuildPosition = GetBuildPosition();
			CheatPositions = GetCheatPositions();
		}

		public virtual void LoadContent()
		{
			Timer = 0;
		}

		protected void UpdateTimer(GameTime gameTime)
		{
			Timer += (UInt16)gameTime.ElapsedGameTime.Milliseconds;
		}

		public virtual void Draw()
		{
		}

		//protected void UpdateVolumeIcon()
		//{
		//    Boolean volume = MyGame.Manager.InputManager.VolumeIcon();
		//    if (volume)
		//    {
		//        MyGame.Manager.SoundManager.AlternateSound();
		//    }
		//}

		protected void LoadTextData()
		{
			LoadTextData(GetType().Name);
		}
		protected void LoadTextData(Byte suffix)
		{
			String name = String.Format("{0}{1}", GetType().Name, suffix);
			LoadTextData(name);
		}
		protected void HideCheatMode()
		{
			MyGame.Manager.SpriteManager.DrawWhite(CheatPositions[0]);
			MyGame.Manager.SpriteManager.DrawWhite(CheatPositions[1]);
		}

		private void LoadTextData(String screen)
		{
			TextDataList = MyGame.Manager.TextManager.LoadTextData(screen);
		}

		private static Vector2 GetBuildPosition()
		{
			return MyGame.Manager.TextManager.GetTextPosition(26, 23);
		}
		private static IList<Vector2> GetCheatPositions()
		{
			IList<Vector2> positions = new List<Vector2>();
			positions.Add(MyGame.Manager.TextManager.GetWhitePosition(25, 9));
			positions.Add(MyGame.Manager.TextManager.GetWhitePosition(27, 9));
			return positions;
		}

	}
}