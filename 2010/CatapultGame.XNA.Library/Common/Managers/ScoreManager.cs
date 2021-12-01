using System;
using Microsoft.Xna.Framework;
using WindowsGame.Common.Data;

namespace WindowsGame.Common.Managers
{
	public interface IScoreManager
	{
		void Initialize();
		void LoadContent();
		void Increment();
		void DrawScore();
		void DrawStats(String totalText, String solveText, String visorText);

		Byte ScoreValu { get; }
	}

	public class ScoreManager : IScoreManager
	{
		private String scoreText, valueText;
		private Vector2 scorePosn;
		private Vector2 totalPosn, solvePosn, rightPosn, visorPosn;

		public void Initialize()
		{
			Reset();

			scorePosn = GetScorePosn();
			CalculatePositions();
		}

		public void LoadContent()
		{
			Reset();
		}

		public void Increment()
		{
			ScoreValu++;
			scoreText = BaseData.GetNumberZO(ScoreValu);
			valueText = BaseData.GetNumberSP(ScoreValu);
		}

		public void DrawScore()
		{
			MyGame.Manager.TextManager.DrawText(scoreText, scorePosn);
		}

		public void DrawStats(String totalText, String solveText, String visorText)
		{
			MyGame.Manager.TextManager.DrawText(totalText, totalPosn);
			MyGame.Manager.TextManager.DrawText(solveText, solvePosn);
			MyGame.Manager.TextManager.DrawText(valueText, rightPosn);
			MyGame.Manager.TextManager.DrawText(visorText, visorPosn);
		}

		public Byte ScoreValu { get; private set; }

		private void Reset()
		{
			ScoreValu = 0;
			scoreText = BaseData.GetNumberZO(ScoreValu);
			valueText = BaseData.GetNumberSP(ScoreValu);
		}

		private static Vector2 GetScorePosn()
		{
			return MyGame.Manager.TextManager.GetTextPosition(29, 3);
		}

		private void CalculatePositions()
		{
			totalPosn = MyGame.Manager.TextManager.GetTextPosition(17, 6);
			solvePosn = MyGame.Manager.TextManager.GetTextPosition(17, 10);
			rightPosn = MyGame.Manager.TextManager.GetTextPosition(17, 14);
			visorPosn = MyGame.Manager.TextManager.GetTextPosition(17, 18);
		}

	}
}