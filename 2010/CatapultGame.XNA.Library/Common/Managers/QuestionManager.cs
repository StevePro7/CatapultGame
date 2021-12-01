using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using WindowsGame.Common.Data;
using WindowsGame.Common.Objects;
using WindowsGame.Common.Static;

namespace WindowsGame.Common.Managers
{
	public interface IQuestionManager
	{
		void Initialize();
		void Initialize(String root);
		void LoadContent();

		void LoadQuestionList(DifficultyType type);
		Question LoadQuestion(Byte index);
		Question LoadQuestion(String line);
		void RandomizeQuestionList();
		void RandomizeAnswerList(Byte index);
		void DrawQuestion(Byte index);
		void DrawQuestionNumber();
		void DrawQuestionTotal();
		void DrawQuizDiffText();
		void SetDifficulty(OptionType optionType);
		void SetQuizLength(OptionType optionType);
		void Increment();
		void Reset();

		Boolean GetCheatMode();
		void SetCheatMode(Boolean theCheatMode);

		// Properties.
		IList<Question> QuestionList { get; }
		Byte QuestionNumber { get; }
		Byte NumberQuestion { get; }
		DifficultyType DifficultyType { get; }
		OptionType CorrectOptionType { get; }
		String DifficultyText { get; }
		String QuizLengthText { get; }
		String QuizLengthText2 { get; }
	}

	public class QuestionManager : IQuestionManager
	{
		private String questionRoot;
		private Vector2[] questionPosn;
		private Vector2[] answerAPosn, answerBPosn, answerCPosn, answerDPosn;
		private Vector2[] originAPosn, originBPosn, originCPosn, originDPosn;
		private Vector2 numberPos, totalPos, diffPos;
		private String numberTxt;
		private Byte[] answerList;
		private Boolean cheatMode;

		private static readonly char[] semicolon = { ';' };
		private static readonly char[] pipe = { '|' };

		public void Initialize()
		{
			Initialize(String.Empty);
		}
		public void Initialize(String root)
		{
			questionRoot = root;
			answerList = new Byte[Constants.NUMBER_SELECTS];

			QuestionList = new List<Question>();
			cheatMode = false;
		}

		public void LoadContent()
		{
			questionPosn = GetQuestionPosn();
			answerAPosn = GetAnswerAPosn();
			answerBPosn = GetAnswerBPosn();
			answerCPosn = GetAnswerCPosn();
			answerDPosn = GetAnswerDPosn();

			originAPosn = answerAPosn;
			originBPosn = answerBPosn;
			originCPosn = answerCPosn;
			originDPosn = answerDPosn;

			numberPos = MyGame.Manager.TextManager.GetTextPosition(12, 3);
			totalPos = MyGame.Manager.TextManager.GetTextPosition(16, 3);
			diffPos = MyGame.Manager.TextManager.GetTextPosition(2, 1);

			Reset();
		}

		public void LoadQuestionList(DifficultyType type)
		{
			QuestionList.Clear();

			String file = String.Format("{0}{1}/{2}/{3}/{4}.txt", questionRoot, Constants.CONTENT_DIRECTORY, Constants.DATA_DIRECTORY,
				Constants.LEVELS_DIRECTORY, type);

			var lines = MyGame.Manager.FileManager.LoadTxt(file);
			foreach (String line in lines)
			{
				Question question = LoadQuestion(line);
				QuestionList.Add(question);
			}
		}

		public Question LoadQuestion(Byte index)
		{
			Question q = QuestionList[index];
			Byte answerCode = q.AnswerCode;
			answerCode -= 1;

			// Set correct option for this question.
			CorrectOptionType = (OptionType) answerCode;
			return q;
		}

		public Question LoadQuestion(String line)
		{
			String[] texts = line.Split(semicolon);

			String[] questionText = texts[1].Split(pipe);
			String[] answerAText = texts[2].Split(pipe);
			String[] answerBText = texts[3].Split(pipe);
			String[] answerCText = texts[4].Split(pipe);
			String[] answerDText = texts[5].Split(pipe);
			Byte answerCode = Convert.ToByte(texts[0]);

			return new Question(questionText, answerAText, answerBText, answerCText, answerDText, answerCode);
		}

