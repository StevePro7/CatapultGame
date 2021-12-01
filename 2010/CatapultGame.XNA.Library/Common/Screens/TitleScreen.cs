using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using WindowsGame.Common.Interfaces;
using WindowsGame.Common.Static;

namespace WindowsGame.Common.Screens
{
	public class TitleScreen : BaseScreen, IScreen
	{
		private IList<Vector2> whitePositions;
		private UInt16 titleDelay;
		private Boolean flash, flag;
		private Boolean globalCheat, localCheat;
		private Byte cheatCount;

		public override void Initialize()
		{
			base.Initialize();
			TextPositions = GetTextPositions();
			whitePositions = GetWhitePositions();
			
			titleDelay = MyGame.Manager.ConfigManager.GlobalConfigData.TitleDelay;
			flash = MyGame.Manager.ConfigManager.GlobalConfigData.FlashTitle;
			globalCheat = MyGame.Manager.ConfigManager.GlobalConfigData.CheatMode;
			localCheat = globalCheat;
			MyGame.Manager.QuestionManager.SetCheatMode(localCheat);

			LoadTextData();
		}

		public override void LoadContent()
		{
			base.LoadContent();
			globalCheat = MyGame.Manager.ConfigManager.GlobalConfigData.CheatMode;
			localCheat = globalCheat;
			MyGame.Manager.QuestionManager.SetCheatMode(localCheat);

			if (MyGame.Manager.ConfigManager.GlobalConfigData.PlayMusic)
			{
				MyGame.Manager.SoundManager.PlayTitleMusic();
			}

			cheatCount = 0;
			flag = false;
		}

		public ScreenType Update(GameTime gameTime)
		{
			UpdateTimer(gameTime);
			if (Timer > titleDelay)
			{
				Timer = 0;
				flag = !flag;
			}

			Boolean escape = MyGame.Manager.InputManager.Escape();
			if (escape)
			{
				return ScreenType.Exit;
			}

			Boolean fullScreen = false;
			Boolean volumeIcon = MyGame.Manager.InputManager.VolumeIcon();
			if (volumeIcon)
			{
				MyGame.Manager.SoundManager.AlternateSound();
			}
			else
			{
				// Check if hit Lisa head first then check cheat mode...
				Boolean cheatMode = MyGame.Manager.InputManager.CheatMode();
				if (cheatMode)
				{
					// Not cheat but tap Lisa head then increment count.
					if (!localCheat)
					{
						cheatCount++;
						if (cheatCount >= Constants.NUMBER_CHEATS)
						{
							// Tap Lisa head enough times to enable cheat!
							localCheat = true;
							MyGame.Manager.QuestionManager.SetCheatMode(localCheat);
							MyGame.Manager.SoundManager.PlayCheatSoundEffect();
						}
					}
					else
					{
						fullScreen = MyGame.Manager.InputManager.FullScreen();
					}
				}
				else
				{
					fullScreen = MyGame.Manager.InputManager.FullScreen();
				}
			}

			if (fullScreen)
			{
				MyGame.Manager.SoundManager.PlayRightSoundEffect();
				return ScreenType.Diff;
			}

			return ScreenType.Title;
		}

		public override void Draw()
		{
			// Draw all text first.
			MyGame.Manager.TextManager.Draw(TextDataList);
			MyGame.Manager.TextManager.DrawText(Constants.BUILD_VERSION, BuildPosition);
			MyGame.Manager.TextManager.DrawText(Globalize.TITLE_LINE1, TextPositions[0]);
			MyGame.Manager.TextManager.DrawText(Globalize.TITLE_LINE2, TextPositions[1]);

			// Draw all images next.
			MyGame.Manager.ImageManager.DrawTitle();
			MyGame.Manager.SoundManager.DrawVolumeIcon();

			// Show / hide cheat mode text.
			if (!localCheat)
			{
				HideCheatMode();
			}

			// Flash Press Start
			if (!flash || !flag)
			{
				return;
			}
			MyGame.Manager.SpriteManager.DrawWhite(whitePositions[0]);
			MyGame.Manager.SpriteManager.DrawWhite(whitePositions[1]);
		}

		private static IList<Vector2> GetTextPositions()
		{
			IList<Vector2> positions = new List<Vector2>();
			positions.Add(MyGame.Manager.TextManager.GetTextPosition(2, 13));
			positions.Add(MyGame.Manager.TextManager.GetTextPosition(2, 14));
			return positions;
		}

		private static IList<Vector2> GetWhitePositions()
		{
			// START
			IList<Vector2> positions = new List<Vector2>();
			positions.Add(MyGame.Manager.TextManager.GetWhitePosition(2, 13));
			positions.Add(MyGame.Manager.TextManager.GetWhitePosition(4, 13));
			return positions;
		}

	}
}