using System;
using Microsoft.Xna.Framework;
using WindowsGame.Common.Data;
using WindowsGame.Common.Interfaces;
using WindowsGame.Common.Static;

namespace WindowsGame.Common.Screens
{
	public class OverScreen : BaseScreen, IScreen
	{
		private String totalText, solveText, visorText;
		private Byte solveValu;
		private Single visorValu;
		private UInt16 overDelay, playDelay;

		public override void Initialize()
		{
			LoadTextData();
		}

		public override void LoadContent()
		{
			base.LoadContent();
			MyGame.Manager.SoundManager.PlayGameOver();

			totalText = MyGame.Manager.QuestionManager.QuizLengthText2;
			solveValu = MyGame.Manager.QuestionManager.QuestionNumber;
			solveText = BaseData.GetNumberSP(solveValu);

			Byte scoreValu = MyGame.Manager.ScoreManager.ScoreValu;
			visorValu = 0;
			if (solveValu > 0)
			{
				visorValu = (Single)scoreValu / (Single)solveValu * 100.0f;
			}

			Byte visorTemp = (Byte)(Math.Round(visorValu, 0));
			visorText = BaseData.GetNumberSP(visorTemp);

			overDelay = MyGame.Manager.ConfigManager.GlobalConfigData.OverDelay;
			playDelay = MyGame.Manager.ConfigManager.GlobalConfigData.PlayDelay;
		}

		public ScreenType Update(GameTime gameTime)
		{
			UpdateTimer(gameTime);

			Boolean escape = MyGame.Manager.InputManager.Escape();
			Boolean left = MyGame.Manager.InputManager.LeftArrow();
			if (escape || left)
			{
				MyGame.Manager.SoundManager.StopMusic();
				return ScreenType.Exit;
			}
			Boolean rght = MyGame.Manager.InputManager.RghtArrow();
			Boolean next = MyGame.Manager.InputManager.Character();
			if (rght || next)
			{
				MyGame.Manager.SoundManager.StopMusic();
				return ScreenType.Title;
			}

			Boolean fullScreen = false;
			Boolean volumeIcon = MyGame.Manager.InputManager.VolumeIcon();
			if (volumeIcon)
			{
				MyGame.Manager.SoundManager.AlternateSound();
			}
			else
			{
				if (Timer > overDelay)
				{
					fullScreen = MyGame.Manager.InputManager.FullScreen();
				}
			}

			if (fullScreen || (Timer > playDelay))
			{
				MyGame.Manager.SoundManager.StopMusic();
				return ScreenType.Title;
			}

			return ScreenType.Over;
		}

		public override void Draw()
		{
			// Draw all text first.
			MyGame.Manager.TextManager.Draw(TextDataList);
			MyGame.Manager.QuestionManager.DrawQuizDiffText();
			MyGame.Manager.ScoreManager.DrawStats(totalText, solveText, visorText);

			// Draw all images next.
			MyGame.Manager.ImageManager.DrawHeader();
			MyGame.Manager.ImageManager.DrawCurrActor();
			MyGame.Manager.SoundManager.DrawVolumeIcon();

			MyGame.Manager.DeviceManager.DrawQuitButton();
			MyGame.Manager.DeviceManager.DrawPlayButton();
		}

	}
}