		public void RandomizeQuestionList()
		{
			IList<Question> list = QuestionList;

			// https://stackoverflow.com/questions/273313/randomize-a-listt
			int n = QuestionList.Count;
			while (n > 1)
			{
				n--;
				int k = MyGame.Manager.RandomManager.Next(n + 1);
				Question value = list[k];
				list[k] = list[n];
				list[n] = value;
			}

			QuestionList = list;
		}

		public void RandomizeAnswerList(Byte index)
		{
			// Get answer code first
			// Stored as 1-4 for A-D
			Question q = QuestionList[index];
			Byte answerCode = q.AnswerCode;
			answerCode -= 1;
			

			const Byte selects = Constants.NUMBER_SELECTS;
			for (Byte idx = 0; idx < selects; idx++)
			{
				answerList[idx] = 0;
			}

			// Randomize answers for question.
			// + record correct random option.
			CorrectOptionType = OptionType.None;
			for (Byte idx = 0; idx < selects; idx++)
			{
				while (true)
				{
					Byte rnd = (Byte)MyGame.Manager.RandomManager.Next(selects);
					if (0 == answerList[rnd])
					{
						answerList[rnd] = idx;
						break;
					}
				}
			}

			// Set the correct option at end of loop.
			for (Byte idx = 0; idx < selects; idx++)
			{
				Byte val = answerList[idx];
				if (answerCode == val)
				{
					CorrectOptionType = (OptionType)idx;
				}
			}

			RandomizeAnswerPosn();
		}

		public void DrawQuestion(Byte index)
		{
			if (index >= NumberQuestion)
			{
				return;
			}

			Question question = QuestionList[index];
			for (Byte line = 0; line < Constants.NUMBER_LINES; line++)
			{
				if (line < question.QuestionText.Length)
				{
					DrawLine(question.QuestionText[line], questionPosn[line]);
				}

				if (line < question.AnswerAText.Length)
				{
					DrawLine(question.AnswerAText[line], answerAPosn[line]);
				}
				if (line < question.AnswerBText.Length)
				{
					DrawLine(question.AnswerBText[line], answerBPosn[line]);
				}
				if (line < question.AnswerCText.Length)
				{
					DrawLine(question.AnswerCText[line], answerCPosn[line]);
				}
				if (line < question.AnswerDText.Length)
				{
					DrawLine(question.AnswerDText[line], answerDPosn[line]);
				}
			}
		}

		public void DrawQuestionNumber()
		{
			MyGame.Manager.TextManager.DrawText(numberTxt, numberPos);
		}

		public void DrawQuestionTotal()
		{
			MyGame.Manager.TextManager.DrawText(QuizLengthText, totalPos);
		}

		public void DrawQuizDiffText()
		{
			MyGame.Manager.TextManager.DrawText(DifficultyText, diffPos);
		}

		public void SetDifficulty(OptionType optionType)
		{
			if (OptionType.B == optionType)
			{
				DifficultyType = DifficultyType.Norm;
			}
			else if (OptionType.C == optionType)
			{
				DifficultyType = DifficultyType.Hard;
			}
			else if (OptionType.D == optionType)
			{
				DifficultyType = DifficultyType.Argh;
			}
			else
			{
				DifficultyType = DifficultyType.Easy;
			}

			Byte index = (Byte) optionType;
			DifficultyText = Globalize.DIFF_TEXT[index];
		}

		public void SetQuizLength(OptionType optionType)
		{
			Byte index = (Byte) optionType;
			NumberQuestion = Constants.QUIZ_LONG[index];
			QuizLengthText = BaseData.GetNumberZO(NumberQuestion);
			QuizLengthText2 = BaseData.GetNumberSP(NumberQuestion);
		}

		public void Increment()
		{
			Byte qNo = (Byte) (QuestionNumber + 1);
			if (qNo > NumberQuestion)
			{
				return;
			}

			QuestionNumber++;
			numberTxt = BaseData.GetNumberZO((Byte)(QuestionNumber + 1));
		}

