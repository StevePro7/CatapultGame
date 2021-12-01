using System;

namespace WindowsGame.Common.Objects
{
	public class Question
	{
		public Question()
		{
		}

		public Question(String[] questionText, String[] answerAText, String[] answerBText, String[] answerCText, String[] answerDText, Byte answerCode)
		{
			QuestionText = questionText;
			AnswerAText = answerAText;
			AnswerBText = answerBText;
			AnswerCText = answerCText;
			AnswerDText = answerDText;
			AnswerCode = answerCode;
		}

		public String[] QuestionText { get; private set; }
		public String[] AnswerAText { get; private set; }
		public String[] AnswerBText { get; private set; }
		public String[] AnswerCText { get; private set; }
		public String[] AnswerDText { get; private set; }
		public Byte AnswerCode { get; private set; }
	}
}