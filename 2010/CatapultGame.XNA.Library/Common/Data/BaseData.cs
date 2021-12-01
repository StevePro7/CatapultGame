using System;
using System.Globalization;
using Microsoft.Xna.Framework;
using WindowsGame.Common.Static;

namespace WindowsGame.Common.Data
{
	public abstract class BaseData
	{
		public static void Initialize()
		{
			BaseRoot = String.Empty;
		}
		public static void Initialize(String root)
		{
			BaseRoot = root;
		}

		public static void LoadContent()
		{
			// Global generic data.
			//var globalData = MyGame.Manager.ConfigManager.GlobalConfigData;
		}

		public static String BaseRoot { get; private set; }

		public static String GetNumberZO(Byte number)
		{
			return GetNumber(number, '0');
		}
		public static String GetNumberSP(Byte number)
		{
			return GetNumber(number, ' ');
		}
		private static String GetNumber(Byte number, Char paddingChar)
		{
			return number.ToString(CultureInfo.InvariantCulture).PadLeft(3, paddingChar);
		}

		public static Vector2[] GetPositionsSelect()
		{
			Vector2[] positionsSelect = new Vector2[Constants.NUMBER_OPTIONS];
			positionsSelect[(Byte)OptionType.A] = BaseData.GetPositionSelect(0, 7);
			positionsSelect[(Byte)OptionType.B] = BaseData.GetPositionSelect(0, 11);
			positionsSelect[(Byte)OptionType.C] = BaseData.GetPositionSelect(0, 15);
			positionsSelect[(Byte)OptionType.D] = BaseData.GetPositionSelect(0, 19);
			return positionsSelect;
		}

		public static Vector2 GetPositionSelect(Byte x, Byte y)
		{
			return new Vector2(Constants.GameOffsetX + x * Constants.SpriteTile + Constants.OffsetSelect, y * Constants.SpriteTile + Constants.OffsetSelect);
		}

		public static Vector2 GetLeftArrowPos()
		{
			const UInt16 arrowHigh = Constants.ScreenHigh - Constants.SpriteSize + Constants.OffsetArrowY;
			return new Vector2(0, arrowHigh);
		}
		public static Vector2 GetRghtArrowPos()
		{
			const UInt16 arrowHigh = Constants.ScreenHigh - Constants.SpriteSize + Constants.OffsetArrowY;
			return new Vector2(Constants.ScreenWide - Constants.SpriteSize, arrowHigh);
		}

		public static Vector2 GetVolumeIconPos()
		{
			return new Vector2(Constants.ScreenWide - Constants.SpriteSize - Constants.GameOffsetX, -Constants.TextsSize / 2.0f);
		}

		public static Vector2 GetCheatModePos()
		{
			return new Vector2(Constants.CheatModeOffsetX + Constants.GameOffsetX, Constants.CheatModeOffsetY);
		}

		public static Vector2 GetCharacterPos()
		{
			return new Vector2(Constants.GameOffsetX + 20 * Constants.TextsSize, 8 * Constants.TextsSize);
		}

	}
}