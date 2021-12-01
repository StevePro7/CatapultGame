using System;
using Microsoft.Xna.Framework;
using WindowsGame.Common.Interfaces;
using WindowsGame.Common.Static;

namespace WindowsGame.Common.Screens
{
	public class DiffScreen : BaseScreen, IScreen
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
					MyGame.Manager.QuestionManager.SetDifficulty(optionType);
					MyGame.Manager.SoundManager.PlayRightSoundEffect();
					flag = true;
				}
			}
			else
			{
				UpdateTimer(gameTime);
				if (Timer > delay)
				{
					return ScreenType.Long;
				}
			}

			return ScreenType.Diff;
		}

		public override void Draw()
		{
			// Draw all text first.
			MyGame.Manager.TextManager.Draw(TextDataList);
			MyGame.Manager.TextManager.DrawText(Constants.BUILD_VERSION, BuildPosition);

			// Draw all images next.
			MyGame.Manager.ImageManager.DrawTitle();
			MyGame.Manager.SoundManager.DrawVolumeIcon();

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
			//if (!flag)
			//{
			//	MyGame.Manager.SpriteManager.DrawSelectAll();
			//}
			//else
			//{
			//	MyGame.Manager.SpriteManager.DrawRight(optionType);
			//}
		}

	}
}