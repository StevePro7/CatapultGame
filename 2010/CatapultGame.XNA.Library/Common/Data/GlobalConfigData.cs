using System;
using WindowsGame.Common.Static;

namespace WindowsGame.Common.Data
{
	public struct GlobalConfigData
	{
		public ScreenType ScreenType;
		public UInt16 SplashDelay;
		public UInt16 TitleDelay;
		public UInt16 OptionDelay;
		public UInt16 ReadyDelay;
		public UInt16 DotsDelay;
		public UInt16 OverDelay;
		public UInt16 PlayDelay;
		public Boolean BlankSplash;
		public Boolean FlashTitle;
		public Boolean PlayMusic;
		public Boolean PlaySound;
		public Boolean CheatMode;
		public Boolean RandomQuestions;
		public Boolean RandomAnswers;
		public Boolean QuitsToExit;
	}
}