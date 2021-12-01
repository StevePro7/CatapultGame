using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using WindowsGame.Common.Interfaces;
using WindowsGame.Common.Screens;
using WindowsGame.Common.Static;
using WindowsGame.Master;

namespace WindowsGame.Common.Managers
{
	public interface IScreenManager
	{
		void Initialize();
		void LoadContent();
		void Update(GameTime gameTime);
		void Draw();
	}

	public class ScreenManager : IScreenManager
	{
		private IDictionary<ScreenType, IScreen> screens;
		private ScreenType currScreen = ScreenType.Splash;
		private ScreenType nextScreen = ScreenType.Splash;
		private Color color;

		public void Initialize()
		{
			screens = GetScreens();
			screens[ScreenType.Splash].Initialize();
			screens[ScreenType.Init].Initialize();
			color = Color.Black;
		}

		public void LoadContent()
		{
			foreach (var key in screens.Keys)
			{
				if (ScreenType.Splash == key || ScreenType.Init == key)
				{
					continue;
				}

				screens[key].Initialize();
			}
		}

		public void Update(GameTime gameTime)
		{
			if (currScreen != nextScreen)
			{
				currScreen = nextScreen;
				screens[currScreen].LoadContent();
				color = GetColor();
			}

			nextScreen = screens[currScreen].Update(gameTime);
		}

		public void Draw()
		{
			MyGame.Manager.ResolutionManager.BeginDraw(color);
			Engine.SpriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, null, null, null, null, MyGame.Manager.ResolutionManager.TransformationMatrix);
			screens[currScreen].Draw();
			Engine.SpriteBatch.End();
		}

		private Color GetColor()
		{
			// TODO revert code!
			return currScreen > ScreenType.Init ? Color.White : Color.Black;
			//return Color.White;
		}

		private static Dictionary<ScreenType, IScreen> GetScreens()
		{
			return new Dictionary<ScreenType, IScreen>
			{
				{ScreenType.Splash, new SplashScreen()},
				{ScreenType.Init, new InitScreen()},
				{ScreenType.Title, new TitleScreen()},
				{ScreenType.Diff, new DiffScreen()},
				{ScreenType.Long, new LongScreen()},
				{ScreenType.Ready, new ReadyScreen()},
				{ScreenType.Play, new PlayScreen()},
				{ScreenType.Quiz, new QuizScreen()},
				{ScreenType.Score, new ScoreScreen()},
				{ScreenType.Over, new OverScreen()},
				{ScreenType.Exit, new ExitScreen()},
				{ScreenType.Test, new TestScreen()},
			};
		}

	}
}