using System;
using Microsoft.Xna.Framework;
using WindowsGame.Common.Interfaces;
using WindowsGame.Common.Static;

namespace WindowsGame.Common.Screens
{
	public class LongScreen : BaseScreen, IScreen
	{
		private Boolean flag;
		private OptionType optionType;
		private UInt16 delay;
		private Boolean cheatMode;

		public override void Initialize()
		{
			base.Initialize();
			LoadTextData();

			delay = MyGame.Manager.ConfigManager.GlobalConfigData.OptionDelay;
			
		}

		public override void LoadContent()
		{
			base.LoadContent();
			cheatMode = MyGame.Manager.QuestionManager.GetCheatMode();
			flag = false;
		}

		public ScreenType Update(GameTime gameTime)
		{
			UpdateVolumeIcon();
			Boolean escape = MyGame.Manager.InputManager.Escape();
			if (escape)
			{
				return ScreenType.Exit;
			}

			if (!flag)
			{
				optionType = MyGame.Manager.InputManager.GetOptionType();
				if (OptionType.None != optionType)
				{
					MyGame.Manager.QuestionManager.SetQuizLength(optionType);
					MyGame.Manager.SoundManager.PlayRightSoundEffect();
					flag = true;
				}
				else
				{
					Boolean leftArrow = MyGame.Manager.InputManager.LeftArrow();
					if (leftArrow)
					{
						return ScreenType.Diff;
					}
				}
			}
			else
			{
				UpdateTimer(gameTime);
				if (Timer > delay)
				{
					return ScreenType.Ready;
				}
			}

			return ScreenType.Long;
		}

		public override void Draw()
		{
			MyGame.Manager.TextManager.Draw(TextDataList);
			MyGame.Manager.TextManager.DrawText(Constants.BUILD_VERSION, BuildPosition);

			MyGame.Manager.ImageManager.DrawTitle();
			MyGame.Manager.SoundManager.DrawVolumeIcon();
			MyGame.Manager.DeviceManager.DrawBackButton();

			// Show / hide cheat mode text.
			if (!cheatMode)
			{
				HideCheatMode();
			}

			MyGame.Manager.SpriteManager.DrawSelectAll();
			if (flag)
			{
				MyGame.Manager.SpriteManager.DrawRight(optionType);
			}
		}

	}
}