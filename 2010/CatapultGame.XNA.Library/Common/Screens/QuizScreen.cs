using System;
using Microsoft.Xna.Framework;
using WindowsGame.Common.Interfaces;
using WindowsGame.Common.Static;

namespace WindowsGame.Common.Screens
{
	public class QuizScreen : BaseScreen, IScreen
	{
		private UInt16 delay;
		private Byte questionNo, noQuestion;
		private Boolean cheatMode;
		private OptionType answerOption, selectOption;
		private Boolean correctAns, flag;

		public override void Initialize()
		{
			LoadTextData();
			delay = MyGame.Manager.ConfigManager.GlobalConfigData.OptionDelay;
		}

		public override void LoadContent()
		{
			base.LoadContent();
			cheatMode = MyGame.Manager.QuestionManager.GetCheatMode();

			noQuestion = MyGame.Manager.QuestionManager.NumberQuestion;
			questionNo = GetQuestionNumber();

			answerOption = GetAnswerOption();
			correctAns = false;
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
				Boolean actor = MyGame.Manager.InputManager.Character();
				if (actor)
				{
					return ScreenType.Score;
				}

				selectOption = MyGame.Manager.InputManager.GetOptionType();
				if (OptionType.None != selectOption)
				{
					correctAns = selectOption == answerOption;
					if (correctAns)
					{
						MyGame.Manager.ScoreManager.Increment();
						MyGame.Manager.SoundManager.PlayRightSoundEffect();
					}
					else
					{
						MyGame.Manager.SoundManager.PlayWrongSoundEffect();
					}

					flag = true;
				}
			}
			else
			{
				UpdateTimer(gameTime);
				if (Timer > delay)
				{
					MyGame.Manager.QuestionManager.Increment();
					questionNo = GetQuestionNumber();

					Byte qNo = (Byte)(questionNo + 1);
					if (qNo > noQuestion)
					{
						return ScreenType.Over;
					}

					return ScreenType.Play;
				}
			}

			return ScreenType.Quiz;
		}

		public override void Draw()
		{
			// Draw all text first.
			MyGame.Manager.QuestionManager.DrawQuestion(questionNo);
			MyGame.Manager.QuestionManager.DrawQuizDiffText();
			MyGame.Manager.QuestionManager.DrawQuestionNumber();
			MyGame.Manager.QuestionManager.DrawQuestionTotal();
			MyGame.Manager.ScoreManager.DrawScore();
			MyGame.Manager.TextManager.Draw(TextDataList);

			// Draw all images next.
			MyGame.Manager.ImageManager.DrawHeader();
			MyGame.Manager.ImageManager.DrawCurrActor();
			MyGame.Manager.SoundManager.DrawVolumeIcon();

			// Draw select unconditionally.
			if (cheatMode)
			{
				MyGame.Manager.SpriteManager.DrawSelect(answerOption);
			}
			else
			{
				MyGame.Manager.SpriteManager.DrawSelectAll();
			}

			// Draw answer when selected.
			if (flag)
			{
				if (correctAns)
				{
					MyGame.Manager.SpriteManager.DrawRight(selectOption);
				}
				else
				{
					MyGame.Manager.SpriteManager.DrawWrong(selectOption);
				}
			}
		}

		private static Byte GetQuestionNumber()
		{
			return MyGame.Manager.QuestionManager.QuestionNumber;
		}

		private static OptionType GetAnswerOption()
		{
			return MyGame.Manager.QuestionManager.CorrectOptionType;
		}

	}
}