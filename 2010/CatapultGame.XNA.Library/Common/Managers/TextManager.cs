using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using WindowsGame.Common.Data;
using WindowsGame.Common.Objects;
using WindowsGame.Common.Static;
using WindowsGame.Master;

namespace WindowsGame.Common.Managers
{
	public interface ITextManager
	{
		void Initialize();
		void Initialize(String root);
		IList<TextData> LoadTextData(String screen);
		IList<TextData> LoadTextData(String screen, Byte textsSize, Byte offsetX, Single fontX, Single fontY);
		Vector2 GetTextPosition(Byte x, Byte y);
		Vector2 GetTextPosition(Byte x, Byte y, Byte textsSize, Byte offsetX, Single fontX, Single fontY);
		Vector2 GetWhitePosition(Byte x, Byte y);
		Vector2 GetWhitePosition(Byte x, Byte y, Byte textsSize, Byte offsetX);
		void Draw(IEnumerable<TextData> textDataList);
		void DrawText(String text, Vector2 position);
		void DrawText(String text, Vector2 position, Color color);
	}

	public class TextManager : ITextManager
	{
		private static Char[] DELIM;
		private static Char[] PIPES;

		public void Initialize()
		{
			Initialize(String.Empty);
		}

		public void Initialize(String root)
		{
			DELIM = new[] { ',' };
			PIPES = new[] { '|' };

			BaseData.Initialize(root);
		}

		public IList<TextData> LoadTextData(String screen)
		{
			return LoadTextData(screen, Constants.TextsSize, Constants.GameOffsetX, Constants.FontOffsetX, Constants.FontOffsetY);
		}
		public IList<TextData> LoadTextData(String screen, Byte textsSize, Byte offsetX, Single fontX, Single fontY)
		{
			String file = GetTextFile(screen + ".txt");
			var lines = MyGame.Manager.FileManager.LoadTxt(file);

			var textDataList = new List<TextData>();
			foreach (string line in lines)
			{
				if (line.StartsWith("##") || line.StartsWith("--"))
				{
					continue;
				}

				String[] items = line.Split(DELIM);
				Byte x = Convert.ToByte(items[0]);
				Byte y = Convert.ToByte(items[1]);
				String message = items[2];

				Vector2 postion = GetTextPosition(x, y, textsSize, offsetX, fontX, fontY);
				TextData item = new TextData(postion, message);

				textDataList.Add(item);
			}

			return textDataList;
		}

		public Vector2 GetTextPosition(Byte x, Byte y)
		{
			return GetTextPosition(x, y, Constants.TextsSize, Constants.GameOffsetX, Constants.FontOffsetX, Constants.FontOffsetY);
		}
		public Vector2 GetTextPosition(Byte x, Byte y, Byte textsSize, Byte offsetX, Single fontX, Single fontY)
		{
			return new Vector2(x * textsSize + offsetX + fontX, y * textsSize + fontY);
		}

		public Vector2 GetWhitePosition(Byte x, Byte y)
		{
			return GetWhitePosition(x, y, Constants.TextsSize, Constants.GameOffsetX);
		}
		public Vector2 GetWhitePosition(Byte x, Byte y, Byte textsSize, Byte offsetX)
		{
			return new Vector2(x * textsSize + offsetX, y * textsSize);
		}

		public void Draw(IEnumerable<TextData> textDataList)
		{
			foreach (TextData data in textDataList)
			{
				Engine.SpriteBatch.DrawString(Assets.EmulogicFont, data.Text, data.Position, data.Color);
			}
		}

		public void DrawText(String text, Vector2 position)
		{
			DrawText(text, position, Color.Black);
		}
		public void DrawText(String text, Vector2 position, Color color)
		{
			Engine.SpriteBatch.DrawString(Assets.EmulogicFont, text, position, color);
		}

		private static String GetTextFile(String textFile)
		{
			return String.Format("{0}{1}/{2}/{3}/{4}", BaseData.BaseRoot, Constants.CONTENT_DIRECTORY, Constants.DATA_DIRECTORY, Constants.TEXTS_DIRECTORY, textFile);
		}

	}
}