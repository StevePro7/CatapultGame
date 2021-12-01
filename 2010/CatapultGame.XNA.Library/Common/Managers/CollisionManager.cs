using System;
using Microsoft.Xna.Framework;
using WindowsGame.Common.Data;
using WindowsGame.Common.Static;

namespace WindowsGame.Common.Managers
{
	public interface ICollisionManager
	{
		void LoadContent();

		Boolean FullScreen(Int32 x, Int32 y);
		OptionType GetOptionType(Int32 x, Int32 y);
		Boolean LeftArrow(Int32 x, Int32 y);
		Boolean RghtArrow(Int32 x, Int32 y);
		Boolean VolumeIcon(Int32 x, Int32 y);
		Boolean CheatMode(Int32 x, Int32 y);
		Boolean Character(Int32 x, Int32 y);
	}

	public class CollisionManager : ICollisionManager
	{
		private Rectangle fullScreenRect;
		private Rectangle leftArrowRect, rghtArrowRect;
		private Rectangle volumeIconRect, cheatModeRect;
		private Rectangle characterRect;
		private Rectangle[] optionRect;
		private Vector2[] optionPos;
		const Byte spriteSize = Constants.SpriteSize;

		public void LoadContent()
		{
			fullScreenRect = new Rectangle(0, 0, Constants.ScreenWide, Constants.ScreenHigh);

			Vector2 leftArrowPos = BaseData.GetLeftArrowPos();
			leftArrowRect = new Rectangle((Int16)leftArrowPos.X, (Int16)leftArrowPos.Y, spriteSize, spriteSize);

			Vector2 rghtArrowPos = BaseData.GetRghtArrowPos();
			rghtArrowRect = new Rectangle((Int16)rghtArrowPos.X, (Int16)rghtArrowPos.Y, spriteSize, spriteSize);

			Vector2 volumeIconPos = BaseData.GetVolumeIconPos();
			volumeIconRect = new Rectangle((Int16)volumeIconPos.X, (Int16)volumeIconPos.Y, spriteSize, spriteSize);

			Vector2 cheatModePos = BaseData.GetCheatModePos();
			cheatModeRect = new Rectangle((Int16)cheatModePos.X, (Int16)cheatModePos.Y, spriteSize, spriteSize);

			Vector2 characterPos = BaseData.GetCharacterPos();
			characterRect = new Rectangle((Int16)characterPos.X, (Int16)characterPos.Y, 12 * Constants.TextsSize, 16 * Constants.TextsSize);

			optionPos = BaseData.GetPositionsSelect();
			optionRect = new Rectangle[Constants.NUMBER_OPTIONS];

			optionRect[(Byte)OptionType.A] = GetOptionRect(OptionType.A);
			optionRect[(Byte)OptionType.B] = GetOptionRect(OptionType.B);
			optionRect[(Byte)OptionType.C] = GetOptionRect(OptionType.C);
			optionRect[(Byte)OptionType.D] = GetOptionRect(OptionType.D);
		}

		public Boolean FullScreen(Int32 x, Int32 y)
		{
			return fullScreenRect.Contains(x, y);
		}

		public OptionType GetOptionType(Int32 x, Int32 y)
		{
			if (optionRect[(Byte)OptionType.A].Contains(x, y))
			{
				return OptionType.A;
			}
			else if (optionRect[(Byte)OptionType.B].Contains(x, y))
			{
				return OptionType.B;
			}
			else if (optionRect[(Byte)OptionType.C].Contains(x, y))
			{
				return OptionType.C;
			}
			else if (optionRect[(Byte)OptionType.D].Contains(x, y))
			{
				return OptionType.D;
			}
			else
			{
				return OptionType.None;
			}
		}

		public Boolean LeftArrow(Int32 x, Int32 y)
		{
			return leftArrowRect.Contains(x, y);
		}

		public Boolean RghtArrow(Int32 x, Int32 y)
		{
			return rghtArrowRect.Contains(x, y);
		}

		public Boolean VolumeIcon(Int32 x, Int32 y)
		{
			return volumeIconRect.Contains(x, y);
		}

		public Boolean CheatMode(Int32 x, Int32 y)
		{
			return cheatModeRect.Contains(x, y);
		}

		public Boolean Character(Int32 x, Int32 y)
		{
			return characterRect.Contains(x, y);
		}

		private Rectangle GetOptionRect(OptionType type)
		{
			// Shrink the collision.
			Byte option = (Byte)type;
			const Byte offset = Constants.OffsetSelect;
			const Byte collSize = spriteSize - (2 * Constants.OffsetSelect);

			return new Rectangle((Int16)optionPos[option].X + offset, (Int16)optionPos[option].Y + offset, collSize, collSize);
		}
	}
}