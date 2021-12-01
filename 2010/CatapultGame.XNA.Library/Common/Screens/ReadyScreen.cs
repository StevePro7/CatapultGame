using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using WindowsGame.Common.Interfaces;
using WindowsGame.Common.Static;

namespace WindowsGame.Common.Screens
{
	public class ReadyScreen : BaseScreen, IScreen
	{
		private IList<Vector2> quizPositions;
		private DifficultyType difficultyType;
		private Byte numberQuestion, dotsCount;
		private UInt16 delay, delay2, timer2;
		private Boolean cheatMode, flag;
		private string diffText, longText;

		public override void Initialize()
		{
			base.Initialize();
			quizPositions = GetQuizPositions();

			delay = MyGame.Manager.ConfigManager.GlobalConfigData.ReadyDelay;
			delay2 = MyGame.Manager.ConfigManager.GlobalConfigData.DotsDelay;

			LoadTextData();
		}

		public override void LoadContent()
		{
			base.LoadContent();
			cheatMode = MyGame.Manager.QuestionManager.GetCheatMode();

			difficultyType = MyGame.Manager.QuestionManager.DifficultyType;
			numberQuestion = MyGame.Manager.QuestionManager.NumberQuestion;
			
			diffText = MyGame.Manager.QuestionManager.DifficultyText;
			longText = MyGame.Manager.QuestionManager.QuizLengthText2;

			MyGame.Manager.ScoreManager.LoadContent();
			dotsCount = 0;
			timer2 = 0;
			flag = false;
		}

		public ScreenType Update(GameTime gameTime)
		{
			Boolean escape = MyGame.Manager.InputManager.Escape();
			if (escape)
			{
				return ScreenType.Exit;
			}

			if (!flag)
			{
				Boolean leftArrow = MyGame.Manager.InputManager.LeftArrow();
				if (leftArrow)
				{
					return ScreenType.Long;
				}

				UpdateTimer(gameTime);
				timer2 += (UInt16)gameTime.ElapsedGameTime.Milliseconds;
				if (Timer > delay)
				{
					flag = true;
				}
			}

			// Moving dots "animation".
			if (timer2 > delay2)
			{
				timer2 = 0;
				dotsCount++;
				if (dotsCount > 3)
				{
					dotsCount = 0;
				}
			}

			// Check if advance or timer complete.
			Boolean volumeIcon = MyGame.Manager.InputManager.VolumeIcon();
			if (volumeIcon)
			{
				MyGame.Manager.SoundManager.AlternateSound();
			}
			else
			{
				Boolean fullScreen = MyGame.Manager.InputManager.FullScreen();
				Boolean rghtArrow = MyGame.Manager.InputManager.RghtArrow();
				if (fullScreen || rghtArrow)
				{
					flag = true;
				}
			}

			if (flag)
			{
				// Reset question manager + load quiz.
				MyGame.Manager.QuestionManager.Reset();
				MyGame.Manager.QuestionManager.LoadQuestionList(difficultyType);

				if (MyGame.Manager.ConfigManager.GlobalConfigData.RandomQuestions)
				{
					MyGame.Manager.QuestionManager.RandomizeQuestionList();
				}

				MyGame.Manager.SoundManager.StopMusic();
				return ScreenType.Play;
			}

			return ScreenType.Ready;
		}

		public override void Draw()
		{
			// Draw all text first.
			MyGame.Manager.TextManager.Draw(TextDataList);
			MyGame.Manager.TextManager.DrawText(Constants.BUILD_VERSION, BuildPosition);
			MyGame.Manager.TextManager.DrawText(diffText, quizPositions[0]);
			MyGame.Manager.TextManager.DrawText(longText, quizPositions[1]);
			MyGame.Manager.TextManager.DrawText(" ", quizPositions[1]);
			DrawDots();

			// Draw all images next.
			MyGame.Manager.ImageManager.DrawTitle();
			MyGame.Manager.SoundManager.DrawVolumeIcon();
			MyGame.Manager.DeviceManager.DrawBackButton();
			MyGame.Manager.DeviceManager.DrawPlayButton();

			// Show / hide cheat mode text.
			if (!cheatMode)
			{
				HideCheatMode();
			}
		}

		private void DrawDots()
		{
			const Byte posn = 2;
			MyGame.Manager.TextManager.DrawText("   ", quizPositions[posn]);

			for (Byte loop = 0; loop < dotsCount; loop++)
			{
				MyGame.Manager.TextManager.DrawText(".", quizPositions[posn + loop]);
			}
		}

		private static IList<Vector2> GetQuizPositions()
		{
			IList<Vector2> positions = new List<Vector2>();
			positions.Add(MyGame.Manager.TextManager.GetTextPosition(2, 7));
			positions.Add(MyGame.Manager.TextManager.GetTextPosition(2, 12));

			positions.Add(MyGame.Manager.TextManager.GetTextPosition(7, 18));
			positions.Add(MyGame.Manager.TextManager.GetTextPosition(8, 18));
			positions.Add(MyGame.Manager.TextManager.GetTextPosition(9, 18));

			return positions;
		}

	}
}