using System;
using Microsoft.Xna.Framework;
using WindowsGame.Common.Interfaces;
using WindowsGame.Common.Static;

namespace WindowsGame.Common.Screens
{
	public class PlayScreen : BaseScreen, IScreen
	{
		private Byte questionNo;
		private Boolean cheatMode;
		private OptionType optionType;

		public override void Initialize()
		{
			LoadTextData();
		}

		public override void LoadContent()
		{
			MyGame.Manager.ImageManager.GenerateNextActor();
			cheatMode = MyGame.Manager.QuestionManager.GetCheatMode();

			questionNo = MyGame.Manager.QuestionManager.QuestionNumber;
			MyGame.Manager.QuestionManager.LoadQuestion(questionNo);
			if (MyGame.Manager.ConfigManager.GlobalConfigData.RandomAnswers)
			{
				MyGame.Manager.QuestionManager.RandomizeAnswerList(questionNo);
			}

			// Correct option is now set!
			optionType = GetOptionType();
		}

		public ScreenType Update(GameTime gameTime)
		{
			UpdateVolumeIcon();
			Boolean escape = MyGame.Manager.InputManager.Escape();
			if (escape)
			{
				return ScreenType.Exit;
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

			if (cheatMode)
			{
				MyGame.Manager.SpriteManager.DrawSelect(optionType);
			}
			else
			{
				MyGame.Manager.SpriteManager.DrawSelectAll();
			}
		}

		private static OptionType GetOptionType()
		{
			return MyGame.Manager.QuestionManager.CorrectOptionType;
		}

	}
}