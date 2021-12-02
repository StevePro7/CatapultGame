using System;
using WindowsGame.Common.Objects;
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
		}

		public void Update(GameTime gameTime)
		{
			
		}

		public void Draw()
		{
			playerCatapult.Draw();
		}
		
	}
}