		public void Reset()
		{
			QuestionNumber = 0;
			numberTxt = BaseData.GetNumberZO((Byte)(QuestionNumber + 1));
		}

		public Boolean GetCheatMode()
		{
			return cheatMode;
		}
		public void SetCheatMode(Boolean theCheatMode)
		{
			cheatMode = theCheatMode;
		}

		// Properties.
		public IList<Question> QuestionList { get; private set; }
		public Byte QuestionNumber { get; private set; }
		public Byte NumberQuestion { get; private set; }
		public DifficultyType DifficultyType { get; private set; }
		public OptionType CorrectOptionType { get; private set; }
		public String DifficultyText { get; private set; }
		public String QuizLengthText { get; private set; }
		public String QuizLengthText2 { get; private set; }

		private static void DrawLine(String line, Vector2 posn)
		{
			if (String.IsNullOrEmpty(line))
			{
				return;
			}

			MyGame.Manager.TextManager.DrawText(line, posn);
		}

		private static Vector2[] GetQuestionPosn()
		{
			Vector2[] positions = new Vector2[Constants.NUMBER_LINES];
			positions[0] = MyGame.Manager.TextManager.GetTextPosition(2, 5);
			positions[1] = MyGame.Manager.TextManager.GetTextPosition(2, 6);
			positions[2] = MyGame.Manager.TextManager.GetTextPosition(2, 7);
			return positions;
		}
		private static Vector2[] GetAnswerAPosn()
		{
			Vector2[] answerPosn = new Vector2[Constants.NUMBER_LINES];
			answerPosn[0] = MyGame.Manager.TextManager.GetTextPosition(4, 9);
			answerPosn[1] = MyGame.Manager.TextManager.GetTextPosition(4, 10);
			answerPosn[2] = MyGame.Manager.TextManager.GetTextPosition(4, 11);
			return answerPosn;
		}
		private static Vector2[] GetAnswerBPosn()
		{
			Vector2[] answerPosn = new Vector2[Constants.NUMBER_LINES];
			answerPosn[0] = MyGame.Manager.TextManager.GetTextPosition(4, 13);
			answerPosn[1] = MyGame.Manager.TextManager.GetTextPosition(4, 14);
			answerPosn[2] = MyGame.Manager.TextManager.GetTextPosition(4, 15);
			return answerPosn;
		}
		private static Vector2[] GetAnswerCPosn()
		{
			Vector2[] answerPosn = new Vector2[Constants.NUMBER_LINES];
			answerPosn[0] = MyGame.Manager.TextManager.GetTextPosition(4, 17);
			answerPosn[1] = MyGame.Manager.TextManager.GetTextPosition(4, 18);
			answerPosn[2] = MyGame.Manager.TextManager.GetTextPosition(4, 19);
			return answerPosn;
		}
		private static Vector2[] GetAnswerDPosn()
		{
			Vector2[] answerPosn = new Vector2[Constants.NUMBER_LINES];
			answerPosn[0] = MyGame.Manager.TextManager.GetTextPosition(4, 21);
			answerPosn[1] = MyGame.Manager.TextManager.GetTextPosition(4, 22);
			answerPosn[2] = MyGame.Manager.TextManager.GetTextPosition(4, 23);
			return answerPosn;
		}

		private void RandomizeAnswerPosn()
		{
			Vector2[] origin = null;
			for (int index = 0; index < Constants.NUMBER_SELECTS; index++)
			{
				switch (index)
				{
					case 0:
						origin = originAPosn;
						break;

					case 1:
						origin = originBPosn;
						break;

					case 2:
						origin = originCPosn;
						break;

					case 3:
						origin = originDPosn;
						break;
				}

				Byte value = answerList[index];
				switch (value)
				{
					case 0:
						answerAPosn = origin;
						break;

					case 1:
						answerBPosn = origin;
						break;

					case 2:
						answerCPosn = origin;
						break;

					case 3:
						answerDPosn = origin;
						break;
				}
			}
		}

	}
}