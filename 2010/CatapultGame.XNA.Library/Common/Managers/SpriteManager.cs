using System;
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
		public void Initialize()
		{
		}

		public void Update(GameTime gameTime)
		{
			
		}

		public void Draw()
		{
		}
		
	}
}