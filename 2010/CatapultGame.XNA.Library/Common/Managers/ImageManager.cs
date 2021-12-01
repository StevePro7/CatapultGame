using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using WindowsGame.Common.Static;
using WindowsGame.Master;

namespace WindowsGame.Common.Managers
{
	public interface IImageManager
	{
		void Initialize();
		void LoadContent();
		void GenerateNextActor();
		void DrawTitle();
		void DrawHeader();
		void DrawCurrActor();
		void DrawNextActor();
		void DrawActor(Byte index);
		void DrawSprite(SpriteType spriteType, Vector2 position);
	}

	public class ImageManager : IImageManager
	{
		private Vector2 zeroPosn, headPosn;

		private Rectangle titleRect, headerRect;
		private Vector2 titleVect, headerVect;

		private Rectangle[] actorRects;
		private Vector2 actorVect;

		private Rectangle[] spriteRects;
		private Single imageRotate;

		private const UInt16 imageWide = 240;
		private const UInt16 imageHigh = 320;
		private const UInt16 spriteSize = Constants.SpriteSize;

		private Byte currActor, nextActor;

		public void Initialize()
		{
			currActor = Constants.NUMBER_CHARACTERS;
			nextActor = 0;

			GenerateNextActor();
		}

		public void LoadContent()
		{
			zeroPosn = new Vector2(Constants.GameOffsetX, 0);
			headPosn = new Vector2(Constants.GameOffsetX, Constants.TextsSize / 2.0f);

			titleRect = new Rectangle(0, 0, 2 * imageWide, 2 * imageHigh);
			titleVect = new Vector2(imageWide * 2, 0);

			headerRect = new Rectangle(4 * imageHigh - spriteSize, 0, spriteSize, 2 * imageHigh);
			headerVect = new Vector2(spriteSize, 0);

			actorRects = PopulateActorRects();
			actorVect = new Vector2(Constants.ScreenWide - imageWide - Constants.GameOffsetX, Constants.ScreenHigh - imageHigh);

			spriteRects = PopulateSpriteRects();
			imageRotate = MathHelper.ToRadians(270);
		}

		public void GenerateNextActor()
		{
			while (true)
			{
				nextActor = (Byte)MyGame.Manager.RandomManager.Next(Constants.NUMBER_CHARACTERS);
				if (currActor != nextActor)
				{
					break;
				}
			}

			currActor = nextActor;
		}

		public void DrawTitle()
		{
			Engine.SpriteBatch.Draw(Assets.SpritesheetTexture, zeroPosn, titleRect, Color.White, imageRotate, titleVect, 1.0f, SpriteEffects.None, 1.0f);
		}

		public void DrawHeader()
		{
			Engine.SpriteBatch.Draw(Assets.SpritesheetTexture, headPosn, headerRect, Color.White, imageRotate, headerVect, 1.0f, SpriteEffects.None, 1.0f);
		}

		public void DrawCurrActor()
		{
			DrawActor(currActor);
		}
		public void DrawNextActor()
		{
			GenerateNextActor();
			DrawActor(currActor);
		}
		public void DrawActor(Byte index)
		{
			currActor = index;
			Vector2 actorPosn = actorVect;
			float scale = 1.0f;

			// Scale for Lisa
			if ((Byte)ActorType.Lisa1 == index || (Byte)ActorType.Lisa2 == index || (Byte)ActorType.Maggie== index)
			{
				scale = 0.85f;
				actorPosn.Y += 2 * Constants.TextsSize;
			}

			Engine.SpriteBatch.Draw(Assets.SpritesheetTexture, actorPosn, actorRects[index], Color.White, 0.0f, Vector2.Zero, scale, SpriteEffects.None, 1.0f);
		}

		public void DrawSprite(SpriteType spriteType, Vector2 position)
		{
			Engine.SpriteBatch.Draw(Assets.SpritesheetTexture, position, spriteRects[(Byte)spriteType], Color.White);
		}

		private Rectangle[] PopulateActorRects()
		{
			actorRects = new Rectangle[Constants.NUMBER_CHARACTERS];
			actorRects[0] = GetActorRect(0, 2); actorRects[1] = GetActorRect(0, 3); actorRects[2] = GetActorRect(1, 2); actorRects[3] = GetActorRect(1, 3);
			actorRects[4] = GetActorRect(2, 0); actorRects[5] = GetActorRect(2, 1); actorRects[6] = GetActorRect(2, 2); actorRects[7] = GetActorRect(2, 3);
			actorRects[8] = GetActorRect(3, 0); actorRects[9] = GetActorRect(3, 1); actorRects[10] = GetActorRect(3, 2); actorRects[11] = GetActorRect(3, 3);
			actorRects[12] = GetActorRect(4, 0); actorRects[13] = GetActorRect(4, 1); actorRects[14] = GetActorRect(4, 2); actorRects[15] = GetActorRect(4, 3);
			return actorRects;
		}
		private static Rectangle GetActorRect(Byte x, Byte y)
		{
			return new Rectangle(x * imageWide, y * imageHigh, imageWide, imageHigh);
		}

		private Rectangle[] PopulateSpriteRects()
		{
			const UInt16 x = 4 * imageHigh - spriteSize;
			const UInt16 y = 2 * imageHigh;
			spriteRects = new Rectangle[Constants.NUMBER_SPRITES];
			spriteRects[(Byte)SpriteType.Select] = GetSpriteRect(x, y, 0);
			spriteRects[(Byte)SpriteType.Right] = GetSpriteRect(x, y, 1);
			spriteRects[(Byte)SpriteType.Wrong] = GetSpriteRect(x, y, 2);
			spriteRects[(Byte)SpriteType.LeftArrow] = GetSpriteRect(x, y, 3);
			spriteRects[(Byte)SpriteType.RightArrow] = GetSpriteRect(x, y, 4);
			spriteRects[(Byte)SpriteType.VolumeOn] = GetSpriteRect(x, y, 5);
			spriteRects[(Byte)SpriteType.VolumeOff] = GetSpriteRect(x, y, 6);
			spriteRects[(Byte)SpriteType.White] = GetSpriteRect(x, y, 7);
			return spriteRects;
		}
		private static Rectangle GetSpriteRect(UInt16 x, UInt16 y, Byte index)
		{
			y = (UInt16)(y + index * spriteSize);
			return GetRectangle(x, y, spriteSize);
		}
		private static Rectangle GetSpriteRect(UInt16 x, UInt16 y)
		{
			return GetRectangle(x, y, spriteSize);
		}
		private static Rectangle GetRectangle(UInt16 x, UInt16 y, UInt16 size)
		{
			return GetRectangle(x, y, size, size);
		}
		private static Rectangle GetRectangle(UInt16 x, UInt16 y, UInt16 wide, UInt16 high)
		{
			return new Rectangle(x, y, wide, high);
		}

		// Rectangle + Vector2 raw data:
		// titleRect = new Rectangle(0, 0, 480, 640);
		// titleVect = new Vector2(640, 0);
		// headerRect = new Rectangle(1200, 0, 80, 640);
		// headerVect = new Vector2(80, 0);
		// selectRect = new Rectangle(1200, 640, 80, 80);
		// rightRect = new Rectangle(1200, 720, 80, 80);
		// wrongRect = new Rectangle(1200, 800, 80, 80);
	}
}