using System;
using WindowsGame.Master.Interfaces;
using WindowsGame.Common.Managers;

namespace WindowsGame.Common.TheGame
{
	public interface IGameManager
	{
		//IButtonManager ButtonManager { get; }
		//ICollisionManager CollisionManager { get; }
		IConfigManager ConfigManager { get; }
		IContentManager ContentManager { get; }
		IDeviceManager DeviceManager { get; }
		//IImageManager ImageManager { get; }
		IInputManager InputManager { get; }
		IRandomManager RandomManager { get; }
		IResolutionManager ResolutionManager { get; }
		IScoreManager ScoreManager { get; }
		IScreenManager ScreenManager { get; }
		ISoundManager SoundManager { get; }
		ISpriteManager SpriteManager { get; }
		IStateManager StateManager { get; }
		IStorageManager StorageManager { get; }
		ITextManager TextManager { get; }
		//IThreadManager ThreadManager { get; }
		IFileManager FileManager { get; }
		ILogger Logger { get; }
	}

	public class GameManager : IGameManager
	{
		public GameManager
		(
			//IButtonManager buttonManager,
			//ICollisionManager collisionManager,
			IConfigManager configManager,
			IContentManager contentManager,
			IDeviceManager deviceManager,
			//IImageManager imageManager,
			IInputManager inputManager,
			//IQuestionManager questionManager,
			IRandomManager randomManager,
			IResolutionManager resolutionManager,
			IScoreManager scoreManager,
			IScreenManager screenManager,
			ISoundManager soundManager,
			ISpriteManager spriteManager,
			IStorageManager storageManager,
			IStateManager stateManager,
			ITextManager textManager,
			//IThreadManager threadManager,
			IFileManager fileManager,
			ILogger logger
		)
		{
			//ButtonManager = buttonManager;
			//CollisionManager = collisionManager;
			ConfigManager = configManager;
			ContentManager = contentManager;
			DeviceManager = deviceManager;
			//ImageManager = imageManager;
			InputManager = inputManager;
			//QuestionManager = questionManager;
			RandomManager = randomManager;
			ResolutionManager = resolutionManager;
			ScoreManager = scoreManager;
			ScreenManager = screenManager;
			SoundManager = soundManager;
			SpriteManager = spriteManager;
			StorageManager = storageManager;
			StateManager = stateManager;
			TextManager = textManager;
			//ThreadManager = threadManager;
			FileManager = fileManager;
			Logger = logger;
		}

		//public IButtonManager ButtonManager { get; private set; }
		//public ICollisionManager CollisionManager { get; private set; }
		public IConfigManager ConfigManager { get; private set; }
		public IContentManager ContentManager { get; private set; }
		public IDeviceManager DeviceManager { get; private set; }
		//public IImageManager ImageManager { get; private set; }
		public IInputManager InputManager { get; private set; }
		//public IQuestionManager QuestionManager { get; private set; }
		public IRandomManager RandomManager { get; private set; }
		public IResolutionManager ResolutionManager { get; private set; }
		public IScoreManager ScoreManager { get; private set; }
		public IScreenManager ScreenManager { get; private set; }
		public ISoundManager SoundManager { get; private set; }
		public ISpriteManager SpriteManager { get; private set; }
		public IStorageManager StorageManager { get; private set; }
		public IStateManager StateManager { get; private set; }
		public ITextManager TextManager { get; private set; }
		//public IThreadManager ThreadManager { get; private set; }
		public IFileManager FileManager { get; private set; }
		public ILogger Logger { get; private set; }
	}
}