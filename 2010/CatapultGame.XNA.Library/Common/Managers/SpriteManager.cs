using System;
using WindowsGame.Common.Objects;
using WindowsGame.Common.Static;
using WindowsGame.Master;
using Microsoft.Xna.Framework;

namespace WindowsGame.Common.Managers
{
	public interface ISpriteManager
	{
		void Initialize();

		void Update(GameTime gameTime);
		void Draw();
	}

	public class SpriteManager : ISpriteManager
	{
		private Catapult playerCatapult;

		public void Initialize()
		{
			playerCatapult = new Catapult();
			playerCatapult.Initialize();
		}

		public void Update(GameTime gameTime)
		{
			playerCatapult.Update(gameTime);
		}

		public void Draw()
		{
			playerCatapult.Draw();
		}
		
	}
}