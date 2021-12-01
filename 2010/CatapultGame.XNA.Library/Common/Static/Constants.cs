using System;

namespace WindowsGame.Common.Static
{
	public static class Constants
	{
		public const String BUILD_VERSION = "V1.0.0";
		public const String CONTENT_DIRECTORY = "Content";

		public const String DATA_DIRECTORY = "Data";
		public const String CONFIG_DIRECTORY = "Config";
		public const String LEVELS_DIRECTORY = "Levels";
		public const String TEXTS_DIRECTORY = "Texts";

		public const String FONTS_DIRECTORY = "Fonts";
		public const String SOUND_DIRECTORY = "Sound";
		public const String TEXTURES_DIRECTORY = "Textures";

		public const String GLOBAL_CONFIG_FILENAME = "GlobalConfig.xml";
		public const String PLATFORM_CONFIG_FILENAME = "PlatformConfig{0}.xml";

		public const Byte NUMBER_OPTIONS = 5;
		public const Byte NUMBER_SELECTS = 4;
		public const Byte NUMBER_LINES = 3;
		public const Byte NUMBER_CHARACTERS = 16;
		public const Byte NUMBER_SPRITES = 8;
		public const Byte NUMBER_CHEATS = 5;

		// Global data.
		public const Boolean IsFixedTimeStep = true;
		public const UInt32 FramesPerSecond = 100;

		// Global sizes.
		public const Byte SpriteSize = 80;
		public const Byte TextsSize = 20;
		public const SByte FontOffsetX = -1;
		public const SByte FontOffsetY = -4;
		public const Byte OffsetArrowY = 16;

		// Local sizes.
		public const Byte SpriteTile = 20;
		public const Byte OffsetSelect = 10;
		public const UInt16 CheatModeOffsetX = 430;
		public const UInt16 CheatModeOffsetY = 260;

		public static readonly Byte[] QUIZ_LONG = new Byte[] { 5, 10, 25, 50 };

#if WINDOWS && !MOBILE && DEBUG
		public const Boolean IsFullScreen = false;
		public const Boolean IsMouseVisible = true;
#endif
#if WINDOWS && !MOBILE && !DEBUG
		public const Boolean IsFullScreen = true;
		public const Boolean IsMouseVisible = true;
#endif
#if WINDOWS && MOBILE
		public const Boolean IsFullScreen = false;
		public const Boolean IsMouseVisible = true;

		public const UInt16 ScreenWide = 800;
		public const UInt16 ScreenHigh = 480;

		public const Boolean UseExposed = true;
		public const UInt16 ExposeWide = 800;
		public const UInt16 ExposeHigh = 480;

		public const Byte GameOffsetX = 80;
#endif
#if WINDOWS && !MOBILE
		public const UInt16 ScreenWide = 640;
		public const UInt16 ScreenHigh = 480;

		public const Boolean UseExposed = true;
		public const UInt16 ExposeWide = 640;
		public const UInt16 ExposeHigh = 480;

		public const Byte GameOffsetX = 0;
#endif
#if !WINDOWS && !MOBILE
		public const Boolean IsFullScreen = true;
		public const Boolean IsMouseVisible = false;
		public const UInt16 ScreenWide = 800;
		public const UInt16 ScreenHigh = 480;

		public const Boolean UseExposed = false;
		public const UInt16 ExposeWide = 800;
		public const UInt16 ExposeHigh = 480;

		public const Byte GameOffsetX = 80;
#endif
	}
}