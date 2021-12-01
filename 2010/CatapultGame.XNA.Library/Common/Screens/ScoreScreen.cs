using System;
using Microsoft.Xna.Framework;
using WindowsGame.Common.Data;
using WindowsGame.Common.Interfaces;
using WindowsGame.Common.Static;

namespace WindowsGame.Common.Screens
{
	public class ScoreScreen : BaseScreen, IScreen
	{
		private String totalText, solveText, visorText;
		private Byte solveValu;
		private Single visorValu;

		public override void Initialize()
		{
			LoadTextData();
		}

		public override void LoadContent()
		{
			totalText = MyGame.Manager.QuestionManager.QuizLengthText2;
			solveValu = MyGame.Manager.QuestionManager.QuestionNumber;
			solveText = BaseData.GetNumberSP(solveValu);

			Byte scoreValu = MyGame.Manager.ScoreManager.ScoreValu;
			visorValu = 0;
			if (solveValu > 0)
			{
				visorValu = (Single) scoreValu/(Single) solveValu * 100.0f;
			}

			Byte visorTemp = (Byte)(Math.Round(visorValu, 0));
			visorText = BaseData.GetNumberSP(visorTemp);
		}

		public ScreenType Update(GameTime gameTime)
		{
			UpdateVolumeIcon();
			Boolean escape = MyGame.Manager.InputManager.Escape();
			if (escape)
			{
				return ScreenType.Exit;
			}
			Boolean left = MyGame.Manager.InputManager.LeftArrow();
			if (left)
			{
				return ScreenType.Over;
			}
			Boolean rght = MyGame.Manager.InputManager.RghtArrow();
			Boolean next = MyGame.Manager.InputManager.Character();
			if (rght || next)
			{
				return ScreenType.Quiz;
			}

			return ScreenType.Score;
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

			MyGame.Manager.DeviceManager.DrawOverButton();
			MyGame.Manager.DeviceManager.DrawQuizButton();
		}

	}
}