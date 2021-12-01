using System;
using Microsoft.Xna.Framework;
//using WindowsGame.Common.Static;
using WindowsGame.Common.TheGame;
using WindowsGame.Master;

namespace WindowsGame.Common
{
	public static class MyGame
	{
		public static void Construct(IGameManager manager)
		{
			Manager = manager;
		}

		public static void Initialize()
		{
			//Manager.Logger.Initialize();
			//Manager.RandomManager.Initialize();

			//Manager.ConfigManager.Initialize();
			//Manager.ConfigManager.LoadContent();

			//Manager.ContentManager.Initialize();
			//Manager.ContentManager.LoadContentSplash();

			//Manager.DeviceManager.Initialize();
			//Manager.ImageManager.Initialize();
			//Manager.InputManager.Initialize();
			//Manager.QuestionManager.Initialize();
			//Manager.ScoreManager.Initialize();

			//Manager.ResolutionManager.Initialize();
			//Manager.ScreenManager.Initialize();
			//Manager.SoundManager.Initialize();
			//Manager.SpriteManager.Initialize();
			//Manager.StorageManager.Initialize();
			//Manager.ThreadManager.Initialize();
		}

		public static void LoadContent()
		{
			//Engine.Game.IsFixedTimeStep = Constants.IsFixedTimeStep;
			//Engine.Game.TargetElapsedTime = TimeSpan.FromSeconds(1.0f / Constants.FramesPerSecond);
			//Engine.Game.IsMouseVisible = Constants.IsMouseVisible;
			//Manager.ResolutionManager.LoadContent(Constants.IsFullScreen, Constants.ScreenWide, Constants.ScreenHigh, Constants.UseExposed, Constants.ExposeWide, Constants.ExposeHigh);
		}

		public static void LoadContentAsync()
		{
			//Manager.TextManager.Initialize();

			//Manager.CollisionManager.LoadContent();
			//Manager.ContentManager.LoadContent();
			//Manager.ImageManager.LoadContent();

			//Manager.QuestionManager.LoadContent();
			//Manager.ScoreManager.LoadContent();
			//Manager.ScreenManager.LoadContent();
			//Manager.StorageManager.LoadContent();

			//GC.Collect();
		}

		public static void UnloadContent()
		{
			Engine.Game.Content.Unload();
		}

		public static void Update(GameTime gameTime)
		{
//            Manager.InputManager.Update(gameTime);

//#if WINDOWS
//            if (Manager.ConfigManager.GlobalConfigData.QuitsToExit)
//            {
//                Boolean escape = Manager.InputManager.Escape();
//                if (escape)
//                {
//                    Engine.Game.Exit();
//                    return;
//                }
//            }
//#endif

//            Manager.ScreenManager.Update(gameTime);
        }

		public static void Draw()
		{
			//Manager.ScreenManager.Draw();
		}

		public static void OnActivated()
		{
//			Manager.StorageManager.LoadContent();
		}
		public static void OnDeactivated()
		{
//			Manager.StorageManager.SaveContent();
		}

		public static IGameManager Manager { get; private set; }
	}
}