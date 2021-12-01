using System;
using Microsoft.Xna.Framework;
using WindowsGame.Common.Data;
using WindowsGame.Common.Static;

namespace WindowsGame.Common.Managers
{
	public interface ISpriteManager
	{
		void Initialize();

		void DrawSelectAll();
		void DrawSelect(OptionType optionType);
		void DrawRight(OptionType optionType);
		void DrawWrong(OptionType optionType);
		void DrawLeftArrow();
		void DrawRghtArrow();
		void DrawVolumeOn();
		void DrawVolumeOff();
		void DrawWhite(Vector2 position);
	}

	public class SpriteManager : ISpriteManager
	{
		private Vector2[] positionsSelect;
		private Vector2[] positionsAnswer;
		private Vector2 leftArrowPos, rghtArrowPos, volumePos;
		private const Byte offsetAnswerY = 18;

		public void Initialize()
		{
			positionsSelect = BaseData.GetPositionsSelect();
			positionsAnswer = GetPositionsAnswer();

			leftArrowPos = BaseData.GetLeftArrowPos();
			rghtArrowPos = BaseData.GetRghtArrowPos();
			volumePos = BaseData.GetVolumeIconPos();
		}

		public void DrawSelectAll()
		{
			DrawSelect(OptionType.A);
			DrawSelect(OptionType.B);
			DrawSelect(OptionType.C);
			DrawSelect(OptionType.D);
		}
		public void DrawSelect(OptionType optionType)
		{
			Vector2 position = positionsSelect[(Byte) optionType];
			DrawSprite(SpriteType.Select, position);
		}

		public void DrawRight(OptionType optionType)
		{
			Vector2 position = positionsAnswer[(Byte) optionType];
			DrawSprite(SpriteType.White, position);
			DrawSprite(SpriteType.Right, position);
		}

		public void DrawWrong(OptionType optionType)
		{
			Vector2 position = positionsAnswer[(Byte) optionType];
			DrawSprite(SpriteType.White, position);
			DrawSprite(SpriteType.Wrong, position);
		}

		public void DrawLeftArrow()
		{
			DrawSprite(SpriteType.LeftArrow, leftArrowPos);
		}

		public void DrawRghtArrow()
		{
			DrawSprite(SpriteType.RightArrow, rghtArrowPos);
		}

		public void DrawVolumeOn()
		{
			DrawSprite(SpriteType.VolumeOn, volumePos);
		}

		public void DrawVolumeOff()
		{
			DrawSprite(SpriteType.VolumeOff, volumePos);
		}

		public void DrawWhite(Vector2 position)
		{
			DrawSprite(SpriteType.White, position);
		}

		private static void DrawSprite(SpriteType spriteType, Vector2 position)
		{
			MyGame.Manager.ImageManager.DrawSprite(spriteType, position);
		}

		private Vector2[] GetPositionsAnswer()
		{
			positionsAnswer = new Vector2[Constants.NUMBER_OPTIONS];
			positionsAnswer[(Byte)OptionType.A] = GetPositionAnswer(0, 7);
			positionsAnswer[(Byte)OptionType.B] = GetPositionAnswer(0, 11);
			positionsAnswer[(Byte)OptionType.C] = GetPositionAnswer(0, 15);
			positionsAnswer[(Byte)OptionType.D] = GetPositionAnswer(0, 19);
			return positionsAnswer;
		}
		private static Vector2 GetPositionAnswer(Byte x, Byte y)
		{
			return new Vector2(2 + Constants.GameOffsetX + x * Constants.SpriteTile, y * Constants.SpriteTile + offsetAnswerY);
		}

	}
}